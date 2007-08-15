using System;
using System.Collections.Generic;
using System.Text;

namespace Examples.CS.ATM.Domain
{
    public class Account : IAccount
    {
        private int _balance = 0;

        public void Transfer(int amount, IAccount toAccount)
        {
            _balance -= amount;
            toAccount.Balance += amount;
        }


        public int Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }


    }
}
