using ModernBusinessWpfSample.Domain.Orders;

namespace ModernBusinessWpfSample.Application.Orders;

public sealed record OrderDto(Guid Id, string OrderNo, string CustomerName, DateTime OrderedAt, decimal Amount, OrderStatus Status);
