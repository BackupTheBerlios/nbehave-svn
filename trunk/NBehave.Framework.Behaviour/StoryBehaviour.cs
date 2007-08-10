using System;
using System.Collections.Generic;
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
    public class StoryBehaviour
    {

        [Test]
        public void StoryShouldHaveNarrative()
        {
            //Given 
            Story<SimplestPossibleWorld> theStory = new FakeStory();
            const string user = "Developer";
            const string feature = "this to work";
            const string benefit = "I can move to the next feature";

            string outcome = string.Format("As a {1}{0}I want {2}{0}So that {3}", Environment.NewLine, user, feature, benefit);
            //When
            theStory.AsA(user).IWant(feature).SoThat(benefit);

            //Then
            Assert.AreEqual(outcome, theStory.Narrative.ToString());
        }


        [Test]
        public void ShouldAddScenarioToStory()
        {
            //Given
            Mockery mocks = new Mockery();
            IScenario<SimplestPossibleWorld> scenario = mocks.NewMock<IScenario<SimplestPossibleWorld>>();
            IStory<SimplestPossibleWorld> story = new FakeStory();

            //When
            story.AddScenario(scenario);

            //Then
            Assert.AreEqual(1, story.ScenarioItems.Count);
        }



        [Test]
        public void ShouldRunStorySuccessfully()
        {
            //Given
            Mockery mocks = new Mockery();
            SimplestPossibleWorld world = new SimplestPossibleWorld();
            IStory<SimplestPossibleWorld> story = new FakeStory();

            //Story has scenarios
            IScenario<SimplestPossibleWorld> scenario = mocks.NewMock<IScenario<SimplestPossibleWorld>>();

            story.AddScenario(scenario);

            Expect.Once.On(scenario).Method("Run").Will(Return.Value(new Outcome(OutcomeResult.Passed, "yadda yadda")));

            //When
            story.Run();

            //Then
            mocks.VerifyAllExpectationsHaveBeenMet();
        }


    }
}
