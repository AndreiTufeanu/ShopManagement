using ShopManagement.Helpers;
using ShopManagement.ViewsForAdmin;
using ShopManagement.ViewsForAdmin.AdminMenuView;
using ShopManagement.ViewsForAdmin.OrganizationOfStore;
using ShopManagement.ViewsForAdmin.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopManagement.ViewModels
{
    class AdminWindowViewModel
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
                    ChangeCredentialsView changeCredentialsView = new ChangeCredentialsView();
                    changeCredentialsView.ShowDialog();
                    break;
                case "2":
                    CategoriesView categoriesView = new CategoriesView();
                    categoriesView.ShowDialog();
                    break;
                case "3":
                    ProductTypeView productTypeView = new ProductTypeView();
                    productTypeView.ShowDialog();
                    break;
                case "4":
                    ProducersView producersView = new ProducersView();
                    producersView.ShowDialog();
                    break;
                case "5":
                    ProducerProductsView producerProductsView = new ProducerProductsView();
                    producerProductsView.ShowDialog();
                    break;
                case "6":
                    StocksView stocksView = new StocksView();
                    stocksView.ShowDialog();
                    break;
                case "7":
                    ProductsView productsView = new ProductsView();
                    productsView.ShowDialog();
                    break;
                case "8":
                    ProducersStatsView producersStatsView = new ProducersStatsView();
                    producersStatsView.ShowDialog();
                    break;
                case "9":
                    CategoriesStatsView categoriesStatsView = new CategoriesStatsView();
                    categoriesStatsView.ShowDialog();
                    break;
                case "10":
                    SalesStatsView salesStatsView = new SalesStatsView();
                    salesStatsView.ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}