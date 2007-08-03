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

            Assert.AreEqual("Behaviour should derive story name from calling method.", this.StoryTitle);
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


    }
}
