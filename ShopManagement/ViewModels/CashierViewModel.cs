using ShopManagement.Helpers;
using ShopManagement.ViewsForAdmin.AdminMenuView;
using ShopManagement.ViewsForAdmin.OrganizationOfStore;
using ShopManagement.ViewsForCashier.CashierMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    class CashierViewModel
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
                    ProductsListView productsListView = new ProductsListView();
                    productsListView.ShowDialog();
                    break;
                case "2":
                    SellView sellView = new SellView();
                    sellView.ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}
