using PChecker;
using PChecker.Runtime;
using PChecker.Runtime.StateMachines;
using PChecker.Runtime.Events;
using PChecker.Runtime.Exceptions;
using PChecker.Runtime.Logging;
using PChecker.Runtime.Values;
using PChecker.Runtime.Specifications;
using Monitor = PChecker.Runtime.Specifications.Monitor;
using System;
using PChecker.SystematicTesting;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 162, 219, 414, 1998
namespace PImplementation
{
}
namespace PImplementation
{
    internal partial class Released : Event
    {
        public Released() : base() {}
        public Released (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new Released();}
    }
}
namespace PImplementation
{
    internal partial class Acquired : Event
    {
        public Acquired() : base() {}
        public Acquired (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new Acquired();}
    }
}
namespace PImplementation
{
    internal partial class Busy : Event
    {
        public Busy() : base() {}
        public Busy (IPValue payload): base(payload){ }
        public override IPValue Clone() { return new Busy();}
    }
}
namespace PImplementation
{
    internal partial class Acquire : Event
    {
        public Acquire() : base() {}
        public Acquire (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new Acquire();}
    }
}
namespace PImplementation
{
    internal partial class Release : Event
    {
        public Release() : base() {}
        public Release (PMachineValue payload): base(payload){ }
        public override IPValue Clone() { return new Release();}
    }
}
namespace PImplementation
{
    internal partial class Fork : StateMachine
    {
        private PBool inUse = ((PBool)false);
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public Fork() {
            this.sends.Add(nameof(Acquire));
            this.sends.Add(nameof(Acquired));
            this.sends.Add(nameof(Busy));
            this.sends.Add(nameof(Release));
            this.sends.Add(nameof(Released));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(Acquire));
            this.receives.Add(nameof(Acquired));
            this.receives.Add(nameof(Busy));
            this.receives.Add(nameof(Release));
            this.receives.Add(nameof(Released));
            this.receives.Add(nameof(PHalt));
        }
        
        public void Anon(Event currentMachine_dequeuedEvent)
        {
            Fork currentMachine = this;
            inUse = (PBool)(((PBool)false));
        }
        public void Anon_1(Event currentMachine_dequeuedEvent)
        {
            Fork currentMachine = this;
            PMachineValue payload = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PBool TMP_tmp0 = ((PBool)false);
            PMachineValue TMP_tmp1 = null;
            Event TMP_tmp2 = null;
            PMachineValue TMP_tmp3 = null;
            PMachineValue TMP_tmp4 = null;
            Event TMP_tmp5 = null;
            TMP_tmp0 = (PBool)(!(inUse));
            if (TMP_tmp0)
            {
                inUse = (PBool)(((PBool)true));
                TMP_tmp1 = (PMachineValue)(((PMachineValue)((IPValue)payload)?.Clone()));
                TMP_tmp2 = (Event)(new Acquired(null));
                TMP_tmp3 = (PMachineValue)(((PMachineValue)((IPValue)payload)?.Clone()));
                TMP_tmp2.Payload = TMP_tmp3;
                currentMachine.SendEvent(TMP_tmp1, (Event)TMP_tmp2);
            }
            else
            {
                TMP_tmp4 = (PMachineValue)(((PMachineValue)((IPValue)payload)?.Clone()));
                TMP_tmp5 = (Event)(new Busy(null));
                currentMachine.SendEvent(TMP_tmp4, (Event)TMP_tmp5);
            }
        }
        public void Anon_2(Event currentMachine_dequeuedEvent)
        {
            Fork currentMachine = this;
            PMachineValue payload_1 = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PMachineValue TMP_tmp0_1 = null;
            Event TMP_tmp1_1 = null;
            PMachineValue TMP_tmp2_1 = null;
            inUse = (PBool)(((PBool)false));
            TMP_tmp0_1 = (PMachineValue)(((PMachineValue)((IPValue)payload_1)?.Clone()));
            TMP_tmp1_1 = (Event)(new Released(null));
            TMP_tmp2_1 = (PMachineValue)(((PMachineValue)((IPValue)payload_1)?.Clone()));
            TMP_tmp1_1.Payload = TMP_tmp2_1;
            currentMachine.SendEvent(TMP_tmp0_1, (Event)TMP_tmp1_1);
        }
        [Start]
        [OnEntry(nameof(Anon))]
        [OnEventDoAction(typeof(Acquire), nameof(Anon_1))]
        [OnEventDoAction(typeof(Release), nameof(Anon_2))]
        class Idle : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class Philosopher : StateMachine
    {
        private PString name = ((PString)"");
        private PMachineValue leftFork = null;
        private PMachineValue rightFork = null;
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public Philosopher() {
            this.sends.Add(nameof(Acquire));
            this.sends.Add(nameof(Acquired));
            this.sends.Add(nameof(Busy));
            this.sends.Add(nameof(Release));
            this.sends.Add(nameof(Released));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(Acquire));
            this.receives.Add(nameof(Acquired));
            this.receives.Add(nameof(Busy));
            this.receives.Add(nameof(Release));
            this.receives.Add(nameof(Released));
            this.receives.Add(nameof(PHalt));
        }
        
        public void Anon_3(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PNamedTuple input = (PNamedTuple)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PString TMP_tmp0_2 = ((PString)"");
            PString TMP_tmp1_2 = ((PString)"");
            PMachineValue TMP_tmp2_2 = null;
            PMachineValue TMP_tmp3_1 = null;
            PMachineValue TMP_tmp4_1 = null;
            PMachineValue TMP_tmp5_1 = null;
            TMP_tmp0_2 = (PString)(((PNamedTuple)input)["n"]);
            TMP_tmp1_2 = (PString)(((PString)((IPValue)TMP_tmp0_2)?.Clone()));
            name = TMP_tmp1_2;
            TMP_tmp2_2 = (PMachineValue)(((PNamedTuple)input)["lf"]);
            TMP_tmp3_1 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp2_2)?.Clone()));
            leftFork = TMP_tmp3_1;
            TMP_tmp4_1 = (PMachineValue)(((PNamedTuple)input)["rf"]);
            TMP_tmp5_1 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp4_1)?.Clone()));
            rightFork = TMP_tmp5_1;
            currentMachine.RaiseGotoStateEvent<AcquireLeftFork>();
            return;
        }
        public void Anon_4(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PString TMP_tmp0_3 = ((PString)"");
            PString TMP_tmp1_3 = ((PString)"");
            PMachineValue TMP_tmp2_3 = null;
            Event TMP_tmp3_2 = null;
            PMachineValue TMP_tmp4_2 = null;
            TMP_tmp0_3 = (PString)(((PString)((IPValue)name)?.Clone()));
            TMP_tmp1_3 = (PString)(((PString) String.Format("{0} trying to acquire left fork",TMP_tmp0_3)));
            currentMachine.LogLine("" + TMP_tmp1_3);
            TMP_tmp2_3 = (PMachineValue)(((PMachineValue)((IPValue)leftFork)?.Clone()));
            TMP_tmp3_2 = (Event)(new Acquire(null));
            TMP_tmp4_2 = (PMachineValue)(currentMachine.self);
            TMP_tmp3_2.Payload = TMP_tmp4_2;
            currentMachine.SendEvent(TMP_tmp2_3, (Event)TMP_tmp3_2);
        }
        public void Anon_5(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue payload_2 = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PString TMP_tmp0_4 = ((PString)"");
            PString TMP_tmp1_4 = ((PString)"");
            TMP_tmp0_4 = (PString)(((PString)((IPValue)name)?.Clone()));
            TMP_tmp1_4 = (PString)(((PString) String.Format("{0} acquired left fork",TMP_tmp0_4)));
            currentMachine.LogLine("" + TMP_tmp1_4);
            currentMachine.RaiseGotoStateEvent<AcquireRightFork>();
            return;
        }
        public void Anon_6(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PString TMP_tmp0_5 = ((PString)"");
            PString TMP_tmp1_5 = ((PString)"");
            PMachineValue TMP_tmp2_4 = null;
            Event TMP_tmp3_3 = null;
            PMachineValue TMP_tmp4_3 = null;
            TMP_tmp0_5 = (PString)(((PString)((IPValue)name)?.Clone()));
            TMP_tmp1_5 = (PString)(((PString) String.Format("{0} left fork busy, retrying...",TMP_tmp0_5)));
            currentMachine.LogLine("" + TMP_tmp1_5);
            TMP_tmp2_4 = (PMachineValue)(((PMachineValue)((IPValue)leftFork)?.Clone()));
            TMP_tmp3_3 = (Event)(new Acquire(null));
            TMP_tmp4_3 = (PMachineValue)(currentMachine.self);
            TMP_tmp3_3.Payload = TMP_tmp4_3;
            currentMachine.SendEvent(TMP_tmp2_4, (Event)TMP_tmp3_3);
        }
        public void Anon_7(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PString TMP_tmp0_6 = ((PString)"");
            PString TMP_tmp1_6 = ((PString)"");
            PMachineValue TMP_tmp2_5 = null;
            Event TMP_tmp3_4 = null;
            PMachineValue TMP_tmp4_4 = null;
            TMP_tmp0_6 = (PString)(((PString)((IPValue)name)?.Clone()));
            TMP_tmp1_6 = (PString)(((PString) String.Format("{0} trying to acquire right fork",TMP_tmp0_6)));
            currentMachine.LogLine("" + TMP_tmp1_6);
            TMP_tmp2_5 = (PMachineValue)(((PMachineValue)((IPValue)rightFork)?.Clone()));
            TMP_tmp3_4 = (Event)(new Acquire(null));
            TMP_tmp4_4 = (PMachineValue)(currentMachine.self);
            TMP_tmp3_4.Payload = TMP_tmp4_4;
            currentMachine.SendEvent(TMP_tmp2_5, (Event)TMP_tmp3_4);
        }
        public void Anon_8(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PString TMP_tmp0_7 = ((PString)"");
            PString TMP_tmp1_7 = ((PString)"");
            PString TMP_tmp2_6 = ((PString)"");
            PString TMP_tmp3_5 = ((PString)"");
            TMP_tmp0_7 = (PString)(((PString)((IPValue)name)?.Clone()));
            TMP_tmp1_7 = (PString)(((PString) String.Format("{0} acquired right fork",TMP_tmp0_7)));
            currentMachine.LogLine("" + TMP_tmp1_7);
            TMP_tmp2_6 = (PString)(((PString)((IPValue)name)?.Clone()));
            TMP_tmp3_5 = (PString)(((PString) String.Format("{0} is eating",TMP_tmp2_6)));
            currentMachine.LogLine("" + TMP_tmp3_5);
            currentMachine.RaiseGotoStateEvent<ReleaseLeftFork>();
            return;
        }
        public void Anon_9(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PString TMP_tmp0_8 = ((PString)"");
            PString TMP_tmp1_8 = ((PString)"");
            PMachineValue TMP_tmp2_7 = null;
            Event TMP_tmp3_6 = null;
            PMachineValue TMP_tmp4_5 = null;
            TMP_tmp0_8 = (PString)(((PString)((IPValue)name)?.Clone()));
            TMP_tmp1_8 = (PString)(((PString) String.Format("{0} right fork busy, retrying...",TMP_tmp0_8)));
            currentMachine.LogLine("" + TMP_tmp1_8);
            TMP_tmp2_7 = (PMachineValue)(((PMachineValue)((IPValue)rightFork)?.Clone()));
            TMP_tmp3_6 = (Event)(new Acquire(null));
            TMP_tmp4_5 = (PMachineValue)(currentMachine.self);
            TMP_tmp3_6.Payload = TMP_tmp4_5;
            currentMachine.SendEvent(TMP_tmp2_7, (Event)TMP_tmp3_6);
        }
        public void Anon_10(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_9 = null;
            Event TMP_tmp1_9 = null;
            PMachineValue TMP_tmp2_8 = null;
            TMP_tmp0_9 = (PMachineValue)(((PMachineValue)((IPValue)leftFork)?.Clone()));
            TMP_tmp1_9 = (Event)(new Release(null));
            TMP_tmp2_8 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_9.Payload = TMP_tmp2_8;
            currentMachine.SendEvent(TMP_tmp0_9, (Event)TMP_tmp1_9);
        }
        public void Anon_11(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            currentMachine.RaiseGotoStateEvent<ReleaseRightFork>();
            return;
        }
        public void Anon_12(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PMachineValue TMP_tmp0_10 = null;
            Event TMP_tmp1_10 = null;
            PMachineValue TMP_tmp2_9 = null;
            TMP_tmp0_10 = (PMachineValue)(((PMachineValue)((IPValue)rightFork)?.Clone()));
            TMP_tmp1_10 = (Event)(new Release(null));
            TMP_tmp2_9 = (PMachineValue)(currentMachine.self);
            TMP_tmp1_10.Payload = TMP_tmp2_9;
            currentMachine.SendEvent(TMP_tmp0_10, (Event)TMP_tmp1_10);
        }
        public void Anon_13(Event currentMachine_dequeuedEvent)
        {
            Philosopher currentMachine = this;
            PString TMP_tmp0_11 = ((PString)"");
            PString TMP_tmp1_11 = ((PString)"");
            PString TMP_tmp2_10 = ((PString)"");
            PString TMP_tmp3_7 = ((PString)"");
            TMP_tmp0_11 = (PString)(((PString)((IPValue)name)?.Clone()));
            TMP_tmp1_11 = (PString)(((PString) String.Format("{0} finished eating and released forks",TMP_tmp0_11)));
            currentMachine.LogLine("" + TMP_tmp1_11);
            TMP_tmp2_10 = (PString)(((PString)((IPValue)name)?.Clone()));
            TMP_tmp3_7 = (PString)(((PString) String.Format("{0} is thinking",TMP_tmp2_10)));
            currentMachine.LogLine("" + TMP_tmp3_7);
            currentMachine.RaiseGotoStateEvent<AcquireLeftFork>();
            return;
        }
        [Start]
        [OnEntry(nameof(Anon_3))]
        class Init : State
        {
        }
        [OnEntry(nameof(Anon_4))]
        [OnEventDoAction(typeof(Acquired), nameof(Anon_5))]
        [OnEventDoAction(typeof(Busy), nameof(Anon_6))]
        class AcquireLeftFork : State
        {
        }
        [OnEntry(nameof(Anon_7))]
        [OnEventDoAction(typeof(Acquired), nameof(Anon_8))]
        [OnEventDoAction(typeof(Busy), nameof(Anon_9))]
        class AcquireRightFork : State
        {
        }
        [OnEntry(nameof(Anon_10))]
        [OnEventDoAction(typeof(Released), nameof(Anon_11))]
        class ReleaseLeftFork : State
        {
        }
        [OnEntry(nameof(Anon_12))]
        [OnEventDoAction(typeof(Released), nameof(Anon_13))]
        class ReleaseRightFork : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class NoDeadLock : Monitor
    {
        private PInt N = ((PInt)0);
        private PMachineValue philosopher = null;
        private PInt singleCount = ((PInt)0);
        private PMap held = new PMap();
        static NoDeadLock() {
            observes.Add(nameof(Acquired));
            observes.Add(nameof(Released));
        }
        
        public void Anon_14(Event currentMachine_dequeuedEvent)
        {
            NoDeadLock currentMachine = this;
            N = (PInt)(((PInt)(10)));
        }
        public void Anon_15(Event currentMachine_dequeuedEvent)
        {
            NoDeadLock currentMachine = this;
            PMachineValue payload_3 = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PSeq TMP_tmp0_12 = new PSeq();
            PBool TMP_tmp1_12 = ((PBool)false);
            PInt TMP_tmp2_11 = ((PInt)0);
            PInt TMP_tmp3_8 = ((PInt)0);
            PSeq TMP_tmp4_6 = new PSeq();
            PInt TMP_i_philosopher_tmp5 = ((PInt)0);
            PInt sizeof_philosopher_tmp6 = ((PInt)0);
            PSeq TMP_tmp7 = new PSeq();
            PInt TMP_tmp8 = ((PInt)0);
            PInt TMP_tmp9 = ((PInt)0);
            PBool TMP_tmp10 = ((PBool)false);
            PBool TMP_tmp11 = ((PBool)false);
            PInt TMP_tmp12 = ((PInt)0);
            PMachineValue TMP_tmp13 = null;
            PMachineValue TMP_tmp14 = null;
            PInt TMP_tmp15 = ((PInt)0);
            PBool TMP_tmp16 = ((PBool)false);
            PInt TMP_tmp17 = ((PInt)0);
            PBool TMP_tmp18 = ((PBool)false);
            PString TMP_tmp19 = ((PString)"");
            PString TMP_tmp20 = ((PString)"");
            PString TMP_tmp21 = ((PString)"");
            TMP_tmp0_12 = (PSeq)((held).CloneKeys());
            TMP_tmp1_12 = (PBool)(((PBool)(((PSeq)TMP_tmp0_12).Contains(payload_3))));
            if (TMP_tmp1_12)
            {
                TMP_tmp2_11 = (PInt)(((PMap)held)[payload_3]);
                TMP_tmp3_8 = (PInt)((TMP_tmp2_11) + (((PInt)(1))));
                ((PMap)held)[payload_3] = TMP_tmp3_8;
            }
            else
            {
                ((PMap)held)[payload_3] = (PInt)(((PInt)(1)));
            }
            singleCount = (PInt)(((PInt)(0)));
            TMP_tmp7 = (PSeq)((held).CloneKeys());
            TMP_tmp4_6 = TMP_tmp7;
            TMP_i_philosopher_tmp5 = (PInt)(((PInt)(-1)));
            TMP_tmp8 = (PInt)(((PInt)(TMP_tmp4_6).Count));
            sizeof_philosopher_tmp6 = TMP_tmp8;
            while (((PBool)true))
            {
                TMP_tmp9 = (PInt)((sizeof_philosopher_tmp6) - (((PInt)(1))));
                TMP_tmp10 = (PBool)((TMP_i_philosopher_tmp5) < (TMP_tmp9));
                TMP_tmp11 = (PBool)(((PBool)((IPValue)TMP_tmp10)?.Clone()));
                if (TMP_tmp11)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp12 = (PInt)((TMP_i_philosopher_tmp5) + (((PInt)(1))));
                TMP_i_philosopher_tmp5 = TMP_tmp12;
                TMP_tmp13 = (PMachineValue)(((PSeq)TMP_tmp4_6)[TMP_i_philosopher_tmp5]);
                TMP_tmp14 = (PMachineValue)(((PMachineValue)((IPValue)TMP_tmp13)?.Clone()));
                philosopher = TMP_tmp14;
                TMP_tmp15 = (PInt)(((PMap)held)[philosopher]);
                TMP_tmp16 = (PBool)((PValues.SafeEquals(TMP_tmp15,((PInt)(1)))));
                if (TMP_tmp16)
                {
                    TMP_tmp17 = (PInt)((singleCount) + (((PInt)(1))));
                    singleCount = TMP_tmp17;
                }
            }
            TMP_tmp18 = (PBool)((singleCount) < (N));
            if (TMP_tmp18)
            {
            }
            else
            {
                TMP_tmp19 = (PString)(((PString) String.Format("PSpec\\NoDeadlockSpec.p:36:13")));
                TMP_tmp20 = (PString)(((PString) String.Format("Deadlock: every philosopher holds one fork")));
                TMP_tmp21 = (PString)(((PString) String.Format("{0} {1}",TMP_tmp19,TMP_tmp20)));
                currentMachine.Assert(TMP_tmp18,"Assertion Failed: " + TMP_tmp21);
            }
        }
        public void Anon_16(Event currentMachine_dequeuedEvent)
        {
            NoDeadLock currentMachine = this;
            PMachineValue payload_4 = (PMachineValue)(gotoPayload ?? ((Event)currentMachine_dequeuedEvent).Payload);
            this.gotoPayload = null;
            PInt TMP_tmp0_13 = ((PInt)0);
            PInt TMP_tmp1_13 = ((PInt)0);
            TMP_tmp0_13 = (PInt)(((PMap)held)[philosopher]);
            TMP_tmp1_13 = (PInt)((TMP_tmp0_13) - (((PInt)(1))));
            ((PMap)held)[philosopher] = TMP_tmp1_13;
        }
        [Start]
        [OnEntry(nameof(Anon_14))]
        [OnEventDoAction(typeof(Acquired), nameof(Anon_15))]
        [OnEventDoAction(typeof(Released), nameof(Anon_16))]
        class Idle : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class DiningDriver : StateMachine
    {
        private PInt N_1 = ((PInt)0);
        private PSeq forks = new PSeq();
        private PSeq phils = new PSeq();
        private PInt i = ((PInt)0);
        private PInt j = ((PInt)0);
        private PInt right = ((PInt)0);
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public DiningDriver() {
            this.sends.Add(nameof(Acquire));
            this.sends.Add(nameof(Acquired));
            this.sends.Add(nameof(Busy));
            this.sends.Add(nameof(Release));
            this.sends.Add(nameof(Released));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(Acquire));
            this.receives.Add(nameof(Acquired));
            this.receives.Add(nameof(Busy));
            this.receives.Add(nameof(Release));
            this.receives.Add(nameof(Released));
            this.receives.Add(nameof(PHalt));
            this.creates.Add(nameof(I_Fork));
            this.creates.Add(nameof(I_Philosopher));
        }
        
        public void Anon_17(Event currentMachine_dequeuedEvent)
        {
            DiningDriver currentMachine = this;
            PBool TMP_tmp0_14 = ((PBool)false);
            PBool TMP_tmp1_14 = ((PBool)false);
            PMachineValue TMP_tmp2_12 = null;
            PInt TMP_tmp3_9 = ((PInt)0);
            PBool TMP_tmp4_7 = ((PBool)false);
            PBool TMP_tmp5_2 = ((PBool)false);
            PInt TMP_tmp6 = ((PInt)0);
            PInt TMP_tmp7_1 = ((PInt)0);
            PBool TMP_tmp8_1 = ((PBool)false);
            PInt TMP_tmp9_1 = ((PInt)0);
            PString TMP_tmp10_1 = ((PString)"");
            PMachineValue TMP_tmp11_1 = null;
            PMachineValue TMP_tmp12_1 = null;
            PNamedTuple TMP_tmp13_1 = (new PNamedTuple(new string[]{"n","lf","rf"},((PString)""), null, null));
            PMachineValue TMP_tmp14_1 = null;
            PInt TMP_tmp15_1 = ((PInt)0);
            PString TMP_tmp16_1 = ((PString)"");
            PMachineValue TMP_tmp17_1 = null;
            PMachineValue TMP_tmp18_1 = null;
            PNamedTuple TMP_tmp19_1 = (new PNamedTuple(new string[]{"n","lf","rf"},((PString)""), null, null));
            PMachineValue TMP_tmp20_1 = null;
            PInt TMP_tmp21_1 = ((PInt)0);
            N_1 = (PInt)(((PInt)(10)));
            i = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp0_14 = (PBool)((i) < (N_1));
                TMP_tmp1_14 = (PBool)(((PBool)((IPValue)TMP_tmp0_14)?.Clone()));
                if (TMP_tmp1_14)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp2_12 = (PMachineValue)(currentMachine.CreateInterface<I_Fork>( currentMachine));
                ((PSeq)forks).Insert(i, TMP_tmp2_12);
                TMP_tmp3_9 = (PInt)((i) + (((PInt)(1))));
                i = TMP_tmp3_9;
            }
            j = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp4_7 = (PBool)((j) < (N_1));
                TMP_tmp5_2 = (PBool)(((PBool)((IPValue)TMP_tmp4_7)?.Clone()));
                if (TMP_tmp5_2)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp6 = (PInt)((j) + (((PInt)(1))));
                TMP_tmp7_1 = (PInt)((TMP_tmp6) % (N_1));
                right = TMP_tmp7_1;
                TMP_tmp8_1 = (PBool)((PValues.SafeEquals(j,((PInt)(0)))));
                if (TMP_tmp8_1)
                {
                    TMP_tmp9_1 = (PInt)((j) + (((PInt)(1))));
                    TMP_tmp10_1 = (PString)(((PString) String.Format("Philosopher{0}",TMP_tmp9_1)));
                    TMP_tmp11_1 = (PMachineValue)(((PSeq)forks)[right]);
                    TMP_tmp12_1 = (PMachineValue)(((PSeq)forks)[j]);
                    TMP_tmp13_1 = (PNamedTuple)((new PNamedTuple(new string[]{"n","lf","rf"}, TMP_tmp10_1, TMP_tmp11_1, TMP_tmp12_1)));
                    TMP_tmp14_1 = (PMachineValue)(currentMachine.CreateInterface<I_Philosopher>( currentMachine, TMP_tmp13_1));
                    ((PSeq)phils).Insert(j, TMP_tmp14_1);
                }
                else
                {
                    TMP_tmp15_1 = (PInt)((j) + (((PInt)(1))));
                    TMP_tmp16_1 = (PString)(((PString) String.Format("Philosopher{0}",TMP_tmp15_1)));
                    TMP_tmp17_1 = (PMachineValue)(((PSeq)forks)[j]);
                    TMP_tmp18_1 = (PMachineValue)(((PSeq)forks)[right]);
                    TMP_tmp19_1 = (PNamedTuple)((new PNamedTuple(new string[]{"n","lf","rf"}, TMP_tmp16_1, TMP_tmp17_1, TMP_tmp18_1)));
                    TMP_tmp20_1 = (PMachineValue)(currentMachine.CreateInterface<I_Philosopher>( currentMachine, TMP_tmp19_1));
                    ((PSeq)phils).Insert(j, TMP_tmp20_1);
                }
                TMP_tmp21_1 = (PInt)((j) + (((PInt)(1))));
                j = TMP_tmp21_1;
            }
        }
        [Start]
        [OnEntry(nameof(Anon_17))]
        class Init : State
        {
        }
    }
}
namespace PImplementation
{
    internal partial class DiningDriverDeadlock : StateMachine
    {
        private PInt N_2 = ((PInt)0);
        private PSeq forks_1 = new PSeq();
        private PSeq phils_1 = new PSeq();
        private PInt i_1 = ((PInt)0);
        private PInt j_1 = ((PInt)0);
        public class ConstructorEvent : Event{public ConstructorEvent(IPValue val) : base(val) { }}
        
        protected override Event GetConstructorEvent(IPValue value) { return new ConstructorEvent((IPValue)value); }
        public DiningDriverDeadlock() {
            this.sends.Add(nameof(Acquire));
            this.sends.Add(nameof(Acquired));
            this.sends.Add(nameof(Busy));
            this.sends.Add(nameof(Release));
            this.sends.Add(nameof(Released));
            this.sends.Add(nameof(PHalt));
            this.receives.Add(nameof(Acquire));
            this.receives.Add(nameof(Acquired));
            this.receives.Add(nameof(Busy));
            this.receives.Add(nameof(Release));
            this.receives.Add(nameof(Released));
            this.receives.Add(nameof(PHalt));
            this.creates.Add(nameof(I_Fork));
            this.creates.Add(nameof(I_Philosopher));
        }
        
        public void Anon_18(Event currentMachine_dequeuedEvent)
        {
            DiningDriverDeadlock currentMachine = this;
            PBool TMP_tmp0_15 = ((PBool)false);
            PBool TMP_tmp1_15 = ((PBool)false);
            PMachineValue TMP_tmp2_13 = null;
            PInt TMP_tmp3_10 = ((PInt)0);
            PBool TMP_tmp4_8 = ((PBool)false);
            PBool TMP_tmp5_3 = ((PBool)false);
            PInt TMP_tmp6_1 = ((PInt)0);
            PString TMP_tmp7_2 = ((PString)"");
            PMachineValue TMP_tmp8_2 = null;
            PInt TMP_tmp9_2 = ((PInt)0);
            PInt TMP_tmp10_2 = ((PInt)0);
            PMachineValue TMP_tmp11_2 = null;
            PNamedTuple TMP_tmp12_2 = (new PNamedTuple(new string[]{"n","lf","rf"},((PString)""), null, null));
            PMachineValue TMP_tmp13_2 = null;
            PInt TMP_tmp14_2 = ((PInt)0);
            N_2 = (PInt)(((PInt)(10)));
            i_1 = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp0_15 = (PBool)((i_1) < (N_2));
                TMP_tmp1_15 = (PBool)(((PBool)((IPValue)TMP_tmp0_15)?.Clone()));
                if (TMP_tmp1_15)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp2_13 = (PMachineValue)(currentMachine.CreateInterface<I_Fork>( currentMachine));
                ((PSeq)forks_1).Insert(i_1, TMP_tmp2_13);
                TMP_tmp3_10 = (PInt)((i_1) + (((PInt)(1))));
                i_1 = TMP_tmp3_10;
            }
            j_1 = (PInt)(((PInt)(0)));
            while (((PBool)true))
            {
                TMP_tmp4_8 = (PBool)((j_1) < (N_2));
                TMP_tmp5_3 = (PBool)(((PBool)((IPValue)TMP_tmp4_8)?.Clone()));
                if (TMP_tmp5_3)
                {
                }
                else
                {
                    break;
                }
                TMP_tmp6_1 = (PInt)(((PInt)((IPValue)i_1)?.Clone()));
                TMP_tmp7_2 = (PString)(((PString) String.Format("Philosopher{0}",TMP_tmp6_1)));
                TMP_tmp8_2 = (PMachineValue)(((PSeq)forks_1)[j_1]);
                TMP_tmp9_2 = (PInt)((j_1) + (((PInt)(1))));
                TMP_tmp10_2 = (PInt)((TMP_tmp9_2) % (N_2));
                TMP_tmp11_2 = (PMachineValue)(((PSeq)forks_1)[TMP_tmp10_2]);
                TMP_tmp12_2 = (PNamedTuple)((new PNamedTuple(new string[]{"n","lf","rf"}, TMP_tmp7_2, TMP_tmp8_2, TMP_tmp11_2)));
                TMP_tmp13_2 = (PMachineValue)(currentMachine.CreateInterface<I_Philosopher>( currentMachine, TMP_tmp12_2));
                ((PSeq)phils_1).Insert(j_1, TMP_tmp13_2);
                TMP_tmp14_2 = (PInt)((j_1) + (((PInt)(1))));
                j_1 = TMP_tmp14_2;
            }
        }
        [Start]
        [OnEntry(nameof(Anon_18))]
        class Init : State
        {
        }
    }
}
namespace PImplementation
{
    public class testDeadlock {
        public static void InitializeLinkMap() {
            PModule.linkMap.Clear();
            PModule.linkMap[nameof(I_Philosopher)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_Fork)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_DiningDriverDeadlock)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_DiningDriverDeadlock)].Add(nameof(I_Fork), nameof(I_Fork));
            PModule.linkMap[nameof(I_DiningDriverDeadlock)].Add(nameof(I_Philosopher), nameof(I_Philosopher));
        }
        
        public static void InitializeInterfaceDefMap() {
            PModule.interfaceDefinitionMap.Clear();
            PModule.interfaceDefinitionMap.Add(nameof(I_Philosopher), typeof(Philosopher));
            PModule.interfaceDefinitionMap.Add(nameof(I_Fork), typeof(Fork));
            PModule.interfaceDefinitionMap.Add(nameof(I_DiningDriverDeadlock), typeof(DiningDriverDeadlock));
        }
        
        public static void InitializeMonitorObserves() {
            PModule.monitorObserves.Clear();
            PModule.monitorObserves[nameof(NoDeadLock)] = new List<string>();
            PModule.monitorObserves[nameof(NoDeadLock)].Add(nameof(Acquired));
            PModule.monitorObserves[nameof(NoDeadLock)].Add(nameof(Released));
        }
        
        public static void InitializeMonitorMap(ControlledRuntime runtime) {
            PModule.monitorMap.Clear();
            PModule.monitorMap[nameof(I_Philosopher)] = new List<Type>();
            PModule.monitorMap[nameof(I_Philosopher)].Add(typeof(NoDeadLock));
            PModule.monitorMap[nameof(I_Fork)] = new List<Type>();
            PModule.monitorMap[nameof(I_Fork)].Add(typeof(NoDeadLock));
            PModule.monitorMap[nameof(I_DiningDriverDeadlock)] = new List<Type>();
            PModule.monitorMap[nameof(I_DiningDriverDeadlock)].Add(typeof(NoDeadLock));
            runtime.RegisterMonitor<NoDeadLock>();
        }
        
        
        [PChecker.SystematicTesting.Test]
        public static void Execute(ControlledRuntime runtime) {
            runtime.RegisterLog(new PCheckerLogTextFormatter());
            runtime.RegisterLog(new PCheckerLogJsonFormatter());
            PModule.runtime = runtime;
            PHelper.InitializeInterfaces();
            PHelper.InitializeEnums();
            InitializeLinkMap();
            InitializeInterfaceDefMap();
            InitializeMonitorMap(runtime);
            InitializeMonitorObserves();
            runtime.CreateStateMachine(typeof(DiningDriverDeadlock), "DiningDriverDeadlock");
        }
    }
}
namespace PImplementation
{
    public class testNoDeadlock {
        public static void InitializeLinkMap() {
            PModule.linkMap.Clear();
            PModule.linkMap[nameof(I_Philosopher)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_Fork)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_DiningDriver)] = new Dictionary<string, string>();
            PModule.linkMap[nameof(I_DiningDriver)].Add(nameof(I_Fork), nameof(I_Fork));
            PModule.linkMap[nameof(I_DiningDriver)].Add(nameof(I_Philosopher), nameof(I_Philosopher));
        }
        
        public static void InitializeInterfaceDefMap() {
            PModule.interfaceDefinitionMap.Clear();
            PModule.interfaceDefinitionMap.Add(nameof(I_Philosopher), typeof(Philosopher));
            PModule.interfaceDefinitionMap.Add(nameof(I_Fork), typeof(Fork));
            PModule.interfaceDefinitionMap.Add(nameof(I_DiningDriver), typeof(DiningDriver));
        }
        
        public static void InitializeMonitorObserves() {
            PModule.monitorObserves.Clear();
            PModule.monitorObserves[nameof(NoDeadLock)] = new List<string>();
            PModule.monitorObserves[nameof(NoDeadLock)].Add(nameof(Acquired));
            PModule.monitorObserves[nameof(NoDeadLock)].Add(nameof(Released));
        }
        
        public static void InitializeMonitorMap(ControlledRuntime runtime) {
            PModule.monitorMap.Clear();
            PModule.monitorMap[nameof(I_Philosopher)] = new List<Type>();
            PModule.monitorMap[nameof(I_Philosopher)].Add(typeof(NoDeadLock));
            PModule.monitorMap[nameof(I_Fork)] = new List<Type>();
            PModule.monitorMap[nameof(I_Fork)].Add(typeof(NoDeadLock));
            PModule.monitorMap[nameof(I_DiningDriver)] = new List<Type>();
            PModule.monitorMap[nameof(I_DiningDriver)].Add(typeof(NoDeadLock));
            runtime.RegisterMonitor<NoDeadLock>();
        }
        
        
        [PChecker.SystematicTesting.Test]
        public static void Execute(ControlledRuntime runtime) {
            runtime.RegisterLog(new PCheckerLogTextFormatter());
            runtime.RegisterLog(new PCheckerLogJsonFormatter());
            PModule.runtime = runtime;
            PHelper.InitializeInterfaces();
            PHelper.InitializeEnums();
            InitializeLinkMap();
            InitializeInterfaceDefMap();
            InitializeMonitorMap(runtime);
            InitializeMonitorObserves();
            runtime.CreateStateMachine(typeof(DiningDriver), "DiningDriver");
        }
    }
}
// TODO: NamedModule Philosopher_1
// TODO: NamedModule Fork_1
// TODO: NamedModule DiningDriverDeadlock_1
// TODO: NamedModule DiningDriver_1
namespace PImplementation
{
    public class I_Fork : PMachineValue {
        public I_Fork (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_Philosopher : PMachineValue {
        public I_Philosopher (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_DiningDriver : PMachineValue {
        public I_DiningDriver (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public class I_DiningDriverDeadlock : PMachineValue {
        public I_DiningDriverDeadlock (StateMachineId machine, List<string> permissions) : base(machine, permissions) { }
    }
    
    public partial class PHelper {
        public static void InitializeInterfaces() {
            PInterfaces.Clear();
            PInterfaces.AddInterface(nameof(I_Fork), nameof(Acquire), nameof(Acquired), nameof(Busy), nameof(Release), nameof(Released), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_Philosopher), nameof(Acquire), nameof(Acquired), nameof(Busy), nameof(Release), nameof(Released), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_DiningDriver), nameof(Acquire), nameof(Acquired), nameof(Busy), nameof(Release), nameof(Released), nameof(PHalt));
            PInterfaces.AddInterface(nameof(I_DiningDriverDeadlock), nameof(Acquire), nameof(Acquired), nameof(Busy), nameof(Release), nameof(Released), nameof(PHalt));
        }
    }
    
}
namespace PImplementation
{
    public partial class PHelper {
        public static void InitializeEnums() {
            PEnum.Clear();
        }
    }
    
}
#pragma warning restore 162, 219, 414
