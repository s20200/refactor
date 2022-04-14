using System;

namespace LegacyApp.Services
{
    public interface IUserCreditService
    {
        int GetCreditLimit(string firstName, string lastName, DateTime dateOfBirth);
    }
}