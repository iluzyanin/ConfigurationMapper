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
        public void AppSettings_CommonFeatures_WorkSuccessully()
        {
            var appSettings = ConfigurationMapper.Map<CommonAppSettings>();
            Assert.IsTrue(appSettings.IsCaseInsensitive);
            Assert.IsTrue(appSettings.EqualsProperty);
        }

        [TestMethod]
        public void AppSettings_BooleanProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<BooleanAppSettings>();
            Assert.IsTrue(appSettings.BooleanUpperCase);
            Assert.IsTrue(appSettings.BooleanLowerCase);
            Assert.IsTrue(appSettings.BooleanMixedCase);
            Assert.IsFalse(appSettings.BooleanFalse);
        }

        [TestMethod]
        public void AppSettings_DateTimeProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<DateTimeAppSettings>();
            Assert.AreEqual(new DateTime(2014, 07, 15, 18, 48, 12), appSettings.DateTimeDefault);
            Assert.AreEqual(new DateTime(2014, 07, 15, 18, 48, 12), appSettings.DateTimeOnlyCulture);
            Assert.AreEqual(new DateTime(2014, 07, 15), appSettings.DateTimeEnShortDate);
            Assert.AreEqual(new DateTime(2014, 07, 15), appSettings.DateTimeRuShortDate);
            Assert.AreEqual(new DateTime(2014, 07, 15), appSettings.DateTimeEnCustomFormat);
        }

        [TestMethod]
        public void AppSettings_DoubleProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<DoubleAppSettings>();
            Assert.AreEqual(12.34, appSettings.DoubleEn);
            Assert.AreEqual(12.34, appSettings.DoubleRu);
            Assert.AreEqual(1.7E+3, appSettings.DoubleEForm);
        }

        [TestMethod]
        public void AppSettings_SingleProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<SingleAppSettings>();
            Assert.AreEqual(12.34f, appSettings.SingleEn);
            Assert.AreEqual(12.34f, appSettings.SingleRu);
            Assert.AreEqual(1.7E+3f, appSettings.SingleEForm);
        }

        [TestMethod]
        public void AppSettings_SByteProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<IntegerAppSetting<SByte>>();
            Assert.AreEqual(42, appSettings.PositiveInteger);
            Assert.AreEqual(-42, appSettings.NegativeInteger);
        }

        [TestMethod]
        public void AppSettings_DecimalProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<IntegerAppSetting<Decimal>>();
            Assert.AreEqual(42, appSettings.PositiveInteger);
            Assert.AreEqual(-42, appSettings.NegativeInteger);
        }

        [TestMethod]
        public void AppSettings_UInt16Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<UIntegerAppSetting<UInt16>>();
            Assert.AreEqual(42, appSettings.PositiveInteger);
        }

        [TestMethod]
        public void AppSettings_UInt32Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<UIntegerAppSetting<UInt32>>();
            Assert.AreEqual((UInt32)42, appSettings.PositiveInteger);
        }


        [TestMethod]
        public void AppSettings_Int32Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<IntegerAppSetting<Int32>>();
            Assert.AreEqual(42, appSettings.PositiveInteger);
            Assert.AreEqual(-42, appSettings.NegativeInteger);
        }

        [TestMethod]
        public void AppSettings_UInt64Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<UIntegerAppSetting<UInt64>>();
            Assert.AreEqual((UInt64)42, appSettings.PositiveInteger);
        }


        [TestMethod]
        public void AppSettings_Int64Properties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<IntegerAppSetting<Int64>>();
            Assert.AreEqual(42, appSettings.PositiveInteger);
            Assert.AreEqual(-42, appSettings.NegativeInteger);
        }

        [TestMethod]
        public void AppSettings_CharProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<CharAppSettings>();
            Assert.AreEqual('c', appSettings.SimpleChar);
        }

        [TestMethod]
        public void AppSettings_StringProperties_MappedSuccessfully()
        {
            var appSettings = ConfigurationMapper.Map<StringAppSettings>();
            Assert.AreEqual("Test string", appSettings.SimpleString);
            Assert.AreEqual(String.Empty, appSettings.EmptyString);
        }
        /// <summary>
        /// Class for testing common mapping features for app settings.
        /// </summary>
        private class CommonAppSettings
        {
            /// <summary>
            /// Property with overriden name.
            /// </summary>
            [AppSetting(Key = "Equals")]
            public Boolean EqualsProperty { get; set; }

            /// <summary>
            /// Property with name in different case than in appSettings.
            /// </summary>
            public Boolean IsCaseInsensitive { get; set; }
        }

        /// <summary>
        /// Class for testing Boolean app settings.
        /// </summary>
        private class BooleanAppSettings
        {
            /// <summary>
            /// Property with value in UPPER case.
            /// </summary>
            public Boolean BooleanUpperCase { get; set; }
            /// <summary>
            /// Property with value in lower case.
            /// </summary>
            public Boolean BooleanLowerCase { get; set; }
            /// <summary>
            /// Property with value in mIxEd case.
            /// </summary>
            public Boolean BooleanMixedCase { get; set; }
            /// <summary>
            /// Property with "false" value.
            /// </summary>
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
            public DateTime DateTimeDefault { get; set; }

            /// <summary>
            /// Property with date value in short format.
            /// </summary>
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
            public Double DoubleEn { get; set; }

            /// <summary>
            /// Property with double value in form x.xE+x.
            /// </summary>
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
            public Single SingleEn { get; set; }

            /// <summary>
            /// Property with float value in form x.xE+x.
            /// </summary>
            public Single SingleEForm { get; set; }
        }

        /// <summary>
        /// Class for testing Char app settings;
        /// </summary>
        private class CharAppSettings
        {
            public Char SimpleChar { get; set; }
        }

        /// <summary>
        /// Class for testing String app settings;
        /// </summary>
        private class StringAppSettings
        {
            public String SimpleString { get; set; }
            public String EmptyString { get; set; }
        }

        /// <summary>
        /// Generic class for simple integer test classes. Just to play with generics.
        /// </summary>
        /// <typeparam name="T">T is IConvertible structure.</typeparam>
        private class UIntegerAppSetting<T> where T: struct, IConvertible
        {
            /// <summary>
            /// Property having positive value of T type.
            /// </summary>
            public T PositiveInteger { get; set; }
        }

        private class IntegerAppSetting<T>: UIntegerAppSetting<T> where T: struct, IConvertible
        {
            /// <summary>
            /// Property having negative value of T type.
            /// </summary>
            public T NegativeInteger { get; set; }
        }
    }
}
