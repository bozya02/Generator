using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System;

namespace GeneratorTests
{
    [TestClass]
    public class GeneratorTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Generator generator = new Generator();
            Assert.IsNotNull(generator.Assembly);
            Assert.IsNotNull(generator.Locales);
        }

        [TestMethod]
        public void GetClassesTest()
        {
            Generator generator = new Generator();
            Assert.IsNotNull(generator.GetClasses());
        }

        [TestMethod]
        public void GetMethodsByCorrectClassNameTest()
        {
            Generator generator = new Generator();
            Assert.IsNotNull(generator.GetMethodsByClass("Finance"));
        }

        [TestMethod]
        public void GetMethodsByClassIncorrectNameTest()
        {
            Generator generator = new Generator();
            Assert.ThrowsException<Exception>(() => generator.GetMethodsByClass("asd"));
        }

        [TestMethod]
        public void GenerateDataCorrectTest()
        {
            Generator generator = new Generator();
            Assert.IsNotNull(generator.GenerateData("Finance", "Account", "ru"));
        }

        [TestMethod]
        public void GenerateDataIncorrectClassNameTest()
        {
            Generator generator = new Generator();
            Assert.ThrowsException<Exception>(() => generator.GenerateData("asd", "Account", "ru"));
        }

        [TestMethod]
        public void GenerateDataIncorrectMethodTest()
        {
            Generator generator = new Generator();
            Assert.ThrowsException<Exception>(() => generator.GenerateData("Finance", "asd", "ru"));
        }

        [TestMethod]
        public void GenerateDataIncorrectLocaleTest()
        {
            Generator generator = new Generator();
            Assert.ThrowsException<Exception>(() => generator.GenerateData("Finance", "Account", "asd"));
        }
    }
}