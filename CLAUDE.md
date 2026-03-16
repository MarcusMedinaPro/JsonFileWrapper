# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**MarcusMedinaPro.JsonFileWrapper** - A C# NuGet package providing a generic JSON file wrapper with automatic Save/Load functionality and backup creation.

- **Language:** C# 14.0
- **Framework:** .NET 10.0+
- **Pattern:** Generic wrapper with implicit conversion
- **License:** MIT

## Quick Commands

### Development
```bash
# Restore and build
cd csharp
dotnet restore
dotnet build --configuration Release

# Run tests
dotnet test --configuration Release

# Pack NuGet package
dotnet pack src/JsonFileWrapper/JsonFileWrapper.csproj --configuration Release
```

### Release & Publishing
```bash
# Create version tag (triggers GitHub Actions)
git tag -a v1.2.0 -m "Release v1.2.0"
git push origin v1.2.0

# The multi-stage GitHub Actions pipeline will:
# 1. Build and test
# 2. Run CodeQL security analysis
# 3. Sign packages with certificate
# 4. Publish to NuGet.org
```

## Architecture

### Core Component

**JsonFile<T>** (`JsonFile.cs`)
- Generic wrapper class implementing `IDisposable`
- Automatic load on construction
- Automatic save on dispose
- Backup file creation before save (`.bak` extension)
- Implicit conversion operator from `JsonFile<T>` to `T`
- Configurable `JsonSerializerOptions` (indented, null-ignoring, cycle-handling)
- Customisable file suffix (default: `.json`)

### Key Patterns

**Basic Usage:**
```csharp
using var settings = new JsonFile<AppSettings>("config");
settings.Data.Theme = "dark";
// Auto-saves on dispose
```

**Implicit Conversion:**
```csharp
var file = new JsonFile<MyData>("data");
MyData data = file; // Implicit conversion
```

### File Structure
```
csharp/
├── GenericJsonWrap.sln
└── src/
    ├── JsonFileWrapper/
    │   ├── JsonFile.cs              # Core generic wrapper
    │   └── JsonFileWrapper.csproj   # NuGet package config
    └── JsonFileWrapperTests/
        └── JsonFileWrapperTests.csproj  # xUnit tests
```

## Testing Strategy

- **Framework:** xUnit
- **Coverage tools:** coverlet.collector
- Tests verify:
  - Load/Save round-trip correctness
  - Backup file creation
  - Error handling for missing/corrupt files
  - Implicit conversion operator
  - Dispose behaviour

## GitHub Actions Workflows

**Release Pipeline** (`.github/workflows/release.yml`)
- Triggers on: `git push` with `v*` tags or to `main`/`release` branches
- 4-stage pipeline:
  1. **Build & Test** - Restore, build, test, pack
  2. **Quality Gate** - CodeQL analysis, vulnerability scanning
  3. **Package Signing** - Sign packages, verify signatures, SHA256 checksums
  4. **Publish to NuGet** - Only runs on version tags

**Develop Pipeline** (`.github/workflows/develop.yml`)
- Unit tests only on develop branch

**Test Pipeline** (`.github/workflows/test.yml`)
- Unit tests + CodeQL on test branch

**Required Secrets:**
- `NUGET_API_KEY` - NuGet.org API key
- `NUGET_SIGNING_CERT` - Base64-encoded signing certificate (.pfx)
- `NUGET_SIGNING_CERT_PASSWORD` - Certificate password

## NuGet Package Configuration

Key settings in `.csproj`:
- **Package ID:** `JsonFileWrapper`
- **Version:** 1.1.0
- **Description:** JSON file wrapper with Save and load functions for .NET 10+
- **License:** MIT
- **Repository:** https://github.com/MarcusMedinaPro/JsonFileWrapper

## Semantic Versioning

Version format: `MAJOR.MINOR.PATCH`

- **MAJOR** - Breaking changes to API
- **MINOR** - New features, backwards compatible
- **PATCH** - Bug fixes, no API changes
