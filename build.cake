#load "parameters.cake"

var parameters = BuildParameters.GetParameters(Context);

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild("src/Cake.Azure.sln", new MSBuildSettings
        {
            ToolPath = parameters.MSBuildPath
        }
        .SetConfiguration(parameters.SolutionBuildConfiguration)
        .WithTarget("restore")
    );
});

Task("Create-Version-Info")
    .Does(() =>
{
    CreateAssemblyInfo(File("AssemblyVersionInfo.cs"), new AssemblyInfoSettings 
	{
		Version = parameters.Version,
		FileVersion = parameters.Version,
		InformationalVersion = parameters.FullVersion
	});
});

Task("Build")
    .IsDependentOn("Create-Version-Info")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild("src/Cake.Azure.sln", new MSBuildSettings
        {
            ToolPath = parameters.MSBuildPath
        }
        .SetConfiguration(parameters.SolutionBuildConfiguration)
        .WithTarget("build")
    );
});

Task("Pack")
    .IsDependentOn("Build")
    .Does(() =>
{
    MSBuild("src/Cake.Azure/Cake.Azure.csproj", new MSBuildSettings
        {
            ToolPath = parameters.MSBuildPath
        }
        .SetConfiguration(parameters.SolutionBuildConfiguration)
        .WithTarget("pack")
        .WithProperty("PackageVersion", new[]{ parameters.Version })
        .WithProperty("PackageOutputPath", new[]{ MakeAbsolute(Directory("build")).FullPath })
    );
});

Task("Default")
    .IsDependentOn("Pack")
    .Does(() =>
{
});

RunTarget(parameters.Target);