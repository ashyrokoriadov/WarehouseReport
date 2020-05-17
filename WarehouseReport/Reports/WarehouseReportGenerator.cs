using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarehouseReport.Models;

namespace WarehouseReport.Reports
{
    public class WarehouseReportGenerator : ReportGenerator<IEnumerable<Warehouse>>
    {
        protected override void OrderEntities() => OrderWarehouses();

        protected override void OrderSubEntities() => OrderItems();

        protected override string FormatReport()
        {
            var sb = new StringBuilder();

            foreach (var warehouse in Data)
            {
                sb.AppendLine($"{warehouse.Name} (total {warehouse.Total})");
                foreach (var item in warehouse.Items)
                {
                    sb.AppendLine($"{item.Key.Id}: {item.Value}");
                }
                sb.AppendLine("");
            }

            return sb.ToString();
        }

        private void OrderWarehouses()
            => Data = Data.OrderByDescending(w => w.Total)
                .ThenByDescending(w => w.Name);

        private void OrderItems()
        {
            foreach (var warehouse in Data)
            {
                warehouse.Items = warehouse.Items
                    .OrderBy(i => i.Key.Id)
                    .ToDictionary(i => i.Key, j => j.Value);
            }
        }
    }
}
