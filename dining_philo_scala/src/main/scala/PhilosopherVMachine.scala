package dining

import dining._
import akka.actor._
import scala.io.StdIn
import scala.concurrent.duration._
import akka.util.Timeout
import scala.concurrent.Future

import com.typesafe.config.ConfigFactory
import scala.concurrent.{Await, ExecutionContext}

object PhilosopherVMachine extends App {

  val forkVMachineAddress =
    "akka://ForkVMachine@127.0.0.1:2551" // Address of the ForkVMachine
  System.setProperty(
    "AKKA_PORT",
    "2552"
  ) // Set the port for PhilosopherVMachine

  val config = ConfigFactory.load("application") // Load the configuration file
  val system = ActorSystem("PhilosopherVMachine", config)

  implicit val ec: ExecutionContext = system.dispatcher

  // get the number of philosophers and deadlock setting from the config
  val n = config.getInt("dining-philosophers.philosopher-count")
  val deadlock = config.getBoolean(
    "dining-philosophers.deadlock"
  ) // whether to enable deadlock or not, modiffy it in application.conf

  println(
    s"PhilosopherVMachine started with $n philosophers and deadlock=$deadlock"
  )

  // get the fork actor references from the ForkVMachine
  val forkSels =
    (1 to n).map(i =>
      system.actorSelection(s"$forkVMachineAddress/user/fork$i")
    )

  implicit val to: Timeout = Timeout(5.seconds)
  val forkRefsF: Future[Seq[ActorRef]] =
    Future.sequence(forkSels.map(_.resolveOne(5.seconds)))

  // wait here until the forks are resolved (or a timeout/error)
  val forkRefs: Seq[ActorRef] =
    Await.result(forkRefsF, 6.seconds)

  val philosophers = (1 to n).map { i =>
    if (i == 1) {
      // Special case for the first philosopher to avoid or not deadlock
      if (deadlock) {
        // If deadlock is enabled, don't reverse the order of forks for the first philosopher
        val rightFork = forkRefs(i % n)
        val leftFork = forkRefs(i - 1)
        system.actorOf(
          Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
          s"philosopher$i"
        )
      } else {
        // If deadlock is not enabled, reverse the order of forks for the first philosopher
        val rightFork = forkRefs(i - 1)
        val leftFork = forkRefs(i % n)

        system.actorOf(
          Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
          s"philosopher$i"
        )
      }
    } else {
      // For all other philosophers, use the normal order of forks
      val rightFork = forkRefs(i % n)
      val leftFork = forkRefs(i - 1)

      system.actorOf(
        Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
        s"philosopher$i"
      )
    }

  }

  // Start all philosophers
  philosophers.foreach(_ ! Philosopher.Start)

}
