using System.Text;
using ModernBusinessWpfSample.Application.Abstractions;

namespace ModernBusinessWpfSample.Infrastructure.Reports;

internal sealed class CsvReportExporter(IOrderRepository orders) : IReportExporter
{
    public async Task<string> ExportOrdersCsvAsync(CancellationToken cancellationToken)
    {
        var orderList = await orders.GetAllAsync(cancellationToken);
        var directory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            "ModernBusinessWpfSampleReports");
        Directory.CreateDirectory(directory);

        var path = Path.Combine(directory, $"orders-{DateTime.Now:yyyyMMdd-HHmmss}.csv");
        var sb = new StringBuilder();
        sb.AppendLine("OrderNo,Customer,OrderedAt,Amount,Status");
        foreach (var order in orderList.OrderByDescending(x => x.OrderedAt))
        {
            sb.AppendLine($"{Escape(order.OrderNo)},{Escape(order.CustomerName)},{order.OrderedAt:yyyy-MM-dd},{order.Amount},{order.Status}");
        }
        await File.WriteAllTextAsync(path, sb.ToString(), Encoding.UTF8, cancellationToken);
        return path;
    }

    private static string Escape(string value) => $"\"{value.Replace("\"", "\"\"")}\"";
}
