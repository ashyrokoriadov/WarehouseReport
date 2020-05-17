namespace WarehouseReport.Models
{
    public class WarehouseItem
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"[{nameof(Id)}: {Id}; {nameof(Name)}: {Name};]";
    }
}
