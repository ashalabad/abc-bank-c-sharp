using NUnit.Framework;
using System;

namespace AbcBank.Test
{
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void transaction()
        {
            Transaction t = new Transaction(5,DateTime.UtcNow);
            Assert.AreEqual(true, t is Transaction);
        }

        [Test]
        public void TransactionsWithTheSameDateShouldCompareAsEqual()
        {
            Transaction t1 = new Transaction(100, new DateTime(1970, 1, 1));
            Transaction t2 = new Transaction(-300, new DateTime(1970, 1, 1));
            Assert.AreEqual(0, t1.CompareTo(t2));
        }

        [Test]
        public void EarlierTransactionShouldBeLessThanLaterTransaction()
        {
            Transaction t1 = new Transaction(100, new DateTime(1970, 1, 1));
            Transaction t2 = new Transaction(-300, new DateTime(1980, 1, 1));
            Assert.AreEqual(-1, t1.CompareTo(t2));

        }

        [Test]
        public void LaterTransactionShouldBeGreaterThanEarlierTransaction()
        {
            Transaction t1 = new Transaction(100, new DateTime(1970, 1, 1));
            Transaction t2 = new Transaction(-300, new DateTime(1980, 1, 1));
            Assert.AreEqual(1, t2.CompareTo(t1));
        }

    }
}
