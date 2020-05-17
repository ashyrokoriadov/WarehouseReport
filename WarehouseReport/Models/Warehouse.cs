using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WarehouseReport.Models
{
    public class Warehouse
    {
        public string Name { get; set; }

        public IDictionary<WarehouseItem, int> Items { get; set; } = new Dictionary<WarehouseItem, int>();

        public int Total => Items.Values.Sum();

        public override string ToString()
        {
           var sb = new StringBuilder();
           sb.AppendLine($"{Name} (total {Total})");
           foreach (var item in Items)
           {
               sb.AppendLine($"{item.Key.Id}: {item.Value}");
           }
           return sb.ToString();
        }
    }
}
