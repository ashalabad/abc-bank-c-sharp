using NUnit.Framework;
using System;

namespace AbcBank.Test
{
    [TestFixture]
    public class DateProviderTest
    {
        [Test]
        public void TestDateProviderInstanceNotNull()
        {
            Assert.IsNotNull(DateProvider.getInstance());
        }
        [Test]
        public void TestGettingNowDateShouldBeUTCCurrentDateTime()
        {
            DateProvider provider = DateProvider.getInstance();
            Assert.IsNotNull(provider);
            DateTime now = DateTime.UtcNow;
            DateTime date=provider.now();
            Assert.IsFalse(date == default(DateTime));
            Assert.IsTrue(date.Kind == DateTimeKind.Utc);
            Assert.AreEqual(now.Year, date.Year);
            Assert.AreEqual(now.Month, date.Month);
            Assert.AreEqual(now.Day, date.Day);
            Assert.AreEqual(now.Hour, date.Hour);
            Assert.AreEqual(now.Minute, date.Minute);
            Assert.AreEqual(now.Hour, date.Hour);
            Assert.AreEqual(now.Minute, date.Minute);
            Assert.AreEqual(now.Second, date.Second);
        }
    }
}
