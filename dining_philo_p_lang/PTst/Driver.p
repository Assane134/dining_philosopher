machine DiningDriver {
    // This implementation doesn't give a deadlock
    var N: int;
    var forks: seq[machine];
    var phils: seq[machine];
    var i : int;
    var j : int;
    var right: int;

    start state Init {
        entry  {
            // Number of philosophers/forks
            N = 10; // Set to your desired number of philosophers

            // Create N forks
            i = 0;
            while (i < N) {
                forks += (i, new Fork());
                i = i + 1;
            }

            j = 0;
            // Create N philosopher machines in a ring topology

            // To avoid deadlock, we inverse the order of fork acquisition for the first philosopher 
            // its usual left fork becomes right fork and its usual right fork becomes left fork

            // The other philosophers acquire their forks in the usual order

            // This way, the first philosopher picks right then left, while all others pick left then right

            while (j < N) {
                right = (j + 1) % N;
                if (j == 0) {
                    // Philosopher1 picks right then left
                    phils += (j, new Philosopher(
                        (n= format("Philosopher{0}", (j + 1)),
                        lf = forks[right],  // leftFork parameter receives forks[right]
                        rf = forks[j] )      // rightFork parameter receives forks[0]
                    ));
                } else {
                    // All others pick left then right
                    phils += (j, new Philosopher(
                        (n= format("Philosopher{0}", (j + 1)),
                        lf = forks[j],      // left fork
                        rf = forks[right]   // right fork)
                    )));
                }

                j = j + 1;
            }
        }
    }
}



machine DiningDriverDeadlock
{
    // This implementation gives a deadlock
    // because all philosophers try to pick left then right

    var N: int;
    var forks: seq[machine];
    var phils: seq[machine];
    var i : int;
    var j : int;

    start state Init {
        entry  {
            // Number of philosophers/forks
            N = 10; // Set to your desired number of philosophers

            // Create N forks
            i = 0;
            while (i < N) {
                forks += (i, new Fork());
                i = i + 1;
            }

            j = 0;
            // Create N philosophers, each picking left then right
            while (j < N) {
                phils += (j, new Philosopher(
                    (n = format("Philosopher{0}", i),
                    // left fork = forks[i]
                    lf = forks[j],
                    // right fork = forks[(i+1) % N]
                    rf = forks[(j + 1) % N])
                ));

                j = j + 1;
            }
        }
    }
}