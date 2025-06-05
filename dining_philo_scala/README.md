# Dining philosopher project in Akka

This project implements the parametric version of the dining philosopher problem in Akka. This project includes concurrent programming,
deadlock provention, remote communication between virtual machines and actor-based communication

## Assignment requirements

In this work, all the requirements have been fulfilled:

✅ At least 5 philosophers and 5 forks

✅ Parametric number of philosophers and forks

✅ Remote virtual machines (one machine for forks and another for philosophers)

✅ Deadlock scenario

✅ A solution to the deadlock by using the asymmetric strategy

## Deadlock prevention mechanism

To prevent deadlock, we use the asymmetric strategy.

As shown in the code below:

- **When `deadlock = true`**

  - Everyone (including `Philosopher1`) tries to pick up their left fork first.
  - This symmetric ordering can produce a **deadlock**: each philosopher grabs the fork to their left, then all wait for the fork to their right and never proceed.

- **When `deadlock = false`**
  - We deliberately invert the fork-acquisition order for `Philosopher1` only.
  - `Philosopher1` picks up the fork on their **right** first, then the left.
  - All other philosophers still pick up **left → right**.
  - This **breaks the circular-wait cycle**, guaranteeing that at least one philosopher can complete eating and release forks, preventing deadlock.

```scala
val philosophers = (1 to n).map { i =>
    if (i == 1) {
      // Special case for the first philosopher to avoid or not deadlock
      if (deadlock) {
        // If deadlock is enabled, don't reverse the order of forks for the first philosopher
        val rightFork = forkRefs(i % n)
        val leftFork = forkRefs(i - 1)
        system.actorOf(
          Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
          s"philosopher$i"
        )
      } else {
        // If deadlock is not enabled, reverse the order of forks for the first philosopher
        val rightFork = forkRefs(i - 1)
        val leftFork = forkRefs(i % n)

        system.actorOf(
          Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
          s"philosopher$i"
        )
      }
    } else {
      // For all other philosophers, use the normal order of forks
      val rightFork = forkRefs(i % n)
      val leftFork = forkRefs(i - 1)

      system.actorOf(
        Props(new Philosopher(s"Philosopher$i", rightFork, leftFork)),
        s"philosopher$i"
      )
    }

  }
```

## Run the program

### Requirements

- Scala 2.13.16
- SBT (Scala Build Tool)
- Java 8 or higher

### Parameters tuning

It is possible to change some parameters to get different behaviors of the program. These parameters are in `src\main\resources\application.conf`

- To change the number of philosophers, change the parameter dining-philosophers.philosopher-count in `application.conf` (the default value is 10)
- To decide to get deadlock or not, change the parameter dining-philosophers.deadlock in `application.conf` (true if you want deadlock, false if you don't want deadlock)

For example, in `application.conf`, you will find:

```hocon

dining-philosophers {
  philosopher-count = 10
  deadlock: false
}

```

### Execution of the program

1. Navigate to project directory

   ```bash
   cd dining_philo_scala
   ```

2. Compile the project

   ```bash
   sbt compile
   ```

3. Launch firstly the virtual machine of the forks

   ```bash
   sbt "runMain dining.ForkVMachine"
   ```

4. Then launch the virtual machine of the philosophers

   ```bash
   sbt "runMain dining.PhilosopherVMachine"
   ```

## Expected outcome

### Console Output Color Guide

When you run the `Philosopher` actors, their `println` messages are colored to make it easier to track each action at a glance. Here is what each color means:

- 🔵 **Blue**

  - A philosopher is **thinking**.

- 💙 **Cyan**

  - A philosopher is **attempting** to acquire a fork (left or right).

- 🟢 **Green**

  - A philosopher has **successfully acquired** a fork.

- 🟡 **Yellow**

  - A philosopher is **eating** or has **finished eating** and is releasing both forks.

- 🔴 **Red**
  - A philosopher tried to acquire a fork but found it **busy**—will retry after a delay.

### Deadlock example output

Here, each philosopher acquire one fork, leading to a deadlock

```
[info] [Philosopher1] acquired left fork
[info] [Philosopher6] acquired left fork
[info] [Philosopher3] acquired left fork
[info] [Philosopher4] acquired left fork
[info] [Philosopher7] acquired left fork
[info] [Philosopher5] acquired left fork
[info] [Philosopher2] acquired left fork
[info] [Philosopher8] acquired left fork
[info] [Philosopher9] acquired left fork
[info] [Philosopher10] acquired left fork
[info] [Philosopher2] now attempting to acquire right fork
[info] [Philosopher3] now attempting to acquire right fork
[info] [Philosopher7] now attempting to acquire right fork
[info] [Philosopher5] now attempting to acquire right fork
[info] [Philosopher4] now attempting to acquire right fork
[info] [Philosopher1] now attempting to acquire right fork
[info] [Philosopher8] now attempting to acquire right fork
[info] [Philosopher9] now attempting to acquire right fork
[info] [Philosopher10] now attempting to acquire right fork
[info] [Philosopher2] right fork busy, retrying in 1s
[info] [Philosopher8] right fork busy, retrying in 1s
[info] [Philosopher1] right fork busy, retrying in 1s
[info] [Philosopher5] right fork busy, retrying in 1s
[info] [Philosopher7] right fork busy, retrying in 1s
[info] [Philosopher9] right fork busy, retrying in 1s
[info] [Philosopher3] right fork busy, retrying in 1s
[info] [Philosopher4] right fork busy, retrying in 1s
[info] [Philosopher6] now attempting to acquire right fork
[info] [Philosopher10] right fork busy, retrying in 1s
[info] [Philosopher6] right fork busy, retrying in 1s
```

### No deadlock example output

Here, the philosophers are able to eat

```
[info] [Philosopher7] right fork busy, retrying in 1s
[info] [Philosopher1] acquired right fork
[info] [Philosopher1] is eating
[info] [Philosopher8] right fork busy, retrying in 1s
[info] [Philosopher5] right fork busy, retrying in 1s
[info] [Philosopher3] right fork busy, retrying in 1s
[info] [Philosopher2] left fork busy, will retry in 1s
[info] [Philosopher4] right fork busy, retrying in 1s
[info] [Philosopher6] right fork busy, retrying in 1s
[info] [Philosopher8] retrying acquire of right fork
[info] [Philosopher4] retrying acquire of right fork
[info] [Philosopher3] retrying acquire of right fork
[info] [Philosopher2] trying to acquire left fork
[info] [Philosopher5] retrying acquire of right fork
[info] [Philosopher7] retrying acquire of right fork
[info] [Philosopher6] retrying acquire of right fork
[info] [Philosopher4] right fork busy, retrying in 1s
[info] [Philosopher2] left fork busy, will retry in 1s
[info] [Philosopher3] right fork busy, retrying in 1s
[info] [Philosopher5] right fork busy, retrying in 1s
[info] [Philosopher8] right fork busy, retrying in 1s
[info] [Philosopher7] right fork busy, retrying in 1s
[info] [Philosopher6] right fork busy, retrying in 1s
[info] [Philosopher2] trying to acquire left fork
[info] [Philosopher8] retrying acquire of right fork
[info] [Philosopher5] retrying acquire of right fork
[info] [Philosopher3] retrying acquire of right fork
[info] [Philosopher4] retrying acquire of right fork
[info] [Philosopher7] retrying acquire of right fork
[info] [Philosopher6] retrying acquire of right fork
[info] [Philosopher7] right fork busy, retrying in 1s
[info] [Philosopher8] right fork busy, retrying in 1s
[info] [Philosopher2] left fork busy, will retry in 1s
[info] [Philosopher5] right fork busy, retrying in 1s
[info] [Philosopher4] right fork busy, retrying in 1s
[info] [Philosopher3] right fork busy, retrying in 1s
[info] [Philosopher6] right fork busy, retrying in 1s
[info] [Philosopher10] trying to acquire left fork
[info] [Philosopher10] left fork busy, will retry in 1s
[info] [Philosopher9] finished eating; releasing forks and starting to think
[info] [Philosopher1] finished eating; releasing forks and starting to think
[info] [Philosopher9] is thinking
[info] [Philosopher1] is thinking
[info] [Philosopher7] retrying acquire of right fork
[info] [Philosopher6] retrying acquire of right fork
[info] [Philosopher3] retrying acquire of right fork
[info] [Philosopher4] retrying acquire of right fork
[info] [Philosopher5] retrying acquire of right fork
[info] [Philosopher2] trying to acquire left fork
[info] [Philosopher8] retrying acquire of right fork
[info] [Philosopher8] acquired right fork
[info] [Philosopher8] is eating
[info] [Philosopher3] right fork busy, retrying in 1s
[info] [Philosopher6] right fork busy, retrying in 1s
[info] [Philosopher2] acquired left fork
[info] [Philosopher5] right fork busy, retrying in 1s
[info] [Philosopher7] right fork busy, retrying in 1s
[info] [Philosopher4] right fork busy, retrying in 1s
[info] [Philosopher10] trying to acquire left fork
[info] [Philosopher10] acquired left fork
```
