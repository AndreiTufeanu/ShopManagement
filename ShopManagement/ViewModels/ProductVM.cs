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
using System.Windows.Controls;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    public class ProductVM : BaseVM
    {
        private ProductBL productBLL;
        private ReceiptBL receiptBL;
        private string errorMessage;

        public ProductVM()
        {
            productBLL = new ProductBL();
            receiptBL = new ReceiptBL();
            productBLL.OperationCompleted += ProductBLL_OperationCompleted;
            receiptBL.OperationCompleted += ReceiptBL_OperationCompleted;
            ProductsList = new ObservableCollection<Product>(productBLL.GetAllProducts().Where(product => product.active == true));
            ReceiptProductsList = new ObservableCollection<Product>();
        }

        private void ReceiptBL_OperationCompleted(object sender, string message)
        {
            if (message.StartsWith("Receipt"))
            {
                MessageBox.Show(message);
                ReceiptProductsList.Clear();
            }
            else
            {
                foreach (var product in ReceiptProductsList)
                {
                    ProductsList.Add(product);
                }
                ReceiptProductsList.Clear();
            }
        }

        private void ProductBLL_OperationCompleted(object sender, string message)
        {
            MessageBox.Show(message);
        }

        #region Data Members

        public ObservableCollection<Product> ProductsList
        {
            get => productBLL.ProductsList;
            set => productBLL.ProductsList = value;
        }

        public ObservableCollection<Product> ReceiptProductsList { get; set; }

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

        public void AddToReceiptMethod(object obj)
        {
            ReceiptProductsList.Add(obj as Product);
            ProductsList.Remove(obj as Product);
            ErrorMessage = productBLL.ErrorMessage;
        }

        private ICommand addToReceiptCommand;
        public ICommand AddToReceiptCommand
        {
            get
            {
                if (addToReceiptCommand == null)
                {
                    addToReceiptCommand = new RelayCommand(AddToReceiptMethod);
                }
                return addToReceiptCommand;
            }
        }

        public void RemoveFromReceiptMethod(object obj)
        {
            ReceiptProductsList.Remove(obj as Product);
            ProductsList.Add(obj as Product);
            ErrorMessage = productBLL.ErrorMessage;
        }

        private ICommand removeFromReceiptCommand;
        public ICommand RemoveFromReceiptCommand
        {
            get
            {
                if (removeFromReceiptCommand == null)
                {
                    removeFromReceiptCommand = new RelayCommand(RemoveFromReceiptMethod);
                }
                return removeFromReceiptCommand;
            }
        }

        public void EmitReceiptMethod(object obj)
        {
            receiptBL.EmitReceiptMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
        }

        private ICommand emitReceiptCommand;
        public ICommand EmitReceiptCommand
        {
            get
            {
                if (emitReceiptCommand == null)
                {
                    emitReceiptCommand = new RelayCommand(EmitReceiptMethod);
                }
                return emitReceiptCommand;
            }
        }

        public void FilterMethod(object obj)
        {
            productBLL.FilterMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
        }

        private ICommand filterCommand;
        public ICommand FilterCommand
        {
            get
            {
                if (filterCommand == null)
                {
                    filterCommand = new RelayCommand(FilterMethod);
                }
                return filterCommand;
            }
        }

        public void FilterProducerMethod(object obj)
        {
            productBLL.FilterProducerMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
        }

        private ICommand filterProducerCommand;
        public ICommand FilterProducerCommand
        {
            get
            {
                if (filterProducerCommand == null)
                {
                    filterProducerCommand = new RelayCommand(FilterProducerMethod);
                }
                return filterProducerCommand;
            }
        }

        public void UpdateMethod(object obj)
        {
            productBLL.UpdateMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
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
            productBLL.DeleteMethod(obj);
            ErrorMessage = productBLL.ErrorMessage;
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
