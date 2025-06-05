error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/ForkVMachine.scala:`<none>`.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/ForkVMachine.scala
empty definition using pc, found symbol in pc: `<none>`.
empty definition using semanticdb
empty definition using fallback
non-local guesses:

offset: 185
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/ForkVMachine.scala
text:
```scala
package dining
import dining._
import akka.actor._
import akka.actor.ExtendedActorSystem
import com.typesafe.config.ConfigFactory

import scala.io.StdIn

object ForkVMachine ext@@ends App {

  System.setProperty("AKKA_PORT", "2552")
  val config = ConfigFactory.load("application.conf")
  val system = ActorSystem("ForkVMachine", config)
  val cfg = system.settings.config
  val n = cfg.getInt("dining-philosophers.philosopher-count")
  val forks = (1 to n).map(i => system.actorOf(Props[Fork], s"fork$i"))
  val extSystem = system.asInstanceOf[ExtendedActorSystem]
  val address = extSystem.provider.getDefaultAddress

  val fullPath = forks(0).path.toStringWithAddress(address)

  println(fullPath)
//   StdIn.readLine()
}

```


#### Short summary: 

empty definition using pc, found symbol in pc: `<none>`.