using ShopManagement.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShopManagement.Converters
{
    class StockBarcodeToProductNameConvert : IValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            int barcodeId = (int)value;

            int productTypeId = context.Barcode
                .Where(barcode => barcode.id == barcodeId)
                .Select(barcode => barcode.product_type_id)
                .FirstOrDefault();

            string productTypeName = context.Product_Type
                .Where(productType => productType.id == productTypeId)
                .Select(productType => productType.name)
                .FirstOrDefault();

            return productTypeName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string productTypeName = (string)value;

            int productTypeId = context.Product_Type
                .Where(productType => productType.name == productTypeName)
                .Select(productType => productType.id)
                .FirstOrDefault();

            int barcodeId = context.Barcode
                .Where(barcode => barcode.product_type_id == productTypeId)
                .Select(barcode => barcode.id)
                .FirstOrDefault();

            return barcodeId;
        }
    }
}
