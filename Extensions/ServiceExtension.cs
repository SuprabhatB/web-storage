using BrowserStorage.API;
using BrowserStorage.Models;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace BrowserStorage
{
    /// <summary>
    /// Defines the <see cref="ServiceExtension" />.
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Adds the client storage.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddClientStorage(this IServiceCollection services)
        {
            services.AddScoped<IClientStorage>(sp => ActivatorUtilities.CreateInstance<ClientStorage>(sp, sp.GetRequiredService<IJSRuntime>()));
        }

        /// <summary>
        /// Adds the client storage.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="options">The options.</param>
        public static void AddClientStorage(this IServiceCollection services, Action<ServiceOptionModel> configure)
        {
            services
                .AddScoped<IClientStorage>(sp => ActivatorUtilities.CreateInstance<ClientStorage>(sp, sp.GetRequiredService<IJSRuntime>()))
                .Configure<ServiceOptionModel>(options => { configure?.Invoke(options); });
        }
    }
}