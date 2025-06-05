error id: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/Philosopher%20-%20Copy.scala:`<none>`.
file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/Philosopher%20-%20Copy.scala
empty definition using pc, found symbol in pc: `<none>`.
empty definition using semanticdb
empty definition using fallback
non-local guesses:
	 -akka/actor/Random.
	 -akka/actor/Random#
	 -akka/actor/Random().
	 -scala/util/Random.
	 -scala/util/Random#
	 -scala/util/Random().
	 -scala/concurrent/duration/Random.
	 -scala/concurrent/duration/Random#
	 -scala/concurrent/duration/Random().
	 -Random.
	 -Random#
	 -Random().
	 -scala/Predef.Random.
	 -scala/Predef.Random#
	 -scala/Predef.Random().
offset: 60
uri: file:///C:/Users/sankara/Documents/Cours%20UM6P/cloud%20programming/dining_philo_prob/dining_philo_scala/src/main/scala/Philosopher%20-%20Copy.scala
text:
```scala
// package dining
import akka.actor._
import scala.util.Ra@@ndom
import scala.concurrent.duration._

// define messages for Philosopher
object Philosopher {
  case object Start
  case object Think
  case object EatDone
  case object RetryAcquireRight
  case object AcquireRightFork
}

class Philosopher(name: String, rightFork: ActorRef, leftFork: ActorRef)
    extends Actor {
  import Philosopher._
  import Fork._
  import context.dispatcher

  private val eatingTime = 3.second // time taken to eat
  private val thinkingTime = 3.second // time taken to think

  def receive: Receive = { case Start =>
    leftFork ! Acquire
    context.become(waitForLeftFork)
  }

  def waitForLeftFork: Receive = {
    case Acquired =>
      println(s"$name acquired left fork")
      context.system.scheduler.scheduleOnce(
        2.second,
        self,
        AcquireRightFork
      ) // after acquiring left fork, try to acquire right fork after a delay
    // I had put that delay to get the deadlock scenario but I keep it since it doesn't change the problem
    case Busy =>
      retry() // if left fork is busy, retry acquiring it after a delay
    case AcquireRightFork =>
      rightFork ! Acquire // try to acquire right fork
      context.become(waitForRightFork)
  }

  def waitForRightFork: Receive = {
    case Acquired =>
      println(s"$name acquired right fork")
      eat()
      context.become(waitForEat)
    case Busy =>
      context.system.scheduler.scheduleOnce(
        1.second,
        self,
        RetryAcquireRight
      ) // if right fork is busy, retry acquiring it after a delay

    case RetryAcquireRight =>
      rightFork ! Acquire
  }

  def waitForEat: Receive = { case EatDone =>
    rightFork ! Release
    leftFork ! Release
    think() // after eating, release both forks and think
  }

  private def retry(): Unit = {
    // wait a bit, then start over
    context.become(receive)
    context.system.scheduler.scheduleOnce(1.second, self, Start)
  }

  private def eat() = {
    println(s"$name is eating") // simulate eating time
    context.system.scheduler.scheduleOnce(
      eatingTime,
      self,
      EatDone
    ) // after eating time, send EatDone message
  }

  private def think() = {
    println(s"$name is thinking")
    context.become(
      receive
    ) // go back to the initial behavior where the philosopher try to eat
    context.system.scheduler.scheduleOnce(
      thinkingTime,
      self,
      Start
    ) // try to eat again after thinking time by sending Start message
  }
}

```


#### Short summary: 

empty definition using pc, found symbol in pc: `<none>`.