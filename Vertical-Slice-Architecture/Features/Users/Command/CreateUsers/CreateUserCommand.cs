using MediatR;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Features.Users.Command.CreateUsers
{
    public class CreateUserCommand : IRequest<Result>
    {
        public int RoleId { get; set; }
        private string _Password { get; set; } = null!;
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password
        {
            get => _Password;
            set => _Password = BCrypt.Net.BCrypt.HashPassword(value);
        }
    }
}
    