using ShopManagement.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    class MainWindowVM
    {
        private ICommand openWindowCommand;
        public ICommand OpenWindowCommand
        {
            get
            {
                if (openWindowCommand == null)
                {
                    openWindowCommand = new RelayCommand(OpenWindow);
                }
                return openWindowCommand;
            }
        }

        public void OpenWindow(object obj)
        {
            string nr = obj as string;
            switch (nr)
            {
                case "1":
                    CreateAccountView createAccountView = new CreateAccountView();
                    createAccountView.ShowDialog();
                    break;
                case "2":
                    LogInView logInView = new LogInView();
                    logInView.ShowDialog();
                    break;
            }
        }
    }
}
