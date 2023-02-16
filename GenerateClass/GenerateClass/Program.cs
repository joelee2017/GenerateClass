// 程序開始 
string filePath = "E:/Project/GenerateClass/GenerateClass/GenerateClass/File/";
Start(filePath);
// 屬性種類及代碼 
static void AttributeCodeFunction(string[] typeArrary)
{
    for (int i = 0; i < typeArrary.Length; i++)
    {
        Console.WriteLine($"{i}：{typeArrary[i]}");
    }
}
// Class 產生 
static void GenerateClass(string className, List<(string propertyName, string propertyType, string description)> properties, string filePath)
{
    string fileName = className + ".cs";
    string namespaceName = className;
    string sourcePath = Path.Combine(filePath, fileName);
    using (StreamWriter streamWriter = new StreamWriter(sourcePath))
    {
        // 加入檔頭   
        streamWriter.WriteLine("using System;");
        streamWriter.WriteLine("using System.ComponentModel;");
        streamWriter.WriteLine();
        // 加入命名空間   
        streamWriter.WriteLine($"namespace {namespaceName}");
        streamWriter.WriteLine("{");
        streamWriter.WriteLine($"\tpublic class {className}");
        streamWriter.WriteLine("\t{");
        // 加入屬性   
        foreach (var property in properties)
        {
            streamWriter.WriteLine($"\t\t/// <summary>");
            streamWriter.WriteLine($"\t\t/// {property.description}");
            streamWriter.WriteLine($"\t\t/// </summary>");
            streamWriter.WriteLine($"\t\t[Description(\"{property.propertyName}\")]");
            streamWriter.WriteLine($"\t\tpublic {property.propertyType} {property.propertyName} {{ get; set; }}");
            streamWriter.WriteLine();
        }
        // 加入檔尾   
        streamWriter.WriteLine("\t}");
        streamWriter.WriteLine("}");
    }
}
// 屬性轉換 
string ConvertTypeNameToType(string[] typeArrary, string typeNumber)
{
    return int.TryParse(typeNumber, out int n) == true ? typeArrary[n] : string.Empty;
}
// 屬性增加、修改、刪除 
bool CreateAttribute(List<(string propertyName, string propertyType, string description)> properties, string[] typeArrary, bool finished)
{
    while (!finished)
    {
        Console.WriteLine("請輸入屬性名稱(Enter結束):");
        string propertyName = Console.ReadLine();
        Console.WriteLine();
        if (string.IsNullOrEmpty(propertyName))
        {
            finished = true;
            break;
        }
        var existingProperty = properties.FirstOrDefault(p => p.propertyName == propertyName);
        var existingPropertyIndex = properties.IndexOf(existingProperty);
        if (existingPropertyIndex != -1)
        {
            Console.WriteLine($"屬性 {propertyName} 已存在，請選擇操作：");
            Console.WriteLine("1. 修改屬性");
            Console.WriteLine("2. 刪除屬性");
            Console.WriteLine("3. 取消操作");
            var property = properties[existingPropertyIndex];
            bool operationFinished = false;
            while (!operationFinished)
            {
                ConsoleKeyInfo operation = Console.ReadKey();
                Console.WriteLine();
                switch (operation.KeyChar)
                {
                    case '1':
                        Console.WriteLine($"請輸入新的屬性名稱(Enter不修改):");
                        string newPropertyName = Console.ReadLine();
                        Console.WriteLine();
                        if (!string.IsNullOrEmpty(newPropertyName))
                        {
                            property.propertyName = newPropertyName;
                        }
                        Console.WriteLine($"請輸入新的屬性類型(Enter不修改):");
                        AttributeCodeFunction(typeArrary);
                        string newPropertyType = ConvertTypeNameToType(typeArrary, Console.ReadLine());
                        Console.WriteLine();
                        if (!string.IsNullOrEmpty(newPropertyType))
                        {
                            Console.WriteLine($"新的屬性 {newPropertyType} 是否可為 null： 0 不可，1 可");
                            string isNull = Console.ReadLine();
                            Console.WriteLine();
                            if (isNull == "0")
                            {
                                property.propertyType = newPropertyType;
                            }
                            else
                            {
                                property.propertyType = newPropertyType + "?";
                            }
                        }
                        Console.WriteLine($"請輸入新的屬性描述(Enter不修改):");
                        string newPropertyDescription = Console.ReadLine();
                        Console.WriteLine();
                        if (!string.IsNullOrEmpty(newPropertyDescription))
                        {
                            property.description = newPropertyDescription;
                        }
                        properties[existingPropertyIndex] = property;
                        Console.WriteLine("修改完成");
                        operationFinished = true;
                        break;
                    case '2':
                        var item = properties.FirstOrDefault(x => x.propertyName == existingProperty.propertyName);
                        properties.Remove(item);
                        Console.WriteLine("刪除完成");
                        operationFinished = true;
                        break;
                    case '3':
                        Console.WriteLine("取消操作");
                        operationFinished = true;
                        break;
                    default:
                        Console.WriteLine("無效操作，請重新輸入");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine($"請輸入屬性類型:");
            AttributeCodeFunction(typeArrary);
            string propertyType = ConvertTypeNameToType(typeArrary, Console.ReadLine());
            Console.WriteLine();
            if (!string.IsNullOrEmpty(propertyType))
            {
                Console.WriteLine($"屬性 {propertyType} 是否可為 null： 0 不可，1 可");
                string isNull = Console.ReadLine();
                Console.WriteLine();
                if (isNull != "0")
                {
                    propertyType = propertyType + "?";
                }
            }
            Console.WriteLine($"請輸入屬性描述:");
            string description = Console.ReadLine();
            Console.WriteLine();
            properties.Add((propertyName, propertyType, description));
            Console.WriteLine("屬性新增完成");
        }
    }
    Console.WriteLine("當前 Class 屬性內容如下：");
    Console.WriteLine();
    foreach (var item in properties)
    {
        Console.WriteLine($"propertyName：{item.propertyName} - Type：{item.propertyType} - description：{item.description}");
    }
    Console.WriteLine("確認(Enter) 或打任一值進行修改");
    Console.WriteLine();
    var isback = Console.ReadLine();
    if (!string.IsNullOrEmpty(isback))
    {
        finished = false;
        CreateAttribute(properties, typeArrary, finished);
    }
    return finished;
}
void GenerateClassCreate(string filePath)
{  
    Console.Write("請輸入類別名稱: ");
    string className = Console.ReadLine();
    Console.WriteLine();
    // 讓使用者輸入屬性 
    var properties = new List<(string propertyName, string propertyType, string description)>();
    // Type 
    string[] typeArrary = new string[] { "bool", "byte", "sbyte", "char", "decimal", "double", "float", "int", "uint", "short", "ushort", "object", "string" };
    bool finished = false;
    finished = CreateAttribute(properties, typeArrary, finished);
    GenerateClass(className, properties, filePath);
}
void Start(string filePath)
{
    while (true)
    {
        Console.WriteLine("開始請按任意鍵，結束請按 q");
        string q = Console.ReadLine();
        if (q == "q")
        {
            Environment.Exit(0);
        }
        else
        {
            Console.WriteLine("GenerateClass Go!");
            GenerateClassCreate(filePath);
            Console.WriteLine("GenerateClass done!");
            Console.WriteLine();
        }
    }
}

