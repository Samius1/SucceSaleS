namespace SucceSales.Infraestructure.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using SucceSales.Domain.Services;
    using SucceSales.Storage.Repositories;

    public static partial class ServiceCollectionExtensions
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<ISaleDomainService, SaleDomainService>();
            services.AddScoped<ISalesRepository, SalesRepository>();
        }
    }
}