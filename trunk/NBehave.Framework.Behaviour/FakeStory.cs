using System;
using System.Collections.Generic;
using System.Text;

using NBehave.Framework.Story;


namespace NBehave.Framework.Behaviour
{
    public class FakeStory : Story<SimplestPossibleWorld>
    {
        public bool didCallSpecify = false;
        public bool didCallRun = false;

        public FakeStory()
            : base(
                new Narrative(
            /* As A */ "Developer",
            /* I want */ "this to work",
            /* So that */ "I can move to the next feature"
                )
            )
        { }

        public override void Specify()
        {
            //throw new Exception("The method or operation is not implemented.");
            didCallSpecify = true;
        }

        override public void Run()
        {
            didCallRun = true;
        }
    }
}
