using LegacyApp.Repositories;

namespace LegacyApp
{
    public class ClientRepository : IClientRep
    {
        public ClientRepository()
        {
        }

        public Client GetById(int clientId)
        {
            //Fetching the data...
            return new Client
            {
                ClientId = clientId
            };
        }
    }
}