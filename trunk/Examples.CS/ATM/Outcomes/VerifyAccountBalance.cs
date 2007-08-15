using NBehave.Framework.World;
using Examples.CS.ATM.Domain;


namespace Examples.CS.ATM.Outcomes
{
    class VerifyAccountBalance : WorldOutcome
    {
        IAccount account;
        int expectedBalance;

        public VerifyAccountBalance(IAccount account, int expectedBalance)
        {
            this.account = account;
            this.expectedBalance = expectedBalance;
        }


        protected override void Verify<T>(T world)
        {
            IAccount w = (IAccount)world;
            this.Ensure.IsTrue(account.Balance == expectedBalance);
        }

    }
}
