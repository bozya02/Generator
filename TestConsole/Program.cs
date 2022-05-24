using System;
using System.Reflection;
using Core;
using System.Linq;
using System.Collections.Generic;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Properties:");
            AnyClass any = new AnyClass();

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