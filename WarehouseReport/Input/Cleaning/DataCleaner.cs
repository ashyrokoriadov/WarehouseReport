using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WarehouseReport.Input.Cleaning
{
    public class DataCleaner : IDataCleaner
    {
        public IEnumerable<string> Clean(IEnumerable<string> data)
        {
            return data.Where(s => !ShouldIgnore(s));
        }

        private bool ShouldIgnore(string dataItem)
        {
            var pattern = "^#{1}.*$";
            return Regex.IsMatch(dataItem, pattern);
        }
    }
}
