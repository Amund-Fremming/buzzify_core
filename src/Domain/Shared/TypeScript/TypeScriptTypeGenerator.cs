using System.Reflection;
using System.Text;
using static Domain.Shared.TypeScript.TypeScriptCommon;

namespace Domain.Shared.TypeScript;

public static class TypeScriptTypeGenerator
{
    public const string Filename = "contenttypes.ts";

    public static void Generate()
    {
        var types = GetTypeScriptModelTypes();
        var fileContent = GenerateFileContent(types);
        WriteToFile(fileContent);
    }

    private static IEnumerable<Type> GetTypeScriptModelTypes()
        => Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                && typeof(ITypeScriptModel).IsAssignableFrom(t) || t.IsEnum);

    private static void WriteToFile(string fileContent)
    {
        var directory = Path.GetFullPath("../..");
        var newDir = directory + "/" + Filename;
        File.WriteAllText(newDir, fileContent);
    }

    private static string GenerateFileContent(IEnumerable<Type> types)
    {
        StringBuilder sb = new();
        foreach (var type in types)
        {
            if (type.IsEnum)
            {
                sb.Append(GetTypeScriptEnum(type));
                continue;
            }

            sb.AppendLine($"export interface {type.Name} {{");

            var properties = GetProperties(type);
            foreach (var property in properties)
            {
                var tsPropertyName = ToCamelCase(property.Name);
                var tsType = ToTypeScriptType(property.PropertyType);
                sb.AppendLine($"    {tsPropertyName}: {tsType};");
            }

            sb.AppendLine($"}}");
            sb.AppendLine();
        }

        return sb.ToString();
    }

    private static string GetTypeScriptEnum(Type type)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"export enum {type.Name} {{");

        var values = Enum.GetValues(type).Cast<object>();
        foreach (var value in values)
        {
            sb.AppendLine($"    {Enum.GetName(type, value)},");
        }

        sb.AppendLine("}");
        return sb.ToString();
    }

    private static IEnumerable<Prop> GetProperties(Type type)
        => type.GetProperties()
               .Select(p => new Prop(p.Name, p.PropertyType))
               .Union(type.GetConstructors()
                   .First()
                   .GetParameters()
                   .Select(p => new Prop(ToPascalCase(p.Name!), p.ParameterType))!);

    private static string ToPascalCase(string name) => name[0..1].ToUpper() + name[1..];
}

internal record Prop(string Name, Type PropertyType);