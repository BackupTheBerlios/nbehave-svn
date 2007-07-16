using System;
using System.Collections.Generic;
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
    public class StoryBehaviour
    {

        public class AStory : Story<SimplestPossibleWorld>
        {


            public override void Story()
            {
                AsA("Developer").IWant("a BDD framework for .NET").SoThat("I can define the behaviour of my code");
            }

            public override void Scenarios()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            // Should I have to override some methods in the baseclass (the ones commented out)
            // and test the result of that
            // in the tests below ?
        }


        [Test]
        public void StoryShouldHaveNarrative()
        {
            //Given 
            Story<SimplestPossibleWorld> theStory = new AStory();
            const string user = "Developer";
            const string feature = "a BDD framework for .NET";
            const string benefit = "I can define the behaviour of my code";

            string outcome = string.Format("As a: {1}{0}I want: {2}{0}So that: {3}", Environment.NewLine, user, feature, benefit);
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
            IStory<SimplestPossibleWorld> story = new AStory();

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
            IStory<SimplestPossibleWorld> story = new AStory();

            //Story has scenarios
            IScenario<SimplestPossibleWorld> scenario = mocks.NewMock<IScenario<SimplestPossibleWorld>>();

            story.AddScenario(scenario);

            Expect.Once.On(scenario).Method("Run");

            //When
            story.Run();

            //Then
            mocks.VerifyAllExpectationsHaveBeenMet();
        }


    }
}
