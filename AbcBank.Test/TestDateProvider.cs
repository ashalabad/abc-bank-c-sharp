using System;

namespace AbcBank.Test
{
    /// <summary>
    /// Test Date Provider
    /// Normally I would use Moq to create a mock provider
    /// </summary>
    /// <seealso cref="AbcBank.IDateProvider" />
    internal class TestDateProvider : IDateProvider
    {
        public Func<DateTime> DateNowFunc { get; set; }

        public TestDateProvider(Func<DateTime> nowFunc)
        {
            DateNowFunc = nowFunc;
        }
        public DateTime now()
        {
            return DateNowFunc();
        }
    }
}
