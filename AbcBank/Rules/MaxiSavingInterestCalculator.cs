using System;
using System.Collections.Generic;
using System.Linq;

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
            Transaction[] trxToDate = transactions.Where(t => t.Date.CompareTo(cutOffDate) <= 0).ToArray();
            Transaction lastWidthdrawal = trxToDate.LastOrDefault(t => t.Amount < 0);
            double rate = lastWidthdrawal != null ? ((cutOffDate - lastWidthdrawal.Date).Days<=10?0.1:5.0) : 5.0;
            return RecursiveCalc(cutOffDate, rate, trxToDate, 0, 0);
        }
        // recursive function to calculate interest
        public double RecursiveCalc(DateTime cutOffDate, double rate, Transaction[] transactions,
            double balance, double acquiredInterest)
        {
            if (!transactions.Any()) return 0;
            Transaction trx = transactions[0];
            if (transactions.Length == 1)
            {
                int days = (cutOffDate - trx.Date).Days;
                acquiredInterest += (trx.Amount+balance).DailyInterest(rate, days);
                return acquiredInterest;
            }
            else
            {
                int days = (transactions[1].Date - transactions[0].Date).Days;
                balance += transactions[0].Amount;
                acquiredInterest=acquiredInterest + (balance.DailyInterest(rate, days));
                return RecursiveCalc(cutOffDate, rate, transactions.Skip(1).ToArray(), balance, acquiredInterest);

            }
        }
    }
}
