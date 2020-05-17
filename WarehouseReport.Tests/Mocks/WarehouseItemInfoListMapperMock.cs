using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WarehouseReport.Input.Mapping;
using WarehouseReport.Models;

namespace WarehouseReport.Tests.Mocks
{
    class WarehouseItemInfoListMapperMock
    {
        public IMapper<string, IEnumerable<WarehouseItemInfo>> Create()
        {
            var mock = new Mock<IMapper<string, IEnumerable<WarehouseItemInfo>>>();

            mock.Setup(mapper => mapper.Map(It.IsAny<string>())).Returns(new List<WarehouseItemInfo>()
            {
                new WarehouseItemInfo ()
                {
                    Quantity = 15,
                    WareHouseName = "WH1"
                },
                new WarehouseItemInfo ()
                {
                    Quantity = 20,
                    WareHouseName = "WH2"
                }
            });

            mock.Setup(mapper => mapper.Map("WH-A,10|Test,12")).Returns(new List<WarehouseItemInfo>()
            {
                new WarehouseItemInfo ()
                {
                    Quantity = 10,
                    WareHouseName = "WH-A"
                },
                new WarehouseItemInfo ()
                {
                    Quantity = 12,
                    WareHouseName = "Test"
                }
            });

            mock.Setup(mapper => mapper.Map("WH-B,5|Test,1")).Returns(new List<WarehouseItemInfo>()
            {
                new WarehouseItemInfo ()
                {
                    Quantity = 5,
                    WareHouseName = "WH-B"
                },
                new WarehouseItemInfo ()
                {
                    Quantity = 1,
                    WareHouseName = "Test"
                }
            });

            return mock.Object;
        }
    }
}
