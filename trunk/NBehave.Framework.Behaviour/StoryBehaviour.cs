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
            public AStory() : base(
                new Narrative(
                    /* As a */ "Developer",
                    /* I want */ "a BDD framework for .NET", 
                    /* So that */ "I can define the behaviour of my code"
                )
            ) { }

            public override void Specify()
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
            //Given, When 
            Story<SimplestPossibleWorld> story = new AStory();
            string outcome = "As a: Developer" + Environment.NewLine +
                            "I want: a BDD framework for .NET" + Environment.NewLine +
                            "So that: I can define the behaviour of my code";
            //Then
            Assert.AreEqual(outcome, story.Narrative.Text);
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
            Assert.AreEqual(1, story.Scenarios.Count);
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
