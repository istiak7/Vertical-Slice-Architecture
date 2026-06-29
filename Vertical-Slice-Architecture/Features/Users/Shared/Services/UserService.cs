using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vertical_Slice_Architecture.Entities;
using Vertical_Slice_Architecture.Features.Users.Shared.Dtos;
using Vertical_Slice_Architecture.Shared;
using Vertical_Slice_Architecture.Shared.Repository;

namespace Vertical_Slice_Architecture.Features.Users.Shared.Services
{
    public interface IUserService
    {

        Task<Result> Login(UserLoginRequestDto request);
        Task<Result> LoginWithRefreshTokenAsync(int userId, string refreshToken);
        Task<Result> Registration(UserRegistrationRequestDto request);
    }
    public class UserService(JwtSettintgs _jwtSettings,
                             IBaseRepository<User> _userRepository
                              ) : IUserService 
    {
        private string RefreshToken => Guid.NewGuid().ToString();
        private async Task<Result> LoggedInUserResponse(User user)
        {
            var refreshToken = RefreshToken;
            user.RefreshToken = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(1);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new Result
            {
                IsSuccess = true,
                StatusCode = 200,
                Status = "Success",
                Message = "User logged in successfully",
                Data = new UserLoginResponseDto
                {
                    UserName = user.Name,
                    Email = user.Email,
                    AccessToken = token,
                    RefreshToken = refreshToken
                }
            };
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _jwtSettings.Subject),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("username", user.Name),
                new Claim("id", user.Id.ToString()),
                new Claim("roleId", user.RoleId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Result> Registration(UserRegistrationRequestDto request)
        {
            var existingUser = await _userRepository.GetAsync(u => u.Email == request.Email);
            if(existingUser == null)
            {
                return new Result
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Status = "Error",
                    Message = "User already exists"
                };
            }
            try
            {
                await _userRepository.AddAsync(new User
                {
                    Name = request.Username,
                    Email = request.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    RoleId = request.RoleId,
                    RefreshToken = RefreshToken,
                    RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7)
                });
                await _userRepository.SaveChangesAsync();
                return new Result
                {
                    IsSuccess = true,
                    StatusCode = 201,
                    Status = "Success",
                    Message = "User registered successfully"
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Status = "Error",
                    Message = $"An error occurred while registering the user: {ex.Message}"
                };
            }
           

        }
