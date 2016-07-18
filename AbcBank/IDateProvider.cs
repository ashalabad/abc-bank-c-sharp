using System;

namespace AbcBank
{
    /// <summary>
    /// Provides "now" date
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
