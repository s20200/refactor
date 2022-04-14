using System;
using LegacyApp.Services;

namespace LegacyApp
{
    public class UserCreditService : IUserCreditService, IDisposable
    {
        public void Dispose()
        {
            //...
        }

        public int GetCreditLimit(string firstName, string lastName, DateTime dateOfBirth)
        {
            //Fetching the data...
            return 10000;
        }
    }
}