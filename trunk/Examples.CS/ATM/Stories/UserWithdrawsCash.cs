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
        public override void Story()
        {
            AsA("Bank card holder").
            IWant("to be able to withdraw cash from an ATM").
            SoThat("I don't have to visit the bank");
        }

        public override void Scenarios()
        {
            AddScenario(new HappyScenario());
            //AddScenario(new HappyScenarioThatFails()); 
        }

    }
}
