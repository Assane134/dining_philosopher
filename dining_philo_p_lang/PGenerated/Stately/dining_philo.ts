import { createMachine, assign } from "xstate";
interface Context {
  retries: number;
}
const Fork = createMachine<Context>({
  id: "Fork",
  initial: "Idle",
  states: {
    Idle: {
      on: {
        Acquire: { target: [] },
        Release: { target: [] },
      },
    },
  },
});
const Philosopher = createMachine<Context>({
  id: "Philosopher",
  initial: "Init",
  states: {
    Init: {
      always: [{ target: ["AcquireLeftFork"] }],
    },
    AcquireLeftFork: {
      on: {
        Acquired: { target: ["AcquireRightFork"] },
        Busy: { target: [] },
      },
    },
    AcquireRightFork: {
      on: {
        Acquired: { target: ["Eating"] },
        Busy: { target: [] },
      },
    },
    Eating: {
      on: {
        EatDone: { target: ["AcquireLeftFork"] },
      },
    },
  },
});
const DiningDriver = createMachine<Context>({
  id: "DiningDriver",
  initial: "Init",
  states: {
    Init: {},
  },
});
const DiningDriverDeadlock = createMachine<Context>({
  id: "DiningDriverDeadlock",
  initial: "Init",
  states: {
    Init: {},
  },
});
