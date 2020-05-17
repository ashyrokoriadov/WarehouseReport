using System.Collections.Generic;
using System.Linq;
using WarehouseReport.Models;
using WarehouseReport.Models.Dto;
using WarehouseReport.Models.Extensions;

namespace WarehouseReport.Repos
{
    public class WarehouseRepo : IRepo<Warehouse, WarehouseItemDto>
    {
        private readonly HashSet<Warehouse> _warehouses = new HashSet<Warehouse>();

        public bool Add(Warehouse warehouse) => _warehouses.Add(warehouse);

        public bool Exists(string warehouseName) => _warehouses.SingleOrDefault(w => w.Name == warehouseName) != null;

        public IEnumerable<Warehouse> GetAll() => _warehouses;

        //Upsert = Update or insert
        public void Upsert(string warehouseName, WarehouseItemDto item)
        {
            var warehouse = Get(warehouseName);

            if (warehouse == null)
                return;

            var quantity = GetItemQuantityInWarehouse(warehouseName, item);

            if (Exists(warehouse.Name, item.Id))
                warehouse.Items[item.ToWarehouseItem()] += quantity;
            else
                warehouse.Items.Add(item.ToWarehouseItem(), quantity);
        }

        private Warehouse Get(string warehouseName) => _warehouses.FirstOrDefault(w => w.Name == warehouseName);

        private bool Exists(string warehouseName, string itemName)
        {
            var warehouse = _warehouses.SingleOrDefault(w => w.Name == warehouseName);
            return warehouse?.Items.Keys.SingleOrDefault(item => item.Name == itemName) != null;
        }

        private int GetItemQuantityInWarehouse(string warehouseName, WarehouseItemDto item) =>
            item.WarehouseItemInfo.SingleOrDefault(i => i.WareHouseName == warehouseName)?.Quantity ?? 0;
    }
}
