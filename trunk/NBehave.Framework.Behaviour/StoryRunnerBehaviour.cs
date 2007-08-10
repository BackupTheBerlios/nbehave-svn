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


namespace NBehave.Framework.BehaviourNUnit
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

            //System.Collections.ArrayList stories = new System.Collections.ArrayList();
            //stories.Add(story);
            //StoryRunner storyRunner = new StoryRunner(stories);
            StoryRunner storyRunner = new StoryRunner();
            storyRunner.AddStory(story);
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
            bool storyCalled = false;

            storyRunner.ExecutingStory += delegate(Object sender, StoryEventArgs e) { storyCalled = true; };

            //When
            storyRunner.Run();

            //Then - Check on how many stories that where executed instead, via the events.
            Assert.IsTrue(storyCalled);
            
        }


        [Test,Ignore("Test has incorrectly setup")]
        public void ShouldFailIfScenarioHasZeroOutcomes()
        {
            //Given
            //Mockery mocks = new Mockery();

            //IStory<SimplestPossibleWorld> story = new FakeStory(); // (IStory<SimplestPossibleWorld>)mocks.NewMock<IStory<SimplestPossibleWorld>>();

            //System.Collections.ArrayList stories = new System.Collections.ArrayList();
            //stories.Add(story);

            //StoryRunner storyRunner = new StoryRunner(stories);
            StoryRunner storyRunner = new StoryRunner(System.Reflection.Assembly.GetAssembly(this.GetType()));

            //When
            storyRunner.Run();

            //Then
            Assert.AreEqual(OutcomeResult.Failed, storyRunner.GetStoryOutcome().Result);
        }


        [Test, ExpectedException(typeof(ArgumentException), "assemblyToParseForStories is NULL")]
        public void ShouldNotBePossibleToAddNULL()
        {
            //Given
            StoryRunner storyRunner = new StoryRunner(null);

            //When
            storyRunner.AddStory<object>(null);
        }


        [Test, ExpectedException(typeof(ArgumentException))]
        public void SHouldNotBePossibleToAddStringAsStory()
        {
            //Given
            //System.Collections.IList lst=new System.Collections.ArrayList();
            StoryRunner storyRunner = new StoryRunner(System.Reflection.Assembly.GetAssembly(this.GetType()));

            string story = "Ths is not a story";
            //When
            storyRunner.AddStory(story);

            //Then
            // we get an exception
        }
    }
}
