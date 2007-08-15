using NBehave.Framework.Story;

using Examples.CS.ATM.Domain;
using Examples.CS.ATM.Givens;
using Examples.CS.ATM.Events;
using Examples.CS.ATM.Outcomes;

namespace Examples.CS.ATM.Stories
{
    
    public class UserWithdrawsCash:Story
    {
        //You can skip the Story part and go directly for AsA(... If you skip Story("title")... then the name of the story is derived from the name of the class
        public override void Story()
        {
            Story("User withdraws cash").            
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
