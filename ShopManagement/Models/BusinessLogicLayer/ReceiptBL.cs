using ShopManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Models.BusinessLogicLayer
{
    public class ReceiptBL
    {
        private ShopEntities context = new ShopEntities();
        public ObservableCollection<Receipt> ReceiptsList { get; set; }
        private Dictionary<DateTime?, DaySalesSummary> salesSummaries;
        public string ErrorMessage { get; set; }
        public event EventHandler<string> OperationCompleted;

        public ReceiptBL()
        {
            ReceiptsList = new ObservableCollection<Receipt>();
            salesSummaries = new Dictionary<DateTime?, DaySalesSummary>();
        }

        public void FilterMethod(object obj)
        {
            var properties = obj.GetType().GetProperties();
            string username = (string)properties[0].GetValue(obj);
            string year = (string)properties[1].GetValue(obj);
            string month = (string)properties[2].GetValue(obj);

            User searchedUser = context.User.Where(user => user.username == username).FirstOrDefault();

            if (searchedUser != null)
            {
                DateTime startDate = new DateTime(int.Parse(year), int.Parse(month), 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                var receiptsForMonth = context.Receipt.Where(r => r.date_of_purchase >= startDate && r.date_of_purchase <= endDate && r.cashier_id == searchedUser.id).ToList();

                salesSummaries = new Dictionary<DateTime?, DaySalesSummary>();

                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                {
                    salesSummaries[date] = new DaySalesSummary { Date = date, TotalSales = 0 };
                }

                foreach (var receipt in receiptsForMonth)
                {
                    var date = receipt.date_of_purchase;
                    var totalSales = GetTotalSalesForReceipt(receipt);
                    salesSummaries[date].TotalSales += totalSales;
                }
            }
        }

        public (string, string) BestReceiptMethod(object obj)
        {
            string cashierName = string.Empty;
            string description = string.Empty;
            if (obj is DateTime selectedDate)
            {
                int receiptId = (from r in context.Receipt
                                 where r.date_of_purchase == selectedDate
                                 join p in context.Product on r.id equals p.receipt_id
                                 group p by r.id into grouped
                                 let sum = grouped.Sum(p => p.selling_price)
                                 orderby sum descending
                                 select grouped.Key).FirstOrDefault();
                cashierName = context.Receipt
                              .Where(receipt => receipt.id == receiptId)
                              .FirstOrDefault()
                              .User
                              .username
                              .ToString();

                description = GetReceiptForDisplay(context.Receipt
                       .Where(receipt => receipt.id == receiptId)
                       .FirstOrDefault());

            }
            return (cashierName, description);
        }

        public void EmitReceiptMethod(object obj)
        {
            List<Product> productsOnReceipt = (obj as IEnumerable<Product>).ToList();
            if (productsOnReceipt.Count == 0)
                return;
            Receipt receipt = new Receipt
            {
                date_of_purchase = DateTime.Now,
                cashier_id = UserBL.loggedInUser.id
            };
            try
            {
                context.Receipt.Add(receipt);
                context.SaveChanges();
                receipt.id = context.Receipt.Max(item => item.id);
                ReceiptsList.Add(receipt);
            }
            catch (Exception)
            {
                context.Receipt.Remove(receipt);
                OperationCompleted?.Invoke(this, "Receipt failed to be created!");
                return;
            }
            for (int index = 0; index < productsOnReceipt.Count; index++)
            {
                Product product = productsOnReceipt[index];
                if (product != null)
                {
                    try
                    {
                        Product productToModify = context.Product.Find(product.id);
                        if (productToModify != null)
                        {
                            productToModify.receipt_id = receipt.id;
                            productToModify.active = false;
                        }
                        context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        OperationCompleted?.Invoke(this, "Receipt failed to add product!");
                    }
                }
            }
            OperationCompleted?.Invoke(this, GetReceiptForDisplay(receipt));
        }

        public string GetReceiptForDisplay(Receipt newReceipt)
        {
            string outputString = "Receipt\n\n";
            Dictionary<string, Tuple<int, double?, string>> counts = new Dictionary<string, Tuple<int, double?, string>>();
            if (newReceipt != null)
            {
                var productsOnReceipt = newReceipt.Product;
                if (productsOnReceipt != null)
                {
                    foreach (var product in productsOnReceipt)
                    {
                        string productName = product.Product_Stock.Barcode.Product_Type.name;
                        string producerName = product.Product_Stock.Barcode.Producer.name;
                        if (counts.ContainsKey(productName))
                        {
                            counts[productName] = Tuple.Create(counts[productName].Item1 + 1, counts[productName].Item2 + product.selling_price, producerName);
                        }
                        else
                        {
                            counts[productName] = Tuple.Create(1, product.selling_price, producerName);
                        }
                    }
                }
            }
            double? sum = 0.0d;
            foreach (var pairs in counts)
            {
                outputString += pairs.Value.Item1 + " x " + pairs.Key + $" ({pairs.Value.Item3})" + "....." + pairs.Value.Item2 + " Lei\n";
                sum += pairs.Value.Item2;
            }
            outputString += "Total: " + sum;
            return outputString;
        }

        public ObservableCollection<Receipt> GetAllReceipts()
        {
            return new ObservableCollection<Receipt>(context.Receipt.ToList());
        }

        public ObservableCollection<KeyValuePair<DateTime?, DaySalesSummary>> GetDailyStats()
        {
            return new ObservableCollection<KeyValuePair<DateTime?, DaySalesSummary>>(
                salesSummaries.Select(kv => new KeyValuePair<DateTime?, DaySalesSummary>(kv.Key, kv.Value))
            );
        }

        public class DaySalesSummary
        {
            public DateTime Date { get; set; }
            public double TotalSales { get; set; }
        }

        private double GetTotalSalesForReceipt(Receipt receipt)
        {
            double totalSales = 0;
            foreach (var product in receipt.Product)
            {
                totalSales += product.selling_price ?? 0;
            }
            return totalSales;
        }
    }
}
