using NUnit.Framework;

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
            return new Account(Account.SAVINGS);
        }

        [Test]
        public void TestEarInterestOnTransactionLessThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(500.0);
            Assert.AreEqual(500.0 * 0.001, account.interestEarned(), DOUBLE_DELTA);
        }

        [Test]
        public void TestEarInterestOnTransactionMoreThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(1000.0);
            Assert.AreEqual(1.0, account.interestEarned(), DOUBLE_DELTA);
            account.deposit(500.0);
            Assert.AreEqual(1+500.0 * 0.002, account.interestEarned(), DOUBLE_DELTA);
        }
    }
}