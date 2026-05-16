using ModernBusinessWpfSample.Domain.Orders;

namespace ModernBusinessWpfSample.Application.Abstractions;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Order order, CancellationToken cancellationToken);
}
