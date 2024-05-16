using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Models.BusinessLogicLayer
{
    public class BarcodeBL
    {
        private ShopEntities context = new ShopEntities();
        public ObservableCollection<Barcode> BarcodesList { get; set; }
        public string ErrorMessage { get; set; }
        public event EventHandler<string> OperationCompleted;

        public BarcodeBL()
        {
            BarcodesList = new ObservableCollection<Barcode>();
        }

        public void AddMethod(object obj)
        {
            Barcode barcode = obj as Barcode;
            if (barcode != null)
            {
                if (string.IsNullOrEmpty(barcode.value))
                {
                    OperationCompleted?.Invoke(this, "You have to pick a barcode!");
                    return;
                }
                try
                {
                    context.Barcode.Add(barcode);
                    context.SaveChanges();
                    barcode.id = context.Barcode.Max(item => item.id);
                    BarcodesList.Add(barcode);
                    OperationCompleted?.Invoke(this, $"Barcode {barcode.value} has been added successfully!");
                }
                catch (Exception)
                {
                    OperationCompleted?.Invoke(this, "Invalid barcode! Try another one.");
                    context.Barcode.Remove(barcode);
                }
            }
        }

        public void UpdateMethod(object obj)
        {
            Barcode barcode = obj as Barcode;
            if (barcode == null)
            {
                OperationCompleted?.Invoke(this, "No barcode selected!");
                return;
            }
            else if (string.IsNullOrEmpty(barcode.value))
            {
                OperationCompleted?.Invoke(this, "Barcode value can't be null!");
                return;
            }
            try
            {
                context.ModifyBarcodeData(barcode.id, barcode.value, barcode.producer_id, barcode.product_type_id);
                context.SaveChanges();
                OperationCompleted?.Invoke(this, "The barcode name was changed successfully!");
            }
            catch (Exception)
            {
                OperationCompleted?.Invoke(this, "Invalid barcode!");
            }
        }

        public void DeleteMethod(object obj)
        {
            Barcode barcode = obj as Barcode;
            if (barcode == null)
            {
                OperationCompleted?.Invoke(this, "You have to select a barcode!");
            }
            else
            {
                context.DeactivateBarcode(barcode.id);
                context.SaveChanges();
                BarcodesList.Remove(barcode);
                OperationCompleted?.Invoke(this, $"Barcode {barcode.value} removed successfully!");
            }
        }

        public ObservableCollection<Barcode> GetAllBarcodes()
        {
            return new ObservableCollection<Barcode>(context.Barcode.ToList());
        }
    }
}
