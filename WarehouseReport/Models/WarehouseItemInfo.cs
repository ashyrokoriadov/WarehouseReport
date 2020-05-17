namespace WarehouseReport.Models
{
    public class WarehouseItemInfo
    {
        public string WareHouseName { get; set; }
        public int Quantity { get; set; }

        public override string ToString() => $"[{nameof(WareHouseName)}: {WareHouseName}; {nameof(Quantity)}: {Quantity}; ]";
    }
}
