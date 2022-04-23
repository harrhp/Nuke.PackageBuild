using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Nuke.PackageBuild.Components;

[ParameterPrefix("Publish")]
public interface PublishComponent : PackComponent, TestComponent
{
    [Parameter, Secret]
    string? NugetApiKey => TryGetValue(() => NugetApiKey);

    [Parameter]
    string Source => TryGetValue(() => Source) ?? "https://api.nuget.org/v3/index.json";

    [Parameter]
    bool Local => TryGetValue<bool?>(() => Local) ?? false;

    IEnumerable<AbsolutePath> Packages => PackagesDirectory.GlobFiles("*.nupkg");

    static AbsolutePath LocalNugetPackagesDirectory =>
        (AbsolutePath)EnvironmentInfo.SpecialFolder(SpecialFolders.UserProfile) / ".nuget" / "packages";

    Target PublishAggegate => _ => _.Unlisted().DependsOn(Test, Pack);

    Target Publish =>
        _ => _.DependsOn(PublishAggegate)
            .Requires(() => Local || NugetApiKey != null)
            .Executes(() => DotNetNuGetPush(s => s
                .SetSource(Source)
                .SetApiKey(NugetApiKey)
                .When(Local,
                    ss => ss.SetSource(LocalNugetPackagesDirectory))
                .EnableSkipDuplicate()
                .CombineWith(Packages,
                    (ss, packagePath) => ss.SetTargetPath(packagePath))));
}
