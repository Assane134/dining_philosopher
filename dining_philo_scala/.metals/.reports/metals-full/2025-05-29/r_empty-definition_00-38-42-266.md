error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/PhilosopherVMachine.scala:actor.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/PhilosopherVMachine.scala
empty definition using pc, found symbol in pc: actor.
empty definition using semanticdb
empty definition using fallback
non-local guesses:
	 -akka/actor/akka/actor.
	 -scala/concurrent/duration/akka/actor.
	 -akka/actor.
	 -scala/Predef.akka.actor.
offset: 17
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/PhilosopherVMachine.scala
text:
```scala
import akka.actor@@._
import scala.io.StdIn
import scala.concurrent.duration._
import com.typesafe.config.ConfigFactory
import scala.concurrent.{Await, ExecutionContext}

object PhilosopherVMachine extends App {

  val config = ConfigFactory.load("philosopher")
  val system = ActorSystem("PhilosopherVMachine", config)
  val forkVMachineAddress = "akka://ForkVMachine@127.0.0.1:2551"
  val coordinator = system.actorOf(
    Props(new Coordinator(forkVMachineAddress)),
    "coordinator"
  )

  StdIn.readLine() // Keep the application running until user input
//   Await.result(system.whenTerminated, Duration.Inf)
}

```


#### Short summary: 

empty definition using pc, found symbol in pc: actor.