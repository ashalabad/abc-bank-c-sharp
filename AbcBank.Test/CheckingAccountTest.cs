using NUnit.Framework;
using AbcBank.Rules;

namespace AbcBank.Test
{
    /// <summary>
    /// Checking Account Test
    /// </summary>
    [TestFixture]
    public class CheckingAccountTest:AccountTest
    {
        protected override Account InstantiateAccount()
        {
            return new Account(AccountType.CHECKING, new CheckingInterestCalculator(),DateProvider.getInstance());
        }

        /// <summary>
        /// Tests the ear interest on transactions.
        /// for Checking account the interest earned should be flat rate 0f 0.01%
        /// </summary>
        [Test]
        public void TestEarInterestOnTransactions()
        {
            Account account = InstantiateAccount();
            account.deposit(100000.0);
            Assert.AreEqual(100000.0*0.001,account.interestEarned(),DOUBLE_DELTA);
        }

    }
}