using System;
using System.Collections.Generic;
using System.Text;
using NBehave.Framework;
using NBehave.Framework.World;
using Example.ATM.Scenarios;
using NBehave.Framework.Story;

namespace Example.ATM.Stories
{
    public class UserWithdrawsCash:Story<Domain.IAccount>
    {
        public UserWithdrawsCash(): base(
            new Narrative(
            /*As a */ "Bank card holder",
            /*I want */ "to be able to withdraw cash from an ATM",
            /* So that */ "I don't have to visit the bank")) 
        { }


        public override void Specify()
        {
            AddScenario(new HappyScenario());
        }

    }
}
