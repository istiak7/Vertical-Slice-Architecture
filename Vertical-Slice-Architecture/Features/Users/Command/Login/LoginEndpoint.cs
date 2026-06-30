using MediatR;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Features.Users.Login
{
    public class LoginEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", async (LoginUserCommand request, IMediator mediator) =>
            {
                var response = await mediator.Send(request);
                return Results.Ok(response);
            });
        }
    }
}
