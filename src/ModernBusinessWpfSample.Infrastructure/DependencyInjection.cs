using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModernBusinessWpfSample.Application.Abstractions;
using ModernBusinessWpfSample.Domain.Common;
using ModernBusinessWpfSample.Infrastructure.ExternalApis;
using ModernBusinessWpfSample.Infrastructure.Persistence;
using ModernBusinessWpfSample.Infrastructure.Reports;

namespace ModernBusinessWpfSample.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<JsonFileStore>();
        services.AddSingleton<IClock, SystemClock>();
        services.AddSingleton<ICustomerRepository, JsonCustomerRepository>();
        services.AddSingleton<IOrderRepository, JsonOrderRepository>();
        services.AddTransient<IReportExporter, CsvReportExporter>();

        services.AddHttpClient<IProductCatalogApiClient, ProductCatalogApiClient>(client =>
        {
            client.BaseAddress = new Uri(configuration["Api:BaseUrl"] ?? "https://jsonplaceholder.typicode.com/");
            client.Timeout = TimeSpan.FromSeconds(10);
        })
        .AddStandardResilienceHandler();

        return services;
    }
}
