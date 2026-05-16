using ModernBusinessWpfSample.Application.Abstractions;
using ModernBusinessWpfSample.Domain.Orders;

namespace ModernBusinessWpfSample.Infrastructure.Persistence;

internal sealed class JsonOrderRepository(JsonFileStore store) : IOrderRepository
{
    private const string FileName = "orders.json";

    public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken)
        => await store.ReadAsync(FileName, Seed, cancellationToken);

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        var orders = await store.ReadAsync(FileName, Seed, cancellationToken);
        orders.Add(order);
        await store.WriteAsync(FileName, orders, cancellationToken);
    }

    private static List<Order> Seed()
    {
        var c1 = Guid.NewGuid();
        var c2 = Guid.NewGuid();
        var c3 = Guid.NewGuid();
        return
        [
            new() { Id = Guid.NewGuid(), OrderNo = "SO-202601-0001", CustomerId = c1, CustomerName = "Northwind Trading", OrderedAt = DateTime.Today.AddDays(-4), Amount = 248000m, Status = OrderStatus.Confirmed },
            new() { Id = Guid.NewGuid(), OrderNo = "SO-202601-0002", CustomerId = c2, CustomerName = "Contoso Retail", OrderedAt = DateTime.Today.AddDays(-2), Amount = 128500m, Status = OrderStatus.Shipped },
            new() { Id = Guid.NewGuid(), OrderNo = "SO-202601-0003", CustomerId = c3, CustomerName = "Fabrikam Works", OrderedAt = DateTime.Today.AddDays(-1), Amount = 332000m, Status = OrderStatus.Closed }
        ];
    }
}
