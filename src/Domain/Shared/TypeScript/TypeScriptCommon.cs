namespace Domain.Shared.TypeScript;

public static class TypeScriptCommon
{
    public static string ToCamelCase(string name) => name[0..1].ToLower() + name[1..];

    public static string ToTypeScriptType(Type type)
           => type switch
           {
               _ when IsNumericType(type) => "number",
               _ when IsTextType(type) => "string",
               _ when IsBooleanType(type) => "boolean",
               _ when IsArrayType(type, out var tsType) => tsType,
               _ when IsGenericType(type, out var tsType) => tsType,
               _ when IsCustomObjectType(type) => type.Name,
               _ when type.IsEnum => type.Name,
               _ => throw new InvalidOperationException("The type was not matched in ToTypeScriptType.")
           };

    private static bool IsNumericType(Type type)
        => type == typeof(int) ||
           type == typeof(uint) ||
           type == typeof(long) ||
           type == typeof(ulong) ||
           type == typeof(float) ||
           type == typeof(double) ||
           type == typeof(decimal);

    private static bool IsTextType(Type type) => type == typeof(string) || type == typeof(char);

    private static bool IsBooleanType(Type type) => type == typeof(bool);

    public static bool IsCustomObjectType(Type type) => typeof(ITypeScriptModel).IsAssignableFrom(type);

    private static bool IsArrayType(Type type, out string tsType)
    {
        if (!type.IsArray)
        {
            tsType = "any";
            return false;
        }

        tsType = ToTypeScriptType(type.GetElementType()!) + "[]";
        return true;
    }

    private static bool IsGenericType(Type type, out string tsType)
    {
        if (!type.IsGenericType)
        {
            tsType = "any";
            return false;
        }

        var genericType = type.GetGenericTypeDefinition();

        if (genericType == typeof(List<>) ||
            genericType == typeof(IList<>) ||
            genericType == typeof(IEnumerable<>))
        {
            tsType = ToTypeScriptType(type.GetGenericArguments()[0]) + "[]";
            return true;
        }

        if (genericType == typeof(Dictionary<,>) || genericType == typeof(IDictionary<,>))
        {
            var key = ToTypeScriptType(type.GetGenericArguments()[0]);
            var value = ToTypeScriptType(type.GetGenericArguments()[1]);
            tsType = $"Map<{key}, {value}>";
            return true;
        }

        if (genericType == typeof(HashSet<>) || genericType == typeof(ISet<>))
        {
            var value = ToTypeScriptType(type.GetGenericArguments()[0]);
            tsType = $"Set<{value}>";
            return true;
        }

        throw new InvalidOperationException("Generic type was not found in IsGenericType.");
    }
}