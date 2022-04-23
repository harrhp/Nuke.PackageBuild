# Nuke Package Build

## What does it do?

Provides dotnet tool that uses [nuke](https://nuke.build/) for testing, publishing, managing changelog and releases of nuget packages

## How to use?

- Create `.nuke` directory so that nuke can automatically determine root directory or pass `--root <path>` argument to all commands
- Create `.nuke/parameters.json` file with following content or pass `--solution <path to sln>` to commands that require access to solution. You can add other parameters to this file
```json
{
	"Solution": "<path to sln relative to parent of .nuke directory>"
}
```

To see execution plan
```
npb --plan
```

This command updates changelog, tags release, publishes to nuget and create github release. It is possible to pass parameters via environment variables, just replace `-` with `_`
```bash
npb TagReleaseAndPush Publish CreateGithubRelease --publish-nuget-api-key *** --github-token ***
```

To see all targets and parametes
```
npb --help
```

[nuke documentation](https://nuke.build/docs/getting-started/philosophy.html)
