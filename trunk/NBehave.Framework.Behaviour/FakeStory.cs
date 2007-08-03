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
    public class FakeStory : Story<SimplestPossibleWorld>
    {
        public bool didCallSpecify = false;
        public bool didCallRun = false;


        public override void Story()
        {
            AsA("Developer").IWant("this to work").SoThat("I can move to the next feature");
        }


        public override void Scenarios()
        {
            didCallSpecify = true;
            IScenario<SimplestPossibleWorld> scenario = PrepareScenario();
            AddScenario(scenario);
        }


        override public Outcome Run()
        {
            didCallRun = true;
            return base.Run();
        }

        private IScenario<SimplestPossibleWorld> PrepareScenario()
        {
            Mockery mocks = new Mockery();
            SimplestPossibleWorld world = new SimplestPossibleWorld();

            SetupGiven(ref mocks, ref world);
            SetupEvent(ref mocks, ref world);
            SetupWorldOutcome(ref mocks, ref world);
            IScenario<SimplestPossibleWorld> scenario = SetupScenario(ref mocks, ref world);

            return scenario;
        }


        private IScenario<SimplestPossibleWorld> SetupScenario(ref Mockery mocks, ref SimplestPossibleWorld world)
        {
            IScenario<SimplestPossibleWorld> scenario = mocks.NewMock<IScenario<SimplestPossibleWorld>>();
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
            Outcome o = new Outcome(true, "cool");
            Outcome[] oArr = new Outcome[1];
            oArr[0] = o;
            o.AddOutcomes(oArr);

            return o;
        }



        private void SetupWorldOutcome(ref Mockery mocks, ref SimplestPossibleWorld world)
        {
            IWorldOutcome<SimplestPossibleWorld> outcome = mocks.NewMock<IWorldOutcome<SimplestPossibleWorld>>();
            Expect.AtLeast(1).On(outcome).Method("Verify").With(world);
            Expect.AtLeast(1).On(outcome).GetProperty("Result").Will(Return.Value(CreateSuccessfulOutcome()));
            WorldOutcomeCollection<SimplestPossibleWorld> outcomeCollection = new WorldOutcomeCollection<SimplestPossibleWorld>();
            outcomeCollection.Add(outcome);
        }

        private void SetupEvent(ref Mockery mocks, ref SimplestPossibleWorld world)
        {
            IEvent<SimplestPossibleWorld> evt = mocks.NewMock<IEvent<SimplestPossibleWorld>>();
            Expect.AtLeast(1).On(evt).Method("OccurIn").With(world);
        }

        private void SetupGiven(ref Mockery mocks, ref SimplestPossibleWorld world)
        {
            IGiven<SimplestPossibleWorld> given = (IGiven<SimplestPossibleWorld>)mocks.NewMock<IGiven<SimplestPossibleWorld>>();
            Expect.AtLeast(1).On(given).Method("Setup").With(world);
            GivenCollection<SimplestPossibleWorld> GivenCollection = new GivenCollection<SimplestPossibleWorld>();
            GivenCollection.Add(given);
        }




    }
}
