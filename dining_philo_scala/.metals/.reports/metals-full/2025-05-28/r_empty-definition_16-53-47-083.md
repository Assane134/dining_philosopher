error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/PhilosopherVMachine.scala:`<none>`.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/PhilosopherVMachine.scala
empty definition using pc, found symbol in pc: `<none>`.
empty definition using semanticdb
empty definition using fallback
non-local guesses:
	 -akka/actor/context/actorOf.
	 -akka/actor/context/actorOf#
	 -akka/actor/context/actorOf().
	 -Coordinator.context.actorOf.
	 -Coordinator.context.actorOf#
	 -Coordinator.context.actorOf().
	 -context/actorOf.
	 -context/actorOf#
	 -context/actorOf().
	 -scala/Predef.context.actorOf.
	 -scala/Predef.context.actorOf#
	 -scala/Predef.context.actorOf().
offset: 492
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/PhilosopherVMachine.scala
text:
```scala
import akka.actor._
import scala.io.StdIn

object Coordinator {
  case object StartDiscovery
}

class Coordinator(forkVMachineAddress: String) extends Actor {
  import Coordinator._
  private var forks = Map.empty[Int, ActorRef]

  override def preStart(): Unit = {
    self ! StartDiscovery
  }

  def receive: Receive = { case StartDiscovery =>
    val n = context.system.settings.config.getInt("philosophers.count")
    forks =
      (1 to n).map(i => i -> context.actorO@@f(Props[Fork], s"fork$i")).toMap
    
  }
}

object PhilosopherVMachine extends App {

  sys.props += "AKKA_PORT" -> "132"
  val system = ActorSystem("PhilosopherVMachine")

  val n = args.headOption.map(_.toInt).getOrElse(5)

  val philosophers = (1 to n).map { i =>
    if (i == 1) {
      val rightFork = forks(i - 1)
      val leftFork = forks(i % n)

      system.actorOf(
        Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
        s"philosopher$i"
      )
    } else {
      val rightFork = forks(i % n)
      val leftFork = forks(i - 1)

      system.actorOf(
        Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
        s"philosopher$i"
      )
    }
  }

  philosophers.foreach(_ ! Philosopher.Start)
  StdIn.readLine() // Keep the application running until user input
}

```


#### Short summary: 

empty definition using pc, found symbol in pc: `<none>`.