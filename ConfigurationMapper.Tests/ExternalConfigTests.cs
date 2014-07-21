﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace ConfigurationMapper.Tests
{
    [TestClass]
    public class ExternalConfigTests
    {
        [TestMethod]
        public void ExternalConfig_MappedSuccessfully()
        {
            var configPath = Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                @"TestConfig\external.config");
            var appSettings = ConfigurationMapper<ExternalAppSettings>.Map(configPath);
            Assert.IsTrue(appSettings.Boolean);
            Assert.AreEqual(42, appSettings.Integer);
        }

        [TestMethod, ExpectedException(typeof(FileNotFoundException))]
        public void ExternalNonExistentConfig_ThrowsException()
        {
            var configPath = (@"nonexistent.config");
            var appSettings = ConfigurationMapper<ExternalAppSettings>.Map(configPath);
        }

        /// <summary>
        /// Class for testing external configuration mapping.
        /// </summary>
        private class ExternalAppSettings
        {
            /// <summary>
            /// Boolean property.
            /// </summary>
            [AppSetting]
            public Boolean Boolean { get; set; }
            /// <summary>
            /// Integer property.
            /// </summary>
            [AppSetting]
            public Int32 Integer { get; set; }
        }
    }
}
