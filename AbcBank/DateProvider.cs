using System;


namespace AbcBank
{
    public class DateProvider:IDateProvider
    {
        private static DateProvider instance = null;

        public static DateProvider getInstance()
        {
            if (instance == null)
                instance = new DateProvider();
            return instance;
        }

        public DateTime now()
        {
            return DateTime.UtcNow;
        }
    }
}
