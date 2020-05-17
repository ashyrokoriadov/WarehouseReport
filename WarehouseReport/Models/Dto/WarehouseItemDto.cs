using System.Collections.Generic;
using System.Text;

namespace WarehouseReport.Models.Dto
{
    public class WarehouseItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<WarehouseItemInfo> WarehouseItemInfo { get; set; }

        public static WarehouseItemDto Empty() => new WarehouseItemDto();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{nameof(Id)}: {Id}; {nameof(Name)}: {Name};");
            sb.AppendLine($"{nameof(WarehouseItemInfo)}:");
            foreach (var info in WarehouseItemInfo)
            {
                sb.AppendLine(info.ToString());
            }

            return sb.ToString();
        }
    }
}
