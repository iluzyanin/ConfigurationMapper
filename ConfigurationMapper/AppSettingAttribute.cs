using System;

namespace ConfigurationMapper
{
    /// <summary>
    /// Attribute for app settings mapping.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AppSettingAttribute : Attribute
    {
        /// <summary>
        /// AppSettings "key" of mapped property. If specified, overrides property name in appSettings lookup
        /// process.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Name of culture used in properties mapping. Default is current thread culture.
        /// </summary>
        public string CultureName { get; set; }

        /// <summary>
        /// Determines whether the property is required.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Default value. Must be convertible to property type, otherwise exception will be thown on mapping
        /// stage.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// Array delimiter. Default is comma. Ignored for non-array properties.
        /// </summary>
        public string ArrayDelimiter { get; set; }
    }
}