using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseReport.Input.Mapping;
using WarehouseReport.Tests.Mocks;

namespace WarehouseReport.Tests.Input.Mapping
{
    [TestClass]
    public class WarehouseItemInfoListMapperTests
    {
        private WarehouseItemInfoListMapper _systemUnderTests;

        [TestInitialize]
        public void SetUp()
        {
            var mapperMock = new WarehouseItemInfoMapperMock();
            var mapper = mapperMock.Create();

            _systemUnderTests = new WarehouseItemInfoListMapper(mapper);
        }

        [TestMethod]
        [DataRow("WH-A,10|Test,12", "WH-A", 10, "Test", 12)]
        [DataRow("WareHouseName,15|WareHouseName,15", "WareHouseName", 15, "WareHouseName", 15)]
        public void GIVEN_an_input_string_separated_by_pipeline_WHEN_map_method_is_invoked_THEN_a_collection_of_objects_with_correct_values_has_to_be_created(
            string input,
            string warehouseName1,
            int quantity1,
            string warehouseName2,
            int quantity2
        )
        {
            var actual = _systemUnderTests.Map(input)?.ToArray();

            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(quantity1, actual[0].Quantity);
            Assert.AreEqual(warehouseName1, actual[0].WareHouseName);
            Assert.AreEqual(quantity2, actual[1].Quantity);
            Assert.AreEqual(warehouseName2, actual[1].WareHouseName);
        }
    }
}
