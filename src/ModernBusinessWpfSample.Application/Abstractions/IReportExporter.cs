namespace ModernBusinessWpfSample.Application.Abstractions;

public interface IReportExporter
{
    Task<string> ExportOrdersCsvAsync(CancellationToken cancellationToken);
}
