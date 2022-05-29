using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using System;
using System.Linq;
using System.Collections.Generic;

namespace GeneratorTests
{
    [TestClass]
    public class ConvertManagerTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ConvertManager convertManager = new ConvertManager();
            Assert.IsNotNull(convertManager.Generator);
        }

        [TestMethod]
        public void GenerateOneTest()
        {
            ConvertManager convertManager = new ConvertManager();
            Assert.IsNotNull(convertManager.Generate("Finance", "Account"));
        }

        [TestMethod]
        public void GenerateNegativeCountTest()
        {
            ConvertManager convertManager = new ConvertManager();

            Dictionary<string, HashSet<string>> query = new Dictionary<string, HashSet<string>>();
            query.Add("Address", new HashSet<string>
            {
                "City", "ZipCode"
            });

            Assert.ThrowsException<Exception>(() => convertManager.Generate(query, -10));
        }

        [TestMethod]
        public void GenerateFromDictionaryTest()
        {
            ConvertManager convertManager = new ConvertManager();

            Dictionary<string, HashSet<string>> query = new Dictionary<string, HashSet<string>>();
            query.Add("Address", new HashSet<string>
            {
                "City", "ZipCode"
            });

            Assert.IsNotNull(convertManager.Generate(query, 10));
        }

        [TestMethod]
        public void GenerateFromDictionaryRowCountTest()
        {
            ConvertManager convertManager = new ConvertManager();

            Dictionary<string, HashSet<string>> query = new Dictionary<string, HashSet<string>>();
            query.Add("Address", new HashSet<string>
            {
                "City", "ZipCode"
            });
            int count = 200;

            Assert.AreEqual(count, convertManager.Generate(query, count).Count);
        }

        [TestMethod]
        public void GenerateFromDictionaryDataCountTest()
        {
            ConvertManager convertManager = new ConvertManager();

            Dictionary<string, HashSet<string>> query = new Dictionary<string, HashSet<string>>();
            query.Add("Address", new HashSet<string>
            {
                "City", "ZipCode"
            });

            int count = 200;
            int dataCount = count * query.Values.Sum(s => s.Count());

            Assert.AreEqual(dataCount, Enumerable.Sum(convertManager.Generate(query, count).Select(x => x.Count)));
        }

        [TestMethod]
        public void GenerateFromListTest()
        {
            ConvertManager convertManager = new ConvertManager();

            List<Data> query = new List<Data>
            {
                new Data("Address", "City"),
                new Data("Address", "ZipCode")
            };

            Assert.IsNotNull(convertManager.Generate(query, 10));
        }

        [TestMethod]
        public void GenerateFromListRowCountTest()
        {
            ConvertManager convertManager = new ConvertManager();

            List<Data> query = new List<Data>
            {
                new Data("Address", "City"),
                new Data("Address", "ZipCode")
            };

            int count = 200;

            Assert.AreEqual(count, convertManager.Generate(query, count).Count);
        }

        [TestMethod]
        public void GenerateFromListDataCountTest()
        {
            ConvertManager convertManager = new ConvertManager();

            List<Data> query = new List<Data>
            {
                new Data("Address", "City"),
                new Data("Address", "ZipCode")
            };

            int count = 200;
            int dataCount = count * query.Count();

            Assert.AreEqual(dataCount, Enumerable.Sum(convertManager.Generate(query, count).Select(x => x.Count)), dataCount.ToString());
        }
    }
}
