using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KazandiRio.Application.Extensions
{
    public static class BuilderExtensions
    {
        public static IServiceCollection AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
