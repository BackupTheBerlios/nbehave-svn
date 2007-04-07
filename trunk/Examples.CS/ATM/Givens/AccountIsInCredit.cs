using System;
using System.Collections.Generic;
using System.Text;
using NBehave.Framework.World;

namespace Example.ATM.Givens
{
    public class AccountIsInCredit: IGiven<Domain.IAccount>   
    {
        
        public void Setup(Example.ATM.Domain.IAccount world)
        {
            world.Balance = 50;
        }
    }
}
