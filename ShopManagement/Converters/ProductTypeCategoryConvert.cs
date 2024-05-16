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
    public class ProductTypeCategoryConvert : IValueConverter
    {
        private ShopEntities context = new ShopEntities();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            int categoryId = (int)value;

            string categoryName = context.Category
                .Where(category => category.id == categoryId)
                .Select(category => category.name)
                .FirstOrDefault();

            return categoryName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string categoryName = (string)value;

            int categoryId = context.Category
                .Where(category => category.name == categoryName)
                .Select(category => category.id)
                .FirstOrDefault();

            return categoryId;
        }
    }
}
