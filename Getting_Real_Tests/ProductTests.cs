using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Getting_Real;
using System.Collections.Generic;


namespace Getting_Real_Tests
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void AddProductWithoutSupplierToDB()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ProductRepository productRepository = new ProductRepository();
           
            ProductRepository.ClearProductTable();
            CategoryRepository.ClearCategoryTable();

            Category newCategory = new Category()
            {
                CategoryName = "TestCategory"
            };

            categoryRepository.AddNewCategory(newCategory);
            Category loadedCategory = categoryRepository.listOfCategories[0];

            Product newProduct = new Product("ProductName", loadedCategory, 10, "Brand new product", 25.95M);

            productRepository.AddNewProduct(newProduct);
            productRepository.LoadAllProducts(null, categoryRepository.listOfCategories); // needs changing

            bool productExists = false;

            foreach (Product product in productRepository.listOfProducts)
            {
                if (
                    newProduct.ProductName == product.ProductName
                    && newProduct.category.CategoryID == product.category.CategoryID
                    && newProduct.ProductAmount == product.ProductAmount
                    && newProduct.ProductDescription == product.ProductDescription
                    && newProduct.ProductPrice == product.ProductPrice
                    && newProduct.supplier.SupplierID == product.supplier.SupplierID
                    )
                {
                    productExists = true;
                }
            }
            Assert.IsTrue(productExists);
        }

        [TestMethod]
        public void AddProductWithSupplierToDB()
        {    
            SupplierRepository supplierRepository = new SupplierRepository();
            CategoryRepository categoryRepository = new CategoryRepository();
            ProductRepository productRepository = new ProductRepository();           

            ProductRepository.ClearProductTable();
            CategoryRepository.ClearCategoryTable();
            SupplierRepository.ClearSupplierAndZipTables();

            Supplier newSupplier = new Supplier()
            {
                SupplierName = "Steve",
                SupplierCompany = "Samsung",
                Email = "samsung@gmail.com",
                PhoneNumber = "123456789",
                Address = "Seebladsgade 1",
                City = "Odense",
                Country = "Denmark",
                Zip = 5000
            };
            supplierRepository.AddNewSupplier(newSupplier);

            supplierRepository.LoadAllSuppliers();
            Supplier loadedSupplier = supplierRepository.listOfSuppliers[0];

            Category newCategory = new Category()
            {
                CategoryName = "TestCategory"
            };

            categoryRepository.AddNewCategory(newCategory);
            Category loadedCategory = categoryRepository.listOfCategories[0];

            Product newProduct = new Product("ProductName", loadedCategory, 10, "Brand new product", 25.95M, loadedSupplier);
            productRepository.AddNewProduct(newProduct);

            productRepository.LoadAllProducts(supplierRepository.listOfSuppliers, categoryRepository.listOfCategories);

            bool productExists = false;

            foreach (Product product in productRepository.listOfProducts)
            {
                if
                    (
                    newProduct.ProductName == product.ProductName
                    && newProduct.category.CategoryID == product.category.CategoryID
                    && newProduct.ProductAmount == product.ProductAmount
                    && newProduct.ProductDescription == product.ProductDescription
                    && newProduct.ProductPrice == product.ProductPrice
                    && newProduct.supplier.SupplierID == product.supplier.SupplierID
                    )
                {
                    productExists = true;
                }
            }
            Assert.IsTrue(productExists);
        }

        [TestMethod]
        public void RemoveProductFromDB()
        {
            TestData testData = new TestData();
            SupplierRepository supplierRepository = new SupplierRepository();
            CategoryRepository categoryRepository = new CategoryRepository();
            ProductRepository productRepository = new ProductRepository();

            ProductRepository.ClearProductTable();
            CategoryRepository.ClearCategoryTable();
            SupplierRepository.ClearSupplierAndZipTables();

            testData.FillCategoryTable(categoryRepository);
            testData.FillSupplierTable(supplierRepository);
            testData.FillProductTable(productRepository, categoryRepository, supplierRepository);
            Assert.IsTrue(productRepository.listOfProducts.Count == 4);

            productRepository.RemoveProduct("Ecologic face wash", supplierRepository.listOfSuppliers, categoryRepository.listOfCategories);
            Assert.IsTrue(productRepository.listOfProducts.Count == 3);
        }
    }
}
