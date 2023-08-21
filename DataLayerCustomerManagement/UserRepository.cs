using DataLayerCustomerManagement.Entities;
using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace DataLayerCustomerManagement
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<User> CreateUserAsync(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password); SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = await connection.QuerySingleAsync<int>(query, user);
            user.UserId = id;
            return user;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT * FROM Users WHERE Username = @Username";
            var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });
            return user;
        }
        public async Task<bool> UserExistsAsync(string username)
        {
            using var connection = new SqlConnection(_connectionString);
            const string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
            var userExists = await connection.ExecuteScalarAsync<bool>(query, new { Username = username });
            return userExists;
        }
    }
}
