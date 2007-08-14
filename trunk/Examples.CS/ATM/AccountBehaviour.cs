using NBehave.Framework;
using Example.ATM.Domain;


namespace Example.ATM
{
    //Derive from NBehave.Framework.Behaviour
    public partial class AccountBehaviour : Behaviour
    {

        private IAccount _account;
        private IAccount _cashAccount;

        //Overide  Setup to do some initiation.
        //Setup is called once before every public method on the class.
        protected override void Setup()
        {
            _account = new Account();
            _cashAccount = new Account();
        }


        //All public methods will be invoked by the framework. Each method should contain one Story and at least one Scenario.
        public void TransferToCashAccount()
        {
            Story("Transfer to cash account").
                AsA("Bank card holder").
                IWant("to transfer money from my savings account").
                SoThat("I can get cash easily from an ATM");

            Scenario("Transfer money").
                Given("my savings account balance is", 100, delegate(int balance) { this._account.Balance = balance; }).
                And("my cash account balance is", 200, delegate(int balance) { this._cashAccount.Balance = balance; }).
                When("I transfer to cash account", 20, delegate(int amount) { this._account.Transfer(amount, _cashAccount); }).
                Then("my savings account balance should be", 80, delegate(int expected) { this.Ensure().IsTrue(_account.Balance == expected); }).
                And("my cash account balance should be", 220, delegate(int expected) { this.Ensure().IsTrue(_cashAccount.Balance == expected); });

            //Add more scenarios here...
        }
    }
}
