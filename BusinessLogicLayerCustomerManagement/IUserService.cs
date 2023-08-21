using DataLayerCustomerManagement.DTOs;

namespace BusinessLogicLayerCustomerManagement
{
    public interface IUserService
    {
        Task<Result> CreateUserAsync(UserDto createUserDto);
        Task<Result> LoginAsync(UserDto userDto);
    }
}