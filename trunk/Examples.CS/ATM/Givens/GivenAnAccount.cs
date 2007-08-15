using System;
using System.Collections.Generic;
using System.Text;
using NBehave.Framework.World;
using Examples.CS.ATM.Domain;

namespace Examples.CS.ATM.Givens
{
    public class GivenAnAccount : IGiven  
    {
        private IAccount account;

        public GivenAnAccount(IAccount account, int balance)
        {
            this.account = account;
            this.account.Balance = balance;
        }

        public void Setup<T>(T world)
        {
            //All setup was made in the constructor
        }
    }
}
