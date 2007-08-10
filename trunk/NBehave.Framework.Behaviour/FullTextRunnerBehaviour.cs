using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using NBehave.Framework.Story;


namespace NBehave.Framework.BehaviourNUnit
{
    [TestFixture]
    public class FullTextRunnerBehaviour
    {
        [Test]
        public void ShouldRunStoryAndGetStuffInStream()
        {
            //Given
            string expectToStartWith = "Story: Fake story" + Environment.NewLine;
            string expectToContain = "--> Passed" + Environment.NewLine;

            FakeStory story = new FakeStory(); // mocks.NewMock<IStory<SimplestPossibleWorld>>();

            System.Collections.ArrayList stories = new System.Collections.ArrayList();
            stories.Add(story);
            Stream sr = new MemoryStream();
            //StreamRunner storyRunner = new FullTextRunner(sr, stories);
            StreamRunner storyRunner = new FullTextRunner(sr, System.Reflection.Assembly.GetAssembly(this.GetType()));

            //When
            storyRunner.Run();

            //Then
            sr.Seek(0, 0);
            TextReader s = new StreamReader(sr);
            string result = s.ReadToEnd();
            Assert.IsTrue(result.StartsWith(expectToStartWith));
            Assert.IsTrue(result.Contains(expectToContain));
        }

    }
}
