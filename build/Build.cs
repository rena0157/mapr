using System.IO;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Pack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter]
    readonly string NugetApiKey;

    [Parameter]
    readonly string GitHubToken;
    
    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion(Framework = "netcoreapp3.1", UpdateBuildNumber = true)] readonly GitVersion GitVersion;
    
    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(t => t
                .SetCollectCoverage(true)
                .SetCoverletOutputFormat(CoverletOutputFormat.cobertura)
                .SetProjectFile(Solution)
            );
        });

    Target Pack => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            DotNetPack(p => p
                .SetProject(Solution)
                .SetVersion(GitVersion.NuGetVersion)
                .SetConfiguration(Configuration)
                .SetProperty("ContinuousIntegrationBuild", true)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetOutputDirectory(OutputDirectory));
        });

    Target Publish => _ => _
        .DependsOn(Pack)
        .Executes(() =>
        {
            if (GitRepository.IsOnDevelopBranch())
            {
                DotNetNuGetPush(p => p
                    .SetTargetPath(OutputDirectory / "*.nupkg")
                    .SetSource("https://nuget.pkg.github.com/rena0157/index.json")
                    .SetApiKey(GitHubToken));
            }
            else if (GitRepository.IsOnMasterBranch())
            {
                DotNetNuGetPush(p => p
                    .SetTargetPath(OutputDirectory / "*.nupkg")
                    .SetSource("https://api.nuget.org/v3/index.json")
                    .SetApiKey(NugetApiKey));
            }
        });
}