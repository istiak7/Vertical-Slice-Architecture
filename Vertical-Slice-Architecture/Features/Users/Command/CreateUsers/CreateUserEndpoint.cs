using MediatR;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Features.Users.Command.CreateUsers
{
    public class CreateUserEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/Registration", async (CreateUserCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
        }
    }
}
