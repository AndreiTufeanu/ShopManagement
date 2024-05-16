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
    class ProductStockToExpirationDateConvert : IValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            int stockId = (int)value;

            DateTime? expDate = context.Product_Stock
                .Where(pt => pt.id == stockId)
                .Select(pt => pt.expiration_date)
                .FirstOrDefault();

            string formattedExpDate = expDate?.ToString("yyyy-MM-dd");
            return formattedExpDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
