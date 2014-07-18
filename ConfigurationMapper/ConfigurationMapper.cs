using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace ConfigurationMapper
{
    public static class ConfigurationMapper
    {
        public static T Map<T>() where T : class, new()
        {            
            var result = new T();
            var properties = result.GetType().GetProperties();
            var appSettings = ConfigurationManager.AppSettings;

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(AppSettingAttribute), false)
                    .FirstOrDefault() as AppSettingAttribute;
                var key = attribute != null && !String.IsNullOrWhiteSpace(attribute.Key)
                    ? attribute.Key : property.Name;
                var propertyType = property.PropertyType;
                var propertyValue = appSettings[key];
                if (propertyValue == null)
                {
                    // If there is no app setting with such key and no instructions for it then just skip it.
                    if (attribute == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (attribute.IsRequired && attribute.DefaultValue == null)
                            throw new PropertyNotFoundException(key);

                        propertyValue = attribute.DefaultValue;
                    }
                }

                var cultureInfo = attribute != null && !String.IsNullOrWhiteSpace(attribute.CultureName)
                    ? CultureInfo.GetCultureInfo(attribute.CultureName)
                    : Thread.CurrentThread.CurrentCulture;

                if (propertyType.GetInterfaces().Contains(typeof(IConvertible)))
                {
                    property.SetValue(result, TypeDescriptor.GetConverter(propertyType)
                        .ConvertFromString(null, cultureInfo, propertyValue));
                }
            }

            return result;
        }
    }
}
