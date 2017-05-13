#tool "nuget:?package=ilmerge"

#load "parameters.cake"

var parameters = BuildParameters.GetParameters(Context);

Task("Restore-NuGet-Packages")
    .Does(() =>
{
    NuGetRestore("src/Cake.Azure.sln");
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
    var buildPath = "src/Cake.Azure/bin/" + parameters.SolutionBuildConfiguration + "/";

    DeleteFile(buildPath + "Cake.Azure.pdb");

    var assemblyPaths = GetFiles(buildPath + "*.dll").Where(f => !f.GetFilename().ToString().StartsWith("Cake."));
    ILMerge(
        buildPath + "Cake.Azure.dll",
        buildPath + "Cake.Azure.dll",
        assemblyPaths,
        new ILMergeSettings { Internalize = true }
    );
});

Task("Pack")
    .IsDependentOn("Build")
    .Does(() =>
{
    var buildPath = "src/Cake.Azure/bin/" + parameters.SolutionBuildConfiguration;

    var settings = new NuGetPackSettings()
    {
        Version = parameters.Version,
        BasePath = buildPath,
        OutputDirectory = "build"
    };
    NuGetPack("src/Cake.Azure/Cake.Azure.nuspec", settings);
});

Task("Default")
    .IsDependentOn("Pack")
    .Does(() =>
{
});

RunTarget(parameters.Target);