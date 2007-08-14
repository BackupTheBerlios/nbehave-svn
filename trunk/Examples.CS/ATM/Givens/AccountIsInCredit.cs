using System;
using System.Collections.Generic;
using System.Text;
using NBehave.Framework.World;
using Example.ATM.Domain;

namespace Example.ATM.Givens
{
    public class AccountIsInCredit: IGiven   
    {
        private IAccount account;

        public AccountIsInCredit(IAccount account, int balance)
        {
            this.account = account;
            this.account.Balance = 50;
        }

        public void Setup<T>(T world)
        {
            //Bad example huh? I did all the setup in the constructor...
        }
    }
}
