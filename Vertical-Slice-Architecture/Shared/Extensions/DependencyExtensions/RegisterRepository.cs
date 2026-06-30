using Vertical_Slice_Architecture.Shared.Repository;

namespace Vertical_Slice_Architecture.Shared.Extensions.DependencyExtensions
{
    public static class RegisterRepository
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
