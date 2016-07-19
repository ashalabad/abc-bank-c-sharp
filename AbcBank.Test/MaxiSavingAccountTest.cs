using NUnit.Framework;
using AbcBank.Rules;
using System;

namespace AbcBank.Test
{
    /// <summary>
    /// Maxi Saving Account Tests
    /// </summary>
    [TestFixture]
    public class MaxiSavingAccountTest : AccountTest
    {
        private DateTime nowDate;
        private TestDateProvider provider;

        [SetUp]
        public void SetUp()
        {
            nowDate = new DateTime(1970, 1, 1);
            provider = new TestDateProvider(() => nowDate);
        }

        protected override Account InstantiateAccount()
        {
            return new Account(AccountType.MAXI_SAVINGS, new MaxiSavingInterestCalculator(), provider);
        }
        [Test]
        public void TestEarnInterestOnTransactionLessThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(500.0);
            double expected = 500.0d.DailyInterest(5, 180);
            nowDate += new TimeSpan(180,0,0,0);
            Assert.AreEqual(expected, account.interestEarned(), DOUBLE_DELTA);
        }
        [Test]
        public void TestEarnInterestOnTransactionMoreThan1000()
        {
            Account account = InstantiateAccount();
            account.deposit(1000.0);
            nowDate += new TimeSpan(180, 0, 0, 0);
            account.deposit(500.0);
            nowDate += new TimeSpan(180, 0, 0, 0);
            double expected = 1000.0.DailyInterest(5, 180) + 1500.0.DailyInterest(5, 180);
            Assert.AreEqual(expected, account.interestEarned(), DOUBLE_DELTA);

        }

        [Test]
        public void TestEarnInterestOnTransactionMoreThan2000()
        {
            Account account = InstantiateAccount();
            account.deposit(2500.0);
            nowDate += new TimeSpan(180, 0, 0, 0);
            double expected = 2500.0d.DailyInterest(5, 180);
            Assert.AreEqual(expected, account.interestEarned(), DOUBLE_DELTA);
        }


    }
}