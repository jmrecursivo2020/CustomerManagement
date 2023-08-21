using DataLayerCustomerManagement.Entities;
using Microsoft.Data.SqlClient;
using Dapper;
namespace DataLayerCustomerManagement
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = @"INSERT INTO Clients (Name, LastName, Email) OUTPUT INSERTED.ClientId VALUES (@Name, @LastName, @Email)";
            var id = await connection.QuerySingleAsync<int>(query, client);
            client.ClientId = id;
            return client;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = @"DELETE FROM Clients WHERE ClientId = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = @"SELECT COUNT(1) FROM Clients WHERE Email = @Email";
            var count = await connection.QuerySingleAsync<int>(query, new { Email = email });
            return count > 0;
        }

        public async Task<Client> GetClientAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = @"SELECT * FROM Clients WHERE ClientId = @Id";
            var client = await connection.QuerySingleOrDefaultAsync<Client>(query, new { Id = id });
            return client;
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = @"SELECT * FROM Clients";
            var clients = await connection.QueryAsync<Client>(query);
            return clients;
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = @"UPDATE Clients SET Name = @Name, LastName = @LastName, Email = @Email WHERE ClientId = @ClientId";
            await connection.ExecuteAsync(query, client);
            return client;
        }
    }
}
