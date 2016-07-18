using AbcBank.Rules;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Test
{
    [TestFixture]
    public class InterestCalculatorsTest
    {
        protected static readonly double DOUBLE_DELTA = 1e-15;

        [Test]
        public void TestDailyRateCalculation()
        {
            // 5% of 100 days of $1000
            double value = 1000.0d.DailyInterest(5.0, 360);
            Assert.AreEqual(50, value, DOUBLE_DELTA);
            value = 1000.0d.DailyInterest(5.0, 180);
            Assert.AreEqual(25, value, DOUBLE_DELTA);
        }
        #region Checking Calculator tests
        [Test]
        public void TestCalculateCheckingInterestForAllTransactions()
        {
            Transaction[] trx = new Transaction[] 
            {
                new Transaction(25000,new DateTime(1970,1,1)),
                new Transaction(25000,new DateTime(1970,2,1)),
                new Transaction(25000,new DateTime(1970,3,1)),
                new Transaction(25000,new DateTime(1970,4,1)),
            };

            CheckingInterestCalculator calculator = new CheckingInterestCalculator();
            double interest = calculator.Calculate(trx, DateTime.MaxValue);
            Assert.AreEqual(100000.0 * 0.001, interest, DOUBLE_DELTA);
        }
        [Test]
        public void TestCalculateCheckingInterestOnSpecificDate()
        {
            Transaction[] trx = new Transaction[]
            {
                new Transaction(25000,new DateTime(1970,1,1)),
                new Transaction(25000,new DateTime(1970,2,1)),
                new Transaction(25000,new DateTime(1970,3,1)),
                new Transaction(25000,new DateTime(1970,4,1)),
            };
            CheckingInterestCalculator calculator = new CheckingInterestCalculator();
            double interest = calculator.Calculate(trx, new DateTime(1970,2,15));
            Assert.AreEqual(50000.0 * 0.001, interest, DOUBLE_DELTA);
            interest = calculator.Calculate(trx, new DateTime(1970, 4, 15));
            Assert.AreEqual(100000.0 * 0.001, interest, DOUBLE_DELTA);
        }
        #endregion
        #region Saving Calculator tests
        [Test]
        public void TestCalculateSavingInterestOnTransactionLessThan1000()
        {
            Transaction[] trx = new []
            {
                new Transaction(500,new DateTime(1970,1,1)),
            };
            SavingInterestCalculator calculator = new SavingInterestCalculator();
            Assert.AreEqual(500.0 * 0.001, calculator.Calculate(trx,DateTime.MaxValue), DOUBLE_DELTA);
        }

        [Test]
        public void TestCalculateSavingInterestOnTransactionsOf1000()
        {
            Transaction[] trx = new []
            {
                new Transaction(500,new DateTime(1970,1,1)),
                new Transaction(500,new DateTime(1970,2,1)),
            };
            SavingInterestCalculator calculator = new SavingInterestCalculator();
            Assert.AreEqual(1000.0 * 0.001, calculator.Calculate(trx, DateTime.MaxValue), DOUBLE_DELTA);
        }

        [Test]
        public void TestCalculateSavingInterestOnTransactionsMoreThan1000()
        {
            Transaction[] trx = new []
            {
                new Transaction(500,new DateTime(1970,1,1)),
                new Transaction(500,new DateTime(1970,2,1)),
                new Transaction(500,new DateTime(1970,3,1)),
                new Transaction(500,new DateTime(1970,4,1)),
            };
            SavingInterestCalculator calculator = new SavingInterestCalculator();
            Assert.AreEqual(1 + 1000 * 0.002, calculator.Calculate(trx,DateTime.MaxValue), DOUBLE_DELTA);
        }
        [Test]
        public void TestCalculateSavingInterestOnIntervals()
        {
            Transaction[] trx = new []
            {
                new Transaction(500,new DateTime(1970,1,1)),
                new Transaction(500,new DateTime(1970,2,1)),
                new Transaction(500,new DateTime(1970,3,1)),
                new Transaction(500,new DateTime(1970,4,1)),
                new Transaction(-1000,new DateTime(1970,5,1)),
            };
            SavingInterestCalculator calculator = new SavingInterestCalculator();
            Assert.AreEqual(1000.0 * 0.001, calculator.Calculate(trx, DateTime.MaxValue), DOUBLE_DELTA);
            Assert.AreEqual(1 + 1000 * 0.002, calculator.Calculate(trx, new DateTime(1970,4,15)), DOUBLE_DELTA);
        }

        #endregion
        #region Maxi-Saving Calculator tests
        //accumulate 5% interest rate for single transaction of 360 days        
        [Test]
        public void TestCalculateMaxiSavingInterestFor360Days()
        {
            Transaction[] trx = new[]
            {
                new Transaction(1000,new DateTime(1970,1,1)),
            };
            MaxiSavingInterestCalculator calculator = new MaxiSavingInterestCalculator();
            double amount = calculator.Calculate(trx, new DateTime(1970, 12, 27));
            Assert.AreEqual(50, amount, DOUBLE_DELTA);
        }
        //accumulate 5% interest rate for single transaction of 180 days
        [Test]
        public void TestCalculateMaxiSavingInterestFor180Days()
        {
            Transaction[] trx = new[]
            {
                new Transaction(1000,new DateTime(1970,1,1)),
            };
            MaxiSavingInterestCalculator calculator = new MaxiSavingInterestCalculator();
            double amount = calculator.Calculate(trx, new DateTime(1970, 6, 30));
            Assert.AreEqual(25, amount, DOUBLE_DELTA);
        }
        //Accumulate 5% interest rate for two transactions of 180 and 360 days
        [Test]
        public void TestCalculateMaxiSavingInterestFor180DAnd360ays()
        {
            Transaction[] trx = new[]
            {
                new Transaction(1000,new DateTime(1970,1,1)),
                new Transaction(1000,new DateTime(1970,6,30)),
            };
            MaxiSavingInterestCalculator calculator = new MaxiSavingInterestCalculator();
            double amount = calculator.Calculate(trx, new DateTime(1970, 12, 30));
            Assert.AreEqual(75, amount, DOUBLE_DELTA);
        }
        // Accumulate 0.1% interest rate if there was a withdrawal within 10 days
        [Test]
        public void TestCalculateMaxiSavingInterestWithdrawalWithin10Days()
        {
            Transaction[] trx = new[]
            {
                new Transaction(1000,new DateTime(1970,1,1)),
                new Transaction(1000, new DateTime(1970,7,1)),
                new Transaction(-1000, new DateTime(1970,12,26))
            };
            MaxiSavingInterestCalculator calculator = new MaxiSavingInterestCalculator();
            double amount = calculator.Calculate(trx, new DateTime(1970, 12, 31));
            double expected = 1000.0.DailyInterest(0.1, 360) + 1000.0.DailyInterest(0.1, 175);
            Assert.AreEqual(expected, amount);
        }
        //Accumulate 5% interest rate if there was a withdrawal older than 10 days
        [Test]
        public void TestCalculateMaxiSavingInterestWithdrawalOlderThen10Days()
        {
            Transaction[] trx = new[]
            {
                new Transaction(1000,new DateTime(1970,1,1)),
                new Transaction(1000, new DateTime(1970,2,1)),
                new Transaction(-1000, new DateTime(1970,2,15))
            };
            MaxiSavingInterestCalculator calculator = new MaxiSavingInterestCalculator();
            double amount = calculator.Calculate(trx, new DateTime(1970, 12, 31));
            double expected = 1000.0.DailyInterest(5.0, 80) + 1000.0.DailyInterest(5.0, 360) + 1000.0.DailyInterest(5.0, 80);
            Assert.AreEqual(expected, amount);        

        }
        #endregion
    }
}
