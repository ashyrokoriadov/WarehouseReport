using Moq;
using WarehouseReport.Input.Mapping;
using WarehouseReport.Models;

namespace WarehouseReport.Tests.Mocks
{
    class WarehouseItemInfoMapperMock
    {
        public IMapper<string, WarehouseItemInfo> Create()
        {
            var mock = new Mock<IMapper<string, WarehouseItemInfo>>();

            mock.Setup(mapper => mapper.Map(It.IsAny<string>())).Returns(new WarehouseItemInfo()
            {
                Quantity = 15,
                WareHouseName = "WareHouseName"
            });

            mock.Setup(mapper => mapper.Map("WH-A,10")).Returns(new WarehouseItemInfo()
            {
                Quantity = 10,
                WareHouseName = "WH-A"
            });

            mock.Setup(mapper => mapper.Map("Test,12")).Returns(new WarehouseItemInfo()
            {
                Quantity = 12,
                WareHouseName = "Test"
            });

            return mock.Object;
        }
    }
}
