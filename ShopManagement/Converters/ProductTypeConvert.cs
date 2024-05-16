using ShopManagement.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShopManagement.Converters
{
    class ProductTypeConvert : IMultiValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null)
            {
                string categoryName = values[2].ToString();

                int categoryId = context.Category
                    .Where(category => category.name == categoryName)
                    .Select(category => category.id)
                    .FirstOrDefault();

                return new Product_Type()
                {
                    name = values[0].ToString(),
                    unit = values[1].ToString(),
                    category_id = categoryId,
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
