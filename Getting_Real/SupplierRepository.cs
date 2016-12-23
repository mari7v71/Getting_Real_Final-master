using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    public class SupplierRepository
    {
        DatabaseAccess databaseAccess;
        public List<Supplier> listOfSuppliers = new List<Supplier>();

        public SupplierRepository()
        {
            databaseAccess = new DatabaseAccess();
        }

        public void AddNewSupplier(Supplier newSupplier)
        {
            if (databaseAccess.DoesZipExist(newSupplier.Zip) == false)
            {
                databaseAccess.AddNewZipNumberToTable(newSupplier.Zip);
            }
            databaseAccess.AddNewSupplierToTable(newSupplier);
            LoadAllSuppliers();
        }

        public void LoadAllSuppliers()
        {
            listOfSuppliers = databaseAccess.LoadAllSuppliersFromTable();
        }

        public void RemoveSupplier(string supplierName, string supplierCompany)
        {
            Supplier supplierToRemove = null;

            foreach (Supplier supplier in listOfSuppliers)
            {
                if (supplier.SupplierName == supplierName && supplier.SupplierCompany == supplierCompany)
                {
                    supplierToRemove = supplier;
                }
            }
            databaseAccess.RemoveSupplierFromTable(supplierToRemove);
            LoadAllSuppliers();
        }

        public void UpdateSupplierInfo(Supplier updatedSupplier)
        {
            databaseAccess.UpdateSupplierInfo(updatedSupplier);
            LoadAllSuppliers();
        }

        public static void ClearSupplierAndZipTables()
        {
            DatabaseAccess databaseAccess = new DatabaseAccess();
            databaseAccess.ClearSupplierAndZipTables();
        }     
    }
}
