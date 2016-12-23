using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    public class DatabaseAccess
    {
        private static string connectionString = "Server=ealdb1.eal.local; Database= ejl89_db; User ID=ejl89_usr; Password=Baz1nga89;";

        public void ClearSupplierAndZipTables()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ClearSupplierAndZipTables", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void ClearProductTable()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ClearProductTable", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void ClearCategoryTable()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ClearCategoryTable", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public List<Supplier> LoadAllSuppliersFromTable()
        {
            List<Supplier> listOfSuppliers = new List<Supplier>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("GetListOfSuppliers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Supplier supplier = new Supplier();
                            supplier.SupplierID = int.Parse(reader["SupplierID"].ToString());
                            supplier.SupplierName = reader["SupplierName"].ToString();
                            supplier.SupplierCompany = reader["SupplierCompany"].ToString();
                            supplier.Email = reader["SupplierEmail"].ToString();
                            supplier.PhoneNumber = reader["SupplierPhone"].ToString();
                            supplier.Address = reader["Address"].ToString();
                            supplier.City = reader["City"].ToString();
                            supplier.Country = reader["Country"].ToString();
                            supplier.Zip = int.Parse(reader["ZipNumber"].ToString());

                            listOfSuppliers.Add(supplier);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
            return listOfSuppliers;
        }

        public List<Category> LoadAllCategoriesFromTable()
        {
            List<Category> listOfCategories = new List<Category>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetListOfCategories", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Category category = new Category();
                            category.CategoryID = int.Parse(reader["CategoryID"].ToString());
                            category.CategoryName = reader["CategoryName"].ToString(); 
                            listOfCategories.Add(category);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
            return listOfCategories;
        }

        public List<Product> LoadAllProductsFromTable(List<Supplier> listOfSuppliers, List<Category> listOfCategories)
        {
            List<Product> listOfProducts = new List<Product>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetListOfProducts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.ProductID = int.Parse(reader["ProductID"].ToString());
                            product.ProductName = reader["ProductName"].ToString();
                            product.ProductAmount = Convert.ToInt32(reader["ProductAmount"]);
                            product.ProductDescription = reader["ProductDescription"].ToString();
                            product.ProductPrice = Convert.ToDecimal(reader["ProductPrice"]);

                            int categoryID = Convert.ToInt32(reader["CategoryID"]);
                            int supplierID = Convert.ToInt32(reader["SupplierID"]);

                            foreach (Category category in listOfCategories)
                            {
                                if (category.CategoryID == categoryID)
                                {
                                    product.category = category;
                                }
                            }

                            if (supplierID != 0)
                            {
                                foreach (Supplier supplier in listOfSuppliers)
                                {
                                    if (supplier.SupplierID == supplierID)
                                    {
                                        product.supplier = supplier;
                                    }
                                }
                            }
                            listOfProducts.Add(product);
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
            return listOfProducts;
        }

        public bool DoesZipExist(int zipNumber)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetZipID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ZipNumber", zipNumber);
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows == true)
                    {
                        return true;
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
                return false;
            }
        }

        public void AddNewZipNumberToTable(int zipNumber)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("AddZip", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ZipNumber", zipNumber);
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void AddNewSupplierToTable(Supplier NewSupplier)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("AddNewSupplier", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("SupplierName", NewSupplier.SupplierName));
                    cmd.Parameters.Add(new SqlParameter("SupplierCompany", NewSupplier.SupplierCompany));
                    cmd.Parameters.Add(new SqlParameter("SupplierEmail", NewSupplier.Email));
                    cmd.Parameters.Add(new SqlParameter("SupplierPhone", NewSupplier.PhoneNumber));
                    cmd.Parameters.Add(new SqlParameter("Address", NewSupplier.Address));
                    cmd.Parameters.Add(new SqlParameter("City", NewSupplier.City));
                    cmd.Parameters.Add(new SqlParameter("Country", NewSupplier.Country));
                    cmd.Parameters.Add(new SqlParameter("ZipNumber", NewSupplier.Zip));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void AddNewProductToTable(Product NewProduct)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("AddNewProduct", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ProductName", NewProduct.ProductName));
                    cmd.Parameters.Add(new SqlParameter("ProductDescription", NewProduct.ProductDescription));
                    cmd.Parameters.Add(new SqlParameter("ProductAmount", NewProduct.ProductAmount));
                    cmd.Parameters.Add(new SqlParameter("ProductPrice", NewProduct.ProductPrice));
                    cmd.Parameters.Add(new SqlParameter("CategoryID", NewProduct.category.CategoryID));
                    cmd.Parameters.Add(new SqlParameter("SupplierID", NewProduct.supplier.SupplierID));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void AddNewCategoryToTable(Category newCategory)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("AddNewCategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("CategoryName", newCategory.CategoryName));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void RemoveCategoryFromTable(Category categoryToRemove)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("RemoveCategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("CategoryID", categoryToRemove.CategoryID));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void RemoveSupplierFromTable(Supplier supplierToRemove)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("RemoveSupplier", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("SupplierID", supplierToRemove.SupplierID));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void RemoveProductFromTable(Product productToRemove)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("RemoveProduct", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ProductID", productToRemove.ProductID));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void UpdateCategoryInfo(Category updatedCategory)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateCategory", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("CategoryID", updatedCategory.CategoryID));
                    cmd.Parameters.Add(new SqlParameter("CategoryName", updatedCategory.CategoryName));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }

        public void UpdateSupplierInfo(Supplier updatedSupplier)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateSupplier", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("SupplierID", updatedSupplier.SupplierID));
                    cmd.Parameters.Add(new SqlParameter("SupplierName", updatedSupplier.SupplierName));
                    cmd.Parameters.Add(new SqlParameter("SupplierCompany", updatedSupplier.SupplierCompany));
                    cmd.Parameters.Add(new SqlParameter("SupplierEmail", updatedSupplier.Email));
                    cmd.Parameters.Add(new SqlParameter("SupplierPhone", updatedSupplier.PhoneNumber));
                    cmd.Parameters.Add(new SqlParameter("Address", updatedSupplier.Address));
                    cmd.Parameters.Add(new SqlParameter("City", updatedSupplier.City));
                    cmd.Parameters.Add(new SqlParameter("Country", updatedSupplier.Country));
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("UPS " + e.Message);
                    Console.ReadKey();
                }
            }
        }
    }
}
