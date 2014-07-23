using System;
using System.Configuration;

namespace ConfigurationMapper.Tests.ConfigSections
{
	/// <summary>
	/// Person configuration element.
	/// </summary>
	public class PersonElement: ConfigurationElement
	{
		/// <summary>
		/// Person Id.
		/// </summary>
		[ConfigurationProperty("id", IsRequired=true)]
		public Guid Id
		{
			get
			{
				return (Guid)this["id"];
			}
		}

		/// <summary>
		/// Person first name.
		/// </summary>
		[ConfigurationProperty("firstName")]
		public string FirstName
		{
			get
			{
				return (string)this["firstName"];
			}
		}

		/// <summary>
		/// Person last name.
		/// </summary>
		[ConfigurationProperty("lastName")]
		public string LastName
		{
			get
			{
				return (string)this["lastName"];
			}
		}
	}
}
