using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    public class ProductRepository
    {
        DatabaseAccess databaseAccess;
        public List<Product> listOfProducts = new List<Product>();   
        
        public ProductRepository()
        {
            databaseAccess = new DatabaseAccess();
        }

        public void AddNewProduct(Product newProduct)
        {
            databaseAccess.AddNewProductToTable(newProduct);
        }

        public void LoadAllProducts(List<Supplier> listOfSuppliers, List<Category> listOfCategories)
        {
            listOfProducts = databaseAccess.LoadAllProductsFromTable(listOfSuppliers, listOfCategories);
        }

        public void RemoveProduct(string productName, List<Supplier> listOfSuppliers, List<Category> listOfCategories)
        {
            Product productToRemove = null;

            foreach (Product product in listOfProducts)
            {
                if (product.ProductName == productName)
                {
                    productToRemove = product;
                }
            }
            databaseAccess.RemoveProductFromTable(productToRemove);
            LoadAllProducts(listOfSuppliers, listOfCategories);
        }

        public static void ClearProductTable()
        {
            DatabaseAccess databaseAccess = new DatabaseAccess();
            databaseAccess.ClearProductTable();
        }    
    }
}
