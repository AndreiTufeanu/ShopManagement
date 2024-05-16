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
    public class ProducerVM : BaseVM
    {
        private ProducerBL producerBLL;
        private string errorMessage;

        public ProducerVM()
        {
            producerBLL = new ProducerBL();
            producerBLL.OperationCompleted += ProducerBLL_OperationCompleted;
            ProducersList = new ObservableCollection<Producer>(producerBLL.GetAllProducers().Where(producer => producer.active == true));
        }

        private void ProducerBLL_OperationCompleted(object sender, string message)
        {
            MessageBox.Show(message);
        }

        #region Data Members

        public ObservableCollection<Producer> ProducersList
        {
            get => producerBLL.ProducersList;
            set => producerBLL.ProducersList = value;
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
            Producer producer = obj as Producer;
            if (producer != null)
            {
                producerBLL.AddMethod(producer);
                ErrorMessage = producerBLL.ErrorMessage;
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
            producerBLL.UpdateMethod(obj);
            ErrorMessage = producerBLL.ErrorMessage;
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
            producerBLL.DeleteMethod(obj);
            ErrorMessage = producerBLL.ErrorMessage;
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
