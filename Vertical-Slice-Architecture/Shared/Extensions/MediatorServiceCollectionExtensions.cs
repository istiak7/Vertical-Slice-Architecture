using System.Reflection;
using Vertical_Slice_Architecture.Shared.MediatR;

namespace Vertical_Slice_Architecture.Shared.Extensions
{
    public static class MediatorServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(
        this IServiceCollection services,
        params Assembly[] assemblies)
        {
            services.AddTransient<IMediator, Mediator>();

            var handlerTypes = assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t is { IsClass: true, IsAbstract: false })
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                             i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .ToList();

            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType &&
                               i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

                services.AddTransient(interfaceType, handlerType);
            }

            return services;
        }
    }
}
