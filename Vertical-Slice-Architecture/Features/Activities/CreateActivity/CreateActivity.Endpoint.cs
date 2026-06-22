//using MediatR;
using Vertical_Slice_Architecture.Shared;
using Vertical_Slice_Architecture.Shared.MediatR;

namespace Vertical_Slice_Architecture.Features.Activities.CreateActivity
{
    public class CreateActivityEndpoint : IEndpoint
    {
        private readonly IMediator mediator;
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/activities", async (CreateActivityCommand command, IMediator _mediator) =>
            {

                var result = await _mediator.SendAsync(command);
                return Results.Ok(result);

            });
        }
    }
}
