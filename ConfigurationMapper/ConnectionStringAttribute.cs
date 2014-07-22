using System;

namespace ConfigurationMapper
{
	/// <summary>
	/// Attribute for connection stringsx mapping.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ConnectionStringAttribute: Attribute
    {
        /// <summary>
        /// Connection string name. If specified, overrides property name in connection string lookup 
        /// process.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines whether the property is required.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Default value. Must be convertible to property type, otherwise exception will be thown on mapping
        /// stage.
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
