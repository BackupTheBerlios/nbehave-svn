using System;
using System.Reflection;
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
            System.Reflection.Assembly asm = this.GetType().Assembly; 

            StoryRunner storyRunner = new StoryRunner(asm);
            bool storyCalled = false;

            storyRunner.ExecutingStory += delegate(Object sender, StoryEventArgs e) { storyCalled = true; };

            //When
            storyRunner.Run();

            //Then - Check on how many stories that where executed instead, via the events.
            Assert.IsTrue(storyCalled);
            
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
            StoryRunner storyRunner = new StoryRunner(Assembly.GetAssembly(this.GetType()));

            string story = "Ths is not a story";
            //When
            storyRunner.AddStory(story);

            //Then
            // we get an exception
        }
    }
}
