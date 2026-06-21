using MediatR;
using Vertical_Slice_Architecture.Database;
using Vertical_Slice_Architecture.Entities;
using Vertical_Slice_Architecture.Shared;

namespace Vertical_Slice_Architecture.Features.Activities.CreateActivity
{
    public class CreateActivityHandler : IRequestHandler<CreateActivityCommand, Result>
    {
        private readonly AppDbContext _appDbContext;
        public CreateActivityHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Result> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = new Activity
            {
                Name = request.Title,
                Description = request.Description
            };
            _appDbContext.Activities.Add(activity);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new Result
            {
                IsSuccess = true,
                StatusCode = 201,
                Status = "Success",
                Message = "Activity created successfully",
                Data = activity
            };
        }
    }
}
