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
    class StockConvert : IMultiValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[3] != null && values[4] != null && values[5] != null)
            {
                string producerName = values[4].ToString();
                string productTypeName = values[5].ToString();

                int producerId = context.Producer
                    .Where(producer => producer.name == producerName)
                    .Select(producer => producer.id)
                    .FirstOrDefault();

                int productTypeId = context.Product_Type
                    .Where(productType => productType.name == productTypeName)
                    .Select(productType => productType.id)
                    .FirstOrDefault();

                int barcodeId = context.Barcode
                    .Where(barcode => barcode.product_type_id == productTypeId
                                   && barcode.producer_id == producerId)
                    .Select(barcode => barcode.id)
                    .FirstOrDefault();

                try
                {
                    return new Product_Stock()
                    {
                        amount = int.Parse(values[0].ToString()),
                        supply_date = DateTime.Parse(values[1].ToString()),
                        expiration_date = DateTime.TryParse(values[2].ToString(), out DateTime expirationDate) ? (DateTime?)expirationDate : null,
                        price_per_unit = double.Parse(values[3].ToString()),
                        selling_price_per_unit = 0.0f,
                        barcode_id = barcodeId,
                        offer_id = null,
                        active = true
                    };
                }
                catch (Exception)
                {
                    return null;
                }
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
