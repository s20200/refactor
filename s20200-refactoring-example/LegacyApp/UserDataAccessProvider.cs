namespace LegacyApp
{
    public class UserDataAccessProvider : IUserDataAccess
    {
        public void AddUser(User user)
        {
            UserDataAccess.AddUser(user);
        }
    }
}
