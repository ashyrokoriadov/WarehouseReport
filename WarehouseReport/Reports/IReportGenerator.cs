namespace WarehouseReport.Reports
{
    public interface IReportGenerator<T>
    {
        string GenerateReport(T data);

        T PrepareData(T data);
    }
}
