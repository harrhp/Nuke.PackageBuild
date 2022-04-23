using Nuke.Common;
using Nuke.PackageBuild.Components;

namespace Nuke.PackageBuild;

internal class Build : NukeBuild, ReleaseComponent
{
    public static int Main() => Execute<Build>();

    protected override void OnBuildCreated()
    {
        base.OnBuildCreated();
        NoLogo = true;
    }
}
