using WarehouseReport.Models.Dto;

namespace WarehouseReport.Models.Extensions
{
    public static class WarehouseItemDtoExtensions
    {
        public static WarehouseItem ToWarehouseItem(this WarehouseItemDto dto)
        {
            return new WarehouseItem()
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
