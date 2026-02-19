# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

OpenCode is a .NET utility library providing extension methods for common types (string, bool, int, decimal, DateTime, Guid). It is published as a NuGet package targeting .NET 8, .NET 9, and .NET 10.

## Build & Test Commands

```bash
# Build
dotnet build
dotnet build --configuration Release

# Run all tests
dotnet test

# Run tests for a specific framework
dotnet test --framework net9.0

# Run a single test class
dotnet test --filter "FullyQualifiedName~OpenCode.Tests.StringExtension.StringExtensionCoreTests"

# Run a single test method
dotnet test --filter "FullyQualifiedName~MethodName"

# Pack NuGet package
dotnet pack --configuration Release --output ./nupkg
```

No separate lint command — code style is enforced via `.editorconfig`.

## Architecture

The solution has two projects:

- **OpenCode/** — The library. Each type's extensions live in a subfolder (e.g., `StringExtension/`, `IntegerExtension/`). StringExtension is split across multiple files using `partial class` (e.g., `StringExtension.Core.cs`, `StringExtension.Hash.cs`, `StringExtension.Bool.cs`).
- **OpenCode.Tests/** — xUnit tests mirroring the source folder structure 1:1.

All extension classes are `public static` in the `namespace OpenCode;` (file-scoped). StringExtension classes are `partial`; other type extensions are `sealed`.

## Key Conventions

- **Null safety**: Extension methods handle null inputs gracefully (return defaults, empty strings, etc.) — they never throw on null. Nullable reference types are enabled.
- **Culture**: Use `CultureInfo.InvariantCulture` for all parsing and formatting.
- **Conditional compilation**: .NET 7+ uses source-generated regex via `[GeneratedRegex]` attributes on `private static partial Regex` methods. Older targets use `static readonly Regex` fields with `RegexOptions.Compiled`.
- **XML docs**: All public members have `<summary>`, `<param>`, `<returns>` documentation.
- **Tests**: xUnit with `[Theory]`/`[InlineData]` for parameterized tests. Test class naming: `{ClassName}Tests`.

## CI/CD

GitHub Actions workflow (`.github/workflows/nuget-publish.yml`) triggers on push to `release/*` branches or `v*` tags. It tests on .NET 8, 9, and 10, then packs and publishes to nuget.org from the .NET 10 matrix run using OIDC authentication.

## Branching

Release branches follow the pattern `release/{version}` (e.g., `release/3.2`). The current main development branch is `release/3.2`.
