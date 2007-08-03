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
    public class EnsureBehaviour
    {

        private class SimpleWorldOutcome<T>: WorldOutcome<T>
        {
            protected override void Verify(T world)
            {
            }
        }

        [Test]
        public void ShouldEnsureFailure()
        {
            //given
            WorldOutcome<SimplestPossibleWorld> outcome = new SimpleWorldOutcome<SimplestPossibleWorld>();
            Ensure<SimplestPossibleWorld> ensure = new Ensure<SimplestPossibleWorld>(outcome);

            //When
            ensure.Failure();

            //Then
            Assert.IsFalse(ensure.Outcome.Passed);
            Assert.AreEqual("Failure", ensure.Outcome.Message);
        }


        [Test]
        public void ShouldEnsureThatItIsTruePasses()
        {
             //given
            WorldOutcome<SimplestPossibleWorld> outcome = new SimpleWorldOutcome<SimplestPossibleWorld>();
            Ensure<SimplestPossibleWorld> ensure = new Ensure<SimplestPossibleWorld>(outcome);

            //When
            ensure.IsTrue(true);

            //Then
            Assert.IsTrue(ensure.Outcome.Passed);
        }


        [Test]
        public void ShouldEnsureThatItIsTrueFails()
        {
            //given
            WorldOutcome<SimplestPossibleWorld> outcome = new SimpleWorldOutcome<SimplestPossibleWorld>();
            Ensure<SimplestPossibleWorld> ensure = new Ensure<SimplestPossibleWorld>(outcome);

            //When
            ensure.IsTrue(false);

            //Then
            Assert.IsFalse(ensure.Outcome.Passed);
        }



        [Test]
        public void ShouldEnsureThatItIsFalsePasses()
        {
            //given
            WorldOutcome<SimplestPossibleWorld> outcome = new SimpleWorldOutcome<SimplestPossibleWorld>();
            Ensure<SimplestPossibleWorld> ensure = new Ensure<SimplestPossibleWorld>(outcome);

            //When
            ensure.IsFalse(false);

            //Then
            Assert.IsTrue(ensure.Outcome.Passed);
        }



    }
}
