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


namespace NBehave.Framework.Behaviour
{
    [TestFixture]
    public class ScenarioBehaviour
    {

        // Our Scenario to play with
        public class MyScenario:Scenario<SimplestPossibleWorld>
        {
            public MyScenario() : base() { }
            //public MyScenario(IGivenCollection<SimplestPossibleWorld> givens, IEvent<SimplestPossibleWorld> evt, IOutcomeCollection<SimplestPossibleWorld> outcomes) : base(givens, evt, outcomes) { }
            public MyScenario(GivenCollection<SimplestPossibleWorld> givens, IEvent<SimplestPossibleWorld> evt, WorldOutcomeCollection<SimplestPossibleWorld> outcomes, SimplestPossibleWorld world) : base(givens, evt, outcomes, world) { }


            override public void Specify()
            {
                // Should Call Given,When, Then in base class.
                Given(null).When(null).Then(null);
            }


            public override SimplestPossibleWorld SetupWorld()
            {
                return new SimplestPossibleWorld();
            }
        }



        [Test]
        public void ShouldAddGivenToScenario()
        {
            //Given
            Mockery mocks = new Mockery();
            IGiven<SimplestPossibleWorld> aGiven = (IGiven<SimplestPossibleWorld>)mocks.NewMock<IGiven<SimplestPossibleWorld>>();
            GivenCollection<SimplestPossibleWorld> GivenCollection = new GivenCollection<SimplestPossibleWorld>();
            Scenario<SimplestPossibleWorld> scenario = new MyScenario(GivenCollection, null, null,null);  

            //When
            scenario.Given(aGiven);

            //Then
            Assert.AreEqual(1, GivenCollection.Count);
        }




        [Test]
        public void ShouldAddMultipleGivensToScenario()
        {
            //Given
            Mockery mocks = new Mockery();
            IGiven<SimplestPossibleWorld> aGiven = (IGiven<SimplestPossibleWorld>)mocks.NewMock<IGiven<SimplestPossibleWorld>>();
            IGiven<SimplestPossibleWorld> anotherGiven = (IGiven<SimplestPossibleWorld>)mocks.NewMock<IGiven<SimplestPossibleWorld>>();
            IGiven<SimplestPossibleWorld> yetAnotherGiven = (IGiven<SimplestPossibleWorld>)mocks.NewMock<IGiven<SimplestPossibleWorld>>();
            GivenCollection<SimplestPossibleWorld> GivenCollection = new GivenCollection<SimplestPossibleWorld>();
            Scenario<SimplestPossibleWorld> scenario = new MyScenario(GivenCollection, null, null, null);

            //When
            scenario.
                Given(aGiven).
                    And(anotherGiven).
                    And(yetAnotherGiven);

            //Then
            Assert.AreEqual(3, GivenCollection.Count);
        }


        [Test]
        public void ShouldAddEventToScenario()
        {
            //Given
            Mockery mocks = new Mockery();
            IGiven<SimplestPossibleWorld> aGiven = (IGiven<SimplestPossibleWorld>)mocks.NewMock<IGiven<SimplestPossibleWorld>>();
            IEvent<SimplestPossibleWorld> evt = mocks.NewMock<IEvent<SimplestPossibleWorld>>();
            Scenario<SimplestPossibleWorld> scenario = new MyScenario();

            //When
            scenario.Given(aGiven).When(evt);

            //Then
            Assert.IsNotNull (scenario.Event);
        }


        [Test]
        public void ShouldAddOutcome()
        {
            //Given
            Mockery mocks = new Mockery();
            IGiven<SimplestPossibleWorld> aGiven = (IGiven<SimplestPossibleWorld>)mocks.NewMock<IGiven<SimplestPossibleWorld>>();
            IEvent<SimplestPossibleWorld> evt = mocks.NewMock<IEvent<SimplestPossibleWorld>>();
            IWorldOutcome<SimplestPossibleWorld> outcome = mocks.NewMock<IWorldOutcome<SimplestPossibleWorld>>();
            GivenCollection<SimplestPossibleWorld> givenCollection = new GivenCollection<SimplestPossibleWorld>();
            WorldOutcomeCollection<SimplestPossibleWorld> outcomeCollection = new WorldOutcomeCollection<SimplestPossibleWorld>();
            Scenario<SimplestPossibleWorld> scenario = new MyScenario(givenCollection, null, outcomeCollection, null);

            //When
            scenario.Given(aGiven).When(evt).Then(outcome);

            //Then
            Assert.AreEqual(1, outcomeCollection.Count);
  
        }


        [Test]
        public void ShouldSetupGivens()
        {
            //Given
            Mockery mocks = new Mockery();

            SimplestPossibleWorld world = new SimplestPossibleWorld();

            IGiven<SimplestPossibleWorld> aGiven = mocks.NewMock<IGiven<SimplestPossibleWorld>>();
            Expect.Once.On(aGiven).Method("Setup").With(world);

            GivenCollection<SimplestPossibleWorld> l = new GivenCollection<SimplestPossibleWorld>();
            l.Add(aGiven);
            Scenario<SimplestPossibleWorld> scenario = new MyScenario(l,null,null,world);
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
            IEvent<SimplestPossibleWorld> evt = mocks.NewMock<IEvent<SimplestPossibleWorld>>();
            Expect.Once.On(evt).Method("OccurIn").With(world);

            Scenario<SimplestPossibleWorld> scenario = new MyScenario(null,evt,null,world);
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

            IWorldOutcome<SimplestPossibleWorld> outcome = mocks.NewMock<IWorldOutcome<SimplestPossibleWorld>>();
            Expect.Once.On(outcome).Method("Verify").With(world);

            ScenarioOutcome outcomeResult = new ScenarioOutcome(true, "Cool");
            Expect.Once.On(outcome).GetProperty("Result").Will(Return.Value(outcomeResult));

            WorldOutcomeCollection<SimplestPossibleWorld> lst = new WorldOutcomeCollection<SimplestPossibleWorld>();
            lst.Add(outcome);
            Scenario<SimplestPossibleWorld> scenario = new MyScenario(null, null, lst, world);
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

            IGiven<SimplestPossibleWorld> given = (IGiven<SimplestPossibleWorld>)mocks.NewMock<IGiven<SimplestPossibleWorld>>();
            Expect.Once.On(given).Method("Setup").With(world);
            GivenCollection<SimplestPossibleWorld> GivenCollection = new GivenCollection<SimplestPossibleWorld>();
            GivenCollection.Add(given);

            IEvent<SimplestPossibleWorld> evt = mocks.NewMock<IEvent<SimplestPossibleWorld>>();
            Expect.Once.On(evt).Method("OccurIn").With(world);

            IWorldOutcome<SimplestPossibleWorld> outcome = mocks.NewMock<IWorldOutcome<SimplestPossibleWorld>>();
            Expect.Once.On(outcome).Method("Verify").With(world);
            Expect.Once.On(outcome).GetProperty("Result").Will(Return.Value(new ScenarioOutcome(true, "Cool")));
            WorldOutcomeCollection<SimplestPossibleWorld> outcomeCollection = new WorldOutcomeCollection<SimplestPossibleWorld>();
            outcomeCollection.Add(outcome);

            Scenario<SimplestPossibleWorld> scenario = new MyScenario(GivenCollection, evt, outcomeCollection, world);
            
            //When
            scenario.Run();

            //Then
            mocks.VerifyAllExpectationsHaveBeenMet();

        }



    }

}
