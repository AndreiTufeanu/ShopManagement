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
    public class ProductTypeVM : BaseVM
    {
        private ProductTypeBL productTypeBLL;
        private string errorMessage;

        public ProductTypeVM()
        {
            productTypeBLL = new ProductTypeBL();
            productTypeBLL.OperationCompleted += ProductTypeBLL_OperationCompleted;

            ProductTypesList = new ObservableCollection<Product_Type>(productTypeBLL.GetAllProductTypes().Where(productType => productType.active == true));
            ProductTypesOptions = new ObservableCollection<string> { "Kg", "L", "Piece" };
        }

        private void ProductTypeBLL_OperationCompleted(object sender, string message)
        {
            MessageBox.Show(message);
        }

        #region Data Members

        public ObservableCollection<Product_Type> ProductTypesList
        {
            get => productTypeBLL.ProductTypesList;
            set => productTypeBLL.ProductTypesList = value;
        }

        public ObservableCollection<string> ProductTypesOptions { get; }

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
            Product_Type product_Type = obj as Product_Type;
            if (product_Type != null)
            {
                productTypeBLL.AddMethod(product_Type);
                ErrorMessage = productTypeBLL.ErrorMessage;
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
            productTypeBLL.UpdateMethod(obj);
            ErrorMessage = productTypeBLL.ErrorMessage;
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
            productTypeBLL.DeleteMethod(obj);
            ErrorMessage = productTypeBLL.ErrorMessage;
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
