using ShopManagement.Converters;
using ShopManagement.Helpers;
using ShopManagement.Models;
using ShopManagement.Models.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    public class CategoryVM : BaseVM
    {
        private CategoryBL categoryBLL;
        private string errorMessage;

        public CategoryVM()
        {
            categoryBLL = new CategoryBL();
            categoryBLL.OperationCompleted += CategoryBLL_OperationCompleted;
            CategoriesList = new ObservableCollection<Category>(categoryBLL.GetAllCategories().Where(category => category.active == true));
        }

        private void CategoryBLL_OperationCompleted(object sender, string message)
        {
            MessageBox.Show(message);
        }

        #region Data Members

        public ObservableCollection<Category> CategoriesList
        {
            get => categoryBLL.CategoriesList;
            set => categoryBLL.CategoriesList = value;
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        #endregion

        #region Command Members

        public void AddMethod(object obj)
        {
            Category category = obj as Category;
            if (category != null)
            {
                categoryBLL.AddMethod(category);
                ErrorMessage = categoryBLL.ErrorMessage;
            }
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand(AddMethod);
                }
                return addCommand;
            }
        }

        public void UpdateMethod(object obj)
        {
            categoryBLL.UpdateMethod(obj);
            ErrorMessage = categoryBLL.ErrorMessage;
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand(UpdateMethod);
                }
                return updateCommand;
            }
        }

        public void DeleteMethod(object obj)
        {
            categoryBLL.DeleteMethod(obj);
            ErrorMessage = categoryBLL.ErrorMessage;
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand(DeleteMethod);
                }
                return deleteCommand;
            }
        }

        #endregion
    }
}
