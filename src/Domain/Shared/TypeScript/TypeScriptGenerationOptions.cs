namespace Domain.Shared.TypeScript;

public class TypeScriptGenerationOptions
{
    public bool GenerateOnBuild { get; set; } = true;
    public bool ClientLogging { get; set; } = true;
    public string RelativeFolderPath { get; set; } = Directory.GetCurrentDirectory();
}