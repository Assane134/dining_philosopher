package dining
import dining._
import akka.actor._
import akka.actor.ExtendedActorSystem
import com.typesafe.config.ConfigFactory

import scala.io.StdIn

object ForkVMachine extends App {

  val config = ConfigFactory.load("fork")
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
