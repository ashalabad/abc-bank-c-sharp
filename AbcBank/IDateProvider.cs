using System;

namespace AbcBank
{
    /// <summary>
    /// Provides "now" date interface.
    /// The interface was introduced to simplify unit tests
    /// and add the ability to DI an implementations
    /// </summary>
    public interface IDateProvider
    {
        /// <summary>
        /// Get 'Now" date time
        /// </summary>
        /// <returns></returns>
        DateTime now();
    }
}
