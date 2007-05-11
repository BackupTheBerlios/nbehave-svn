using System;
using System.Collections.Generic;
using System.Text;
using NBehave.Framework;
using NBehave.Framework.World;
using Example.ATM.Givens;
using Example.ATM.Events;
using Example.ATM.Outcomes;
using Example.ATM.Domain;
using NBehave.Framework.Scenario;

using NMock2;

namespace Example.ATM.Scenarios
{
    class HappyScenario:Scenario<Domain.IAccount>
    {
        override public void Specify()
        {
            Given(new AccountIsInCredit()).
            When(new UserRequestsCash()).
            Then(new AccountBalanceShouldBeReduced());
        }

        
        private Mockery mocks = new Mockery();

        override public Domain.IAccount SetupWorld()
        {
            //Normally you wouldn't mock the entire world. 
            IAccount account=mocks.NewMock<IAccount>();

            Expect.Once.On(account).SetProperty("Balance").To(50);
            Expect.Once.On(account).Method("Withdraw").With(20);
            Expect.Once.On(account).GetProperty("Balance").Will(Return.Value(30));

            return account;
        }

    }
}
