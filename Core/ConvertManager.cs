using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ConvertManager
    {
        private Generator Generator { get; }

        public ConvertManager()
        {
            Generator = new Generator();
        }

        public List<List<object>> Generate(Dictionary<string, HashSet<string>> dataList, int count, string locale = "en")
        {
            List<List<object>> GeneratedData = new List<List<object>>();

            for (int i = 0; i < count; i++)
            {
                GeneratedData.Add(new List<object>());
                foreach (var className in dataList.Keys)
                {
                    foreach (var method in dataList[className])
                    {
                        GeneratedData[i].Add(Generator.GenerateData(className, method, locale));
                    }
                }
            }

            return GeneratedData;
        }

        public object Generate(string name, string method, string locale = "en")
        {
            return Generator.GenerateData(name, method, locale);
        }

        public List<List<object>> Generate(List<Data> dataList, int count, string locale = "en")
        {
            List<List<object>> GeneratedData = new List<List<object>>();

            for (var i = 0; i < count; i++)
            {
                GeneratedData.Add(new List<object>());

                foreach (var data in dataList)
                {
                    GeneratedData[i].Add(Generator.GenerateData(data.ClassName, data.Method, locale));
                }
            }

            return GeneratedData;
        }
    }
}
