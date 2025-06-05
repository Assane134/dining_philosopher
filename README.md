# Dining Philosopher Project

## Projects

### Akka implementation

- uses actor programming from akka and scala
- parametric number of philosophers
- deadlock prevention thanks to the asymmetric strategy (one philosopher takes the forks in the reverse order)

### P Language implementation

- uses the P Language for verification
- demonstrates two scenario: a case with deadlock and a scenario without deadlock (asymmetric solution scenario)

This repository contains:

- `dining_philo_p_lang/` — P Language implementation for verification
- `dining_philo_scala/` — Akka implementation of the problem

## Requirements

### Akka implementation

- Scala 2.13.16
- SBT (Scala Build Tool)
- Java 8 or higher

### P implementation

- P langage compiler and runtime

## Quick start

### Run Akka implementation

Launch firstly the virtual machine of the forks

```bash
cd dining_philo_scala
sbt "runMain dining.ForkVMachine"
```

Then launch the virtual machine of the philosophers

```bash
sbt "runMain dining.PhilosopherVMachine"
```

### Run P Language implementation

```bash
cd dining_philo_p_lang
p compile
p check -tc testDeadlock -s 5 # check the deadlock scenario without the implementation of the asymmetric strategy
p check -tc testNoDeadlock -s 5 # check the solution implemented to avoid deadlock
```

For detailed instructions and documentation, please refer to the README of each project
