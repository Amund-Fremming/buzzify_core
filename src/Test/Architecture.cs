﻿using System.Xml.Linq;

namespace Test;

public class DependencyTest
{
    private static string GetProjectReference(string projectPath)
    {
        var doc = XDocument.Load(projectPath);
        return string.Join(", ", doc.Descendants("ProjectReference")
            .Select(x => x.Attribute("Include")?.Value ?? ""));
    }

    [Fact]
    public void Presentation_Should_Only_Reference_Application_And_Infrastructure()
    {
        var references = GetProjectReference("../../../../../src/Presentation/Presentation.csproj");

        Assert.Contains("Infrastructure.csproj", references);
        Assert.Contains("Application.csproj", references);
        Assert.DoesNotContain("Domain.csproj", references);
    }

    [Fact]
    public void Application_Should_Only_Reference_Domain()
    {
        var references = GetProjectReference("../../../../../src/Application/Application.csproj");

        Assert.DoesNotContain("Infrastructure.csproj", references);
        Assert.DoesNotContain("Presentation.csproj", references);
        Assert.Contains("Domain.csproj", references);
    }

    [Fact]
    public void Infrastructure_Should_Only_Reference_Application()
    {
        var references = GetProjectReference("../../../../../src/Infrastructure/Infrastructure.csproj");

        Assert.DoesNotContain("Presentation.csproj", references);
        Assert.DoesNotContain("Domain.csproj", references);
        Assert.Contains("Application.csproj", references);
    }
}