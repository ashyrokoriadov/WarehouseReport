using System.Collections.Generic;
using System.Linq;
using WarehouseReport.Models;

namespace WarehouseReport.Input.Mapping
{
    public class WarehouseItemInfoListMapper : InputMapper<IEnumerable<WarehouseItemInfo>>
    {
        private readonly IMapper<string, WarehouseItemInfo> _mapper;

        public WarehouseItemInfoListMapper(IMapper<string, WarehouseItemInfo> mapper)
        {
            _mapper = mapper;
        }

        protected override IEnumerable<WarehouseItemInfo> Map() => SplitInput.Select(input => _mapper.Map(input)).ToList();

        protected override void Split(string input) => SplitInput = input.Split('|');
    }
}
