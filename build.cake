#load "parameters.cake"

var parameters = BuildParameters.GetParameters(Context);

Task("Build")
    .Does(() =>
{
	DotNetCoreBuild("src/Cake.Azure.sln", new DotNetCoreBuildSettings
	{
		Configuration = parameters.SolutionBuildConfiguration,
		MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersion(parameters.AssemblyVersion)
	});
});

Task("Unit-Test")
	.IsDependentOn("Build")
    .Does(() =>
{
    DotNetCoreTest("src/Cake.Azure.sln");
});

Task("Pack")
    .IsDependentOn("Unit-Test")
    .Does(() =>
{
	DotNetCorePack("src/Cake.Azure.sln", new DotNetCorePackSettings
	{
		Configuration = parameters.SolutionBuildConfiguration,
		NoBuild = true,
		NoRestore = true,
		OutputDirectory = "build",
		MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersion(parameters.AssemblyVersion)
	});
});

Task("Default")
    .IsDependentOn("Pack")
    .Does(() =>
{
});

RunTarget(parameters.Target);