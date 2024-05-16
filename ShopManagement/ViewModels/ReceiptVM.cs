using ShopManagement.Models.BusinessLogicLayer;
using ShopManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Helpers;
using System.Windows.Input;
using System.ComponentModel;

namespace ShopManagement.ViewModels
{
    class ReceiptVM : BaseVM
    {
        private ReceiptBL receiptBLL;
        private string errorMessage;

        public ReceiptVM()
        {
            receiptBLL = new ReceiptBL();
            ReceiptsList = new ObservableCollection<Receipt>(receiptBLL.GetAllReceipts());
            DailySalesSummaries = new ObservableCollection<KeyValuePair<DateTime?, ReceiptBL.DaySalesSummary>>(receiptBLL.GetDailyStats());
        }

        #region Data Members

        public ObservableCollection<Receipt> ReceiptsList
        {
            get => receiptBLL.ReceiptsList;
            set => receiptBLL.ReceiptsList = value;
        }

        public ObservableCollection<KeyValuePair<DateTime?, ReceiptBL.DaySalesSummary>> DailySalesSummaries { get; set; }

        private string cashierName;
        public string CashierName
        {
            get { return cashierName; }
            set
            {
                if (cashierName != value)
                {
                    cashierName = value;
                    NotifyPropertyChanged(nameof(CashierName));
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyPropertyChanged(nameof(Description));
                }
            }
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

        public void FilterMethod(object obj)
        {
            receiptBLL.FilterMethod(obj);
            ErrorMessage = receiptBLL.ErrorMessage;
            DailySalesSummaries.Clear();
            foreach (var item in receiptBLL.GetDailyStats())
            {
                DailySalesSummaries.Add(item);
            }
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

        public void BestReceiptMethod(object obj)
        {
            (CashierName, Description) = receiptBLL.BestReceiptMethod(obj);
            ErrorMessage = receiptBLL.ErrorMessage;
        }

        private ICommand bestReceiptCommand;
        public ICommand BestReceiptCommand
        {
            get
            {
                if (bestReceiptCommand == null)
                {
                    bestReceiptCommand = new RelayCommand(BestReceiptMethod);
                }
                return bestReceiptCommand;
            }
        }

        #endregion
    }
}
