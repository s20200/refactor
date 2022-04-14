using System;

namespace LegacyApp.Services
{
    public class DateTimeManager : IDateTimeManager
    {
        public DateTime DateTimeNow => DateTime.Now;
    }
}