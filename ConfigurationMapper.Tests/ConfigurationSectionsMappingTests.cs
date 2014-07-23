using System;
using ConfigurationMapper.Tests.ConfigSections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigurationMapper.Tests
{
	[TestClass]
	public class ConfigurationSectionsMappingTests
	{
		[TestMethod]
		public void ConfigSections_PersonsConfigSection_MappedSuccessfully()
		{
			var configuration = ConfigurationMapper<PersonConfigSectionTest>.Map();
			Assert.AreEqual(2, configuration.PersonConfiguration.PersonElements.Count);
			Assert.AreEqual("http://sometesturl.com", configuration.PersonConfiguration.SettingsElement.
				ServiceUrl);
		}

		[TestMethod, ExpectedException(typeof(ConfigurationSectionInvalidTypeException))]
		public void ConfigSections_PropertyWithInvalidType_ThrowsException()
		{
			var configuration = ConfigurationMapper<InvalidConfigSectionTypeTest>.Map();
		}

		/// <summary>
		/// Class for testing person config section mapping.
		/// </summary>
		private class PersonConfigSectionTest
		{
			[ConfigSection(Name = "personConfig")]
			public PersonConfigSection PersonConfiguration { get; set; }
		}

		/// <summary>
		/// Class for testing config section mapping with wrong type.
		/// </summary>
		private class InvalidConfigSectionTypeTest
		{
			[ConfigSection(Name = "personConfig")]
			public String PersonConfiguration { get; set; }
		}
	}
}
