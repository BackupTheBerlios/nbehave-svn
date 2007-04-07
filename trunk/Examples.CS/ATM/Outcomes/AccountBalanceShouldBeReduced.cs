using System;
using System.Collections.Generic;
using System.Text;
using NBehave.Framework;
using NBehave.Framework.World;

namespace Example.ATM.Outcomes
{
    class AccountBalanceShouldBeReduced:WorldOutcome<Domain.IAccount>
    {

        protected override void Verify(Example.ATM.Domain.IAccount world)
        {
            this.Ensure.IsTrue(world.Balance == 30);
        }     

    }
}
