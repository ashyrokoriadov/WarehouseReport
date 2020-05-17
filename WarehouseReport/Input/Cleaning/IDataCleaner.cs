using System.Collections.Generic;

namespace WarehouseReport.Input.Cleaning
{
    public interface IDataCleaner
    {
        IEnumerable<string> Clean(IEnumerable<string> data);
    }
}
