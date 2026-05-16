using ModernBusinessWpfSample.Application.Abstractions;
using ModernBusinessWpfSample.Domain.Common;
using ModernBusinessWpfSample.Domain.Orders;

namespace ModernBusinessWpfSample.Application.Orders;

public sealed record RegisterOrderCommand(Guid CustomerId, decimal Amount);

public sealed class RegisterOrderUseCase(
    ICustomerRepository customerRepository,
    IOrderRepository orderRepository,
    IClock clock)
{
    public async Task ExecuteAsync(RegisterOrderCommand command, CancellationToken cancellationToken)
    {
        if (command.Amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        var customers = await customerRepository.GetAllAsync(cancellationToken);
        var customer = customers.FirstOrDefault(x => x.Id == command.CustomerId)
            ?? throw new InvalidOperationException("Customer was not found.");

        var existing = await orderRepository.GetAllAsync(cancellationToken);
        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderNo = $"SO-{clock.Now:yyyyMM}-{existing.Count + 1:0000}",
            CustomerId = customer.Id,
            CustomerName = customer.Name,
            OrderedAt = clock.Now,
            Amount = command.Amount,
            Status = OrderStatus.Confirmed
        };
        await orderRepository.AddAsync(order, cancellationToken);
    }
}
