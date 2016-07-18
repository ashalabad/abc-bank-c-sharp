using System;

namespace AbcBank
{
    public static class Utilities
    {
        /// <summary>
        /// Validation Extension method - check for positive value
        /// </summary>
        /// <param name="val">The value.</param>
        /// <exception cref="System.ArgumentException">Positive value is expected</exception>
        public static void Positive(this double val)
        {
            if (val <= 0)
                throw new ArgumentException("Positive value is expected");
        }
    }
}
