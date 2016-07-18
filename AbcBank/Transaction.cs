using System;

namespace AbcBank
{
    /// <summary>
    /// Simple Transaction class
    /// Transactions are immutable and imlemet IComparable to to help with sorting
    /// Date component of Transactions is incuded in sorting
    /// </summary>
    /// <seealso cref="System.IComparable{AbcBank.Transaction}" />
    public class Transaction:IComparable<Transaction>
    {
        /// <summary>
        /// Gets the transaction amount.
        /// </summary>
        /// <value>
        /// The transaction amount.
        /// </value>
        public double Amount { get; private set; }
        /// <summary>
        /// Gets the transaction date.
        /// </summary>
        /// <value>
        /// The transaction date.
        /// </value>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="amount">The transaction amount.</param>
        /// <param name="date">The  transaction date.</param>
        public Transaction(double amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }

        /// <summary>
        /// Compares two transaction's DateTime component.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// 0 if this=other; 1 if this>other; -1 if this<other 
        /// </returns>
        public int CompareTo(Transaction other)
        {
            return Date.CompareTo(other.Date);
        }
    }
}
