using NUnit.Framework;

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
            return new Account(Account.MAXI_SAVINGS);
        }
        [Test]
        public void TestEarInterestOnTransactionLessThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(500.0);
            Assert.AreEqual(500.0 * 0.02, account.interestEarned(), DOUBLE_DELTA);
        }
        [Test]
        public void TestEarInterestOnTransactionMoreThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(1000.0);
            account.deposit(500.0);
            Assert.AreEqual(20 + 500.0 * 0.05, account.interestEarned(), DOUBLE_DELTA);

        }

        [Test]
        public void TestEarInterestOnTransactionMoreThan2000()
        {
            Account account = InstantiateAccount();
            account.deposit(2500.0);
            Assert.AreEqual(70 + 500 * 0.1, account.interestEarned(), DOUBLE_DELTA);
        }


    }
}