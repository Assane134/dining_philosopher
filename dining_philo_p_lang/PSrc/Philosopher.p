

// Events
event Acquire: machine; // event to acquire a fork
event Release : machine; // event to release a fork

// Philosopher machine models a single philosopher in the dining philosophers problem
machine Philosopher {
    var name: string; // Name of the philosopher
    var leftFork: machine; 
    var rightFork: machine;

    start state Init {
        entry (input: (n: string, lf: machine, rf: machine)) {
            name = input.n;
            leftFork = input.lf;
            rightFork = input.rf;
            goto AcquireLeftFork;
        }
       
    }

    state AcquireLeftFork{
        entry{
            print format("{0} trying to acquire left fork", name);
            send leftFork, Acquire, this; // request left fork
        }

        on Acquired do (payload: machine) {
            print format("{0} acquired left fork", name);
            // schedule attempt to get right fork
            goto AcquireRightFork;
        }
        on Busy do {
            print format("{0} left fork busy, retrying...", name);
            send leftFork, Acquire, this; // request left fork again
        }
    }

    state AcquireRightFork {

        entry{
            print format("{0} trying to acquire right fork", name);
            send rightFork, Acquire, this;
        }

        on Acquired do {
            print format("{0} acquired right fork", name);
            print format("{0} is eating", name);
            goto ReleaseLeftFork; // release the forks after eating, we begin with left fork
        }

        on Busy do {
            print format("{0} right fork busy, retrying...", name);
            send rightFork, Acquire, this;   
        }

    }

    state ReleaseLeftFork {
        entry {
            // release both forks after eating, begin with left fork 
            send leftFork, Release, this;
        }
        on Released do {
            goto ReleaseRightFork; // wait for right fork to be released
        }
    }

    state ReleaseRightFork {
        entry {
            send rightFork, Release, this; // release right fork
        }

        on Released do {
            print format("{0} finished eating and released forks", name);
            // start thinking after eating
            print format("{0} is thinking", name);
            goto AcquireLeftFork; // go back to acquiring left fork
        }
    }

}
