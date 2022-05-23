using System.Reflection;

namespace Core
{
    public class AnyClass
    {
        private Assembly _assembly;
        public Assembly Assembly => _assembly;

        public AnyClass()
        {
            _assembly = Assembly.Load("Bogus");
        }

        public List<string> GetClasses()
        {
            List<string> classes = new List<string>();

            foreach (Type typeInfo in Assembly.GetTypes().Where(t => t.Namespace == "Bogus.DataSets"))
            {
                if (typeInfo.IsClass && typeInfo.IsPublic)
                    classes.Add(typeInfo.Name);
            }

            return classes;
        }

        public List<string> GetMethodsByClass(string className)
        {
            Type type = Assembly.GetType($"Bogus.DataSets.{className}", true, true);

            List<string> methods = new List<string>();

            foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                methods.Add(methodInfo.Name);
            }

            return methods;
        }

        public object[] GetParametersForMethod(MethodInfo member)
        {
            List<object> parameters = new List<object>();
            foreach (var parameter in member.GetParameters())
            {
                parameters.Add(parameter.DefaultValue);
            }

            return parameters.ToArray();
        }

        // Нужно изменить на возможность создавать некоторые классы без locale
        public object GenerateData(string className, string method, string language = "en")
        {
            Type type = Assembly.GetType($"Bogus.DataSets.{className}", true, true);

            object anyObject = Activator.CreateInstance(type);

            MethodInfo methodOfObject = type.GetMethod(method);

            List<object> parameters = new List<object>();
            
            return methodOfObject.Invoke(anyObject, GetParametersForMethod(methodOfObject));
        }
    }
}