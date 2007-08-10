using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using NBehave.Framework.Story;
using NBehave.Framework;

namespace NBehave.Framework.BehaviourNUnit
{
    class TestBehavior : NBehave.Framework.Behaviour
    {}

    [TestFixture]
    public class BehaviourBehaviour : NBehave.Framework.Behaviour
    {
        [Test]
        public void behaviourShouldDeriveStoryNameFromCallingMethod()
        {
            //Call Story
            Story();
            Assert.IsTrue(this.StoryTitle.StartsWith("Behaviour should derive story name from calling method"));

        }


        [Test]
        public void storyShouldHaveNarrative()
        {
            Story("Story name").AsA("fool").IWant("world peace").SoThat("I get happy");

            Assert.AreEqual("As a fool" + Environment.NewLine +"I want world peace" + Environment.NewLine + "So that I get happy", this.Narrative.ToString());
        }

        [Test]
        public void scenarioShouldSpecifyGivenWhenThen()
        {
            Scenario("world peace").Given("wars", 7).When("all war ends", 0).Then("world peace", 42);

            //Assert.AreEqual(1, this.Scenarios.Count);
            Assert.AreEqual(1, this.Scenarios[0].Givens.Count);
            Assert.IsNotNull(this.Scenarios[0].Event);
            Assert.AreEqual(1, this.Scenarios[0].Outcomes.Count);

        }



        [Test]
        public void aGivenWithNoActionShouldResultInPendingAsOutcome()
        {
            Story();

            Scenario("Given with no action").Given("A value",42).
                When("Something happens",0,delegate(int a) {}).
                Then("There's an outcome", 0, delegate(int a) { this.Ensure().IsTrue(true); });

            Outcome o = this.Run();

            Assert.AreEqual(OutcomeResult.Pending, o.Result);
        }

        [Test]
        public void anEventWithNoActionShouldResultInPendingAsOutcome()
        {
            Story();

            Scenario("Given with no action").Given("A value", 42, delegate(int a) { }).
                When("Something happens", 0).
                Then("There's an outcome", 0, delegate(int a) { this.Ensure().IsTrue(true); });

            Outcome o = this.Run();

            Assert.AreEqual(OutcomeResult.Pending, o.Result);
        }

        [Test]
        public void anOutcomeWithNoActionShouldResultInPendingAsOutcome()
        {
            Story();
            
            Scenario("Given with no action").Given("A value", 42, delegate(int a) { }).
                When("Something happens", 0, delegate(int a) { }).
                Then("There's an outcome", 0);

            Outcome o = this.Run();

            Assert.AreEqual(OutcomeResult.Pending, o.Result);
        }


    }
}
