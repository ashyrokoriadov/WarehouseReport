using System.Collections.Generic;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseReport.Models;

namespace WarehouseReport.Tests.Models
{
    [TestClass]
    public class WarehouseTests
    {
        private readonly Fixture _fixture = new Fixture();
        private Warehouse _systemUnderTests;
        private int _quantityFixture1;
        private int _quantityFixture2;

        [TestInitialize]
        public void SetUp()
        {
            _quantityFixture1 = _fixture.Create<int>();
            _quantityFixture2 = _fixture.Create<int>();

            _systemUnderTests = new Warehouse()
            {
                Name = _fixture.Create<string>(),
                Items = new Dictionary<WarehouseItem, int>
                {
                    {
                        new WarehouseItem()
                        {
                            Id = _fixture.Create<string>(),
                            Name = _fixture.Create<string>()
                        },
                        _quantityFixture1
                    },
                    {
                        new WarehouseItem()
                        {
                            Id = _fixture.Create<string>(),
                            Name = _fixture.Create<string>()
                        },
                        _quantityFixture2
                    }
                }
            };
        }

        [TestMethod]
        public void GIVEN_a_warehouse_WHEN_a_getter_on_property_total_is_invoked_THEN_total_quantity_of_items_has_to_be_returned()
        {
            var actual = _systemUnderTests.Total;
            Assert.AreEqual(_quantityFixture1 + _quantityFixture2, actual);
        }
    }
}
