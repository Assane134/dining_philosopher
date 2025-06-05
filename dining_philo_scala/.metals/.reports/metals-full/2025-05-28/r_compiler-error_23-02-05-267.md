file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/ForkVMachine.scala
### java.lang.IndexOutOfBoundsException: -1

occurred in the presentation compiler.

presentation compiler configuration:


action parameters:
offset: 943
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/ForkVMachine.scala
text:
```scala
import akka.actor._
import akka.actor.ExtendedActorSystem

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

  val cfg = system.settings.config
  val n = cfg.getInt("dining-philosophers.philosopher-count")
  val forks = (1 to n).map(i => system.actorOf(Props[Fork], s"fork$i"))
  val extSystem = system.asInstanceOf[ExtendedActorSystem]
  val address = extSystem.provider.getDefaultAddress

  val fullPath = forks(@@).path.toStringWithAddress(address)

  println(fullPath)
  StdIn.readLine()
}

```



#### Error stacktrace:

```
scala.collection.LinearSeqOps.apply(LinearSeq.scala:129)
	scala.collection.LinearSeqOps.apply$(LinearSeq.scala:128)
	scala.collection.immutable.List.apply(List.scala:79)
	dotty.tools.dotc.util.Signatures$.applyCallInfo(Signatures.scala:244)
	dotty.tools.dotc.util.Signatures$.computeSignatureHelp(Signatures.scala:101)
	dotty.tools.dotc.util.Signatures$.signatureHelp(Signatures.scala:88)
	dotty.tools.pc.SignatureHelpProvider$.signatureHelp(SignatureHelpProvider.scala:46)
	dotty.tools.pc.ScalaPresentationCompiler.signatureHelp$$anonfun$1(ScalaPresentationCompiler.scala:435)
```
#### Short summary: 

java.lang.IndexOutOfBoundsException: -1