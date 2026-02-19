# Changelog

## [4.0.0] - 2026-02-10

### Added
- **New String extensions:** `Reverse`, `ContainsIgnoreCase`, `Left`, `Right`, `WordCount`, `EnsureStartsWith`, `EnsureEndsWith`, `FromShortGuid`
- **New DateTime extensions:** `IsWeekday`, `IsBetween`, `ToUnixTimestamp`, `StartOfMonth`, `EndOfMonth`, `StartOfWeek`, `DaysInMonth`
- **New Integer extensions:** `IsBetween`, `IsPositive`, `IsNegative`
- **New Decimal extensions:** `IsPositive`, `IsNegative`
- .NET 10 support — the project now targets .NET 8, .NET 9, and .NET 10

### Changed
- Updated CI/CD pipeline to build, test, and publish from .NET 10

### Fixed
- `IsEmail` and `IsJson` parameter type changed from `string` to `string?` for consistency
- `IsInteger` now trims input before parsing, consistent with `ParseToInt`
- `ToKebabCase` now uses pre-compiled/source-generated regex instead of inline `Regex.Replace`
- `FromBase64` now catches `FormatException` specifically instead of bare `catch`
- `ToSHA256Hash` null check changed from `IsNullOrWhiteSpace` to `IsNullOrEmpty` for consistency with `ToMD5Hash`

### Breaking Changes
- **`ToSHA256Hash`** now returns lowercase hexadecimal (matching `ToMD5Hash`) instead of Base64
- **`ToCurrency`** default culture changed from `CurrentCulture` to `InvariantCulture` for consistency with `ToMoneyFormat`

---

## [3.0.0] - YYYY-MM-DD

### Added
- N/A

### Changed
- **Upgrade from .NET Standard 2.1 to .NET 9**  
  The project has been upgraded from .NET Standard 2.1 to .NET 9. This brings enhanced performance, improved security, and access to modern features in .NET 9.

### Deprecated
- N/A

### Removed
- N/A

### Fixed
- N/A

### Security
- N/A

### Breaking Changes
- Projects previously depending on .NET Standard 2.1 may face compatibility issues with .NET 9. Any dependencies using .NET Standard 2.1 need to be updated to versions compatible with .NET 9 or higher.

### Future Considerations
- Ensure that all dependencies are updated to be compatible with .NET 9 to fully benefit from the platform's capabilities.

