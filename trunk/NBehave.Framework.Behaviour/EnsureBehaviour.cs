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

        private class SimpleWorldOutcome: WorldOutcome
        {
            protected override void Verify<T>(T world)
            {
            }
        }

        [Test]
        public void ShouldEnsureFailure()
        {
            //given
            IWorldOutcome outcome = new SimpleWorldOutcome();
            Ensure ensure = new Ensure(outcome);

            //When
            ensure.Failure();

            //Then
            Assert.AreEqual(OutcomeResult.Failed, ensure.Outcome.Result);
            Assert.AreEqual("Failure", ensure.Outcome.Message);
        }


        [Test]
        public void ShouldEnsureThatItIsTruePasses()
        {
             //given
            WorldOutcome outcome = new SimpleWorldOutcome();
            Ensure ensure = new Ensure(outcome);

            //When
            ensure.IsTrue(true);

            //Then
            Assert.AreEqual(OutcomeResult.Passed, ensure.Outcome.Result);
        }


        [Test]
        public void ShouldEnsureThatItIsTrueFails()
        {
            //given
            WorldOutcome outcome = new SimpleWorldOutcome();
            Ensure ensure = new Ensure(outcome);

            //When
            ensure.IsTrue(false);

            //Then
            Assert.AreEqual(OutcomeResult.Failed, ensure.Outcome.Result);
        }



        [Test]
        public void ShouldEnsureThatItIsFalsePasses()
        {
            //given
            WorldOutcome outcome = new SimpleWorldOutcome();
            Ensure ensure = new Ensure(outcome);

            //When
            ensure.IsFalse(false);

            //Then
            Assert.AreEqual(OutcomeResult.Passed, ensure.Outcome.Result);
        }



    }
}
