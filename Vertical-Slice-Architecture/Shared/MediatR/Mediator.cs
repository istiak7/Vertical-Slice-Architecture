namespace Vertical_Slice_Architecture.Shared.MediatR
{
    public interface IMediator
    {
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    }
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>)
            .MakeGenericType(requestType, typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);

            if (handler is null)
            {
                throw new InvalidOperationException(
                    $"No handler registered for request type {requestType.Name}");
            }
            var handleMethod = handlerType.GetMethod("HandleAsync");

            if (handleMethod is null)
            {
                throw new InvalidOperationException(
                    $"HandleAsync method not found on handler for {requestType.Name}");
            }

            var resultTask = (Task<TResponse>)handleMethod.Invoke(
                handler,
                new object[] { request, cancellationToken })!;

            return await resultTask;

        }
    }
}
