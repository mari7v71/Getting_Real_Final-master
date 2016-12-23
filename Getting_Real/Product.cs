using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }  
        public int ProductAmount { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public Category category { get; set; }
        public Supplier supplier { get; set; }

        public Product()
        {
            supplier = new Supplier { SupplierID = 0 };
        }

        public Product(int productID, string productName, Category category, int productAmount, string productDescription, decimal productPrice, Supplier supplier)
        {
            ProductID = productID;
            ProductName = productName;
            this.category = category;
            ProductAmount = productAmount;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            this.supplier = supplier;
        }

        public Product(string productName, Category category, int productAmount, string productDescription, decimal productPrice, Supplier supplier)
        {
            ProductName = productName;
            this.category = category;
            ProductAmount = productAmount;
            ProductDescription = productDescription;
            ProductPrice = productPrice;
            this.supplier = supplier;
        }

        public Product(string productName, Category category, int productAmount, string productDescription, decimal productPrice)
        {
            ProductName = productName;
            this.category = category;
            ProductAmount = productAmount;
            ProductDescription = productDescription;
            ProductPrice = productPrice;

            supplier = new Supplier { SupplierID = 0 };
        }
    }
}
