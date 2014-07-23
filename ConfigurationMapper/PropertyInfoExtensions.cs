using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ConfigurationMapper
{
	/// <summary>
	/// PropertyInfo extension methods.
	/// </summary>
	public static class PropertyInfoExtensions
	{
		/// <summary>
		/// Sets the "app setting" property value of a specified object.
		/// </summary>
		/// <param name="property">Property to set.</param>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="appSettings">AppSettings section where to look for property value.</param>
		public static void SetAppSettingValue(this PropertyInfo property, object obj,
			AppSettingsSection appSettings)
		{
			var appSettingAttribute = property.GetCustomAttributes(typeof(AppSettingAttribute), false)
				.FirstOrDefault() as AppSettingAttribute;
			if (appSettingAttribute == null)
				return;
			var key = String.IsNullOrWhiteSpace(appSettingAttribute.Key)
				? property.Name : appSettingAttribute.Key;
			var propertyType = property.PropertyType;
			var propertyValue = appSettings.Settings[key] == null
				? null : appSettings.Settings[key].Value;
			if (propertyValue == null)
			{
				if (appSettingAttribute.IsRequired && appSettingAttribute.DefaultValue == null)
					throw new PropertyNotFoundException(key);

				propertyValue = appSettingAttribute.DefaultValue;
			}

			var cultureInfo = String.IsNullOrWhiteSpace(appSettingAttribute.CultureName)
				? Thread.CurrentThread.CurrentCulture
				: CultureInfo.GetCultureInfo(appSettingAttribute.CultureName);

			if (propertyType.GetInterfaces().Contains(typeof(IConvertible)))
			{
				property.SetValue(obj, TypeDescriptor.GetConverter(propertyType)
					.ConvertFromString(null, cultureInfo, propertyValue));
			}
			if (propertyType.IsArray)
			{
				var delimiter = appSettingAttribute.ArrayDelimiter != null
					? appSettingAttribute.ArrayDelimiter
					: ",";
				var values = propertyValue.Split(new[] { delimiter }, StringSplitOptions.None);
				var array = Array.CreateInstance(propertyType.GetElementType(), values.Length);
				for (var i = 0; i < values.Length; i++)
					array.SetValue(TypeDescriptor.GetConverter(propertyType.GetElementType())
							.ConvertFromString(null, cultureInfo, values[i]), i);
				property.SetValue(obj, array);
			}
		}

		/// <summary>
		/// Sets the "connection string" property value of a specified object.
		/// </summary>
		/// <param name="property">Property to set.</param>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="connectionStrings">ConnectionStrings section where to look for property 
		/// value.</param>
		public static void SetConnectionStringValue(this PropertyInfo property, object obj,
			ConnectionStringsSection connectionStrings)
		{
			var connectionStringsAttribute = property.GetCustomAttributes(
				typeof(ConnectionStringAttribute), false).FirstOrDefault() as ConnectionStringAttribute;

			if (connectionStringsAttribute == null)
				return;
			var name = String.IsNullOrWhiteSpace(connectionStringsAttribute.Name)
				? property.Name : connectionStringsAttribute.Name;
			var propertyType = property.PropertyType;
			var propertyValue = connectionStrings.ConnectionStrings[name] == null
				? null : connectionStrings.ConnectionStrings[name].ConnectionString;
			if (propertyValue == null)
			{
				if (connectionStringsAttribute.IsRequired &&
					connectionStringsAttribute.DefaultValue == null)
					throw new PropertyNotFoundException(name);

				propertyValue = connectionStringsAttribute.DefaultValue;
			}

			if (propertyType == typeof(SqlConnectionStringBuilder))
			{
				property.SetValue(obj, new SqlConnectionStringBuilder(propertyValue));
			}
			else
			{
				property.SetValue(obj, propertyValue);
			}
		}

		/// <summary>
		/// Sets the "configuration section" property value of a specified object.
		/// </summary>
		/// <param name="property">Property to set.</param>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="connectionStrings">Configuration section to assign.</param>
		public static void SetConfigSectionValue(this PropertyInfo property, object obj, Configuration 
			configuration)
		{
			var configSectionAttribute = property.GetCustomAttributes(
				typeof(ConfigSectionAttribute), false).FirstOrDefault() as ConfigSectionAttribute;

			if (configSectionAttribute == null)
				return;

			var name = String.IsNullOrWhiteSpace(configSectionAttribute.Name)
				? property.Name : configSectionAttribute.Name;
			var propertyType = property.PropertyType;
			var propertyValue = configuration.GetSection(name);
			if (propertyValue != null)
			{
				if (propertyValue.GetType() != propertyType)
				{
					throw new ConfigurationSectionInvalidTypeException(propertyValue.GetType(), 
						propertyType);
				}
				property.SetValue(obj, propertyValue);
			}
			else
			{
				if (configSectionAttribute.IsRequired)
					throw new PropertyNotFoundException(name);
			}
		}
	}
}
