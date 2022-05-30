using Core;

Generator generator = new Generator();
ConvertManager manager = new ConvertManager();

var nl = Environment.NewLine;
Console.WriteLine($"[*] This is a test data generator.{nl}" +
    $"[*] With this service, you can create data in the formats,{nl}" +
    $"[*] quantities, and languages you need.{nl}" +
    $"[*] The generated data can be exported to the format you need.");

Prompt();

bool inWork = true;

while (inWork)
{
    var command = Console.ReadLine().ToLower();
    switch (command)
    {
        case "classes":
            foreach (var className in generator.GetClasses())
            {
                Console.WriteLine(className);
            }

            break;
        case "methodsbyclassname":
            Console.WriteLine($"[=] Write class");
            var name = Console.ReadLine();

            try
            {
                foreach (var method in generator.GetMethodsByClass(name))
                {
                    Console.WriteLine(method);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            break;
        case "locales":
            foreach (var locale in generator.Locales)
            {
                Console.WriteLine(locale);
            }
            break;
        case "generate":
            Console.WriteLine($"[=] Write count");
            int count = int.Parse(Console.ReadLine());

            var dataList = FillList(count);

            Console.WriteLine($"[=] Write locale");
            var temp = Console.ReadLine();

            string locales = temp != null ? temp : "en";

            try
            {
                var generatedData = manager.Generate(dataList, count, locales);
                WriteData(generatedData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            break;
        case "repeat":
            Prompt();
            break;
        case "exit":
            inWork = false;
            break;
        default:
            Console.WriteLine("Invalid command");
            break;
    }
}

static void Prompt()
{
    Console.WriteLine("******************************");
    Console.WriteLine($"[*] For use this program, use next commands:");
    Console.WriteLine($"[?] Classes (Get all classes)");
    Console.WriteLine($"[?] MethodsByClassName (Get methods for class)");
    Console.WriteLine($"[?] Locales (Get locales)");
    Console.WriteLine($"[+] Generate (Generate by count)");
    Console.WriteLine($"[+] Repeat (Repeat commands)");
    Console.WriteLine($"[-] Exit (Close program)");
    Console.WriteLine("******************************");
}

static void GetParams(out string className, out string methodName, out string locale)
{
    Console.WriteLine($"[=] Write class");
    className = Console.ReadLine();

    Console.WriteLine($"[=] Write method");
    methodName = Console.ReadLine();

    Console.WriteLine($"[=] Write locale");
    var temp = Console.ReadLine();

    locale = temp != null ? temp : "en";
}

static List<Data> FillList(int count)
{
    List<Data> list = new List<Data>();

    for (int i = 0; i < count; i++)
    {
        Console.WriteLine($"[=] Write class");
        string className = Console.ReadLine();

        Console.WriteLine($"[=] Write method");
        string methodName = Console.ReadLine();
        list.Add(new Data
        {
            ClassName = className,
            Method = methodName
        });

    }

    return list;
}

static void WriteData(List<List<object>> dataList)
{
    foreach (var list in dataList)
    {
        Console.WriteLine($"{dataList.IndexOf(list) + 1}.");
        foreach (var data in list)
        {
            Console.WriteLine($"\t{data}");
        }
    }
}