error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Fork.scala:`<none>`.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Fork.scala
empty definition using pc, found symbol in pc: `<none>`.
empty definition using semanticdb
empty definition using fallback
non-local guesses:

offset: 342
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Fork.scala
text:
```scala
package dining
import akka.actor._
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
      if (!inUse) { // if the fork is not in use, give @@it
        inUse = true
        sender ! Acquired
      } else {
        sender ! Busy
      }

    case Release =>
      inUse = false // release the fork
  }

}

```


#### Short summary: 

empty definition using pc, found symbol in pc: `<none>`.