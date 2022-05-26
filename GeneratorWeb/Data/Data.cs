namespace GeneratorWeb.Data
{
    public class Data
    {
        public string ClassName { get; set; }
        public string Method { get; set; }

        public string FieldName { get; set; }

        public Data() { }

        public Data(string className)
        {
            ClassName = className;
        }

        public Data(string className, string method)
        {
            ClassName = className;
            Method = method;
        }

        public Data(string className, string method, string fieldName)
        {
            ClassName = className;
            Method = method;
            FieldName = fieldName;
        }
    }
}
