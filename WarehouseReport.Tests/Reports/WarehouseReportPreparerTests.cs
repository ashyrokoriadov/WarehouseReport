using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseReport.Models;
using WarehouseReport.Reports;

namespace WarehouseReport.Tests.Reports
{
    [TestClass]
    public class WarehouseReportPreparerTests
    {
        private WarehouseReportGenerator _systemUnderTests;
        private IList<Warehouse> _warehouses;

        [TestInitialize]
        public void SetUp()
        {
            _systemUnderTests = new WarehouseReportGenerator();
            PopulateWarehouseCollection();
        }

        [TestMethod]
        public void GIVEN_a_warehouse_collection_and_report_preparer_WHEN_prepare_method_is_invoked_THEN_warehouse_collection_has_to_be_sorted_and_grouped()
        {
            var sortedWareHouses = _systemUnderTests.PrepareData(_warehouses).ToArray();

            Assert.AreEqual("A", sortedWareHouses[0].Name);
            Assert.AreEqual("C", sortedWareHouses[1].Name);
            Assert.AreEqual("B", sortedWareHouses[2].Name);

            var warehouseOneItems = sortedWareHouses[0].Items.Keys.ToArray();
            Assert.AreEqual("abc", warehouseOneItems[0].Id);
            Assert.AreEqual("def", warehouseOneItems[1].Id);
            Assert.AreEqual("ghi", warehouseOneItems[2].Id);

            var warehouseTwoItems = sortedWareHouses[1].Items.Keys.ToArray();
            Assert.AreEqual("abc", warehouseTwoItems[0].Id);
            Assert.AreEqual("def", warehouseTwoItems[1].Id);
            Assert.AreEqual("ghi", warehouseTwoItems[2].Id);

            var warehouseThreeItems = sortedWareHouses[2].Items.Keys.ToArray();
            Assert.AreEqual("abc", warehouseThreeItems[0].Id);
            Assert.AreEqual("def", warehouseThreeItems[1].Id);
            Assert.AreEqual("ghi", warehouseThreeItems[2].Id);
        }

        private void PopulateWarehouseCollection()
        {
            _warehouses = new List<Warehouse>
            {
                new Warehouse()
                {
                    Name = "A",
                    Items = new Dictionary<WarehouseItem, int>
                    {
                        {
                            new WarehouseItem()
                            {
                                Id = "abc",
                                Name = "Item 1"
                            },
                            15
                        },
                        {
                            new WarehouseItem()
                            {
                                Id = "def",
                                Name = "Item 3"
                            },
                            35
                        },
                        {
                            new WarehouseItem()
                            {
                                Id = "ghi",
                                Name = "Item 2"
                            },
                            25
                        }
                    }

                },
                new Warehouse()
                {
                    Name = "C",
                    Items = new Dictionary<WarehouseItem, int>
                    {
                        {
                            new WarehouseItem()
                            {
                                Id = "ghi",
                                Name = "Item 3"
                            },
                            30
                        },
                        {
                            new WarehouseItem()
                            {
                                Id = "def",
                                Name = "Item 2"
                            },
                            20
                        },
                        {
                            new WarehouseItem()
                            {
                                Id = "abc",
                                Name = "Item 1"
                            },
                            10
                        },
                    }
                },
                new Warehouse()
                {
                    Name = "B",
                    Items = new Dictionary<WarehouseItem, int>
                    {
                        {
                            new WarehouseItem()
                            {
                                Id = "def",
                                Name = "Item 3"
                            },
                            30
                        },
                        {
                            new WarehouseItem()
                            {
                                Id = "abc",
                                Name = "Item 1"
                            },
                            10
                        },
                        {
                            new WarehouseItem()
                            {
                                Id = "ghi",
                                Name = "Item 2"
                            },
                            20
                        },
                    }
                }
            };
        }
    }
}
