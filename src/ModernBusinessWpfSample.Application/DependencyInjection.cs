using Microsoft.Extensions.DependencyInjection;
using ModernBusinessWpfSample.Application.Customers;
using ModernBusinessWpfSample.Application.Dashboard;
using ModernBusinessWpfSample.Application.Orders;
using ModernBusinessWpfSample.Application.Reports;

namespace ModernBusinessWpfSample.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<GetDashboardSummaryUseCase>();
        services.AddTransient<GetCustomersUseCase>();
        services.AddTransient<RegisterCustomerUseCase>();
        services.AddTransient<GetOrdersUseCase>();
        services.AddTransient<RegisterOrderUseCase>();
        services.AddTransient<ExportOrdersReportUseCase>();
        return services;
    }
}
