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

            foreach (Type typeInfo in Assembly.GetTypes()
                                   .Where(t => t.Namespace == "Bogus.DataSets"))
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
            if (member == null)
                return new object[0];
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

            object anyObject = CheckConstructorsOnLocale(type) ?
                               Activator.CreateInstance(type, language) :
                               Activator.CreateInstance(type);

            MethodInfo methodOfObject = type.GetMethod(method);

            List<object> parameters = new List<object>();
            
            return methodOfObject.Invoke(anyObject, GetParametersForMethod(methodOfObject));
        }

        public bool CheckConstructorsOnLocale(Type type)
        {
            foreach (var item in type.GetConstructors())
            {
                foreach (var parameter in item.GetParameters())
                {
                    if (parameter.Name == "locale")
                        return true;
                }
            }

            return false;
        }

        public List<string> GetLanguages()
        {
            
        }
    }
}