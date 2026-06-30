using MediatR;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Features.Users.Login
{
    public class RefreshTokenCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}
