using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;
using System.Text;
using static Domain.Shared.TypeScript.TypeScriptCommon;

namespace Domain.Shared.TypeScript;

/*
 * TODO
 *
 * - Return some message when 200, 401, 403, 404 and 500 (Result pattern or TanStack)
 */

public static class TypeScriptClientGenerator
{
    private static bool _clientLogging = false;
    private static string _relativePath = string.Empty;

    public static void Generate(bool clientLogging, string relativePath)
    {
        _clientLogging = clientLogging;
        _relativePath = relativePath;

        var controllerClasses = GetControllerClasses();
        var textClients = controllerClasses
            .Select(GetCustomMethods)
            .Select(ToTextClient)
            .ToList();

        for (var i = 0; i < controllerClasses.Count(); i++)
        {
            var controllerName = controllerClasses.ElementAt(i).Name;
            var textClient = textClients.ElementAt(i);
            CreatedFile(controllerName, textClient);
        }
    }

    private static void CreatedFile(string controllerName, string content)
    {
        controllerName = controllerName.Replace("Controller", "Api.ts");
        var fixedPath = Path.GetFullPath(_relativePath);
        var filePath = Path.Combine(fixedPath, "api", controllerName);
        Directory.CreateDirectory(string.Concat(fixedPath, "/api"));
        File.WriteAllText(filePath, content);
    }

    private static string ToTextClient(MethodInfo[] methodInfos)
    {
        var tsImports = new StringBuilder();
        var tsFunctions = new StringBuilder();
        var customReturnTypes = new HashSet<string>();

        tsImports.Append("import { ");
        foreach (var method in methodInfos)
        {
            customReturnTypes.Add(GetImportType(method));
            var parameterTypes = GetParameterTypes(method.GetParameters());
            customReturnTypes.UnionWith(parameterTypes);

            var clientMethodText = GetClientMethodString(method);
            tsFunctions.AppendLine(clientMethodText);
        }

        customReturnTypes.RemoveWhere(string.IsNullOrWhiteSpace);
        var allTypes = string.Join(", ", customReturnTypes);

        tsImports.AppendLine($"{allTypes} }} from \"@/contenttypes\";" + "\n");
        tsImports.Append(tsFunctions);

        return tsImports.ToString();
    }

    private static HashSet<string> GetParameterTypes(ParameterInfo[] parameterInfos)
    {
        var objects = new HashSet<string>();
        foreach (var param in parameterInfos)
        {
            var type = param.ParameterType;

            if (IsCustomObjectType(type))
            {
                objects.Add(type.Name);
                continue;
            }

            if (type.IsArray && IsCustomObjectType(type))
            {
                objects.Add(type.GetElementType()!.Name);
                continue;
            }

            if (type.IsEnum)
            {
                objects.Add(type.Name);
                continue;
            }

            if (type.IsGenericType)
            {
                objects.UnionWith(type.GetGenericArguments()
                    .Where(IsCustomObjectType)
                    .Select(t => t.Name));
            }
        }

        return objects;
    }

    private static string GetEndpointBase(MethodInfo method)
    {
        var type = method.DeclaringType!;

        var endpointTemplateBase = type.GetCustomAttribute<RouteAttribute>()?.Template ?? "";
        var controllerName = type.Name.Replace("Controller", string.Empty);
        var endpointBase = endpointTemplateBase.Replace("[controller]", controllerName);

        return endpointBase;
    }

    private static string GetHttpVerb(IEnumerable<Attribute> attributes)
        => attributes switch
        {
            _ when attributes.OfType<HttpGetAttribute>().Any() => "GET",
            _ when attributes.OfType<HttpPostAttribute>().Any() => "POST",
            _ when attributes.OfType<HttpPutAttribute>().Any() => "PUT",
            _ when attributes.OfType<HttpDeleteAttribute>().Any() => "DELETE",
            _ when attributes.OfType<HttpPatchAttribute>().Any() => "PATCH",
            _ => "GET"
        };

    private static string ToTypeScriptParameter(ParameterInfo paramInfo)
    {
        try
        {
            var space = ": ";
            var paramTsType = ToTypeScriptType(paramInfo.ParameterType);
            var result = string.Concat(paramInfo.Name, space, paramTsType);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private static IEnumerable<Type> GetControllerClasses()
        => Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                && typeof(ControllerBase).IsAssignableFrom(t));

    private static MethodInfo[] GetCustomMethods(Type controllerClass)
        => controllerClass.GetMethods(
            BindingFlags.Public |
            BindingFlags.Instance |
            BindingFlags.DeclaredOnly);

    private static string GetClientMethodString(MethodInfo method)
    {
        var needsAuth = method.GetCustomAttributes()
            .OfType<AuthorizeAttribute>()
            .Any();

        var methodName = ToCamelCase(method.Name);
        var typeScriptParams = GetTypeScriptParams(method, needsAuth);

        var endpointBase = GetEndpointBase(method);
        var methodUri = GetMethodEndpoint(method);

        foreach (var param in method.GetParameters().Select(p => p.Name))
        {
            methodUri = methodUri.Replace($"{{{param}}}", $"${{{param}}}");
        }

        var httpVerb = GetHttpVerb(method.GetCustomAttributes());
        var authorization = needsAuth ? $"Authorization: `Bearer ${{token}}`" : "";
        var body = GetBody(method.GetParameters());

        var returnType = GetReturnType(method);
        var returnText = string.IsNullOrEmpty(returnType)
            ? ""
            : $"const data : {returnType} = await response.json();\nreturn data;";

        var catchClauseConsoleLog = _clientLogging
            ? $"console.log(\"{method.Name} -\", error);"
            : $"throw new Error(\"{method.Name} \" + error)";

        return $$"""

		export const {{methodName}} = async ({{typeScriptParams}}) => {
			try {
				const response = await fetch(`{{endpointBase}}/{{methodUri}}}`, {
					method: "{{httpVerb}}",
					headers: {
						"Content-Type": "application/json",
						{{authorization}}
					},
					{{body}}
				});

				if (!response.ok) {
					const errorMessage = await response.json();
					throw new Error(errorMessage);
				}

				{{returnText}}
			} catch (error) {
				{{catchClauseConsoleLog}}
			}
		};
		""";
    }

    private static string GetReturnType(MethodInfo method)
    {
        var returnType = method.ReturnType;
        if (!returnType.IsGenericType)
        {
            return string.Empty;
        }

        var param = returnType.GetGenericArguments()[0];

        if (param.IsGenericType && param.GetGenericTypeDefinition() == typeof(ActionResult<>))
        {
            param = param.GetGenericArguments()[0];
        }

        if (param == typeof(IActionResult))
        {
            return string.Empty;
        }

        var tsType = ToTypeScriptType(param);
        return tsType;
    }

    private static string GetImportType(MethodInfo method)
    {
        var returnType = method.ReturnType;
        if (!returnType.IsGenericType)
        {
            return string.Empty;
        }

        var param = ExtractInnerGenericArgument(returnType);

        if (!typeof(ITypeScriptModel).IsAssignableFrom(param))
        {
            return string.Empty;
        }

        var tsType = ToTypeScriptType(param).Replace("[]", string.Empty);
        return tsType;
    }

    private static Type ExtractInnerGenericArgument(Type type)
    {
        if (!type.IsGenericType)
        {
            return type;
        }

        type = type.GetGenericArguments()[0];
        return ExtractInnerGenericArgument(type);
    }

    private static string GetBody(ParameterInfo[] parameterInfos)
        => parameterInfos.Where(p => p.GetCustomAttributes().OfType<FromBodyAttribute>().Any())
               .Select(p => $"body: JSON.stringify({p.Name}),")
               .FirstOrDefault() ?? "";

    private static string GetTypeScriptParams(MethodInfo method, bool needsAuth)
    {
        var methodParams = string.Join(", ", method.GetParameters().Select(ToTypeScriptParameter));
        if (needsAuth)
        {
            var tokenString = methodParams.Length == 0 ? "token: string" : ", token: string";
            methodParams = string.Concat(methodParams, tokenString);
        }

        return methodParams;
    }

    private static string GetMethodEndpoint(MethodInfo method)
        => (method.GetCustomAttributes()
               .FirstOrDefault(a => a is HttpMethodAttribute) as HttpMethodAttribute)?
               .Template ?? "";
}