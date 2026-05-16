using ModernBusinessWpfSample.Application.Abstractions;
using ModernBusinessWpfSample.Domain.Orders;

namespace ModernBusinessWpfSample.Application.Dashboard;

public sealed class GetDashboardSummaryUseCase(
    ICustomerRepository customers,
    IOrderRepository orders,
    IProductCatalogApiClient apiClient)
{
    public async Task<DashboardSummary> ExecuteAsync(CancellationToken cancellationToken)
    {
        var customerList = await customers.GetAllAsync(cancellationToken);
        var orderList = await orders.GetAllAsync(cancellationToken);
        var apiHealth = await apiClient.GetApiHealthTextAsync(cancellationToken);
        var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        var monthly = orderList.Where(x => x.OrderedAt >= monthStart && x.Status != OrderStatus.Canceled).ToList();
        return new DashboardSummary(
            customerList.Count(x => x.IsActive),
            monthly.Count,
            monthly.Sum(x => x.Amount),
            apiHealth);
    }
}
