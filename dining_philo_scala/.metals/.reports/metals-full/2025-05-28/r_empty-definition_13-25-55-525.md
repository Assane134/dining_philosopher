error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Philosopher.scala:`<none>`.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Philosopher.scala
empty definition using pc, found symbol in pc: `<none>`.
empty definition using semanticdb
empty definition using fallback
non-local guesses:
	 -akka/actor.
	 -akka/actor#
	 -akka/actor().
	 -scala/concurrent/duration.
	 -scala/concurrent/duration#
	 -scala/concurrent/duration().
	 -Philosopher.
	 -Philosopher#
	 -Philosopher().
	 -Fork.
	 -Fork#
	 -Fork().
	 -.
	 -#
	 -().
	 -scala/Predef.
	 -scala/Predef#
	 -scala/Predef().
offset: 338
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/src/main/scala/Philosopher.scala
text:
```scala
package dining

import akka.actor._

import scala.concurrent.duration._

object Philosopher {
  case object Start
  case object Think
  case object EatDone
}

class Philosopher(name: String, rightFork: ActorRef, leftFork: ActorRef)
    extends Actor {
  import Philosopher._
  import Fork._

  private val eatingTime = 3.@@second

  override def receive: Receive = { case Start =>
    context.become()
  }

  def acquireLeftFork() = {
    leftFork ! Acquire
  }

  def waitForLeftFork: Receive = {
    case Acquired =>
      rightFork ! Acquire
      context.become(waitForRightFork)
    case Busy =>
      retry()
  }

  def waitForRightFork: Receive = { case Acquired =>

  }

  private def retry(): Unit = {
    // wait a bit, then start over
    context.become(receive)
    context.system.scheduler.scheduleOnce(1.second, self, Start)
  }

  private def eat() = {}
}

```


#### Short summary: 

empty definition using pc, found symbol in pc: `<none>`.