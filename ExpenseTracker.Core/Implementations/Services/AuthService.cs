using ExpenseTracker.Common.DTOs;
using ExpenseTracker.Common.Models;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Interfaces.IRepositories;
using ExpenseTracker.Core.Interfaces.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Core.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<UserResponseDto> RegisterAsync(UserRequestDTO user)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(user.Name);
            if (existingUser != null)
            {
                throw new CustomAppException("User Already Exists. Please use a different userName or login!");
            }

            var newUser = new User { Username = user.Name, Password = HashPassword(user.Password) };

            var userInDb = await _userRepository.AddUserAsync(newUser);

            return new UserResponseDto()
            {
                UserId = userInDb.Id,
                Token = GenerateToken(newUser)
            };
        }

        public async Task<UserResponseDto?> LoginAsync(UserRequestDTO user)
        {
            var userInDb = await _userRepository.GetUserByUsernameAsync(user.Name);

            if (userInDb != null && ValidatePassword(user.Password, userInDb.Password))
            {
                return new UserResponseDto()
                {
                    UserId = userInDb.Id,
                    Token = GenerateToken(userInDb)
                };
            }

            return null;
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt());
        }

        private bool ValidatePassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }
    }
}
