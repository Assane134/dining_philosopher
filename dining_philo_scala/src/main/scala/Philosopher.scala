package dining

import akka.actor._
import scala.concurrent.duration._
import scala.util.Random

// define messages for Philosopher
object Philosopher {
  case object Start // message to start the philosopher's actions
  case object EatDone // message sent when the philosopher finishes eating
  case object RetryAcquireRight // message to retry acquiring the right fork
  case object AcquireRightFork // message to acquire the right fork
}

class Philosopher(name: String, rightFork: ActorRef, leftFork: ActorRef)
    extends Actor {
  import Philosopher._
  import Fork._
  import context.dispatcher

  // ─── color codes for printing ───
  private val RESET = "\u001B[0m" // reset color
  private val RED = "\u001B[31m"
  private val GREEN = "\u001B[32m"
  private val YELLOW = "\u001B[33m"
  private val BLUE = "\u001B[34m"
  private val PURPLE = "\u001B[35m"
  private val CYAN = "\u001B[36m"

  private val eatingTime = 3.seconds // time taken to eat
  private val thinkingTime = 3.seconds // time taken to think

  def receive: Receive = { case Start =>
    println(s"$CYAN[$name] trying to acquire left fork$RESET")
    leftFork ! Acquire
    context.become(waitForLeftFork)
  }

  def waitForLeftFork: Receive = {
    case Acquired =>
      println(s"$GREEN[$name] acquired left fork$RESET")
      // after acquiring left fork, try to acquire right fork after a brief “thinking” delay
      context.system.scheduler.scheduleOnce(2.seconds, self, AcquireRightFork)

    case Busy =>
      println(s"$RED[$name] left fork busy, will retry in 1s$RESET")
      retry() // retry acquiring left fork after 1 second

    case AcquireRightFork =>
      println(s"$CYAN[$name] now attempting to acquire right fork$RESET")
      rightFork ! Acquire
      context.become(waitForRightFork)
  }

  def waitForRightFork: Receive = {
    case Acquired =>
      println(s"$GREEN[$name] acquired right fork$RESET")
      eat() // after acquiring both forks, start eating
      context.become(waitForEat) // waiting for eating to finish

    case Busy =>
      println(s"$RED[$name] right fork busy, retrying in 1s$RESET")
      context.system.scheduler.scheduleOnce(1.second, self, RetryAcquireRight)

    case RetryAcquireRight =>
      println(s"$CYAN[$name] retrying acquire of right fork$RESET")
      rightFork ! Acquire
  }

  def waitForEat: Receive = { case EatDone =>
    println(
      s"$YELLOW[$name] finished eating; releasing forks and starting to think$RESET"
    )
    rightFork ! Release
    leftFork ! Release
    think() // after eating, release both forks and think
  }

  private def retry(): Unit = {
    // wait 1 second, then send Start to self to retry left‐fork acquisition
    context.become(receive)
    context.system.scheduler.scheduleOnce(1.second, self, Start)
  }

  private def eat(): Unit = {
    println(s"$YELLOW[$name] is eating$RESET")
    // simulate eating time, then send EatDone
    context.system.scheduler.scheduleOnce(eatingTime, self, EatDone)
  }

  private def think(): Unit = {
    println(s"$BLUE[$name] is thinking$RESET")
    // go back to initial behavior to try to eat again after thinkingTime
    context.become(receive)
    context.system.scheduler.scheduleOnce(thinkingTime, self, Start)
  }
}
