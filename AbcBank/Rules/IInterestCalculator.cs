using System;
using System.Collections.Generic;

namespace AbcBank.Rules
{
    /// <summary>
    /// The interface for Interest Calculation
    /// </summary>
    public interface IInterestCalculator
    {
        /// <summary>
        /// Calculates the interest for specific transactions on specific date
        /// </summary>
        /// <param name="transactions">The source transactions.</param>
        /// <param name="cutOffDate">The cut off date.</param>
        /// <returns>calculated interest</returns>
        double Calculate(IEnumerable<Transaction> transactions, DateTime cutOffDate);
    }
}
