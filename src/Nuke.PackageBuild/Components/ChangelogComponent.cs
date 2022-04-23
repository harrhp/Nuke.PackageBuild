using Nuke.Common;
using Nuke.Common.Git;
using Nuke.Common.IO;
using static Nuke.Common.ChangeLog.ChangelogTasks;
using static Nuke.Common.Tools.Git.GitTasks;

namespace Nuke.PackageBuild.Components;

[ParameterPrefix("Changelog")]
public interface ChangelogComponent : RepositoryComponent, VersionComponent, PublishComponent
{
    AbsolutePath ChangelogFile => RootDirectory / "CHANGELOG.md";

    Target Changelog =>
        _ => _.Requires(() => IsLocalBuild)
            .Requires(() => Repository.IsOnMainBranch() || Repository.IsOnMasterBranch())
            .Requires(() => GitHasCleanWorkingCopy())
            .Requires(() => File.Exists(ChangelogFile))
            .After(PublishAggegate)
            .Executes(() =>
            {
                FinalizeChangelog(ChangelogFile, Version, Repository);
                Git($"add {ChangelogFile}");
                Git($"commit -m \"Finalize {Path.GetFileName(ChangelogFile)} for {Version}\"");
            });
}
