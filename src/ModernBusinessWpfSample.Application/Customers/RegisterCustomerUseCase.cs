using ModernBusinessWpfSample.Application.Abstractions;
using ModernBusinessWpfSample.Domain.Customers;

namespace ModernBusinessWpfSample.Application.Customers;

public sealed record RegisterCustomerCommand(string Name, string Email, string Segment);

public sealed class RegisterCustomerUseCase(ICustomerRepository repository)
{
    public async Task ExecuteAsync(RegisterCustomerCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Name))
            throw new ArgumentException("Customer name is required.");

        var existing = await repository.GetAllAsync(cancellationToken);
        var nextNumber = existing.Count + 1;
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Code = $"C{nextNumber:0000}",
            Name = command.Name.Trim(),
            Email = command.Email.Trim(),
            Segment = string.IsNullOrWhiteSpace(command.Segment) ? "Standard" : command.Segment.Trim(),
            IsActive = true
        };
        await repository.AddAsync(customer, cancellationToken);
    }
}
