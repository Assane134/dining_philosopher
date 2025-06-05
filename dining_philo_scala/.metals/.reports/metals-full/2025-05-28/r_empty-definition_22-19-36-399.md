error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/ForkVMachine.scala:config.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/ForkVMachine.scala
empty definition using pc, found symbol in pc: config.
empty definition using semanticdb
empty definition using fallback
non-local guesses:
	 -akka/actor/system/settings/config.
	 -akka/actor/system/settings/config#
	 -akka/actor/system/settings/config().
	 -system/settings/config.
	 -system/settings/config#
	 -system/settings/config().
	 -scala/Predef.system.settings.config.
	 -scala/Predef.system.settings.config#
	 -scala/Predef.system.settings.config().
offset: 638
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/ForkVMachine.scala
text:
```scala
package dining
import akka.actor._

import scala.io.StdIn

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

object ForkVMachine extends App {

  sys.props += "AKKA_PORT" -> "2551"
  val system = ActorSystem("ForkVMachine")

  val cfg = system.settings.c@@onfig
  val n = cfg.getInt("dining-philosophers.philosopher-count")
  val forks = (1 to n).map(i => system.actorOf(Props[Fork], s"fork$i"))

  StdIn.readLine()
}

```


#### Short summary: 

empty definition using pc, found symbol in pc: config.