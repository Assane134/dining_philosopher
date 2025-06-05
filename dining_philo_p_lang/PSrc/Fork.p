// Events
event Released: machine; // event to signal a fork has been released
event Acquired: machine; // event to signal a fork has been acquired
event Busy; // event to signal a fork is busy

// Fork machine models a single fork in the dining philosophers problem
machine Fork {
    var inUse: bool; // Indicates if the fork is currently in use

    start state Idle {

        entry {
            inUse = false; // Fork is initially not in use
        }

        // A philosopher requests this fork, with the payload being the philosopher machine
        on Acquire do (payload: machine) {
            if (!inUse) {
                inUse = true;
                // grant the fork
                send payload, Acquired, payload; // send acquired event to the philosopher
            } else {
                // fork busy, tell philosopher to retry
                send payload, Busy;
            }
        }

        // Philosopher releases the fork
        on Release do (payload: machine) {
            inUse = false; // Fork is now free
            send payload, Released, payload;  // send released event to the philosopher
        }
    }
}
