using Nuke.Common;
using Nuke.Common.ProjectModel;

namespace Nuke.PackageBuild.Components;

public interface VersionComponent : SolutionComponent
{
    string Version =>
        Solution.AllProjects.First(x => !string.IsNullOrEmpty(x.GetProperty("PackageId")))
            .GetProperty("Version")
            .NotNull();
}
