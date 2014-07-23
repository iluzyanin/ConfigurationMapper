using System.Configuration;

namespace ConfigurationMapper.Tests.ConfigSections
{
	/// <summary>
	/// General settings.
	/// </summary>
	public class SettingsElement : ConfigurationElement
	{
		/// <summary>
		/// Service url.
		/// </summary>
		[ConfigurationProperty("serviceUrl", DefaultValue = "http://localhost:85", IsRequired = true)]
		public string ServiceUrl
		{
			get
			{
				return (string)this["serviceUrl"];
			}
		}

		/// <summary>
		/// Is service enabled.
		/// </summary>
		[ConfigurationProperty("isEnabled")]
		public bool IsEnabled
		{
			get
			{
				return (bool)this["isEnabled"];
			}
		}
	}
}
