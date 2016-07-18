using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbcBank.Rules
{
    /// <summary>
    /// Calculation helper
    /// </summary>
    public static class CalcHelper
    {
        /// <summary>
        /// Calculates acquired daily interest
        /// </summary>
        /// <param name="value">The amount value.</param>
        /// <param name="yearRate">The yearly interest rate.</param>
        /// <param name="days">The days to calculate.</param>
        /// <param name="daysInYear">The days in year.</param>
        /// <returns>calculated interest</returns>
        public static double DailyInterest(this double value, double yearRate, int days, int daysInYear = 360)
        {
            double rate = yearRate / 100.0;
            return value * (rate / daysInYear) * days;
        }

    }
}
