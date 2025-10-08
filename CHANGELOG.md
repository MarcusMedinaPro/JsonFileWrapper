# Changelog

All notable changes to JsonFileWrapper will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2025-01-XX

### Added
- Comprehensive README with examples and migration guide
- Modern documentation with badges and better formatting

### Changed
- **BREAKING**: Migrated from Newtonsoft.Json to System.Text.Json
- **BREAKING**: Updated target framework from .NET 6 to .NET 7
- **BREAKING**: Migrated tests from MSTest to xUnit
- **BREAKING**: Changed license from Apache 2.0 to MIT
- Updated copyright year to 2022-2025
- Improved null-coalescing operator usage (`??=`)
- Updated package description to specify .NET 7+ support
- Added `LangVersion` to use latest C# features
- Enabled XML documentation generation

### Fixed
- Removed duplicate version properties in csproj file

### Removed
- Dependency on Newtonsoft.Json (now uses built-in System.Text.Json)

## [1.0.4] - 2022-XX-XX

### Added
- New icons for package

## [1.0.3] - 2022-XX-XX

### Changed
- Minor updates and improvements

## [1.0.2] - 2022-XX-XX

### Changed
- Minor updates and improvements

## [1.0.1] - 2022-XX-XX

### Changed
- Initial improvements

## [1.0.0] - 2022-XX-XX

### Added
- Initial release
- Generic JSON file wrapper class
- Automatic backup on save
- IDisposable implementation
- Implicit operator for type conversion
- Support for .NET 6

---

[1.1.0]: https://github.com/MarcusMedinaPro/JsonFileWrapper/compare/v1.0.4...v1.1.0
[1.0.4]: https://github.com/MarcusMedinaPro/JsonFileWrapper/releases/tag/v1.0.4
