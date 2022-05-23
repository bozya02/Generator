using System;
using GeneratorCore;
using System.Reflection;
using Bogus;
using System.Linq;
using System.Collections.Generic;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly faker = typeof(Bogus.Faker).Assembly;
            Type dataSets = faker.GetType($"Bogus.Faker", true, true);

            Console.WriteLine($"Properties:");
            foreach (PropertyInfo propertyInfo in dataSets.GetProperties())
            {
                Console.WriteLine($"\t{propertyInfo.Name}");
            }

            string memeber = Console.ReadLine();

            Assembly assembly = typeof(Bogus.Faker).Assembly;

            Type type = assembly.GetType($"Bogus.DataSets.{memeber}", true, true);
            /*
            Console.WriteLine($"Members:");
            foreach (MemberInfo memberInfo in type.GetMembers())
            {
               
                Console.WriteLine($"\t{memberInfo.Name}");
            }
            
            Console.WriteLine("Genders: ");
            foreach (var item in Enum.GetValues(typeof(Bogus.DataSets.Name.Gender)))
            {
                Console.WriteLine(item);
            }
            string gender = Console.ReadLine();
            */
            Console.WriteLine($"Methods:");
            foreach (MethodInfo methodInfo in type.GetMethods())
            {
                Console.Write($"\t{methodInfo.Name} (");
                foreach (ParameterInfo param in methodInfo.GetParameters())
                {
                    Console.Write($"{param.ParameterType.Name} {param.Name}, ");
                }
                Console.WriteLine(" )");
            }

            
            string method = Console.ReadLine();
            string language = "ru";

            //Type genderType = assembly.GetType($"Bogus.DataSets.Name.Gender", true, true);

            //object gen = Activator.CreateInstance(genderType);
            object name = Activator.CreateInstance(type, language);

            MethodInfo fullName = type.GetMethod(method);

            List<object> parameters =  new List<object>();
            foreach (var p in fullName.GetParameters())
            {
                parameters.Add(p.DefaultValue);
            }
            Console.WriteLine(fullName.Invoke(name, parameters.ToArray()));
            
        }
    }
}