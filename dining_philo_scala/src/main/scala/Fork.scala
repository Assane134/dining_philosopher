package dining
import akka.actor._

// define messages for Fork
object Fork {
  case object Acquired // message sent when the fork is acquired
  case object Busy // message sent when the fork is busy
  case object Acquire // message sent by the philosopher to acquire the fork
  case object Release // message sent by the philosopher to release the fork
}

class Fork extends Actor {

  import Fork._

  private var inUse = false // flag to indicate if the fork is in use

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
