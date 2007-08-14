using NBehave.Framework.World;
using Example.ATM.Domain;


namespace Example.ATM.Outcomes
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
