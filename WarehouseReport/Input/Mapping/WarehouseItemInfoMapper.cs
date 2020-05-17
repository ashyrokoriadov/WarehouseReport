using System;
using WarehouseReport.Models;

namespace WarehouseReport.Input.Mapping
{
    public class WarehouseItemInfoMapper : InputMapper<WarehouseItemInfo>
    {
        protected override WarehouseItemInfo Map() 
            => new WarehouseItemInfo()
            {
                WareHouseName = SplitInput[0],
                Quantity = Convert.ToInt32(SplitInput[1])
            };

        protected override void Split(string input) => SplitInput = input.Split(',');
    }
}
