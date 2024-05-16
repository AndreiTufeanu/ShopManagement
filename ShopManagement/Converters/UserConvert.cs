using ShopManagement.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ShopManagement.Converters
{
    class UserConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string username)
            {
                if (values.Length == 2 && values[1] is string user_type)
                    return new User
                    {
                        username = username,
                        password = null,
                        user_type = user_type,
                        is_active = true
                    };
                else if (values.Length == 1)
                {
                    return new User
                    {
                        username = username,
                        password = null,
                        user_type = null,
                        is_active = true
                    };
                }
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
