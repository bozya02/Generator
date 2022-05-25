using System.Reflection;

namespace Core
{
    public class AnyClass
    {
        private Assembly _assembly;
        public Assembly Assembly => _assembly;

        public List<string> Locales { get; }

        public AnyClass()
        {
            _assembly = Assembly.Load("Bogus");
            Locales = FillLocales();
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
                if (methodInfo.ReturnType.Name.ToLower() == "string")
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

        public object GenerateData(string className, string method, string locale = "en")
        {
            Type type = Assembly.GetType($"Bogus.DataSets.{className}", true, true);

            object anyObject = CheckConstructorsOnLocale(type) ?
                               Activator.CreateInstance(type, locale) :
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

        private List<string> FillLocales()
        {
            return new List<string>
            {
                "af_ZA",
                "ar",
                "az",
                "cz",
                "de",
                "de_AT",
                "de_CH",
                "el",
                "en",
                "en_AU",
                "en_AU_ocker",
                "en_BORK",
                "en_CA",
                "en_GB",
                "en_IE",
                "en_IND",
                "en_NG",
                "en_US",
                "en_ZA",
                "es",
                "es_MX",
                "fa",
                "fi",
                "fr",
                "fr_CA",
                "fr_CH",
                "ge",
                "hr",
                "id_ID",
                "it",
                "ja",
                "ko",
                "lv",
                "nb_NO",
                "ne",
                "nl",
                "nl_BE",
                "pl",
                "pt_BR",
                "pt_PT",
                "ro",
                "ru",
                "sk",
                "sv",
                "tr",
                "uk",
                "vi",
                "zh_CN",
                "zh_TW",
                "zu_ZA"
            };
        }
    }
}