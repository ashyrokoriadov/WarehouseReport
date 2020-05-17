using System.Collections.Generic;
using WarehouseReport.Models;
using WarehouseReport.Models.Dto;
using WarehouseReport.Models.Extensions;

namespace WarehouseReport.Repos
{
    public class WarehouseRepoFiller : IRepoFiller<WarehouseItemDto>
    {
        private readonly IRepo<Warehouse, WarehouseItemDto> _warehouseRepo;

        public WarehouseRepoFiller(IRepo<Warehouse, WarehouseItemDto> repo)
        {
            _warehouseRepo = repo;
        }

        public void Fill(IEnumerable<WarehouseItemDto> items)
        {
            foreach (var item in items)
            {
                foreach (var itemInfo in item.WarehouseItemInfo)
                {
                    if (_warehouseRepo.Exists(itemInfo.WareHouseName))
                    {
                        _warehouseRepo.Upsert(itemInfo.WareHouseName, item);
                    }
                    else
                    {
                        var warehouse = new Warehouse() { Name = itemInfo.WareHouseName };
                        warehouse.Items.Add(item.ToWarehouseItem(), itemInfo.Quantity);
                        _warehouseRepo.Add(warehouse);
                    }
                }
            }
        }
    }
}
