using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseReport.Input.Mapping;
using WarehouseReport.Tests.Mocks;

namespace WarehouseReport.Tests.Input.Mapping
{
    [TestClass]
    public class WarehouseItemDtoMapperTests
    {
        private WarehouseItemDtoMapper _systemUnderTests;

        [TestInitialize]
        public void SetUp()
        {
            var mapperMock = new WarehouseItemInfoListMapperMock();
            var mapper = mapperMock.Create();

            _systemUnderTests = new WarehouseItemDtoMapper(mapper);
        }

        [TestMethod]
        [DataRow("Warehouse item 1;item1;WH-A,10|Test,12", "Warehouse item 1", "item1", "WH-A", 10, "Test", 12)]
        [DataRow("Warehouse item 2;item2;WH-B,5|Test,1", "Warehouse item 2", "item2", "WH-B", 5, "Test", 1)]
        [DataRow("Warehouse item 3;item3;WH1,15|WH2,20", "Warehouse item 3", "item3", "WH1", 15, "WH2", 20)]
        public void GIVEN_an_input_string_separated_by_semicolon_WHEN_map_method_is_invoked_THEN_an_objects_with_correct_values_has_to_be_created(
            string input,
            string itemName,
            string itemId,
            string warehouseName1,
            int quantity1,
            string warehouseName2,
            int quantity2
        )
        {
            var actual = _systemUnderTests.Map(input);

            Assert.IsNotNull(actual);
            Assert.IsNotNull(itemName, actual.Name);
            Assert.IsNotNull(itemId, actual.Id);

            var actualItemInfo = actual.WarehouseItemInfo.ToArray();

            Assert.AreEqual(quantity1, actualItemInfo[0].Quantity);
            Assert.AreEqual(warehouseName1, actualItemInfo[0].WareHouseName);
            Assert.AreEqual(quantity2, actualItemInfo[1].Quantity);
            Assert.AreEqual(warehouseName2, actualItemInfo[1].WareHouseName);
        }
    }
}
