using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseReport.Models;
using WarehouseReport.Models.Dto;
using WarehouseReport.Models.Extensions;

namespace WarehouseReport.Tests.Models.Extensions
{
    [TestClass]
    public class WarehouseItemDtoExtensionsTests
    {
        private readonly Fixture _fixture = new Fixture();
        private WarehouseItemDto _systemUnderTests;
        private string _nameFixture;
        private string _idFixture;

        [TestInitialize]
        public void SetUp()
        {
            _nameFixture = _fixture.Create <string>();
            _idFixture = _fixture.Create<string>();

            _systemUnderTests = new WarehouseItemDto()
            {
                Id = _idFixture,
                Name = _nameFixture,
                WarehouseItemInfo = new []
                {
                    new WarehouseItemInfo()
                    {
                        Quantity = _fixture.Create<int>(),
                        WareHouseName = _fixture.Create<string>()
                    },
                    new WarehouseItemInfo()
                    {
                        Quantity = _fixture.Create<int>(),
                        WareHouseName = _fixture.Create<string>()
                    },
                }
            };
        }

        [TestMethod]
        public void GIVEN_a_dto_object_WHEN_to_method_is_invoked_THEN_an_object_with_correct_values_has_to_be_created()
        {
            var expected = new WarehouseItem()
            {
                Id = _idFixture,
                Name = _nameFixture
            };

            var actual = _systemUnderTests.ToWarehouseItem();

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}
