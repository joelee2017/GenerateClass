Console.WriteLine("GenerateClass Go!");
var properties = new List<(string propertyName, string propertyType, string description)>()
{
    ("Country", "string?","國家別"),
    ("Proportion", "int?","1/3比例(%)")
};
GenerateClass(
    "CountryConfiguration.cs",
    properties,
    "CountryConfiguration",
    "D:/ProjectTest/GenerateClass/GenerateClass/File"
);
Console.WriteLine("GenerateClass done!");
Console.ReadKey();
static void GenerateClass(string className, List<(string propertyName, string propertyType, string description)> properties, string namespaceName, string filePath)
{
    filePath = Path.Combine(filePath, className);
    using (StreamWriter streamWriter = new StreamWriter(filePath))
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