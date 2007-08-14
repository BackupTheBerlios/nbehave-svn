using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using NUnit.Framework;
using NMock2;

using NBehave.Framework;
using NBehave.Framework.World;
using NBehave.Framework.Story;
using NBehave.Framework.Scenario;


namespace NBehave.Framework.BehaviourNUnit
{
    [TestFixture]
    public class ScenarioBehaviour
    {



        // Our Scenario to play with
        public class MyScenario:Scenario.Scenario 
        {
            public MyScenario() : base() { }
            //public MyScenario(IList<IGiven> givens, IEvent evt, IOutcomeCollection<SimplestPossibleWorld> outcomes) : base(givens, evt, outcomes) { }
            public MyScenario(IList<IGiven> givens, IEvent evt, IList<IWorldOutcome> outcomes, SimplestPossibleWorld world) : base(givens, evt, outcomes, world) { }


            override public void Specify()
            {
                // Added stuff via constructor, no need to add stuff again
            }


            public override object  SetupWorld()
            {
                return this.World ;
            }
        }



        [Test]
        public void ShouldAddGivenToScenario()
        {
            //Given
            Mockery mocks = new Mockery();
            IGiven aGiven = (IGiven)mocks.NewMock<IGiven>();
            List<IGiven> GivenCollection = new List<IGiven>();
            IScenario scenario = new MyScenario(GivenCollection, null, null,null);  

            //When
            scenario.Given("a given", aGiven);

            //Then
            Assert.AreEqual(1, GivenCollection.Count);
        }




        [Test]
        public void ShouldAddMultipleGivensToScenario()
        {
            //Given
            Mockery mocks = new Mockery();
            IGiven aGiven = (IGiven)mocks.NewMock<IGiven>();
            IGiven anotherGiven = (IGiven)mocks.NewMock<IGiven>();
            IGiven yetAnotherGiven = (IGiven)mocks.NewMock<IGiven>();
            List<IGiven> GivenCollection = new List<IGiven>();
            IScenario scenario = new MyScenario(GivenCollection, null, null, null);

            //When
            scenario.
                Given("a given", aGiven).
                    And("a given", anotherGiven).
                    And("a given", yetAnotherGiven);

            //Then
            Assert.AreEqual(3, GivenCollection.Count);
        }


        [Test]
        public void ShouldAddEventToScenario()
        {
            //Given
            Mockery mocks = new Mockery();
            IGiven aGiven = (IGiven)mocks.NewMock<IGiven>();
            IEvent evt = mocks.NewMock<IEvent>();
            Scenario.Scenario  scenario = new MyScenario();

            //When
            scenario.Given("a given", aGiven).When("an event", evt);

            //Then
            Assert.IsNotNull (scenario.Event);
        }


        [Test]
        public void ShouldAddOutcome()
        {
            //Given
            Mockery mocks = new Mockery();
            IGiven aGiven = (IGiven)mocks.NewMock<IGiven>();
            IEvent evt = mocks.NewMock<IEvent>();
            IWorldOutcome outcome = mocks.NewMock<IWorldOutcome>();
            List<IGiven> givenCollection = new List<IGiven>();
            List<IWorldOutcome> outcomeCollection = new List<IWorldOutcome>();
            IScenario scenario = new MyScenario(givenCollection, null, outcomeCollection, null);

            //When
            scenario.Given("a given", aGiven).When("an event", evt).Then("an outcome", outcome);

            //Then
            Assert.AreEqual(1, outcomeCollection.Count);
  
        }


        [Test]
        public void ShouldSetupGivens()
        {
            //Given
            Mockery mocks = new Mockery();

            SimplestPossibleWorld world = new SimplestPossibleWorld();

            IGiven aGiven = mocks.NewMock<IGiven>();
            Expect.Once.On(aGiven).Method("Setup").With(world);

            List<IGiven> l = new List<IGiven>();
            l.Add(aGiven);
            Scenario.Scenario scenario = new MyScenario(l, null, null, world);
            //When 
            scenario.SetupGivens();

            //Then
            mocks.VerifyAllExpectationsHaveBeenMet();
        }


        [Test]
        public void ShouldRaiseWorldEvent()
        {
            //Given
            Mockery mocks = new Mockery();
            SimplestPossibleWorld world = new SimplestPossibleWorld();
            IEvent evt = mocks.NewMock<IEvent>();
            Expect.Once.On(evt).Method("OccurIn").With(world);

            Scenario.Scenario scenario = new MyScenario(null, evt, null, world);
            //When 
            scenario.WorldEvent();

            //Then
            mocks.VerifyAllExpectationsHaveBeenMet();
        }


        [Test]
        public void ShouldVerifyOutcome()
        {
            //Given
            Mockery mocks = new Mockery();
            SimplestPossibleWorld world = new SimplestPossibleWorld();

            IWorldOutcome outcome = mocks.NewMock<IWorldOutcome>();
            Expect.Once.On(outcome).Method("Verify").With(world);

            Outcome outcomeResult = new Outcome(OutcomeResult.Passed, "Cool");
            Expect.Once.On(outcome).GetProperty("Result").Will(Return.Value(outcomeResult));

            List<IWorldOutcome> lst = new List<IWorldOutcome>();
            lst.Add(outcome);
            Scenario.Scenario scenario = new MyScenario(null, null, lst, world);
            //When 
            scenario.VerifyOutcomes();

            //Then
            mocks.VerifyAllExpectationsHaveBeenMet();
        }


        [Test]
        public void ShouldRunScenario()
        {
            //Given
            Mockery mocks = new Mockery();
            SimplestPossibleWorld world = new SimplestPossibleWorld();


            IGiven given = (IGiven)mocks.NewMock<IGiven>();
            Expect.Once.On(given).Method("Setup").With(world);
            List<IGiven> GivenCollection = new List<IGiven>();
            GivenCollection.Add(given);

            IEvent evt = mocks.NewMock<IEvent>();
            Expect.Once.On(evt).Method("OccurIn").With(world);

            IWorldOutcome outcome = mocks.NewMock<IWorldOutcome>();
            Expect.Once.On(outcome).Method("Verify").With(world);
            Expect.Once.On(outcome).GetProperty("Result").Will(Return.Value(new Outcome(OutcomeResult.Passed, "Cool")));
            List<IWorldOutcome> outcomeCollection = new List<IWorldOutcome>();
            outcomeCollection.Add(outcome);

            IScenario scenario = new MyScenario(GivenCollection, evt, outcomeCollection, world);
            
            //When
            scenario.Run();

            //Then
            mocks.VerifyAllExpectationsHaveBeenMet();

        }



    }

}
