using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank.Rules
{
    /// <summary>
    /// Checking Account interest calculation
    /// </summary>
    /// <seealso cref="AbcBank.Rules.IInterestCalculator" />
    public class CheckingInterestCalculator : IInterestCalculator
    {
        /// <summary>
        /// Calculates the interest for specific transactions on specific date
        /// </summary>
        /// <param name="transactions">The source transactions.</param>
        /// <param name="cutOffDate">The cut off date.</param>
        /// <returns>
        /// calculated interest
        /// </returns>
        public double Calculate(IEnumerable<Transaction> transactions, DateTime cutOffDate)
        {
            return transactions.Where(t => t.Date.CompareTo(cutOffDate) <= 0).Sum(t => t.Amount) * 0.001;
        }
    }
}
