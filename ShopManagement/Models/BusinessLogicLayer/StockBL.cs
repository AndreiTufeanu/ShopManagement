using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopManagement.Models.BusinessLogicLayer
{
    class StockBL
    {
        private ShopEntities context = new ShopEntities();
        public ObservableCollection<Product_Stock> StocksList { get; set; }
        public string ErrorMessage { get; set; }
        public event EventHandler<string> OperationCompleted;

        public StockBL()
        {
            StocksList = new ObservableCollection<Product_Stock>();
        }

        public void AddMethod(object obj)
        {
            Product_Stock stock = obj as Product_Stock;
            if (stock != null)
            {
                if (stock.supply_date == null)
                {
                    OperationCompleted?.Invoke(this, "You have to pick a supply date!");
                    return;
                }
                try
                {
                    context.InsertStock(stock.amount, stock.supply_date, stock.expiration_date, stock.price_per_unit, stock.barcode_id, stock.offer_id);
                    context.SaveChanges();
                    StocksList.Add(context.Product_Stock.OrderByDescending(s => s.id).FirstOrDefault());
                    if (stock.expiration_date < DateTime.Now)
                    {
                        Product_Stock addedStock = context.Product_Stock.OrderByDescending(s => s.id).FirstOrDefault();
                        context.DeactivateStock(addedStock.id);
                        context.SaveChanges();
                        StocksList.Remove(addedStock);
                    }
                    else
                    {
                        StocksList.Add(context.Product_Stock.OrderByDescending(s => s.id).FirstOrDefault());
                    }
                    OperationCompleted?.Invoke(this, $"Stock has been added successfully!");
                }
                catch (Exception)
                {
                    OperationCompleted?.Invoke(this, "Invalid stock! Try another one.");
                }
            }
            else
                OperationCompleted?.Invoke(this, "Invalid stock input!");
        }

        public void UpdateMethod(object obj)
        {
            Product_Stock stock = obj as Product_Stock;
            if (stock == null)
            {
                OperationCompleted?.Invoke(this, "No stock selected!");
                return;
            }
            else if (stock.supply_date == null || stock.supply_date == DateTime.MinValue)
            {
                OperationCompleted?.Invoke(this, "Supply date was not set correctly!");
                return;
            }
            else
            {
                double? oldPrice = context.Product_Stock
                    .Where(product_stock => product_stock.id == stock.id)
                    .Select(product_stock => product_stock.price_per_unit)
                    .FirstOrDefault();

                if (oldPrice != stock.price_per_unit && stock.supply_date < DateTime.Now)
                {
                    OperationCompleted?.Invoke(this, "Can't change buying price to a stock that has already arrived!");
                    return;
                }
            }
            try
            {
                context.ModifyStock(stock.id, stock.amount, stock.supply_date, stock.expiration_date, stock.price_per_unit, stock.selling_price_per_unit, stock.barcode_id, stock.offer_id);
                context.SaveChanges();
                OperationCompleted?.Invoke(this, "The stock was changed successfully!");
            }
            catch (Exception)
            {
                OperationCompleted?.Invoke(this, "Invalid stock data!");
            }
        }

        public void DeleteMethod(object obj)
        {
            Product_Stock stock = obj as Product_Stock;
            if (stock == null)
            {
                OperationCompleted?.Invoke(this, "You have to select a stock!");
            }
            else
            {
                context.DeactivateStock(stock.id);
                context.SaveChanges();
                StocksList.Remove(stock);
                OperationCompleted?.Invoke(this, $"Stock removed successfully!");
            }
        }

        public ObservableCollection<Product_Stock> GetAllStocks()
        {
            return new ObservableCollection<Product_Stock>(context.Product_Stock.ToList());
        }
    }
}
