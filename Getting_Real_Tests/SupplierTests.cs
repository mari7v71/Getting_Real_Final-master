using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Getting_Real;
using System.Collections.Generic;

namespace Getting_Real_Tests
{
    [TestClass]
    public class SupplierTests
    {
        [TestMethod]
        public void AddSupplierToDB()
        {
            SupplierRepository supplierRepository = new SupplierRepository();
            SupplierRepository.ClearSupplierAndZipTables();

            Supplier newSupplier = new Supplier()
            {
                SupplierName = "Steve",
                SupplierCompany = "EAL",
                Email = "steve@eal.dk",
                PhoneNumber = "123456789",
                Address = "Seebladsgade 1",
                City = "Odense",
                Country = "Denmark",
                Zip = 5000
            };

            supplierRepository.AddNewSupplier(newSupplier);
            supplierRepository.LoadAllSuppliers();
            Assert.IsTrue(supplierRepository.listOfSuppliers.Count == 1);

            bool supplierExists = false;
            foreach (Supplier supplier in supplierRepository.listOfSuppliers)
            {
                if (
                    supplier.SupplierName == newSupplier.SupplierName
                    && supplier.SupplierCompany == newSupplier.SupplierCompany
                    && supplier.Email == newSupplier.Email
                    && supplier.PhoneNumber == newSupplier.PhoneNumber
                    && supplier.Address == newSupplier.Address
                    && supplier.City == newSupplier.City
                    && supplier.Country == newSupplier.Country
                    && supplier.Zip == newSupplier.Zip)
                {
                    supplierExists = true;
                }
            }
            Assert.IsTrue(supplierExists);
        }

        [TestMethod]
        public void RemoveSupplierFromDB()
        {
            SupplierRepository supplierRepository = new SupplierRepository();
            TestData testData = new TestData();

            ProductRepository.ClearProductTable();
            SupplierRepository.ClearSupplierAndZipTables();

            testData.FillSupplierTable(supplierRepository);
            Assert.IsTrue(supplierRepository.listOfSuppliers.Count == 3);

            supplierRepository.RemoveSupplier("Skyler Simpson", "BeautyProducts");
            Assert.IsTrue(supplierRepository.listOfSuppliers.Count == 2);
        }
       
        [TestMethod]
        public void UpdateSupplierInfo()
        {
            SupplierRepository supplierRepository = new SupplierRepository();
            ProductRepository.ClearProductTable();
            SupplierRepository.ClearSupplierAndZipTables();

            Supplier newSupplier = new Supplier()
            {
                SupplierName = "Steve",
                SupplierCompany = "EAL",
                Email = "steve@eal.dk",
                PhoneNumber = "123456789",
                Address = "Seebladsgade 1",
                City = "Odense",
                Country = "Denmark",
                Zip = 5000
            };
            supplierRepository.AddNewSupplier(newSupplier);

            Supplier updatedSupplier = supplierRepository.listOfSuppliers[0];
            updatedSupplier.SupplierName = "Alexander";
            supplierRepository.UpdateSupplierInfo(updatedSupplier);

            Supplier loadedSupplier = supplierRepository.listOfSuppliers[0];
            Assert.IsTrue(updatedSupplier.SupplierName == loadedSupplier.SupplierName);
        }
        
    }
}
