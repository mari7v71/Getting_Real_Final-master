using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Getting_Real;
using System.Collections.Generic;

namespace Getting_Real_Tests
{
    [TestClass]
    public class OtherTests
    {
        [TestMethod]
        public void FillTablesWithTestData()
        {
            TestData testData = new TestData();
            CategoryRepository categoryRepository = new CategoryRepository();
            SupplierRepository supplierRepository = new SupplierRepository();
            ProductRepository productRepository = new ProductRepository();

            ProductRepository.ClearProductTable();
            CategoryRepository.ClearCategoryTable();
            SupplierRepository.ClearSupplierAndZipTables();

            testData.FillCategoryTable(categoryRepository);
            Assert.IsTrue(categoryRepository.listOfCategories.Count == 15);

            testData.FillSupplierTable(supplierRepository);
            Assert.IsTrue(supplierRepository.listOfSuppliers.Count == 3);

            testData.FillProductTable(productRepository, categoryRepository, supplierRepository);
            Assert.IsTrue(productRepository.listOfProducts.Count == 4);
        }
    }
}
