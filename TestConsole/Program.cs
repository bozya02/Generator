using System;
using System.Reflection;
using Core;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            AnyClass any = new AnyClass();
            
            //
            Dictionary<string, HashSet<string>> list = new Dictionary<string, HashSet<string>>();

            foreach (var anyClass in any.GetClasses())
            {
                list.Add(anyClass, new HashSet<string>());
                foreach (var anyMethod in any.GetMethodsByClass(anyClass))
                {
                    list[anyClass].Add(anyMethod);
                }
            }

            var json = JsonSerializer.Serialize(list);

            var anyList = JsonSerializer.Deserialize<Dictionary<string, HashSet<string>>>(json);

            Console.WriteLine($"Properties:");
            
            foreach (var item in any.GetClasses())
            {
                Console.WriteLine("\t" + item);
            }

            string className = Console.ReadLine();

            Console.WriteLine($"Methods:");
            foreach (var item in any.GetMethodsByClass(className))
            {
                Console.WriteLine("\t" + item);
            }

            string language = "ru";
            
            string method = Console.ReadLine();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(any.GenerateData(className, method, language));
            }
            
        }
    }
}