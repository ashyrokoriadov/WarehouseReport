using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseReport.Models;
using WarehouseReport.Models.Dto;
using WarehouseReport.Repos;

namespace WarehouseReport.Tests.Repos
{
    [TestClass]
    public class WarehouseRepoTests
    {
        private readonly Fixture _fixture = new Fixture();
        private WarehouseRepo _systemUnderTests;
        private Warehouse _warehouseWithNoItems1;
        private Warehouse _warehouseWithNoItems2;

        [TestInitialize]
        public void SetUp()
        {
            _systemUnderTests = new WarehouseRepo();

            _warehouseWithNoItems1 = new Warehouse()
            {
                Name = _fixture.Create<string>()
            };

            _warehouseWithNoItems2 = new Warehouse()
            {
                Name = _fixture.Create<string>()
            };
        }

        [TestMethod]
        public void GIVEN_a_warehouse_repo_WHEN_add_method_is_invoked_THEN_an_object_has_to_be_added_to_repo()
        {
            var addResult = _systemUnderTests.Add(_warehouseWithNoItems1);
            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void GIVEN_a_warehouse_repo_WHEN_add_method_is_invoked_with_an_object_that_already_exists_in_a_repo_THEN_an_object_has_not_to_be_added_to_repo()
        {
            var firstAddResult = _systemUnderTests.Add(_warehouseWithNoItems1);
            var secondAddResult = _systemUnderTests.Add(_warehouseWithNoItems1);

            Assert.IsTrue(firstAddResult);
            Assert.IsFalse(secondAddResult);
        }

        [TestMethod]
        public void GIVEN_a_warehouse_repo_WHEN_exist_method_is_invoked_with_a_name_of_wh_that_already_exists_in_a_repo_THEN_true_value_has_to_be_returned()
        {
            var addResult = _systemUnderTests.Add(_warehouseWithNoItems1);
            var actualExists = _systemUnderTests.Exists(_warehouseWithNoItems1.Name);

            Assert.IsTrue(addResult);
            Assert.IsTrue(actualExists);
        }

        [TestMethod]
        public void GIVEN_a_warehouse_repo_WHEN_exist_method_is_invoked_with_a_name_of_wh_that_does_not_exists_in_a_repo_THEN_false_value_has_to_be_returned()
        {
            var actualExists = _systemUnderTests.Exists(_warehouseWithNoItems1.Name);
            Assert.IsFalse(actualExists);
        }

        [TestMethod]
        public void GIVEN_a_warehouse_repo_WHEN_get_all_method_is_invoked_THEN_all_values_have_to_be_returned()
        {
            var firstAddResult = _systemUnderTests.Add(_warehouseWithNoItems1);
            var secondAddResult = _systemUnderTests.Add(_warehouseWithNoItems2);
            var warehouses = _systemUnderTests.GetAll();
            var actualWarehouses = warehouses.ToArray();

            Assert.IsTrue(firstAddResult);
            Assert.IsTrue(secondAddResult);
            Assert.AreEqual(2, actualWarehouses.Length);
            Assert.AreEqual(_warehouseWithNoItems1.Name, actualWarehouses[0].Name);
            Assert.AreEqual(_warehouseWithNoItems2.Name, actualWarehouses[1].Name);
        }

        [TestMethod]
        public void GIVEN_an_empty_warehouse_repo_WHEN_get_all_method_is_invoked_THEN_empty_enumerable_has_to_be_returned()
        {
            var warehouses = _systemUnderTests.GetAll();
            var actualWarehouses = warehouses.ToArray();

            Assert.AreEqual(0, actualWarehouses.Length);
        }

        [TestMethod]
        public void GIVEN_a_warehouse_repo_WHEN_upsert_method_is_invoked_with_an_item_that_is_not_in_warehouse_THEN_new_item_is_added_to_a_warehouse()
        {
            var warehouseName = _fixture.Create<string>();
            var warehouse = new Warehouse()
            {
                Name = warehouseName
            };

            var warehouseItemName = _fixture.Create<string>();
            var quantity = _fixture.Create<int>();
            var warehouseItemDto = new WarehouseItemDto()
            {
                Id = _fixture.Create<string>(),
                Name = warehouseItemName,
                WarehouseItemInfo = new List<WarehouseItemInfo>()
                {
                    new WarehouseItemInfo()
                    {
                        Quantity = quantity,
                        WareHouseName = warehouseName
                    }
                }
            };

            var addResult = _systemUnderTests.Add(warehouse);
            _systemUnderTests.Upsert(warehouseName, warehouseItemDto);
            var warehouses = _systemUnderTests.GetAll();
            var actualWarehouses = warehouses.ToArray();

            Assert.IsTrue(addResult);
            Assert.AreEqual(1, actualWarehouses.Length);
            Assert.AreEqual(quantity, actualWarehouses[0].Items.First(x => x.Key.Name == warehouseItemName).Value);
        }

        [TestMethod]
        public void GIVEN_a_warehouse_repo_WHEN_upsert_method_is_invoked_with_an_item_that_is_already_in_warehouse_THEN_current_item_quantity_has_to_be_updated()
        {
            var warehouseName = _fixture.Create<string>();
            var warehouse = new Warehouse()
            {
                Name = warehouseName
            };

            var warehouseItemName = _fixture.Create<string>();
            var quantity1 = _fixture.Create<int>();
            var warehouseItemDto1 = new WarehouseItemDto()
            {
                Id = _fixture.Create<string>(),
                Name = warehouseItemName,
                WarehouseItemInfo = new List<WarehouseItemInfo>()
                {
                    new WarehouseItemInfo()
                    {
                        Quantity = quantity1,
                        WareHouseName = warehouseName
                    }
                }
            };

            var quantity2 = _fixture.Create<int>();
            var warehouseItemDto2 = new WarehouseItemDto()
            {
                Id = _fixture.Create<string>(),
                Name = warehouseItemName,
                WarehouseItemInfo = new List<WarehouseItemInfo>()
                {
                    new WarehouseItemInfo()
                    {
                        Quantity = quantity2,
                        WareHouseName = warehouseName
                    }
                }
            };

            var addResult = _systemUnderTests.Add(warehouse);
            _systemUnderTests.Upsert(warehouseName, warehouseItemDto1);
            _systemUnderTests.Upsert(warehouseName, warehouseItemDto2);
            var warehouses = _systemUnderTests.GetAll();
            var actualWarehouses = warehouses.ToArray();

            Assert.IsTrue(addResult);
            Assert.AreEqual(1, actualWarehouses.Length);
            Assert.AreEqual(quantity1+quantity2, actualWarehouses[0].Items
                .Where(x => x.Key.Name == warehouseItemName)
                .Sum(x => x.Value)
            );
        }
    }
}
