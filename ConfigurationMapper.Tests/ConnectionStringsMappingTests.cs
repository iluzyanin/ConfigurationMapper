using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConfigurationMapper.Tests
{
	[TestClass]
	public class ConnectionStringsMappingTests
	{
		[TestMethod]
		public void ConnectionStrings_PlainConnectionString_MappedSuccessfully()
		{
			var connectionStrings = ConfigurationMapper<PlainConnectionString>.Map();
			Assert.AreEqual("Data Source=(local);Initial Catalog=TestDB;Integrated Security=True",
				connectionStrings.PlainCS);
		}

		[TestMethod]
		public void ConnectionStrings_SqlConnectionStringBuilder_MappedSuccessfully()
		{
			var connectionStrings = ConfigurationMapper<SqlConnectionString>.Map();
			Assert.AreEqual("TestDB", connectionStrings.ConnectionStringBuilder.InitialCatalog);
			Assert.AreEqual("(local)", connectionStrings.ConnectionStringBuilder.DataSource);
			Assert.AreEqual(true, connectionStrings.ConnectionStringBuilder.IntegratedSecurity);
		}
		
		[TestMethod]
		public void ConnectionStings_PropertyName_CanBeOverriden()
		{
			var connectionStrings = ConfigurationMapper<PropertyNameOverridenConnectionString>.Map();
			Assert.AreEqual("Data Source=(local);Initial Catalog=TestDB;Integrated Security=True",
				connectionStrings.EqualsProperty);
		}

		[TestMethod, ExpectedException(typeof(PropertyNotFoundException))]
		public void ConnectionStings_MissingRequiredProperty_ThrowException()
		{
			var appSettings = ConfigurationMapper<RequiredConnectionString>.Map();
		}

		[TestMethod]
		public void ConnectionStings_MissedRequiredPropertyWithDefaultValue_MappedSuccessfully()
		{
			var appSettings = ConfigurationMapper<RequiredDefaultConnectionString>.Map();
			Assert.AreEqual("Test connection string", appSettings.Required);
		}

		[TestMethod]
		public void ConnectionStings_SpecifiedConfigValue_OverridesDefault()
		{
			var appSettings = ConfigurationMapper<DefaultConnectionString>.Map();
			Assert.AreEqual("Data Source=(local);Initial Catalog=TestDB;Integrated Security=True", 
				appSettings.Default);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void ConnectionStings_InvalidDefaultValue_ThrowsException()
		{
			var appSettings = ConfigurationMapper<InvalidDefaultValueConnectionString>.Map();
		}

		#region Private classes for test puproses.
		/// <summary>
		/// Class for testing default connection string.
		/// </summary>
		private class PlainConnectionString
		{
			[ConnectionString]
			public string PlainCS { get; set; }
		}

		/// <summary>
		/// Class for testing mapping of property with SqlConnectionStringBuilder type.
		/// </summary>
		private class SqlConnectionString
		{
			[ConnectionString]
			public SqlConnectionStringBuilder ConnectionStringBuilder { get; set; }
		}

		/// <summary>
		/// Class for testing property name override.
		/// </summary>
		private class PropertyNameOverridenConnectionString
		{
			[ConnectionString(Name = "Equals")]
			public string EqualsProperty { get; set; }
		}

		/// <summary>
		/// Class for testing IsRequired flag for missing property.
		/// </summary>
		private class RequiredConnectionString
		{
			[ConnectionString(IsRequired = true)]
			public string Required { get; set; }
		}

		/// <summary>
		/// Class for testing IsRequired flag for missing property with default value.
		/// </summary>
		private class RequiredDefaultConnectionString
		{
			[ConnectionString(IsRequired = true, DefaultValue = "Test connection string")]
			public string Required { get; set; }
		}

		/// <summary>
		/// Class for testing that configuration value overrides default.
		/// </summary>
		private class DefaultConnectionString
		{
			[ConnectionString(DefaultValue = "Default value")]
			public string Default { get; set; }
		}

		/// <summary>
		/// Class for testing invalid default value.
		/// </summary>
		private class InvalidDefaultValueConnectionString
		{
			[ConnectionString(DefaultValue = "Test")]
			public SqlConnectionStringBuilder InvalidString { get; set; }
		}
		#endregion
	}
}
