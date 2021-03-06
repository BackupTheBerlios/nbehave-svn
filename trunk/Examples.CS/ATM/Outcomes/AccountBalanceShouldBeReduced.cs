using NBehave.Framework;
using NBehave.Framework.World;
using Example.ATM.Domain;


namespace Example.ATM.Outcomes
{
    class AccountBalanceShouldBeReduced:WorldOutcome
    {
        IAccount account;
        int expectedBalance;

        public AccountBalanceShouldBeReduced(IAccount account, int expectedBalance)
        {
            this.account = account;
            this.expectedBalance = expectedBalance;
        }

        protected override void Verify<T>(T world)
        {
            this.Ensure.IsTrue(account.Balance == expectedBalance);
        }
    }
}
