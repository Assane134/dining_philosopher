
// test script that gives a deadlock in the Dining Philosophers problem
test testDeadlock [main=DiningDriverDeadlock]:
assert NoDeadLock in
(union Philosopher, Fork, DiningDriverDeadlock);


// test script that does not give a deadlock in the Dining Philosophers problem
test testNoDeadlock [main=DiningDriver]:
assert NoDeadLock in
(union Philosopher, Fork, DiningDriver);