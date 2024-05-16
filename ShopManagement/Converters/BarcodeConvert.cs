using ShopManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShopManagement.Converters
{
    class BarcodeConvert : IMultiValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null)
            {
                string producerName = values[1].ToString();
                string productTypeName = values[2].ToString();

                int producerId = context.Producer
                .Where(p => p.name == producerName)
                .Select(p => p.id)
                .FirstOrDefault();

                int productTypeId = context.Product_Type
                    .Where(pt => pt.name == productTypeName)
                    .Select(pt => pt.id)

                    .FirstOrDefault();

                return new Barcode()
                {
                    value = values[0].ToString(),
                    producer_id = producerId,
                    product_type_id = productTypeId,
                    active = true
                };
            }
            else
            {
                return null;
            }
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
