using DataLayerCustomerManagement;
using DataLayerCustomerManagement.DTOs;
using DataLayerCustomerManagement.Entities;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogicLayerCustomerManagement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Result> CreateUserAsync(UserDto createUserDto)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(createUserDto.Username);

            if (existingUser != null)
            {
                return new Result { Success = false, Message = "Username already exists." };
            }
            var userEntity = new User
            {
                Username = createUserDto.Username,
                Password = HashPassword(createUserDto.Password)
            };

            var createdUserEntity = await _userRepository.CreateUserAsync(userEntity);

            return new Result { Success = createdUserEntity != null };
        }

        public async Task<Result> LoginAsync(UserDto userDto)
        {
            var userEntity = await _userRepository.GetUserByUsernameAsync(userDto.Username);

            if (userEntity == null || !VerifyPassword(userDto.Password, userEntity.Password))
            {
                return new Result { Success = false };
            }

            return new Result { Success = true };
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            { 
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
 
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            string hashOfInput = HashPassword(password);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hashedPassword) == 0;
        }
    }
}

