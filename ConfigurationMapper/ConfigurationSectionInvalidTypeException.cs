using System;

namespace ConfigurationMapper
{
	/// <summary>
	/// Configuration section invalid type exception exception. Thrown when configuration section actual type
	/// differs from property type.
	/// </summary>
    public sealed class ConfigurationSectionInvalidTypeException: Exception
    {
		public ConfigurationSectionInvalidTypeException(Type propertyType, Type configSectionType)
            : base(String.Format(Errors.ConfigurationSectionTypeMismatch, propertyType, configSectionType))
        {
            PropertyType = propertyType;
			ConfigurationSectionType = configSectionType;
        }

		/// <summary>
		/// Property type.
		/// </summary>
		public Type PropertyType { get; set; }

		/// <summary>
		/// ConfigurationSectionType.
		/// </summary>
		public Type ConfigurationSectionType { get; set; }
    }
}
