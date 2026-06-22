namespace Vertical_Slice_Architecture.Shared.MediatR
{
    public interface IRequestHandler <TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }
}
