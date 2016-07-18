using NUnit.Framework;
using AbcBank.Rules;

namespace AbcBank.Test
{
    /// <summary>
    /// Maxi Saving Account Tests
    /// </summary>
    [TestFixture]
    public class MaxiSavingAccountTest : AccountTest
    {


        protected override Account InstantiateAccount()
        {
            return new Account(AccountType.MAXI_SAVINGS, new MaxiSavingInterestCalculator(), DateProvider.getInstance());
        }
        [Test]
        public void TestEarnInterestOnTransactionLessThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(500.0);
            Assert.AreEqual(500.0 * 0.02, account.interestEarned(), DOUBLE_DELTA);
        }
        [Test]
        public void TestEarnInterestOnTransactionMoreThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(1000.0);
            account.deposit(500.0);
            Assert.AreEqual(20 + 500.0 * 0.05, account.interestEarned(), DOUBLE_DELTA);

        }

        [Test]
        public void TestEarnInterestOnTransactionMoreThan2000()
        {
            Account account = InstantiateAccount();
            account.deposit(2500.0);
            Assert.AreEqual(70 + 500 * 0.1, account.interestEarned(), DOUBLE_DELTA);
        }


    }
}