using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AbcBank.Test
{
    /// <summary>
    /// Base class for Account test operations
    /// </summary>
    public abstract class AccountTest
    {
        protected static readonly double DOUBLE_DELTA = 1e-15;
        [Test]
        public void NegativeDepositShouldThrowException()
        {
            Account account = InstantiateAccount();
            Assert.Throws<ArgumentException>(() => {
                account.deposit(-100);
            });

        }
        [Test]
        public void NegativeWithdrawalShouldThrowException()
        {
            Account account = InstantiateAccount();
            Assert.Throws<ArgumentException>(()=> {
                account.withdraw(-100);
            });
        }
        [Test]
        public void AccountCreatedWithNoTransactions()
        {

            Account account = InstantiateAccount();
            Assert.AreEqual(0, account.Transactions.Count());
            Assert.AreEqual(0, account.sumTransactions());
            Assert.AreEqual(0, account.interestEarned());
        }

        [Test]
        public void DepositShouldCreateOneTransactionWithPositiveValue()
        {
            Account account = InstantiateAccount();
            account.deposit(100.00d);
            Assert.AreEqual(1, account.Transactions.Count());
            Assert.AreEqual(100.0d, account.sumTransactions());
        }

        [Test]
        public void WithdrawalFromEmptyAccountShouldThrowException()
        {
            Assert.Throws<InsufficientFundsException>(() =>
            {
                Account account = InstantiateAccount();
                account.withdraw(100.0);
            });
        }

        [Test]
        public void WithdrawalMoreThanAccountHasShouldThrowException()
        {
            Assert.Throws<InsufficientFundsException>(() =>
            {
                Account account = InstantiateAccount();
                account.deposit(50.0);
                account.withdraw(100.0);
            });
        }

        [Test]
        public void EveryAccountOperationShouldGenerateTransaction()
        {
            Account account = InstantiateAccount();
            account.deposit(100.0);
            account.deposit(200.0);
            account.withdraw(100.0);
            Assert.AreEqual(3, account.Transactions.Count());
            Assert.AreEqual(200.0, account.sumTransactions());
        }

        [Test]
        public void AccountTransactionsShouldBeReturnedInHistoricOrder()
        {
            Account account = InstantiateAccount();
            account.deposit(100.0);
            Thread.Sleep(100);
            account.deposit(200.0);
            Thread.Sleep(100);
            account.deposit(50.0);
            Assert.AreEqual(3, account.Transactions.Count());
            Transaction t0 = account.Transactions.ElementAt(0);
            Assert.AreEqual(100,t0.Amount);
            Transaction t1 = account.Transactions.ElementAt(1);
            Assert.AreEqual(200,t1.Amount);
            Transaction t2 = account.Transactions.ElementAt(2);
            Assert.AreEqual(50, t2.Amount);
            //TODO: no way to verify the historic order of transactions
        }
        [Test]
        public void AccountWithdrawalShouldGenerateTransactionsWithNegativeAmount()
        {
            Account account = InstantiateAccount();
            account.deposit(100.0);
            account.deposit(200.0);
            account.withdraw(100.0);
            Assert.AreEqual(3, account.Transactions.Count());
            Assert.AreEqual(200.0, account.sumTransactions());
            
        }
        [Test]
        public void AccountAllowOfWithdrawalLesserAmountAfterDeposit()
        {
            Account account = InstantiateAccount();            
            account.deposit(200.0d);
            account.withdraw(100.0);
            Assert.AreEqual(100.0,account.sumTransactions());
            account.withdraw(100.0);
            Assert.AreEqual(0,account.sumTransactions());
        }

        #region Concurrent Testing

        /// <summary>
        /// Concurrents the withdrawal should not exeed account balance on the account.
        /// 
        /// </summary>
        [Test]
        public void ConcurrentWithdrawalShouldNotExeedAccountBalance()
        {
            int total = 0;
            Account account = InstantiateAccount();
            account.deposit(1000.0d);
            // 1000 threads will perform $1 withdrawal from the account
            List<Task> allTasks=new List<Task>();
            for (int i = 0; i < 1000; i++)
            {
                var task=Task.Factory.StartNew(() =>
                {
                    account.withdraw(1);
                    Interlocked.Increment(ref total);
                });
                allTasks.Add(task);
            }
            Task.WaitAll(allTasks.ToArray());
            Assert.AreEqual(1000,total);
            Assert.AreEqual(1001, account.Transactions.Count());
            Assert.AreEqual(0.0,account.sumTransactions());
        }
        #endregion
        protected abstract Account InstantiateAccount();
    }

}
