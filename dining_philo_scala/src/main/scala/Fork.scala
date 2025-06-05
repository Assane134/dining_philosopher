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
      if (!inUse) { // if the fork is not in use, give it
        inUse = true
        sender ! Acquired
      } else { // if the fork is in use, inform the sender that it is busy
        sender ! Busy
      }

    case Release =>
      inUse = false // release the fork
  }

}
