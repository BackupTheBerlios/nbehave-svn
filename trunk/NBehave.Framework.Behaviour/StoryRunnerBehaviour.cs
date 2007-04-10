using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using System.Collections.ObjectModel;

using NBehave.Framework;
using NBehave.Framework.World;
using NBehave.Framework.Story;
using NBehave.Framework.Scenario;


namespace NBehave.Framework.Behaviour
{

    [TestFixture]
    public class StoryRunnerBehaviour
    {
        //Can't mock Story, because the runner will check if the stories are inherited from Story<T>
        //And there's a thing with Reflection to fetch events (IStory<T>.StoryOutcome)
      


        [Test]
        public void ShouldRunStory()
        {
            //Given
            FakeStory story = new FakeStory(); 

            System.Collections.ArrayList stories = new System.Collections.ArrayList();
            stories.Add(story);
            StoryRunner storyRunner = new StoryRunner(stories);

            //When
            storyRunner.Run();

            //Then
            Assert.IsTrue(story.didCallRun);
        }


        [Test]
        public void ShouldFindStoryInAssembly()
        {
            // Since I'm lazy, I will use Examlpes.CS

            //Given
            System.IO.FileInfo f = new System.IO.FileInfo(this.GetType().Assembly.Location);
            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFile( f.DirectoryName  +  "\\Examples.CS.exe");

            StoryRunner storyRunner = new StoryRunner(asm);

            //When
            storyRunner.Run();

            //Then
            Assert.IsTrue (storyRunner.GetStoryOutcome().Passed);
            
        }


        [Test]
        public void SHouldFailIfScenarioHasZeroOutcomes()
        {
            //Given
            Mockery mocks = new Mockery();

            // The scenario has no outcome.
            IScenario<SimplestPossibleWorld> scenario = (IScenario<SimplestPossibleWorld>)mocks.NewMock<IScenario<SimplestPossibleWorld>>();
            Expect.Once.On(scenario).Method("Run").WithNoArguments(); 

            IStory<SimplestPossibleWorld> story =new FakeStory(); // (IStory<SimplestPossibleWorld>)mocks.NewMock<IStory<SimplestPossibleWorld>>();
            story.AddScenario(scenario);

            System.Collections.ArrayList stories = new System.Collections.ArrayList();
            stories.Add(story);

            StoryRunner storyRunner = new StoryRunner(stories);

            //Expect.Once.On(story).EventAdd("StoryOutcome",
            //Expect.Once.On(story).Method("Specify").WithNoArguments();
            //Expect.Once.On(story).Method("Run").WithNoArguments();
            //When
            storyRunner.Run();

            //Then
            Assert.IsFalse(storyRunner.GetStoryOutcome().Passed);
        }


        [Test,ExpectedException(typeof(ArgumentException),"story is NULL")]
        public void ShouldNotBePossibleToAddNULL()
        {
            //Given
            StoryRunner storyRunner = new StoryRunner();

            //When
            storyRunner.AddStory(null);
        }

        [Test]
        public void SHouldNotBePossibleToAddStringAsStory()
        {
            //Given
            System.Collections.IList lst=new System.Collections.ArrayList();
            StoryRunner storyRunner = new StoryRunner(lst);
            string story = "Ths is not a story";
            //When
            storyRunner.AddStory(story);

            //Then
            Assert.AreEqual(0, lst.Count);
        }
    }
}
