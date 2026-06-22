//using MediatR;
using Vertical_Slice_Architecture.Shared;
using Vertical_Slice_Architecture.Shared.MediatR;

namespace Vertical_Slice_Architecture.Features.Activities.CreateActivity
{
    public sealed record CreateActivityCommand(
        string Title,
        string Description) : IRequest<Result>;
}
