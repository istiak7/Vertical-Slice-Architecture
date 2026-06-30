using Vertical_Slice_Architecture.Features.Users.Shared.Services;

namespace Vertical_Slice_Architecture.Shared.Extensions.DependencyExtensions
{
    public static class RegisterService
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
