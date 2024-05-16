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
    public class ProductTypeBL
    {
        private ShopEntities context = new ShopEntities();
        public ObservableCollection<Product_Type> ProductTypesList { get; set; }
        public string ErrorMessage { get; set; }
        public event EventHandler<string> OperationCompleted;

        public ProductTypeBL()
        {
            ProductTypesList = new ObservableCollection<Product_Type>();
        }

        public void AddMethod(object obj)
        {
            Product_Type productType = obj as Product_Type;
            if (productType != null)
            {
                if (string.IsNullOrEmpty(productType.name))
                {
                    OperationCompleted?.Invoke(this, "You have to name your product type!");
                    return;
                }
                else if (string.IsNullOrEmpty(productType.unit))
                {
                    OperationCompleted?.Invoke(this, "You have to specify the unit of measurement!");
                    return;
                }
                try
                {
                    context.Product_Type.Add(productType);
                    context.SaveChanges();
                    productType.id = context.Product_Type.Max(item => item.id);
                    ProductTypesList.Add(productType);
                    OperationCompleted?.Invoke(this, $"Product type {productType.name} has been added successfully!");
                }
                catch (Exception)
                {
                    OperationCompleted?.Invoke(this, "Invalid product type name! Try another one.");
                    context.Product_Type.Remove(productType);
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Product_Type productType = obj as Product_Type;
            if (productType == null)
            {
                OperationCompleted?.Invoke(this, "No product type selected!");
                return;
            }
            else if (string.IsNullOrEmpty(productType.name))
            {
                OperationCompleted?.Invoke(this, "Product type name can't be null!");
                return;
            }
            else if (string.IsNullOrEmpty(productType.unit))
            {
                OperationCompleted?.Invoke(this, "Unit of measurement can't be null!");
                return;
            }
            try
            {
                context.ModifyProductType(productType.id, productType.name, productType.unit, productType.category_id);
                context.SaveChanges();
                OperationCompleted?.Invoke(this, "Product type changed successfully!");
            }
            catch (Exception)
            {
                OperationCompleted?.Invoke(this, "Invalid product type data!");
            }
        }

        public void DeleteMethod(object obj)
        {
            Product_Type product_Type = obj as Product_Type;
            if (product_Type == null)
            {
                OperationCompleted?.Invoke(this, "You have to select a product type!");
            }
            else
            {
                context.DeactivateProductType(product_Type.id);
                context.SaveChanges();
                ProductTypesList.Remove(product_Type);
                OperationCompleted?.Invoke(this, $"{product_Type.name} removed successfully!");
            }
        }

        public ObservableCollection<Product_Type> GetAllProductTypes()
        {
            return new ObservableCollection<Product_Type>(context.Product_Type.ToList());
        }
    }
}
