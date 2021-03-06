using NuGet.Versioning;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Utilities;
using Octokit;
using static Nuke.Common.ChangeLog.ChangelogTasks;
using static Nuke.Common.Tools.Git.GitTasks;

namespace Nuke.PackageBuild.Components;

public interface ReleaseComponent : ChangelogComponent
{
    [Parameter, Secret]
    string? GithubToken => TryGetValue(() => GithubToken) ?? GitHubActions.Instance?.Token;

    Target TagReleaseAndPush =>
        _ => _.Requires(() => IsLocalBuild)
            .DependsOn(Changelog)
            .Before(Publish)
            .Executes(() =>
            {
                Git($"tag {Version}");
                Git($"push --atomic origin {Repository.Branch} {Version}");
            });

    Target CreateGithubRelease =>
        _ => _.Requires(() => GithubToken)
            .After(Publish, TagReleaseAndPush)
            .OnlyWhenStatic(() => Repository.IsGitHubRepository())
            .Executes(async () =>
            {
                var githubClient = new GitHubClient(new ProductHeaderValue(nameof(NukeBuild)))
                {
                    Credentials = new Credentials(GithubToken)
                };

                await githubClient.Repository.Release.Create(
                    Repository.GetGitHubOwner(),
                    Repository.GetGitHubName(),
                    new NewRelease(Version)
                    {
                        Name = $"v{Version}",
                        Body = ExtractChangelogSectionNotes(ChangelogFile, Version).JoinNewLine(),
                        Prerelease = NuGetVersion.Parse(Version).IsPrerelease
                    });
            });
}
