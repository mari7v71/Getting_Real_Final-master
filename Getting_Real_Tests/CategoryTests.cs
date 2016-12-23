using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Getting_Real;
using System.Collections.Generic;

namespace Getting_Real_Tests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void AddCategoryToDB()
        {
            CategoryRepository categoryRepository = new CategoryRepository();

            ProductRepository.ClearProductTable();
            CategoryRepository.ClearCategoryTable();

            Category newCategory = new Category()
            {
                CategoryName = "TestCategory"
            };
            categoryRepository.AddNewCategory(newCategory);            

            bool categoryExists = false;
            foreach (Category category in categoryRepository.listOfCategories)
            {
                if (category.CategoryName == newCategory.CategoryName)
                {
                    categoryExists = true;
                }
            }
            Assert.IsTrue(categoryExists);
        }

        [TestMethod]
        public void RemoveCategoryFromDB()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            TestData testData = new TestData();

            ProductRepository.ClearProductTable();
            CategoryRepository.ClearCategoryTable();

            testData.FillCategoryTable(categoryRepository);
            Assert.IsTrue(categoryRepository.listOfCategories.Count == 15);

            categoryRepository.RemoveCategory("Skirts");
            Assert.IsTrue(categoryRepository.listOfCategories.Count == 14);
        }

        [TestMethod]
        public void UpdateCategoryInfo()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            ProductRepository.ClearProductTable();
            CategoryRepository.ClearCategoryTable();

            Category newCategory = new Category()
            {
                CategoryName = "TestCategory"
            };
            categoryRepository.AddNewCategory(newCategory);

            Category updatedCategory = categoryRepository.listOfCategories[0];
            updatedCategory.CategoryName = "UpdatedCategory";
            categoryRepository.UpdateCategoryInfo(updatedCategory);

            Category loadedCategory = categoryRepository.listOfCategories[0];
            Assert.IsTrue(updatedCategory.CategoryName == loadedCategory.CategoryName);
        }

    }
}
