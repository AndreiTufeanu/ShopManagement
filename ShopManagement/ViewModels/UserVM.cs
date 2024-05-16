using ShopManagement.Converters;
using ShopManagement.Helpers;
using ShopManagement.Models;
using ShopManagement.Models.BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    class UserVM : BaseVM
    {
        private UserBL userBLL;
        private SecureString password;
        private string errorMessage;
        public event EventHandler<bool> OnMoveToNextWindow;


        public UserVM()
        {
            userBLL = new UserBL();
            userBLL.OperationCompleted += UserBLL_OperationCompleted;
            userBLL.OnLogInSuccessfull += UserBLL_OnLogInSuccessfull;

            UsersList = new ObservableCollection<User>(userBLL.GetAllUsers().Where(user => user.is_active == true));
            UserTypes = new ObservableCollection<string> { "admin", "cashier" };
        }

        private void UserBLL_OnLogInSuccessfull(object sender, bool isAdmin)
        {
            OnMoveToNextWindow?.Invoke(this, isAdmin);
        }

        private void UserBLL_OperationCompleted(object sender, string message)
        {
            MessageBox.Show(message);
        }

        #region Data Members

        public ObservableCollection<User> UsersList
        {
            get => userBLL.UsersList;
            set => userBLL.UsersList = value;
        }
        public ObservableCollection<string> UserTypes { get; }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                NotifyPropertyChanged("ErrorMessage");
            }
        }

        public SecureString Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }

        #endregion

        #region Command Members

        public void AddMethod(object obj)
        {
            User user = obj as User;
            if (user != null)
            {
                string plainPassword = ConvertToUnsecureString.ConvertToString(Password);
                user.password = plainPassword;

                userBLL.AddMethod(user);
                ErrorMessage = userBLL.ErrorMessage;
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
            userBLL.UpdateMethod(obj);
            ErrorMessage = userBLL.ErrorMessage;
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


        public void CheckExistingUserMethod(object obj)
        {
            User user = obj as User;
            if (user != null)
            {
                string plainPassword = ConvertToUnsecureString.ConvertToString(Password);
                user.password = plainPassword;

                userBLL.CheckExistingUser(user);
                ErrorMessage = userBLL.ErrorMessage;
            }
        }

        private ICommand logInCommand;
        public ICommand LogInCommand
        {
            get
            {
                if (logInCommand == null)
                {
                    logInCommand = new RelayCommand(CheckExistingUserMethod);
                }
                return logInCommand;
            }
        }

        public void DeleteMethod(object obj)
        {
            userBLL.DeleteMethod(obj);
            ErrorMessage = userBLL.ErrorMessage;
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
