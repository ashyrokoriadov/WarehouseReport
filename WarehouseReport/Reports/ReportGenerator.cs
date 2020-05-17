namespace WarehouseReport.Reports
{
    public abstract class ReportGenerator<T> : IReportGenerator<T>
    {
        protected T Data { get; set; }

        public string GenerateReport(T data)
        {
            PrepareData(data);
            return FormatReport();
        }

        public T PrepareData(T data)
        {
            Data = data;
            OrderEntities();
            OrderSubEntities();
            return Data;
        }

        protected abstract string FormatReport();
        protected abstract void OrderEntities();
        protected abstract void OrderSubEntities();
    }
}
