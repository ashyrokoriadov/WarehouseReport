using System.Collections.Generic;
using WarehouseReport.Models;
using WarehouseReport.Models.Dto;

namespace WarehouseReport.Input.Mapping
{
    public class WarehouseItemDtoMapper : InputMapper<WarehouseItemDto>
    {
        private readonly IMapper<string, IEnumerable<WarehouseItemInfo>> _infoListMapper;

        public WarehouseItemDtoMapper(IMapper<string, IEnumerable<WarehouseItemInfo>> infoListMapper)
        {
            _infoListMapper = infoListMapper;
        }

        protected override WarehouseItemDto Map()
            => new WarehouseItemDto()
            {
                Name = SplitInput[0],
                Id = SplitInput[1],
                WarehouseItemInfo = _infoListMapper.Map(SplitInput[2])
            };

        protected override void Split(string input) => SplitInput = input.Split(';');

        protected override bool ValidateSplitInput() => SplitInput.Length == 3;
    }
}
