using MediatR;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Features.Activities.CreateActivity
{
   public class CreateActivityEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/activities", async (CreateActivityCommand command, IMediator mediator) =>
            {

                var result = await mediator.Send(command);
                return Results.Ok(result);
               
            });
        }
    }
}
