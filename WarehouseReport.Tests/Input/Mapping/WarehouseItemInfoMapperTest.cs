using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseReport.Input.Mapping;
using WarehouseReport.Models;

namespace WarehouseReport.Tests.Input.Mapping
{
    [TestClass]
    public class WarehouseItemInfoMapperTest
    {
        private WarehouseItemInfoMapper _systemUnderTests;

        [TestInitialize]
        public void SetUp()
        {
            _systemUnderTests = new WarehouseItemInfoMapper();
        }

        [TestMethod]
        [DataRow("WH-A,10", "WH-A", 10)]
        [DataRow("Test,12", "Test", 12)]
        public void GIVEN_an_input_string_separated_by_coma_WHEN_map_method_is_invoked_THEN_an_object_with_correct_values_has_to_be_created(
            string input,
            string warehouseName,
            int quantity
            )
        {
            var expected = new WarehouseItemInfo()
            {
                Quantity = quantity,
                WareHouseName = warehouseName
            };

            var actual = _systemUnderTests.Map(input);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Quantity, actual.Quantity);
            Assert.AreEqual(expected.WareHouseName, actual.WareHouseName);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}
