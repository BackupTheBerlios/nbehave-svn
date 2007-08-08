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
    public class StreamRunnerBehaviour
    {

        [Test]
        public void ShouldRunStoryAndGetStuffInStream()
        {
            //Given
            string expectToHave = "Passed: 1";
            string expectToEndWith = "Passed !" + Environment.NewLine;

            FakeStory story = new FakeStory();
            Stream sr = new MemoryStream();
            StreamRunner storyRunner = new StreamRunner(sr, System.Reflection.Assembly.GetAssembly(this.GetType()));

            //When
            storyRunner.Run();

            //Then
            sr.Seek(0, 0);
            TextReader  s = new StreamReader(sr);
            string result = s.ReadToEnd();
            Assert.IsTrue(result.Contains(expectToHave));
            Assert.IsTrue(result.EndsWith(expectToEndWith));
        }        

    }
}
