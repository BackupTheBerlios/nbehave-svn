using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;


using NBehave.Framework;
using NBehave.Framework.World;
using NBehave.Framework.Story;
using NBehave.Framework.Scenario;

using NMock2;

namespace NBehave.Framework.BehaviourNUnit
{
    public class FakeStory : Story.Story 
    {
        public bool didCallScenarios = false;
        public bool didCallRun = false;


        public override void Story()
        {
            AsA("Developer").IWant("this to work").SoThat("I can move to the next feature");
        }


        public override void Scenarios()
        {
            didCallScenarios = true;
            IScenario scenario = PrepareScenario();
            AddScenario(scenario);
        }


        override public Outcome Run()
        {
            didCallRun = true;
            return base.Run();
        }

        private IScenario PrepareScenario()
        {
            Mockery mocks = new Mockery();
            SimplestPossibleWorld world = new SimplestPossibleWorld();

            SetupGiven(ref mocks, ref world);
            SetupEvent(ref mocks, ref world);
            SetupWorldOutcome(ref mocks, ref world);
            IScenario scenario = SetupScenario(ref mocks, ref world);

            return scenario;
        }


        private IScenario SetupScenario(ref Mockery mocks, ref SimplestPossibleWorld world)
        {
            IScenario scenario = mocks.NewMock<IScenario>();
            Expect.AtLeast(1).On(scenario).Method("SetupWorld").Will(Return.Value(world));
            Expect.AtLeast(0).On(scenario).SetProperty("World").To(world);
            Expect.AtLeast(0).On(scenario).GetProperty("World").Will(Return.Value(world));
            Expect.AtLeast(1).On(scenario).Method("Specify");

            Outcome o = CreateSuccessfulOutcome();
            Expect.AtLeast(1).On(scenario).Method("Run").Will(Return.Value(o));

            return scenario;
        }


        private Outcome CreateSuccessfulOutcome()
        {
            Outcome o = new Outcome(OutcomeResult.Passed, "cool");
            Outcome[] oArr = new Outcome[1];
            oArr[0] = o;
            o.AddOutcomes(oArr);

            return o;
        }



        private void SetupWorldOutcome(ref Mockery mocks, ref SimplestPossibleWorld world)
        {
            IWorldOutcome outcome = mocks.NewMock<IWorldOutcome>();
            Expect.AtLeast(1).On(outcome).Method("Verify").With(world);
            Expect.AtLeast(1).On(outcome).GetProperty("Result").Will(Return.Value(CreateSuccessfulOutcome()));
            List<IWorldOutcome> outcomeCollection = new List<IWorldOutcome>();
            outcomeCollection.Add(outcome);
        }

        private void SetupEvent(ref Mockery mocks, ref SimplestPossibleWorld world)
        {
            IEvent evt = mocks.NewMock<IEvent>();
            Expect.AtLeast(1).On(evt).Method("OccurIn").With(world);
        }

        private void SetupGiven(ref Mockery mocks, ref SimplestPossibleWorld world)
        {
            IGiven given = (IGiven)mocks.NewMock<IGiven>();
            Expect.AtLeast(1).On(given).Method("Setup").With(world);
            List<IGiven> GivenCollection = new List<IGiven>();
            GivenCollection.Add(given);
        }




    }
}
