error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Utils.scala:`<none>`.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Utils.scala
empty definition using pc, found symbol in pc: `<none>`.
empty definition using semanticdb
empty definition using fallback
non-local guesses:
	 -akka/actor/scala/concurrent.
	 -scala/concurrent/duration/scala/concurrent.
	 -scala/concurrent.
	 -scala/Predef.scala.concurrent.
offset: 59
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Utils.scala
text:
```scala
import akka.actor._
import scala.io.StdIn
import scala.co@@ncurrent.duration._

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
        sender ! Acquired
      } else {
        sender ! Busy
      }

    case Release =>
      inUse = false
  }

}

object Coordinator {
  case object StartDiscovery
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
    rightFork: ActorSelection,
    leftFork: ActorSelection
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

  val cfg = context.system.settings.config
  val n = cfg.getInt("dining-philosophers.philosopher-count")
  import Coordinator._
  private var forks = Map.empty[Int, ActorRef]

  override def preStart(): Unit = {
    self ! StartDiscovery
  }

  def receive: Receive = { case StartDiscovery =>
    println(s"Starting discovery of forks at $forkVMachineAddress")
    val forks: Seq[ActorSelection] =
      (1 to n).map(i =>
        context.system.actorSelection(s"$forkVMachineAddress/user/fork$i")
      )
    println(s"Discovered forks: ${forks.mkString(", ")}")

    val philosophers = (1 to n).map { i =>
      if (i == 1) {
        val rightFork = forks(i - 1)
        val leftFork = forks(i % n)

        context.system.actorOf(
          Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
          s"philosopher$i"
        )
      } else {
        val rightFork = forks(i % n)
        val leftFork = forks(i - 1)

        context.system.actorOf(
          Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
          s"philosopher$i"
        )
      }
    }

    // println(s"Discovered philosophers: ${philosophers.mkString(", ")}")

    philosophers.foreach(_ ! Philosopher.Start)

  }
}

```


#### Short summary: 

empty definition using pc, found symbol in pc: `<none>`.