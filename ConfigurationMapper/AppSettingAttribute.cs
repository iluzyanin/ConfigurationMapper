using System;

namespace ConfigurationMapper
{
    /// <summary>
    /// Attribute for customizing mapping process.
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
        /// Name of culture used in DateTime property mapping. Default is current thread culture.
        /// Ignored for non-DateTime properties.
        /// </summary>
        public string CultureName {get; set;}       
    }
}