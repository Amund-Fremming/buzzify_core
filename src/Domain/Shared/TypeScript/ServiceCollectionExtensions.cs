using Microsoft.Extensions.DependencyInjection;

namespace Domain.Shared.TypeScript;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTypeScriptSupport(this IServiceCollection services, Action<TypeScriptGenerationOptions>? configureOptions)
    {
        var options = new TypeScriptGenerationOptions();
        configureOptions?.Invoke(options);

        if (!options.GenerateOnBuild)
        {
            return services;
        }

        TypeScriptTypeGenerator.Generate();
        TypeScriptClientGenerator.Generate(options.ClientLogging, options.RelativeFolderPath);

        return services;
    }
}