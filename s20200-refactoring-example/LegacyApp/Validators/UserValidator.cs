using LegacyApp.Services;
using System;

namespace LegacyApp.Validators
{
    public class UserValidator
    {
        private readonly IDateTimeManager _dateTimeManager;
        private readonly IUserCreditService _userCreditService;

        public UserValidator(IDateTimeManager dateTimeManager, IUserCreditService userCreditService)
        {
            _dateTimeManager = dateTimeManager;
            _userCreditService = userCreditService;
        }

        public bool DidntProvideFirstOrLastName(string firstName, string lastName)
        {
            return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
        }

        public bool HasInvalidEmailAddress(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        public bool IsLessThan21YearsOld(DateTime dateOfBirth)
        {
            var now = _dateTimeManager.DateTimeNow;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day) age--;

            return age < 21;
        }

        public void AssignCreditLimitBasedOnClient(Client client, User user)
        {
            if (client.Name == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Name == "ImportantClient")
            {
                user.HasCreditLimit = true;
                int creditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
            else
            {
                user.HasCreditLimit = true;
                int creditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }

        public bool HasCreditLimitLessThan500(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }
    }
}