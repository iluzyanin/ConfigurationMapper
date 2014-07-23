using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationMapper.Tests.ConfigSections
{
	/// <summary>
	/// Person configuration section.
	/// </summary>
	public class PersonConfigSection: ConfigurationSection
	{
		/// <summary>
		/// Collection of elements.
		/// </summary>
		[ConfigurationProperty("persons")]
		[ConfigurationCollection(typeof(PersonElementCollection),
			AddItemName = "add",
			ClearItemsName = "clear",
			RemoveItemName = "remove")]
		public PersonElementCollection PersonElements
		{
			get
			{
				return (PersonElementCollection)this["persons"];
			}
		}

		/// <summary>
		/// Element with general settings.
		/// </summary>
		[ConfigurationProperty("settings")]
		public SettingsElement SettingsElement
		{
			get
			{
				return (SettingsElement)this["settings"];
			}
		}
	}
}
