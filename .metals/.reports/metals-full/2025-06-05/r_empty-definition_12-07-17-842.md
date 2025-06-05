error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/PhilosopherVMachine.scala:`<none>`.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/PhilosopherVMachine.scala
empty definition using pc, found symbol in pc: `<none>`.
empty definition using semanticdb
empty definition using fallback
non-local guesses:

offset: 382
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/PhilosopherVMachine.scala
text:
```scala
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

  val forkVMachineAddress = "akka://ForkVMachine@127.0.0.1:2551"
  @@val config = ConfigFactory.load("philosopher")
  val system = ActorSystem("PhilosopherVMachine", config)

  implicit val ec: ExecutionContext = system.dispatcher

  // 2) read n & forkAddress from config
  val n = config.getInt("dining-philosophers.philosopher-count")
  val deadlock = config.getBoolean("dining-philosophers.deadlock")

  println(
    s"PhilosopherVMachine started with $n philosophers and deadlock=$deadlock"
  )

  // 3) build the selections
  val forkSels =
    (1 to n).map(i =>
      system.actorSelection(s"$forkVMachineAddress/user/fork$i")
    )

  // 4) resolve them all in parallel, with a 5s timeout each
  implicit val to: Timeout = Timeout(5.seconds)
  val forkRefsF: Future[Seq[ActorRef]] =
    Future.sequence(forkSels.map(_.resolveOne(5.seconds)))

  // 5) wait here until they’re all resolved (or a timeout/error)
  val forkRefs: Seq[ActorRef] =
    Await.result(forkRefsF, 6.seconds)

  println(s"Resolved ${forkRefs.size} forks")

  println(s"Successfully resolved all forks: $forkRefs")

  val philosophers = (1 to n).map { i =>
    if (i == 1) {
      val rightFork = forkRefs(i - 1)
      val leftFork = forkRefs(i % n)

      system.actorOf(
        Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
        s"philosopher$i"
      )
    } else {
      val rightFork = forkRefs(i % n)
      val leftFork = forkRefs(i - 1)

      system.actorOf(
        Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
        s"philosopher$i"
      )
    }

  }

  philosophers.foreach(_ ! Philosopher.Start)
//   StdIn.readLine() // Keep the application running until user input
//   Await.result(system.whenTerminated, Duration.Inf)

}

```


#### Short summary: 

empty definition using pc, found symbol in pc: `<none>`.