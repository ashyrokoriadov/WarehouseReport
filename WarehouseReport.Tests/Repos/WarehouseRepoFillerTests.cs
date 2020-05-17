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
    public class WarehouseRepoFillerTests
    {
        private readonly Fixture _fixture = new Fixture();

        private WarehouseRepo _repo;
        private WarehouseRepoFiller _systemUnderTests;
        private IEnumerable<WarehouseItemDto> _items;

        private string _wareHouseName1;
        private string _wareHouseItemId1;
        private string _wareHouseItemName1;
        private int _quantityItemOneWareHouseOne;
        private int _quantityItemOneWareHouseTwo;
        private string _wareHouseName2;
        private string _wareHouseItemId2;
        private string _wareHouseItemName2;
        private int _quantityItemTwoWareHouseOne;
        private int _quantityItemTwoWareHouseTwo;

        [TestInitialize]
        public void SetUp()
        {
            _repo = new WarehouseRepo();
            _systemUnderTests = new WarehouseRepoFiller(_repo);

            InitializeFixtures();
            PopulateWarehouseDtoCollection();
        }

        [TestMethod]
        public void GIVEN_a_warehouse_repo_and_repo_filler_WHEN_fill_method_is_invoked_THEN_the_repo_has_to_be_populated_with_values()
        {
            _systemUnderTests.Fill(_items);
            var actualItems = _repo.GetAll()?.ToArray();

            Assert.IsNotNull(actualItems);
            Assert.AreEqual(2, actualItems.Length);
            Assert.AreEqual(_quantityItemOneWareHouseOne + _quantityItemTwoWareHouseOne, actualItems[0].Total);
            Assert.AreEqual(_quantityItemOneWareHouseTwo + _quantityItemTwoWareHouseTwo, actualItems[1].Total);
        }

        private void InitializeFixtures()
        {
            _wareHouseName1 = _fixture.Create<string>();
            _wareHouseItemId1 = _fixture.Create<string>();
            _wareHouseItemName1 = _fixture.Create<string>();
            _wareHouseName2 = _fixture.Create<string>();
            _wareHouseItemId2 = _fixture.Create<string>();
            _wareHouseItemName2 = _fixture.Create<string>();
            _quantityItemOneWareHouseOne = _fixture.Create<int>();
            _quantityItemOneWareHouseTwo = _fixture.Create<int>();
            _quantityItemTwoWareHouseOne = _fixture.Create<int>();
            _quantityItemTwoWareHouseTwo = _fixture.Create<int>();
        }

        private void PopulateWarehouseDtoCollection()
        {
            _items = new List<WarehouseItemDto>()
            {
                new WarehouseItemDto()
                {
                    Id = _wareHouseItemId1,
                    Name = _wareHouseItemName1,
                    WarehouseItemInfo = new List<WarehouseItemInfo>()
                    {
                        new WarehouseItemInfo ()
                        {
                            Quantity = _quantityItemOneWareHouseOne,
                            WareHouseName = _wareHouseName1
                        },
                        new WarehouseItemInfo ()
                        {
                            Quantity = _quantityItemOneWareHouseTwo,
                            WareHouseName = _wareHouseName2
                        }
                    }
                },
                new WarehouseItemDto()
                {
                    Id = _wareHouseItemId2,
                    Name = _wareHouseItemName2,
                    WarehouseItemInfo = new List<WarehouseItemInfo>()
                    {
                        new WarehouseItemInfo ()
                        {
                            Quantity = _quantityItemTwoWareHouseOne,
                            WareHouseName = _wareHouseName1
                        },
                        new WarehouseItemInfo ()
                        {
                            Quantity = _quantityItemTwoWareHouseTwo,
                            WareHouseName = _wareHouseName2
                        }
                    }
                }
            };
        }
    }
}
