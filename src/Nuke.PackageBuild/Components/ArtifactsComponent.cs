using Nuke.Common.IO;
using static Nuke.Common.NukeBuild;

namespace Nuke.PackageBuild.Components;

public interface ArtifactsComponent
{
    static AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
}
