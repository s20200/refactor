using LegacyApp.Validators;
using LegacyApp.Repositories;
using LegacyApp.Services;
using System;

namespace LegacyApp
{
    public class UserService
    { 
        private readonly IClientRep _clientRep;
        private readonly IUserDataAccess _userDataAccess;
        private readonly UserValidator _userValidator;

        public UserService(
            IClientRep clientRep, 
            IUserDataAccess userDataAccess,
            UserValidator userValidator)
        {
            _clientRep = clientRep;
            _userDataAccess = userDataAccess;
            _userValidator = userValidator;
        }

        public UserService() :
            this(new ClientRepository(),
                new UserDataAccessProvider(),
                new UserValidator(new DateTimeManager(), new UserCreditService()))
        {

        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (_userValidator.DidntProvideFirstOrLastName(firstName, lastName))
            {
                return false;
            }

            if (_userValidator.HasInvalidEmailAddress(email))
            {
                return false;
            }

            if (_userValidator.IsLessThan21YearsOld(dateOfBirth))
            {
                return false;
            }
            var client = _clientRep.GetById(clientId);
             var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            _userValidator.AssignCreditLimitBasedOnClient(client, user);

            if (_userValidator.HasCreditLimitLessThan500(user))
            {
                return false;
            }

            _userDataAccess.AddUser(user);
            return true;
        }
    }
}
