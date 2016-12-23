using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Getting_Real;
using System.Collections.Generic;

namespace Getting_Real_Tests
{
    public class TestData
    {
        public void FillCategoryTable(CategoryRepository categoryRepository)
        {
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Cosmetics" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Shoes" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Scarves" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Skirts" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Perfume" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Henna hair color" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Dresses" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Hair bands" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Soap" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Massage oil" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Praying mats" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Male dresses" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Miscellaneous" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Afro cosmetics" });
            categoryRepository.AddNewCategory(new Category() { CategoryName = "Scented incense" });
            categoryRepository.LoadAllCategories();
        }

        public void FillSupplierTable(SupplierRepository supplierRepository)
        {
            supplierRepository.AddNewSupplier(new Supplier()
            {
                SupplierName = "Megan Brook",
                SupplierCompany = "Eco Cosmetics",
                Email = "megan@ecosmetics.com",
                PhoneNumber = "+447700900747",
                Country = "UK",
                City = "Harrow",
                Address = "87 Cavendish Ave",
                Zip = 16486 
            });

            supplierRepository.AddNewSupplier(new Supplier()
            {
                SupplierName = "Skyler Simpson",
                SupplierCompany = "BeautyProducts",
                Email = "skyler@bproducts.com",
                PhoneNumber = "+447700900321",
                Country = "UK",
                City = "Bradford",
                Address = "1 Forster Ct",
                Zip = 17489 
            });

            supplierRepository.AddNewSupplier(new Supplier()
            {
                SupplierName = "Ayda El-Hashem",
                SupplierCompany = "Dubai Clothing",
                Email = "ayda@dclothing.com",
                PhoneNumber = "+97143255680",
                Country = "UAE",
                City = "Dubai",
                Address = "Al Barsha 1",
                Zip = 9746
            });
        }

        public void FillProductTable(ProductRepository productRepository, CategoryRepository categoryRepository , SupplierRepository supplierRepository) 
        {
            // "FillSupplierTable" and "FillCategoryTable" must be called before executing this method

            Category productCategory = FindCategory(categoryRepository.listOfCategories, "Dresses");
            Supplier productSupplier = FindSupplier(supplierRepository.listOfSuppliers, "Ayda El-Hashem", "Dubai Clothing");
            productRepository.AddNewProduct(new Product()
            {
                ProductName = "Yellow dress",
                ProductAmount = 8,
                ProductDescription = "A classic yellow dress",
                ProductPrice = 189.50M,
                category = productCategory,
                supplier = productSupplier
            });

            productCategory = FindCategory(categoryRepository.listOfCategories, "Cosmetics");
            productSupplier = FindSupplier(supplierRepository.listOfSuppliers, "Megan Brook", "Eco Cosmetics");
            productRepository.AddNewProduct(new Product()
            {
                ProductName = "Ecologic face wash",
                ProductAmount = 30,
                ProductDescription = "An effective face wash with natural ingredients",
                ProductPrice = 36.95M,
                category = productCategory,
                supplier = productSupplier
            });

            productCategory = FindCategory(categoryRepository.listOfCategories, "Miscellaneous");
            productSupplier = FindSupplier(supplierRepository.listOfSuppliers, "Skyler Simpson", "BeautyProducts");
            productRepository.AddNewProduct(new Product()
            {
                ProductName = "Comb",
                ProductAmount = 15,
                ProductDescription = "A classic small comb",
                ProductPrice = 19.50M,
                category = productCategory,
                supplier = productSupplier
            });

            productCategory = FindCategory(categoryRepository.listOfCategories, "Miscellaneous");
            productSupplier = new Supplier(); // Acts as "no supplier"
            productRepository.AddNewProduct(new Product()
            {
                ProductName = "Ayurvedic tea",
                ProductAmount = 8,
                ProductDescription = "A good tasting and spicy tea",
                ProductPrice = 23.95M,
                category = productCategory,
                supplier = productSupplier
            });

            productRepository.LoadAllProducts(supplierRepository.listOfSuppliers, categoryRepository.listOfCategories);
        }

        private Category FindCategory(List<Category> listOfCategories, string categoryName)
        {
            foreach (Category category in listOfCategories)
            {
                if (category.CategoryName == categoryName)
                {
                    return category;
                }
            }
            return null;
        }

        private Supplier FindSupplier(List<Supplier> listOfSuppliers, string supplierName, string supplierCompany)
        {
            foreach (Supplier supplier in listOfSuppliers)
            {
                if (supplier.SupplierName == supplierName && supplier.SupplierCompany == supplierCompany)
                {
                    return supplier;
                }
            }
            return null;
        }
    }
}
