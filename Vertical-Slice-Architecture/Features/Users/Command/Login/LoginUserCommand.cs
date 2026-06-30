using MediatR;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Features.Users.Login
{
    public class LoginUserCommand : IRequest<Result>
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }
}
