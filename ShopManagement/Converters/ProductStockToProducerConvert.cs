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
    class ProductStockToProducerConvert : IValueConverter
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

            int producerId = context.Barcode
                .Where(barcode => barcode.id == barcodeId)
                .Select(barcode => barcode.producer_id)
                .FirstOrDefault();

            string producerName = context.Producer
                .Where(producer => producer.id == producerId)
                .Select(producer => producer.name)
                .FirstOrDefault();

            return producerName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }
    }
}
