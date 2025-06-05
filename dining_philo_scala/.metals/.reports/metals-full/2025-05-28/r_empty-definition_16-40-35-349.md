error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/ForkVMachine.scala:`<none>`.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/ForkVMachine.scala
empty definition using pc, found symbol in pc: `<none>`.
empty definition using semanticdb
empty definition using fallback
non-local guesses:
	 -akka/actor/sys.
	 -sys.
	 -scala/Predef.sys.
offset: 85
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/ForkVMachine.scala
text:
```scala
import akka.actor._
import scala.io.StdIn

object ForkVMachine extends App {

  @@sys.props += "AKKA_PORT" -> "2551"
  val system = ActorSystem("ForkVMachine")

  val n = args.headOption.map(_.toInt).getOrElse(5)
  val forks = (1 to n).map(i => system.actorOf(Props[Fork], s"fork$i"))

  StdIn.readLine()
}

```


#### Short summary: 

empty definition using pc, found symbol in pc: `<none>`.