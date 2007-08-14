using System;
using System.Collections.Generic;
using System.Text;
using NBehave.Framework;
using NBehave.Framework.World;
using NBehave.Framework.Scenario;
using NBehave.Framework.Story;

using Example.ATM.Domain;
using Example.ATM.Givens;
using Example.ATM.Events;
using Example.ATM.Outcomes;

namespace Example.ATM.Stories
{
    //The name of the story is taken from the class name
    public class UserWithdrawsCash:Story
    {
        public override void Story()
        {
            AsA("Bank card holder").
            IWant("to be able to withdraw cash from an ATM").
            SoThat("I don't have to visit the bank");
        }

        public override void Scenarios()
        {
            IAccount account = new Account();        //TODO: Move Account Implementation out of that class
            IAccount cashAccount = new Account();

            Scenario("Transfer money").
                Given("my cash account balance is", new GivenAnAccount(account, 50)).
                And("my cash account balance is", new GivenAnAccount(cashAccount, 20)).
                When("I transfer to cash account", new TransferToCashAccount(account, cashAccount, 20)).
                Then("my savings account balance should be reduced", new VerifyAccountBalance(account,30)).
                And("my cash account balance should be increased", new VerifyAccountBalance(cashAccount,40));
            //AddScenario(scenario);
        }
    }
}
