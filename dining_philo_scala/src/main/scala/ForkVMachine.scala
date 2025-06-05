package dining
import dining._
import akka.actor._
import akka.actor.ExtendedActorSystem
import com.typesafe.config.ConfigFactory

import scala.io.StdIn

object ForkVMachine extends App {

  System.setProperty("AKKA_PORT", "2551") // Set the port for ForkVMachine

  val config = ConfigFactory.load("application")

  val system = ActorSystem("ForkVMachine", config)

  // system.logConfiguration()
  val cfg = system.settings.config
  val n = cfg.getInt("dining-philosophers.philosopher-count")
  val forks = (1 to n).map(i => system.actorOf(Props[Fork], s"fork$i"))

  val extSystem = system.asInstanceOf[ExtendedActorSystem]
  val boundAddress = extSystem.provider.getDefaultAddress
  val boundPort = boundAddress.port.getOrElse(-1)
  println(s"ActorSystem bound to port = $boundPort")
}
