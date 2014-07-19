using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace ConfigurationMapper
{
    public class ConfigurationMapper<T> where T : class, new()
    {
        public static T Map() 
        {
            return map(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));
        }
        
        public static T Map(string configPath)
        {
            var configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configPath;
            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, 
                ConfigurationUserLevel.None);

            return map(config);
        }

        private static T map(Configuration configuration)
        {
            var result = new T();
            var properties = result.GetType().GetProperties();
            var appSettings = configuration.AppSettings;
            
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttributes(typeof(AppSettingAttribute), false)
                    .FirstOrDefault() as AppSettingAttribute;
                var key = attribute != null && !String.IsNullOrWhiteSpace(attribute.Key)
                    ? attribute.Key : property.Name;
                var propertyType = property.PropertyType;
                var propertyValue = appSettings.Settings[key] == null 
                    ? null : appSettings.Settings[key].Value;
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
                else
                    if (propertyType.IsArray)
                    {
                        var delimiter = attribute != null && attribute.ArrayDelimiter != null
                            ? attribute.ArrayDelimiter
                            : ",";
                        var values = propertyValue.Split(new[] { delimiter }, StringSplitOptions.None);
                        var typeArray = Array.CreateInstance(propertyType.GetElementType(), values.Length);
                        for (var i = 0; i < values.Length; i++)
                            typeArray.SetValue(TypeDescriptor.GetConverter(propertyType.GetElementType())
                                    .ConvertFromString(null, cultureInfo, values[i]), i);
                        property.SetValue(result, typeArray);
                    }
            }

            return result;
        }
    }
}
