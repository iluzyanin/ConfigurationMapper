using System;

namespace ConfigurationMapper
{
	/// <summary>
	/// Attribute for configuration sections mapping.
	/// </summary>
	public class ConfigSectionAttribute: Attribute
	{
		/// <summary>
		/// Configuration section name. If specified, overrides property name in configSections lookup
		/// process.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Determines whether the property is required.
		/// </summary>
		public bool IsRequired { get; set; }
	}
}
