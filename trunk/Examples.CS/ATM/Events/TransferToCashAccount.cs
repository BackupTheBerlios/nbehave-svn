using System;
using System.Collections.Generic;
using System.Text;
using NBehave.Framework.World;
using Examples.CS.ATM.Domain;


namespace Examples.CS.ATM.Events 
{
    class TransferToCashAccount : IEvent
    {
         IAccount fromAccount;
        IAccount toAccount;
        int amount;
        public TransferToCashAccount(IAccount fromAccount, IAccount toAccount, int amount)
        {
            this.fromAccount = fromAccount;
            this.toAccount = toAccount;
            this.amount = amount;
        }

        public void OccurIn<IAccount>(IAccount world)
        {
            fromAccount.Transfer(amount, toAccount);
        }
    }
}
