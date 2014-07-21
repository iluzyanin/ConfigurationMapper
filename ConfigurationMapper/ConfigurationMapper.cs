using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
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
            if (!File.Exists(configPath))
                throw new FileNotFoundException(Errors.ConfigurationFileNotFound, configPath);
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
                var appSettingsAttribute = property.GetCustomAttributes(typeof(AppSettingAttribute), false)
                    .FirstOrDefault() as AppSettingAttribute;
                // AppSetting attribute is mandatory for property to be mapped.
                if (appSettingsAttribute == null)
                    continue;

                var key = String.IsNullOrWhiteSpace(appSettingsAttribute.Key)
                    ? property.Name : appSettingsAttribute.Key;
                var propertyType = property.PropertyType;
                var propertyValue = appSettings.Settings[key] == null 
                    ? null : appSettings.Settings[key].Value;
                if (propertyValue == null)
                {
                    if (appSettingsAttribute.IsRequired && appSettingsAttribute.DefaultValue == null)
                        throw new PropertyNotFoundException(key);

                    propertyValue = appSettingsAttribute.DefaultValue;
                }

                var cultureInfo = String.IsNullOrWhiteSpace(appSettingsAttribute.CultureName)
                    ? Thread.CurrentThread.CurrentCulture
                    : CultureInfo.GetCultureInfo(appSettingsAttribute.CultureName);

                if (propertyType.GetInterfaces().Contains(typeof(IConvertible)))
                {
                    property.SetValue(result, TypeDescriptor.GetConverter(propertyType)
                        .ConvertFromString(null, cultureInfo, propertyValue));
                }
                else
                    if (propertyType.IsArray)
                    {
                        var delimiter = appSettingsAttribute.ArrayDelimiter != null
                            ? appSettingsAttribute.ArrayDelimiter
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
