using NBehave.Framework.Story;

using Example.ATM.Domain;
using Example.ATM.Givens;
using Example.ATM.Events;
using Example.ATM.Outcomes;

namespace Example.ATM.Stories
{
    
    public class UserWithdrawsCash:Story
    {
        //If you skip on the constructor the story's name is taken from the name of the class, which in this case would result in the exakt same name.
        public UserWithdrawsCash() : base("User withdraws cash") { }


        public override void Story()
        {
            AsA("Bank card holder").
            IWant("to be able to withdraw cash from an ATM").
            SoThat("I don't have to visit the bank");
        }

        public override void Scenarios()
        {
            IAccount account = new Account(); 
            IAccount cashAccount = new Account();

            Scenario("Transfer money").
                Given("my cash account balance is", new GivenAnAccount(account, 50)).
                And("my cash account balance is", new GivenAnAccount(cashAccount, 20)).
                When("I transfer to cash account", new TransferToCashAccount(account, cashAccount, 20)).
                Then("my savings account balance should be reduced", new VerifyAccountBalance(account,30)).
                And("my cash account balance should be increased", new VerifyAccountBalance(cashAccount,40));
        }
    }
}
