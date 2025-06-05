// package dining
import akka.actor._
import scala.io.StdIn
import akka.pattern.pipe
import scala.concurrent.Future
import scala.concurrent.duration._
import scala.util.Success

object Fork {
  case object Acquire
  case object Release
  case object Acquired
  case object Busy
}

class Fork extends Actor {

  import Fork._

  private var inUse = false

  override def receive: Receive = {
    case Acquire =>
      println(s"${self.path.name} received Acquire request")
      if (!inUse) {
        inUse = true
        sender() ! Acquired
      } else {
        sender() ! Busy
      }

    case Release =>
      inUse = false
  }

}

object Coordinator {
  case object StartDiscovery
  case class ForksResolved(forks: Map[Int, ActorRef])
  case class ForksFailed(cause: Throwable)
}

object Philosopher {
  case object Start
  case object Think
  case object EatDone
  case object RetryAcquireRight
  case object AcquireRightFork
}

class Philosopher(
    name: String,
    rightFork: ActorRef,
    leftFork: ActorRef
) extends Actor {
  import Philosopher._
  import Fork._
  import context.dispatcher

  private val eatingTime = 3.second
  private val thinkingTime = 3.second

  def receive: Receive = { case Start =>
    println(s"$name is starting to think")
    leftFork ! Acquire
    context.become(waitForLeftFork)
  }

  def waitForLeftFork: Receive = {
    case Acquired =>
      println(s"$name acquired left fork")
      context.system.scheduler.scheduleOnce(2.second, self, AcquireRightFork)
    case Busy =>
      retry()
    case AcquireRightFork =>
      rightFork ! Acquire
      context.become(waitForRightFork)
  }

  def waitForRightFork: Receive = {
    case Acquired =>
      println(s"$name acquired right fork")
      eat()
      context.become(waitForEat)
    case Busy =>
      println(s"$name could not acquire right fork, retrying...")
      context.system.scheduler.scheduleOnce(
        1.second,
        self,
        RetryAcquireRight
      )

    case RetryAcquireRight =>
      rightFork ! Acquire
  }

  def waitForEat: Receive = { case EatDone =>
    rightFork ! Release
    leftFork ! Release
    think()
  }

  private def retry(): Unit = {
    // wait a bit, then start over
    context.become(receive)
    context.system.scheduler.scheduleOnce(1.second, self, Start)
  }

  private def eat() = {
    println(s"$name is eating")
    context.system.scheduler.scheduleOnce(eatingTime, self, EatDone)
  }

  private def think() = {
    println(s"$name is thinking")
    context.become(receive)
    context.system.scheduler.scheduleOnce(thinkingTime, self, Start)
  }
}

class Coordinator(forkVMachineAddress: String) extends Actor {
  import Coordinator._
  import context.dispatcher

  private var forks = Map.empty[Int, ActorRef]
  private val n = context.system.settings.config
    .getInt("dining-philosophers.philosopher-count")

  def receive: Receive = {
    case StartDiscovery =>
      val lookups: Seq[Future[(Int, ActorRef)]] =
        (1 to n).map { i =>
          context.system
            .actorSelection(s"$forkVMachineAddress/user/fork$i")
            .resolveOne(10.seconds)
            .map(ref => i -> ref)
        }
      println(lookups.mkString(", "))
      Future
        .sequence(lookups) // Future[Seq[(Int,ActorRef)]]
        .map(_.toMap) // Future[Map[Int,ActorRef]]
        .andThen { // side‐effecting logging on success
          case Success(forks) =>
            println(s"(in Future) forks resolved: $forks")
        }(context.dispatcher) // must pass the ExecutionContext explicitly
        .map(ForksResolved) // Future[ForksResolved]
        .recover { case ex =>
          println(s" fork resolution failed: ${ex.getMessage}")
          ForksFailed(ex)
        }
        .pipeTo(self)
    case ForksResolved(forksMap: Map[Int, ActorRef]) =>
      // now forksMap(i) is a real ActorRef
      println(s"Discovered forks: ${forksMap.keys.mkString(", ")}")
      startPhilosophers(forksMap)

    case ForksFailed(ex) =>
      println(s"Failed to discover forks: $ex")
  }

  def startPhilosophers(forks: Map[Int, ActorRef]): Unit = {
    (1 to n).foreach { i =>
      val left = forks(i)
      val right = forks((i % n) + 1)
      val (first, second) =
        if (i == 1) (right, left) else (left, right)

      val p = context.actorOf(
        Props(new Philosopher(s"P$i", first, second)),
        s"philosopher$i"
      )
      p ! Philosopher.Start
    }
  }
}

// class Coordinator2(forkVMachineAddress: String) extends Actor {

//   val cfg = context.system.settings.config
//   val n = cfg.getInt("dining-philosophers.philosopher-count")
//   import Coordinator._
//   private var forks = Map.empty[Int, ActorRef]

//   override def preStart(): Unit = {
//     self ! StartDiscovery
//   }

//   def receive: Receive = { case y =>
//     println(s"Starting discovery of forks at $forkVMachineAddress")
//     val forks: Seq[ActorSelection] =
//       (1 to n).map(i =>
//         context.system.actorSelection(s"$forkVMachineAddress/user/fork$i")
//       )
//     println(s"Discovered forks: ${forks.mkString(", ")}")

//     val philosophers = (1 to n).map { i =>
//       if (i == 1) {
//         val rightFork = forks(i - 1)
//         val leftFork = forks(i % n)

//         context.system.actorOf(
//           Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
//           s"philosopher$i"
//         )
//       } else {
//         val rightFork = forks(i % n)
//         val leftFork = forks(i - 1)

//         context.system.actorOf(
//           Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
//           s"philosopher$i"
//         )
//       }
//     }

//     // println(s"Discovered philosophers: ${philosophers.mkString(", ")}")
//     philosophers.foreach(_ ! Philosopher.Start)

//   }
// }
