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