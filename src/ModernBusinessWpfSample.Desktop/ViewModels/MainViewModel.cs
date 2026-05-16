using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernBusinessWpfSample.Application.Customers;
using ModernBusinessWpfSample.Application.Dashboard;
using ModernBusinessWpfSample.Application.Orders;
using ModernBusinessWpfSample.Application.Reports;

namespace ModernBusinessWpfSample.Desktop.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly GetDashboardSummaryUseCase _getDashboard;
    private readonly GetCustomersUseCase _getCustomers;
    private readonly RegisterCustomerUseCase _registerCustomer;
    private readonly GetOrdersUseCase _getOrders;
    private readonly RegisterOrderUseCase _registerOrder;
    private readonly ExportOrdersReportUseCase _exportOrders;

    public ObservableCollection<string> Sections { get; } = ["Dashboard", "Customers", "Orders", "Reports", "Settings"];
    public ObservableCollection<CustomerDto> Customers { get; } = [];
    public ObservableCollection<OrderDto> Orders { get; } = [];

    [ObservableProperty] private string selectedSection = "Dashboard";
    [ObservableProperty] private int activeCustomers;
    [ObservableProperty] private int monthlyOrders;
    [ObservableProperty] private decimal monthlySales;
    [ObservableProperty] private string apiHealth = "Not checked";
    [ObservableProperty] private string statusMessage = "Ready.";
    [ObservableProperty] private string reportMessage = string.Empty;

    [ObservableProperty] private string newCustomerName = "Sample Customer";
    [ObservableProperty] private string newCustomerEmail = "customer@example.com";
    [ObservableProperty] private string newCustomerSegment = "Standard";

    [ObservableProperty] private CustomerDto? selectedCustomer;
    [ObservableProperty] private string newOrderAmount = "100000";

    public MainViewModel(
        GetDashboardSummaryUseCase getDashboard,
        GetCustomersUseCase getCustomers,
        RegisterCustomerUseCase registerCustomer,
        GetOrdersUseCase getOrders,
        RegisterOrderUseCase registerOrder,
        ExportOrdersReportUseCase exportOrders)
    {
        _getDashboard = getDashboard;
        _getCustomers = getCustomers;
        _registerCustomer = registerCustomer;
        _getOrders = getOrders;
        _registerOrder = registerOrder;
        _exportOrders = exportOrders;

        _ = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        await LoadCustomersAsync();
        await LoadOrdersAsync();
        await RefreshDashboardAsync();
    }

    [RelayCommand]
    private async Task RefreshDashboardAsync()
    {
        try
        {
            var summary = await _getDashboard.ExecuteAsync(CancellationToken.None);
            ActiveCustomers = summary.ActiveCustomers;
            MonthlyOrders = summary.MonthlyOrders;
            MonthlySales = summary.MonthlySales;
            ApiHealth = summary.ApiHealth;
            StatusMessage = "Dashboard updated.";
        }
        catch (Exception ex)
        {
            StatusMessage = ex.Message;
        }
    }

    [RelayCommand]
    private async Task LoadCustomersAsync()
    {
        Customers.Clear();
        foreach (var customer in await _getCustomers.ExecuteAsync(CancellationToken.None))
            Customers.Add(customer);
        SelectedCustomer ??= Customers.FirstOrDefault();
        StatusMessage = "Customers loaded.";
    }

    [RelayCommand]
    private async Task AddCustomerAsync()
    {
        try
        {
            await _registerCustomer.ExecuteAsync(
                new RegisterCustomerCommand(NewCustomerName, NewCustomerEmail, NewCustomerSegment),
                CancellationToken.None);
            await LoadCustomersAsync();
            await RefreshDashboardAsync();
            StatusMessage = "Customer added.";
        }
        catch (Exception ex)
        {
            StatusMessage = ex.Message;
        }
    }

    [RelayCommand]
    private async Task LoadOrdersAsync()
    {
        Orders.Clear();
        foreach (var order in await _getOrders.ExecuteAsync(CancellationToken.None))
            Orders.Add(order);
        StatusMessage = "Orders loaded.";
    }

    [RelayCommand]
    private async Task AddOrderAsync()
    {
        try
        {
            if (SelectedCustomer is null)
                throw new InvalidOperationException("Select a customer.");
            if (!decimal.TryParse(NewOrderAmount, out var amount))
                throw new InvalidOperationException("Amount must be numeric.");

            await _registerOrder.ExecuteAsync(
                new RegisterOrderCommand(SelectedCustomer.Id, amount),
                CancellationToken.None);
            await LoadOrdersAsync();
            await RefreshDashboardAsync();
            StatusMessage = "Order added.";
        }
        catch (Exception ex)
        {
            StatusMessage = ex.Message;
        }
    }

    [RelayCommand]
    private async Task ExportOrdersReportAsync()
    {
        try
        {
            var path = await _exportOrders.ExecuteAsync(CancellationToken.None);
            ReportMessage = $"Exported: {path}";
            StatusMessage = "Report exported.";
        }
        catch (Exception ex)
        {
            ReportMessage = ex.Message;
            StatusMessage = ex.Message;
        }
    }
}
