# Plan: Update OpenCode to .NET 10

## Context

OpenCode currently targets .NET 8 and .NET 9 (version 3.2.0). This update adds .NET 10 as a supported target framework and bumps the package version to 3.3.0. All three targets (net8.0, net9.0, net10.0) will be kept. Existing conditional compilation directives (`NET7_0_OR_GREATER`, `NET8_0_OR_GREATER`) automatically apply to .NET 10 — no source code changes needed.

## Changes

### 1. Update library project
**File:** `OpenCode/OpenCode.csproj`
- Line 4: `<TargetFrameworks>net8.0;net9.0</TargetFrameworks>` → `<TargetFrameworks>net8.0;net9.0;net10.0</TargetFrameworks>`
- Line 7: `<Version>3.2.0</Version>` → `<Version>3.3.0</Version>`

### 2. Update test project
**File:** `OpenCode.Tests/OpenCode.Tests.csproj`
- Line 4: `<TargetFrameworks>net8.0;net9.0</TargetFrameworks>` → `<TargetFrameworks>net8.0;net9.0;net10.0</TargetFrameworks>`

### 3. Update CI/CD pipeline
**File:** `.github/workflows/nuget-publish.yml`
- Line 17: `dotnet-version: [8.x, 9.x]` → `dotnet-version: [8.x, 9.x, 10.x]`
- Lines 37, 41, 48: Change `if: matrix.dotnet-version == '9.x'` → `if: matrix.dotnet-version == '10.x'` (pack/publish from latest SDK)

### 4. Update README
**File:** `README.md`
- Line 3: "targets **.NET 8 through .NET 9**" → "targets **.NET 8 through .NET 10**"
- Line 9: "- .NET 8, .NET 9" → "- .NET 8, .NET 9, .NET 10"

### 5. Update CLAUDE.md
**File:** `CLAUDE.md`
- Line 7: "targeting .NET 8 and .NET 9" → "targeting .NET 8, .NET 9, and .NET 10"

### 6. Update CHANGELOG
**File:** `CHANGELOG.md`
- Add a new `[3.3.0]` section at the top documenting the addition of .NET 10 support

## No source code changes needed
- `StringExtension.Core.cs` — `#if NET7_0_OR_GREATER` covers .NET 10
- `StringExtension.Hash.cs` — `#if NET8_0_OR_GREATER` covers .NET 10

## Verification
```bash
dotnet build                                              # builds all 3 TFMs
dotnet test                                               # tests pass on all 3 TFMs
dotnet pack --configuration Release --output ./nupkg      # package includes all targets
```
