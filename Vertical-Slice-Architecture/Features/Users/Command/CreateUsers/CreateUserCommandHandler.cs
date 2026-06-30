using MediatR;
using Vertical_Slice_Architecture.Entities;
using Vertical_Slice_Architecture.Shared;
using Vertical_Slice_Architecture.Shared.Repository;

namespace Vertical_Slice_Architecture.Features.Users.Command.CreateUsers
{
    public class CreateUserCommandHandler(IBaseRepository<User> _userRepository)
    : IRequestHandler<CreateUserCommand, Result>
    {
        private string RefreshToken => Guid.NewGuid().ToString();
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetAsync(u => u.Email == request.Email);
            if (existingUser != null)
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
    }
}
