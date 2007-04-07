using System;
using System.Collections.Generic;
using System.Text;
using NBehave.Framework.World;
using Example.ATM.Domain;


namespace Example.ATM.Events
{
    class UserRequestsCash:IEvent<Domain.IAccount>
    {

        public void OccurIn(IAccount world)
        {
            world.Withdraw(20);
        }
    }
}
