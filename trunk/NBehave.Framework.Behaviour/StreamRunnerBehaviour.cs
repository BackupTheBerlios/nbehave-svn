using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NMock2;
using NBehave.Framework.Story;


namespace NBehave.Framework.Behaviour
{
    [TestFixture]
    public class StreamRunnerBehaviour
    {

        [Test]
        public void ShouldRunStoryAndGetStuffInStream()
        {
            //Given
            string expected = "." + Environment.NewLine + "Passed: 1" + Environment.NewLine + "Failed: 0" + Environment.NewLine + "Passed !" + Environment.NewLine;

            FakeStory story = new FakeStory(); // mocks.NewMock<IStory<SimplestPossibleWorld>>();

            System.Collections.ArrayList stories = new System.Collections.ArrayList();
            stories.Add(story);
            Stream sr = new MemoryStream();
            StreamRunner storyRunner = new StreamRunner(sr, stories);

            //When
            storyRunner.Run();

            //Then
            sr.Seek(0, 0);
            TextReader  s = new StreamReader(sr);
            Assert.AreEqual(expected, s.ReadToEnd());
        }

    }
}
