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
    class SumPerCategroyConvert : IValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            int categoryId = (int)value;

            double? categorySum = (from c in context.Category
                                   join pt in context.Product_Type on c.id equals pt.category_id
                                   join b in context.Barcode on pt.id equals b.product_type_id
                                   join s in context.Product_Stock on b.id equals s.barcode_id
                                   where c.id == categoryId && s.supply_date < DateTime.Now
                                   select s.selling_price_per_unit).Sum();

            return categorySum;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
