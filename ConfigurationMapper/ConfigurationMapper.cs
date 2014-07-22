using System.Configuration;
using System.IO;

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
			var connectionStrings = configuration.ConnectionStrings;
            
            foreach (var property in properties)
            {
				property.SetAppSettingValue(result, appSettings);
				property.SetConnectionStringValue(result, connectionStrings);
            }

            return result;
        }
    }
}
