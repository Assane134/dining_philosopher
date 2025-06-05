error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Fork.scala:actor.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Fork.scala
empty definition using pc, found symbol in pc: actor.
empty definition using semanticdb
empty definition using fallback
non-local guesses:
	 -akka/actor/akka/actor.
	 -akka/actor.
	 -scala/Predef.akka.actor.
offset: 43
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Fork.scala
text:
```scala
package diningphilosophers
import akka.act@@or._
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

```


#### Short summary: 

empty definition using pc, found symbol in pc: actor.