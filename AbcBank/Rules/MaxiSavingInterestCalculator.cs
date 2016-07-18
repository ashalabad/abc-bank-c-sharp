using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Rules
{
    /// <summary>
    /// The calculator class to calculate interest for maxi-saving account
    /// </summary>
    /// <seealso cref="AbcBank.Rules.IInterestCalculator" />
    public class MaxiSavingInterestCalculator : IInterestCalculator
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
            double interest = 5.0;
            IEnumerable<Transaction> trxToDate = transactions.Where(t => t.Date.CompareTo(cutOffDate) <= 0);
            Transaction lastWidthdrawal = trxToDate.LastOrDefault(t => t.Amount < 0);
            if (lastWidthdrawal != null)
            {
                TimeSpan ts = cutOffDate - lastWidthdrawal.Date;
                if (ts.Days <= 10)
                    interest = 1.0;
            }
            return 0;
        }
    }
}
