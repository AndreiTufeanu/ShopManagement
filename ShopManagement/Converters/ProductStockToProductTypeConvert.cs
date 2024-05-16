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
    class ProductStockToProductTypeConvert : IValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            int stockId = (int)value;

            int barcodeId = context.Product_Stock
                .Where(stock => stock.id == stockId)
                .Select(stock => stock.barcode_id)
                .FirstOrDefault();

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
            throw new NotImplementedException();
        }
    }
}
