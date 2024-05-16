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
    class BarcodeProductTypeConvert : IValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            int productTypeId = (int)value;

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
                .Select(productType => productType.id).
                FirstOrDefault();

            return productTypeId;
        }
    }
}
