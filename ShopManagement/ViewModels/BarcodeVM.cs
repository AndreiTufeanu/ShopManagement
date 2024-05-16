using ShopManagement.Helpers;
using ShopManagement.Models.BusinessLogicLayer;
using ShopManagement.Models;
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
    public class BarcodeVM : BaseVM
    {
        private BarcodeBL barcodeBLL;
        private string errorMessage;

        public BarcodeVM()
        {
            barcodeBLL = new BarcodeBL();
            barcodeBLL.OperationCompleted += BarcodeBLL_OperationCompleted; ;
            BarcodesList = new ObservableCollection<Barcode>(barcodeBLL.GetAllBarcodes().Where(barcode => barcode.active == true));
        }

        private void BarcodeBLL_OperationCompleted(object sender, string message)
        {
            MessageBox.Show(message);
        }

        #region Data Memebers

        public ObservableCollection<Barcode> BarcodesList
        {
            get => barcodeBLL.BarcodesList;
            set => barcodeBLL.BarcodesList = value;
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
            Barcode barcode = obj as Barcode;
            if (barcode != null)
            {
                barcodeBLL.AddMethod(barcode);
                ErrorMessage = barcodeBLL.ErrorMessage;
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
            barcodeBLL.UpdateMethod(obj);
            ErrorMessage = barcodeBLL.ErrorMessage;
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
            barcodeBLL.DeleteMethod(obj);
            ErrorMessage = barcodeBLL.ErrorMessage;
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
