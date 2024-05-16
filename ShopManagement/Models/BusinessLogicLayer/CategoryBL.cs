using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManagement.Models.BusinessLogicLayer
{
    public class CategoryBL
    {
        private ShopEntities context = new ShopEntities();
        public ObservableCollection<Category> CategoriesList { get; set; }
        public string ErrorMessage { get; set; }
        public event EventHandler<string> OperationCompleted;

        public CategoryBL()
        {
            CategoriesList = new ObservableCollection<Category>();
        }

        public void AddMethod(object obj)
        {
            Category category = obj as Category;
            if (category != null)
            {
                if (string.IsNullOrEmpty(category.name))
                {
                    OperationCompleted?.Invoke(this, "You have to name your category!");
                    return;
                }
                try
                {
                    context.Category.Add(category);
                    context.SaveChanges();
                    category.id = context.Category.Max(item => item.id);
                    CategoriesList.Add(category);
                    OperationCompleted?.Invoke(this, $"Category {category.name} has been added successfully!");
                }
                catch (Exception)
                {
                    OperationCompleted?.Invoke(this, "Invalid category name! Try another one.");
                    context.Category.Remove(category);
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Category category = obj as Category;
            if (category == null)
            {
                OperationCompleted?.Invoke(this, "No category selected!");
                return;
            }
            else if (string.IsNullOrEmpty(category.name))
            {
                OperationCompleted?.Invoke(this, "Category name can't be null!");
                return;
            }
            try
            {
                context.ModifyCategory(category.id, category.name);
                context.SaveChanges();
                OperationCompleted?.Invoke(this, "The category name was changed successfully!");
            }
            catch (Exception)
            {
                OperationCompleted?.Invoke(this, "Invalid category!");
            }
        }

        public void DeleteMethod(object obj)
        {
            Category category = obj as Category;
            if (category == null)
            {
                OperationCompleted?.Invoke(this, "You have to select a category!");
            }
            else
            {
                context.DeactivateCategory(category.id);
                context.SaveChanges();
                CategoriesList.Remove(category);
                OperationCompleted?.Invoke(this, $"Category {category.name} removed successfully!");
            }
        }

        public ObservableCollection<Category> GetAllCategories()
        {
            return new ObservableCollection<Category>(context.Category.ToList());
        }
    }
}
