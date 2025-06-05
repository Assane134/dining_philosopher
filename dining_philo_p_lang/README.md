# Dining philosopher project in P Language

This project implements dining philosopher problem in P language to check if the solution implemented in Akka prevent deadlocks.
The project includes two scenarios: a scenario leading to deadlock (solution is not implemented) and a scenario where the asymmetric strategy is implemented.

## Assignment requirements

In this work, all the requirements have been fulfilled:

✅ At least 5 philosophers and 5 forks

✅ Parametric number of philosophers and forks

✅ Deadlock scenario test

✅ A test demonstrating the asymmetric strategy

✅ Implementation matches Akka implementation structure

## Implementation and execution

### Requirements

- P langage compiler and runtime

### Parameters tuning

In this work it is possible to tune the number of philosophers and forks. For that, modify the variable `N` in the files
`PTst\Driver.p` (in the machines `DiningDriver` and `DiningDriverDeadlock`) and in the file `PSpec\NoDeadlockSpec.p` (in the spec `NoDeadLock`). The default number of philosophers is 10.

### Drivers

Two driver machines have been implemented in `PTst\Driver.p` for the two test scenarios. The first machine is `DiningDriver` (strategy to avoid the deadlock) and the second machine is `DiningDriverDeadlock` (leads to a deadlock)

### Test scripts

The test scripts are in the file `PTst\Script.p`. In that file, we have two tests for the two scenarios

```p
    // test script that gives a deadlock in the Dining Philosophers problem
    test testDeadlock [main=DiningDriverDeadlock]:
    assert NoDeadLock in
    (union Philosopher, Fork, DiningDriverDeadlock);


    // test script that does not give a deadlock in the Dining Philosophers problem
    test testNoDeadlock [main=DiningDriver]:
    assert NoDeadLock in
    (union Philosopher, Fork, DiningDriver);
```

### Specification implementation

The specification can be found in the file `PSpec\NoDeadlockSpec.p`. The idea is to store the number of forks acquired by each philosopher. We can then count and if all the philosophers have one fork, we can say there is a deadlock.

```p
// This specification checks for deadlock in a dining philosophers scenario
spec NoDeadLock observes Acquired, Released {

    var N: int; // number of philosophers (and forks)
    var philosopher: machine;

    var singleCount: int; // count of philosophers holding exactly one fork

    // map from philosopher → how many forks currently held by each philosopher
    var held: map[machine, int];

    start state Idle {
        entry {
            N = 10; // Set to your desired number of philosophers
            }

        on Acquired do (payload: machine) {

            // when a fork is acquired, increment the count for that philosopher
            if (payload in keys(held)){
                held[payload] = held[payload] + 1;
            }
            else {
                held[payload] = 1;
            }

            // count how many philosophers hold one fork
            singleCount = 0;
            foreach (philosopher in keys(held)) {
                if (held[philosopher] == 1) {
                    singleCount = singleCount + 1;
                }
            }

            // if all N have one → deadlock
            assert singleCount < N,
                   "Deadlock: every philosopher holds one fork";
        }

        on Released do (payload: machine) {
            // decrement when a fork is given back
            held[philosopher] = held[philosopher] - 1;
        }
}
}
```

### Execution of the program

1. Navigate to project directory

   ```bash
   cd dining_philo_p_lang
   ```

2. Compile the project

   ```bash
   p compile
   ```

3. Launch the test for the scenario with deadlock

   ```bash
   p check -tc testDeadlock -s 5
   ```

4. Launch the test for the scenario without deadlock

   ```bash
   p check -tc testNoDeadlock -s 5
   ```

## Expected outcome

### Deadlock example output

After running `p check -tc testDeadlock -s 5`:

```
.. Searching for a P compiled file locally in folder ./PGenerated/
.. Found a P compiled file: ./PGenerated/CSharp\net8.0\dining_philo.dll
.. Checking ./PGenerated/CSharp\net8.0\dining_philo.dll
.. Test case :: testDeadlock
... Checker is using 'random' strategy (seed:3775966519).
..... Schedule #1
..... Schedule #2
..... Schedule #3
Checker found a bug.
... Emitting traces:
..... Writing PCheckerOutput\BugFinding\dining_philo_0_0.txt
..... Writing PCheckerOutput\BugFinding\dining_philo_0_0.trace.json
..... Writing PCheckerOutput\BugFinding\dining_philo_0_0.schedule
... Elapsed 41.22 sec and used 0.32 GB.
... Emitting coverage report:
..... Writing PCheckerOutput\BugFinding\dining_philo.coverage.txt
..... Writing PCheckerOutput\BugFinding\dining_philo.sci
... Checking statistics:
..... Found 1 bug.
... Scheduling statistics:
..... Explored 3 schedules
..... Explored 2 timelines
..... Found 33.33% buggy schedules.
..... Number of scheduling points in terminating schedules: 90 (min), 66696 (avg), 100000 (max).
..... Exceeded the max-steps bound of '10000' in 66.67% of the fair schedules.
..... Writing PCheckerOutput\BugFinding\dining_philo_pchecker_summary.txt
... Elapsed 41.4284786 sec.
. Done
~~ [PTool]: Thanks for using P! ~~
```

### Deadlock-free example output

After running `p check -tc testNoDeadlock -s 5`

```
.. Searching for a P compiled file locally in folder ./PGenerated/
.. Found a P compiled file: ./PGenerated/CSharp\net8.0\dining_philo.dll
.. Checking ./PGenerated/CSharp\net8.0\dining_philo.dll
.. Test case :: testNoDeadlock
... Checker is using 'random' strategy (seed:685608200).
..... Schedule #1
..... Schedule #2
..... Schedule #3
..... Schedule #4
..... Schedule #5
... Emitting coverage report:
..... Writing PCheckerOutput\BugFinding\dining_philo.coverage.txt
..... Writing PCheckerOutput\BugFinding\dining_philo.sci
... Checking statistics:
..... Found 0 bugs.
... Scheduling statistics:
..... Explored 5 schedules
..... Explored 1 timeline
..... Number of scheduling points in terminating schedules: 100000 (min), 100000 (avg), 100000 (max).
..... Exceeded the max-steps bound of '10000' in 100.00% of the fair schedules.
..... Writing PCheckerOutput\BugFinding\dining_philo_pchecker_summary.txt
... Elapsed 100.9606301 sec.
. Done
~~ [PTool]: Thanks for using P! ~~
```
