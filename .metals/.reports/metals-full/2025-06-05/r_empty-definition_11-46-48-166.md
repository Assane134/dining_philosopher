error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/ForkVMachine.scala:withFallback.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/ForkVMachine.scala
empty definition using pc, found symbol in pc: withFallback.
empty definition using semanticdb
empty definition using fallback
non-local guesses:
	 -dining/overrideCfg/withFallback.
	 -dining/overrideCfg/withFallback#
	 -dining/overrideCfg/withFallback().
	 -akka/actor/overrideCfg/withFallback.
	 -akka/actor/overrideCfg/withFallback#
	 -akka/actor/overrideCfg/withFallback().
	 -overrideCfg/withFallback.
	 -overrideCfg/withFallback#
	 -overrideCfg/withFallback().
	 -scala/Predef.overrideCfg.withFallback.
	 -scala/Predef.overrideCfg.withFallback#
	 -scala/Predef.overrideCfg.withFallback().
offset: 618
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/ForkVMachine.scala
text:
```scala
package dining
import dining._
import akka.actor._
import akka.actor.ExtendedActorSystem
import com.typesafe.config.ConfigFactory

import scala.io.StdIn

object ForkVMachine extends App {

  // System.setProperty("AKKA_PORT", "2552")

  // val config = ConfigFactory.load("fork")

  val overrideCfg = ConfigFactory.parseString(
    """akka.remote.artery.canonical.port=3"""
  )

  // 2) Load the normal HOCON stack (app.conf, ref.conf, sys props, env vars):
  val regularConfig = ConfigFactory.load()

  // 3) Merge your override on top of the regular stack:
  val combined = overrideCfg.withFall@@back(regularConfig)

  // 4) (Optional) Re-load so any leftover ${?…} substitutions from sysprops/ref.conf happen:
  val config = ConfigFactory.load(combined)

  // 5) Create the ActorSystem with that merged Config:
  // val system: ActorSystem = ActorSystem.create("ForkVMachine", )

  val system = ActorSystem("ForkVMachine", config)

  system.logConfiguration()
  val cfg = system.settings.config
  val n = cfg.getInt("dining-philosophers.philosopher-count")
  val forks = (1 to n).map(i => system.actorOf(Props[Fork], s"fork$i"))
  // val extSystem = system.asInstanceOf[ExtendedActorSystem]
  // val address = extSystem.provider.getDefaultAddress

  // val fullPath = forks(0).path.toStringWithAddress(address)
  val extSystem = system.asInstanceOf[ExtendedActorSystem]
  val boundAddress = extSystem.provider.getDefaultAddress
  val boundPort = boundAddress.port.getOrElse(-1)
  println(s"ActorSystem bound to port = $boundPort")

  // println(fullPath)
//   StdIn.readLine()
}

```


#### Short summary: 

empty definition using pc, found symbol in pc: withFallback.