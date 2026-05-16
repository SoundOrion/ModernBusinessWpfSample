namespace ModernBusinessWpfSample.Application.Dashboard;

public sealed record DashboardSummary(
    int ActiveCustomers,
    int MonthlyOrders,
    decimal MonthlySales,
    string ApiHealth);
