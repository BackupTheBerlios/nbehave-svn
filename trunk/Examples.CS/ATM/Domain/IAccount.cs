using System;
using System.Collections.Generic;
using System.Text;

namespace Example.ATM.Domain
{
    public interface IAccount
    {
        int Balance { get; set;}
        void Transfer(int amount, IAccount toAccount);
    }
}
