using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace Nuke.PackageBuild.Components;

public interface TestComponent : BuildComponent
{
    Target Test =>
        _ => _.DependsOn(Build)
            .Executes(() => DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)));
}
