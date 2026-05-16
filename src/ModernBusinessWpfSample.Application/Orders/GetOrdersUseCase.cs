using ModernBusinessWpfSample.Application.Abstractions;

namespace ModernBusinessWpfSample.Application.Orders;

public sealed class GetOrdersUseCase(IOrderRepository repository)
{
    public async Task<IReadOnlyList<OrderDto>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var orders = await repository.GetAllAsync(cancellationToken);
        return orders
            .OrderByDescending(x => x.OrderedAt)
            .Select(x => new OrderDto(x.Id, x.OrderNo, x.CustomerName, x.OrderedAt, x.Amount, x.Status))
            .ToList();
    }
}
