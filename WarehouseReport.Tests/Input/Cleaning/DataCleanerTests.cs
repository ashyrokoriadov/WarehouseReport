using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WarehouseReport.Input.Cleaning;

namespace WarehouseReport.Tests.Input.Cleaning
{
    [TestClass]
    public class DataCleanerTests
    {
        private DataCleaner _systemUnderTests;

        [TestInitialize]
        public void SetUp()
        {
            _systemUnderTests = new DataCleaner();
        }

        [TestMethod]
        public void GIVEN_an_array_with_4_items_1_of_items_starts_from_hash_WHEN_clean_method_is_invoked_THEN_item_starts_from_hash_has_to_be_ignored()
        {
            //arrange
            var oneOfItemsStartsFromHash = new[] { "test 1", "# test 2", "test 3", "test 4" };

            //act
            var actual = _systemUnderTests.Clean(oneOfItemsStartsFromHash).ToArray();

            //assert
            Assert.AreEqual(3, actual.Length);
            Assert.AreEqual(actual[0], oneOfItemsStartsFromHash[0]);
            Assert.AreEqual(actual[1], oneOfItemsStartsFromHash[2]);
            Assert.AreEqual(actual[2], oneOfItemsStartsFromHash[3]);
        }

        [TestMethod]
        public void GIVEN_an_array_with_4_items_2_of_items_starts_from_hash_WHEN_clean_method_is_invoked_THEN_items_start_from_hash_has_to_be_ignored()
        {
            //arrange
            var twoOfItemsStartsFromHash = new[] { "test 1", "# test 2", "test 3", "# test 4" };

            //act
            var actual = _systemUnderTests.Clean(twoOfItemsStartsFromHash).ToArray();

            //assert
            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(actual[0], twoOfItemsStartsFromHash[0]);
            Assert.AreEqual(actual[1], twoOfItemsStartsFromHash[2]);
        }

        [TestMethod]
        public void GIVEN_an_array_with_4_items_no_items_starts_from_hash_WHEN_clean_method_is_invoked_THEN_no_items_has_to_be_ignored()
        {
            //arrange
            var noItemsStartsFromHash = new[] { "test 1", "test 2", "test 3", "test 4" };

            //act
            var actual = _systemUnderTests.Clean(noItemsStartsFromHash).ToArray();

            //assert
            Assert.AreEqual(4, actual.Length);

            for (var i =0; i < noItemsStartsFromHash.Length; i++)
                Assert.AreEqual(actual[i], noItemsStartsFromHash[i]);
        }

        [TestMethod]
        public void GIVEN_an_empty_array_WHEN_clean_method_is_invoked_THEN_no_exceptions_has_to_be_thrown()
        {
            //arrange
            var emptyArray = new string[]{};

            //act
            var actual = _systemUnderTests.Clean(emptyArray).ToArray();

            //assert
            Assert.AreEqual(0, actual.Length);
        }
    }
}
