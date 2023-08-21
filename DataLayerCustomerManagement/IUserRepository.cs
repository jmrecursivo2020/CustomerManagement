using DataLayerCustomerManagement.Entities;

namespace DataLayerCustomerManagement
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> UserExistsAsync(string username);
    }
}
