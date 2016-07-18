using System;

namespace AbcBank
{
    /// <summary>
    /// Typed exception for insufficient funds
    /// </summary>
    public class InsufficientFundsException:ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientFundsException"/> class.
        /// </summary>
        public InsufficientFundsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InsufficientFundsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public InsufficientFundsException(string message) : base(message)
        {
        }
    }
}
