using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getting_Real
{
    public class CategoryRepository
    {
        DatabaseAccess databaseAccess;
        public List<Category> listOfCategories = new List<Category>();

        public CategoryRepository()
        {
            databaseAccess = new DatabaseAccess();
        }

        public void AddNewCategory(Category newCategory)
        {
            databaseAccess.AddNewCategoryToTable(newCategory);
            LoadAllCategories();
        }

        public void LoadAllCategories()
        {
            listOfCategories = databaseAccess.LoadAllCategoriesFromTable();
        }

        public void RemoveCategory(string categoryName)
        {
            Category categoryToRemove = null;

            foreach(Category category in listOfCategories)
            {
                if (category.CategoryName == categoryName)
                {
                    categoryToRemove = category;
                }
            }
            databaseAccess.RemoveCategoryFromTable(categoryToRemove);
            LoadAllCategories();
        }

        public void UpdateCategoryInfo(Category updatedCategory)
        {
            databaseAccess.UpdateCategoryInfo(updatedCategory);
            LoadAllCategories();
        }

        public static void ClearCategoryTable()
        {
            DatabaseAccess databaseAccess = new DatabaseAccess();
            databaseAccess.ClearCategoryTable();
        }

        
    }
}
