using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopManagement.Models.BusinessLogicLayer
{
    public class ProductBL
    {
        private ShopEntities context = new ShopEntities();
        public ObservableCollection<Product> ProductsList { get; set; }
        public string ErrorMessage { get; set; }
        public event EventHandler<string> OperationCompleted;

        public ProductBL()
        {
            ProductsList = new ObservableCollection<Product>();
        }

        public void FilterProducerMethod(object obj)
        {
            string producerName = obj as string;
            var query = from p in context.Product
                        where p.active == true
                        join s in context.Product_Stock on p.stock_id equals s.id
                        join b in context.Barcode on s.barcode_id equals b.id
                        join pr in context.Producer on b.producer_id equals pr.id
                        where pr.name.StartsWith(producerName)
                        group p by b.product_type_id into g
                        select g;

            if (query != null)
            {

                ProductsList.Clear();
                var products = query.ToList();
                foreach (var group in products)
                {
                    ProductsList.Add(group.First());
                }
            }
        }

        public void FilterMethod(object obj)
        {
            var properties = obj.GetType().GetProperties();
            string filterText;
            string filterType;
            filterText = (string)properties[0].GetValue(obj);
            filterType = (string)properties[1].GetValue(obj);

            IQueryable<Product> query = null;

            switch (filterType)
            {
                case "Name":
                    query = from p in context.Product
                            where p.active == true
                            join s in context.Product_Stock on p.stock_id equals s.id
                            where s.supply_date < DateTime.Now
                            join b in context.Barcode on s.barcode_id equals b.id
                            join pt in context.Product_Type on b.product_type_id equals pt.id
                            where pt.name.StartsWith(filterText)
                            select p;
                    break;
                case "Producer":
                    query = from p in context.Product
                            where p.active == true
                            join s in context.Product_Stock on p.stock_id equals s.id
                            where s.supply_date < DateTime.Now
                            join b in context.Barcode on s.barcode_id equals b.id
                            join pt in context.Producer on b.producer_id equals pt.id
                            where pt.name.StartsWith(filterText)
                            select p;
                    break;
                case "Expiration date":
                    if (string.IsNullOrEmpty(filterText))
                        return;
                    DateTime filterDate = DateTime.Parse(filterText).Date;
                    query = from p in context.Product
                            where p.active == true
                            join s in context.Product_Stock on p.stock_id equals s.id
                            where s.expiration_date == filterDate.Date
                            select p;
                    break;
                case "Barcode":
                    query = from p in context.Product
                            where p.active == true
                            join s in context.Product_Stock on p.stock_id equals s.id
                            where s.supply_date < DateTime.Now
                            join b in context.Barcode on s.barcode_id equals b.id
                            where b.value.StartsWith(filterText)
                            select p;
                    break;
                case "Category":
                    query = from p in context.Product
                            where p.active == true
                            join s in context.Product_Stock on p.stock_id equals s.id
                            where s.supply_date < DateTime.Now
                            join b in context.Barcode on s.barcode_id equals b.id
                            join pt in context.Product_Type on b.product_type_id equals pt.id
                            join c in context.Category on pt.category_id equals c.id
                            where c.name.StartsWith(filterText)
                            select p;
                    break;
                default:
                    break;
            };
            if (query != null)
            {
                var filteredProducts = query.ToList();
                ProductsList.Clear();
                foreach (var product in filteredProducts)
                {
                    ProductsList.Add(product);
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Product product = obj as Product;
            if (product == null)
            {
                OperationCompleted?.Invoke(this, "No product selected!");
                return;
            }
            else
            {
                double? buyingPrice = context.Product_Stock
                    .Where(ps => ps.id == product.stock_id)
                    .Select(ps => ps.price_per_unit)
                    .FirstOrDefault();

                if (buyingPrice > product.selling_price)
                {
                    OperationCompleted?.Invoke(this, "Invalid price!");
                    return;
                }
            }
            try
            {
                context.ModifyProduct(product.id, product.selling_price);
                context.SaveChanges();
                OperationCompleted?.Invoke(this, "The product price was changed successfully!");
            }
            catch (Exception)
            {
                OperationCompleted?.Invoke(this, "Invalid product price!");
            }
        }

        public void DeleteMethod(object obj)
        {
            Product product = obj as Product;
            if (product == null)
            {
                OperationCompleted?.Invoke(this, "You have to select a product!");
            }
            else
            {
                context.DeactivateProduct(product.id);
                context.SaveChanges();
                ProductsList.Remove(product);
                OperationCompleted?.Invoke(this, $"Product removed successfully!");
            }
        }

        public ObservableCollection<Product> GetAllProducts()
        {
            DateTime currentDate = DateTime.Now;
            var query = from p in context.Product
                        where p.active == true
                        join s in context.Product_Stock on p.stock_id equals s.id
                        where s.supply_date <= currentDate
                        select p;
            return new ObservableCollection<Product>(query.ToList());
        }
    }
}
