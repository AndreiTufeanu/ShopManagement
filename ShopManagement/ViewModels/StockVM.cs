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
    public class StockVM : BaseVM
    {
        private StockBL stockBLL;
        private string errorMessage;

        public StockVM()
        {
            stockBLL = new StockBL();
            stockBLL.OperationCompleted += StockBLL_OperationCompleted; ;
            StocksList = new ObservableCollection<Product_Stock>(stockBLL.GetAllStocks().Where(stock => stock.active == true));
        }

        private void StockBLL_OperationCompleted(object sender, string message)
        {
            MessageBox.Show(message);
        }

        #region Data Members

        public ObservableCollection<Product_Stock> StocksList
        {
            get => stockBLL.StocksList;
            set => stockBLL.StocksList = value;
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
            Product_Stock stock = obj as Product_Stock;
            if (stock != null)
            {
                stockBLL.AddMethod(stock);
                ErrorMessage = stockBLL.ErrorMessage;
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
            stockBLL.UpdateMethod(obj);
            ErrorMessage = stockBLL.ErrorMessage;
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
            stockBLL.DeleteMethod(obj);
            ErrorMessage = stockBLL.ErrorMessage;
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
