using System;
using System.Collections.Generic;
using System.Linq;

namespace AbcBank.Rules
{
    /// <summary>
    /// Saving Account interest calculator
    /// </summary>
    /// <seealso cref="AbcBank.Rules.IInterestCalculator" />
    public class SavingInterestCalculator : IInterestCalculator
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
            double amount=transactions.Where(t=>t.Date.CompareTo(cutOffDate)<=0).Sum(t=>t.Amount);
            return amount <= 1000?
                amount * 0.001 : 1+ (amount - 1000) * 0.002;
        }
    }
}
