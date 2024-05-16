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
    class BarcodeProducerConvert : IValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            int producerId = (int)value;

            string producerName = context.Producer
                .Where(producer => producer.id == producerId)
                .Select(producer => producer.name)
                .FirstOrDefault();

            return producerName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string producerName = (string)value;

            int producerId = context.Producer
                .Where(producer => producer.name == producerName)
                .Select(producer => producer.id)
                .FirstOrDefault();

            return producerId;
        }
    }
}
