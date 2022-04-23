using Nuke.Common;
using Nuke.Common.Git;
using Nuke.PackageBuild.Extensions;

namespace Nuke.PackageBuild.Components;

public interface RepositoryComponent : INukeBuild
{
    [GitRepository]
    GitRepository Repository => this.GetValue(() => Repository);
}
