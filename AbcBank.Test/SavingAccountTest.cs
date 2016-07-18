using NUnit.Framework;
using AbcBank.Rules;

namespace AbcBank.Test
{
    /// <summary>
    /// Saving accounts test
    /// </summary>
    [TestFixture]
    public class SavingAccountTest : AccountTest
    {


        protected override Account InstantiateAccount()
        {
            return new Account(AccountType.SAVINGS,new MaxiSavingInterestCalculator(), DateProvider.getInstance());
        }

        [Test]
        public void TestEarnInterestOnTransactionLessThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(500.0);
            Assert.AreEqual(500.0 * 0.001, account.interestEarned(), DOUBLE_DELTA);
        }

        [Test]
        public void TestEarnInterestOnTransactionMoreThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(1000.0);
            Assert.AreEqual(1.0, account.interestEarned(), DOUBLE_DELTA);
            account.deposit(500.0);
            Assert.AreEqual(1+500.0 * 0.002, account.interestEarned(), DOUBLE_DELTA);
        }
    }
}