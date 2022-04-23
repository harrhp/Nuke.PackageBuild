using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.PackageBuild.Extensions;

namespace Nuke.PackageBuild.Components;

public interface SolutionComponent : INukeBuild
{
    [Solution]
    Solution Solution => this.GetValue(() => Solution);
}
