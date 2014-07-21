using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading;

namespace ConfigurationMapper.Tests
{
    [TestClass]
    public class AppSettingsMappingTests
    {
        public AppSettingsMappingTests()
        {
            // Make "en-US" culture the default.
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [TestMethod]
        public void AppSettings_PropertyNames_AreCaseInsensitive()
        {
            var appSettings = ConfigurationMapper<CaseInsensitiveAppSettings>.Map();
            Assert.IsTrue(appSettings.IsCaseInsensitive);
        }

        [TestMethod]
        public void AppSettings_PropertyNames_CanBeOverriden()
        {
            var appSettings = ConfigurationMapper<PropertyNameOverridenAppSettings>.Map();
            Assert.IsTrue(appSettings.EqualsProperty);
        }

        [TestMethod, ExpectedException(typeof(PropertyNotFoundException))]
        public void AppSettings_MissingRequiredProperty_ThrowException()
        {
            var appSettings = ConfigurationMapper<RequiredAppSettings>.Map();
        }

        [TestMethod]
        public void AppSettings_MissedRequiredPropertyWithDefaultValue_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<RequiredDefaultAppSettings>.Map();
            Assert.AreEqual(42, appSettings.Required);
        }

        [TestMethod]
        public void AppSettings_SpecifiedConfigValue_OverridesDefault()
        {
            var appSettings = ConfigurationMapper<DefaultAppSettings>.Map();
            Assert.AreEqual("Configuration value", appSettings.Default);
        }

        [TestMethod, ExpectedException(typeof(FormatException))]
        public void AppSettings_InvalidDefaultValue_ThrowsException()
        {
            var appSettings = ConfigurationMapper<InvalidDefaultAppSettings>.Map();
        }

        [TestMethod]
        public void AppSettings_ArrayProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<ArrayAppSettings>.Map();
            Assert.AreEqual(3, appSettings.Int32Array.Length);
            Assert.AreEqual(2, appSettings.Int32Array[0]);
            Assert.AreEqual(5, appSettings.Int32Array[1]);
            Assert.AreEqual(42, appSettings.Int32Array[2]);
        }

        [TestMethod]
        public void AppSettings_ArrayPropertiesWithCustomDelimiter_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<ArrayCustomDelimiterAppSettings>.Map();
            Assert.AreEqual(3, appSettings.Int32ArrayCustomDelimiter.Length);
            Assert.AreEqual(2, appSettings.Int32ArrayCustomDelimiter[0]);
            Assert.AreEqual(5, appSettings.Int32ArrayCustomDelimiter[1]);
            Assert.AreEqual(42, appSettings.Int32ArrayCustomDelimiter[2]);
        }

        [TestMethod]
        public void AppSettings_ArrayPropertiesWithDefaultValues_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<ArrayDefaultAppSettings>.Map();
            Assert.AreEqual(4, appSettings.BooleanDefaultArray.Length);
            Assert.IsTrue(appSettings.BooleanDefaultArray[0]);
            Assert.IsTrue(appSettings.BooleanDefaultArray[1]);
            Assert.IsFalse(appSettings.BooleanDefaultArray[2]);
            Assert.IsFalse(appSettings.BooleanDefaultArray[3]);
        }

        [TestMethod]
        public void AppSettings_BooleanProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<BooleanAppSettings>.Map();
            Assert.IsTrue(appSettings.BooleanUpperCase);
            Assert.IsTrue(appSettings.BooleanLowerCase);
            Assert.IsTrue(appSettings.BooleanMixedCase);
            Assert.IsFalse(appSettings.BooleanFalse);
        }

        [TestMethod]
        public void AppSettings_DateTimeProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<DateTimeAppSettings>.Map();
            Assert.AreEqual(new DateTime(2014, 07, 15, 18, 48, 12), appSettings.DateTimeDefault);
            Assert.AreEqual(new DateTime(2014, 07, 15, 18, 48, 12), appSettings.DateTimeOnlyCulture);
            Assert.AreEqual(new DateTime(2014, 07, 15), appSettings.DateTimeEnShortDate);
            Assert.AreEqual(new DateTime(2014, 07, 15), appSettings.DateTimeRuShortDate);
            Assert.AreEqual(new DateTime(2014, 07, 15), appSettings.DateTimeEnCustomFormat);
        }

        [TestMethod]
        public void AppSettings_DoubleProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<DoubleAppSettings>.Map();
            Assert.AreEqual(12.34, appSettings.DoubleEn);
            Assert.AreEqual(12.34, appSettings.DoubleRu);
            Assert.AreEqual(1.7E+3, appSettings.DoubleEForm);
        }

        [TestMethod]
        public void AppSettings_SingleProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<SingleAppSettings>.Map();
            Assert.AreEqual(12.34f, appSettings.SingleEn);
            Assert.AreEqual(12.34f, appSettings.SingleRu);
            Assert.AreEqual(1.7E+3f, appSettings.SingleEForm);
        }

        [TestMethod]
        public void AppSettings_SByteProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<IntegerAppSetting<SByte>>.Map();
            Assert.AreEqual(42, appSettings.PositiveInteger);
            Assert.AreEqual(-42, appSettings.NegativeInteger);
        }

        [TestMethod]
        public void AppSettings_DecimalProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<IntegerAppSetting<Decimal>>.Map();
            Assert.AreEqual(42, appSettings.PositiveInteger);
            Assert.AreEqual(-42, appSettings.NegativeInteger);
        }

        [TestMethod]
        public void AppSettings_UInt16Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<UIntegerAppSetting<UInt16>>.Map();
            Assert.AreEqual(42, appSettings.PositiveInteger);
        }

        [TestMethod]
        public void AppSettings_UInt32Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<UIntegerAppSetting<UInt32>>.Map();
            Assert.AreEqual(42U, appSettings.PositiveInteger);
        }


        [TestMethod]
        public void AppSettings_Int32Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<IntegerAppSetting<Int32>>.Map();
            Assert.AreEqual(42, appSettings.PositiveInteger);
            Assert.AreEqual(-42, appSettings.NegativeInteger);
        }

        [TestMethod]
        public void AppSettings_UInt64Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<UIntegerAppSetting<UInt64>>.Map();
            Assert.AreEqual(42UL, appSettings.PositiveInteger);
        }


        [TestMethod]
        public void AppSettings_Int64Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<IntegerAppSetting<Int64>>.Map();
            Assert.AreEqual(42, appSettings.PositiveInteger);
            Assert.AreEqual(-42, appSettings.NegativeInteger);
        }

        [TestMethod]
        public void AppSettings_CharProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<CharAppSettings>.Map();
            Assert.AreEqual('c', appSettings.SimpleChar);
        }

        [TestMethod]
        public void AppSettings_StringProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper<StringAppSettings>.Map();
            Assert.AreEqual("Test string", appSettings.SimpleString);
            Assert.AreEqual(String.Empty, appSettings.EmptyString);
        }

        #region Private classes for test puproses.
        /// <summary>
        /// Class for testing properties case insensitivity.
        /// </summary>
        private class CaseInsensitiveAppSettings
        {
            /// <summary>
            /// Property with name in different case than in appSettings.
            /// </summary>
            [AppSetting]
            public Boolean IsCaseInsensitive { get; set; }
        }

        /// <summary>
        /// Class for testing property name override.
        /// </summary>
        private class PropertyNameOverridenAppSettings
        {
            /// <summary>
            /// Property with overriden name.
            /// </summary>
            [AppSetting(Key = "Equals")]
            public Boolean EqualsProperty { get; set; }
        }

        /// <summary>
        /// Class for testing IsRequired flag for missing property.
        /// </summary>
        private class RequiredAppSettings
        {
            [AppSetting(IsRequired = true)]
            public Int32 Required { get; set; }
        }

        /// <summary>
        /// Class for testing IsRequired flag for missing property with default value.
        /// </summary>
        private class RequiredDefaultAppSettings
        {
            [AppSetting(IsRequired = true, DefaultValue = "42")]
            public Int32 Required { get; set; }
        }

        /// <summary>
        /// Class for testing that configuration value overrides default.
        /// </summary>
        private class DefaultAppSettings
        {
            [AppSetting(DefaultValue = "Default value")]
            public string Default { get; set; }
        }

        /// <summary>
        /// Class for testing invalid default value.
        /// </summary>
        private class InvalidDefaultAppSettings
        {
            [AppSetting(DefaultValue = "Test")]
            public bool InvalidBoolean { get; set; }
        }

        /// <summary>
        /// Class for testing array properties.
        /// </summary>
        private class ArrayAppSettings
        {
            [AppSetting]
            public Int32[] Int32Array { get; set; }
        }

        /// <summary>
        /// Class for testing array properties with custom delimiter.
        /// </summary>
        private class ArrayCustomDelimiterAppSettings
        {
            [AppSetting(ArrayDelimiter="; ")]
            public Int32[] Int32ArrayCustomDelimiter { get; set; }
        }

        /// <summary>
        /// Class for testing array with default value.
        /// </summary>
        private class ArrayDefaultAppSettings
        {
            [AppSetting(DefaultValue="true,True,false,False")]
            public Boolean[] BooleanDefaultArray { get; set; }
        }

        /// <summary>
        /// Class for testing Boolean app settings.
        /// </summary>
        private class BooleanAppSettings
        {
            /// <summary>
            /// Property with value in UPPER case.
            /// </summary>
            [AppSetting]
            public Boolean BooleanUpperCase { get; set; }
            /// <summary>
            /// Property with value in lower case.
            /// </summary>
            [AppSetting]
            public Boolean BooleanLowerCase { get; set; }
            /// <summary>
            /// Property with value in mIxEd case.
            /// </summary>
            [AppSetting]
            public Boolean BooleanMixedCase { get; set; }
            /// <summary>
            /// Property with "false" value.
            /// </summary>
            [AppSetting]
            public Boolean BooleanFalse { get; set; }
        }

        /// <summary>
        /// Class for testing DateTime app settings.
        /// </summary>
        private class DateTimeAppSettings
        {
            /// <summary>
            /// Property with sortable DateTime value, understandable by various cultures.
            /// </summary>
            [AppSetting]
            public DateTime DateTimeDefault { get; set; }

            /// <summary>
            /// Property with date value in short format.
            /// </summary>
            [AppSetting]
            public DateTime DateTimeEnShortDate { get; set; }

            /// <summary>
            /// Property with date value in short format and Russian culture.
            /// </summary>
            [AppSetting(CultureName = "ru-RU")]
            public DateTime DateTimeRuShortDate { get; set; }

            /// <summary>
            /// Property with date value, having culture set.
            /// </summary>
            [AppSetting(CultureName = "ru-RU")]
            public DateTime DateTimeOnlyCulture { get; set; }

            /// <summary>
            /// Property with date value in custom format.
            /// </summary>
            [AppSetting(CultureName = "en-US")]
            public DateTime DateTimeEnCustomFormat { get; set; }
        }

        /// <summary>
        /// Class for testing Double app settings.
        /// </summary>
        private class DoubleAppSettings
        {
            /// <summary>
            /// Property with double value in Russian culture.
            /// </summary>
            [AppSetting(CultureName = "ru-RU")]
            public Double DoubleRu { get; set; }

            /// <summary>
            /// Property with double value in default (English) culture.
            /// </summary>
            [AppSetting]
            public Double DoubleEn { get; set; }

            /// <summary>
            /// Property with double value in form x.xE+x.
            /// </summary>
            [AppSetting]
            public Double DoubleEForm { get; set; }
        }

        /// <summary>
        /// Class for testing Single (float) app settings.
        /// </summary>
        private class SingleAppSettings
        {
            /// <summary>
            /// Property with float value in Russian culture.
            /// </summary>
            [AppSetting(CultureName = "ru-RU")]
            public Single SingleRu { get; set; }

            /// <summary>
            /// Property with float value in default (English) culture.
            /// </summary>
            [AppSetting]
            public Single SingleEn { get; set; }

            /// <summary>
            /// Property with float value in form x.xE+x.
            /// </summary>
            [AppSetting]
            public Single SingleEForm { get; set; }
        }

        /// <summary>
        /// Class for testing Char app settings;
        /// </summary>
        private class CharAppSettings
        {
            [AppSetting]
            public Char SimpleChar { get; set; }
        }

        /// <summary>
        /// Class for testing String app settings;
        /// </summary>
        private class StringAppSettings
        {
            [AppSetting]
            public String SimpleString { get; set; }
            [AppSetting]
            public String EmptyString { get; set; }
        }

        /// <summary>
        /// Generic class for simple integer test classes. Just to play with generics.
        /// </summary>
        /// <typeparam name="T">T is IConvertible structure.</typeparam>
        private class UIntegerAppSetting<T> where T : struct, IConvertible
        {
            /// <summary>
            /// Property having positive value of T type.
            /// </summary>
            [AppSetting]
            public T PositiveInteger { get; set; }
        }

        private class IntegerAppSetting<T> : UIntegerAppSetting<T> where T : struct, IConvertible
        {
            /// <summary>
            /// Property having negative value of T type.
            /// </summary>
            [AppSetting]
            public T NegativeInteger { get; set; }
        }
        #endregion
    }
}
