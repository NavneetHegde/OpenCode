# OpenCode

**OpenCode** is a lightweight, cross-targeted .NET library providing useful extension methods for strings, numbers, booleans, GUIDs, and dates. It simplifies common tasks such as safe trimming, casing conversions, masking, parsing, numeric formatting, hashing, and more. The library targets **.NET 8 through .NET 9**.

---

## Supported Targets

- .NET 8, .NET 9

---

## Features

### Core Helpers

- **`NullIfEmpty`** — returns `null` for `null`/empty/whitespace input.  
- **`OrDefault`** — returns a specified default value when input is `null`/empty/whitespace.  
- **`SafeTrim`** — trims input safely; returns empty string if input is `null`.  
- **`Truncate`** — safely truncates a string to a maximum length.  
- **`SafeSubstring`** — extracts substring without throwing exceptions for out-of-range indices.  
- **`EqualsIgnoreCase`** — ordinal case-insensitive comparison.  
- **`IsNumeric`** — checks if a string can be parsed as a number (decimal).  

### Formatting Helpers

- **`ToTitleCase`** — title-cases each word using invariant culture.  
- **`ToPascalCase`**, **`ToCamelCase`** — convert strings to PascalCase or camelCase, removing non-alphanumeric separators.  
- **`ToSnakeCase`**, **`ToKebabCase`** — convert strings to `snake_case` or `kebab-case`.  
- **`ToSlug`** — produces a URL-safe slug (diacritics removed, spaces → dashes).  
- **`Mask`** — mask middle characters, leaving configurable head/tail visible.  
- **`FormatWith`** — culture-invariant wrapper for `string.Format`.  

### Utility Helpers

- **`RemoveDiacritics`** — removes accent marks from characters.  
- **`RemoveNonAlphanumeric`** — keeps only letters and digits.  
- **`IsEmail`** — lightweight check for valid email format.  
- **`IsJson`** — heuristic check for JSON-like input (starts/ends with `{}` or `[]`).  

### Numeric Helpers (Decimal & Integer)

- **`ParseToDecimal`**, **`ParseToInt`**, **`ParseToLong`** — safe parsing with configurable defaults (invariant culture).  
- **`RemoveTrailingZero`** — removes redundant trailing zeros (uses `G29` to preserve significance).  
- **`IsDecimal`**, **`IsInteger`** — validate numeric strings.  
- **`ToOrdinal`** — converts numeric string to English ordinal (e.g., `1` → `1st`).  

### Boolean Helpers

- **`ParseToBool`** — accepts `true/false`, `1/0`, `yes/no`, `on/off`, etc., with configurable default.  
- **`IsBool`** — checks whether a string can be interpreted as boolean.  
- **`ToYesNo`** — returns `"Yes"` or `"No"` based on parsed truthiness.  

### Hashing & Encoding

- **`ToMD5Hash`** — computes MD5 hash (lowercase hex).  
- **`ToSHA256Hash`** — computes SHA256 hash (Base64).  
- **`ToBase64`** / **`FromBase64`** — encode/decode Base64 strings safely.  

### Guid Helpers

- **`ParseToGuid`**, **`IsGuid`**, **`IsValidGuid`** — parse and validate GUIDs.  
- **`ToShortGuid`**, **`ToCompactString`** — compact representations of GUIDs.  

### Date & Time Helpers

- **`ParseToDateTime`**, **`ParseToDateOnly`** — safe parsing with default values.  
- **`IsDateTime`** — check if string can be parsed to DateTime.  
- **`ToFormattedDate`** — format parsed DateTime string.  

---

## Getting Started

### Prerequisites

- [.NET 8 SDK or later](https://dotnet.microsoft.com/en-us/download/dotnet)  
- Visual Studio 2022 or later (or any editor/IDE supporting .NET)

### Installation

Add a project reference to `OpenCode` or install the package from NuGet if published.

```bash
dotnet add package OpenCode
```

### Usage Examples

```csharp
using OpenCode;

// Remove trailing zeros
string compact = "2.5000".RemoveTrailingZero(); // "2.5"

// SHA256 hash
string hash = "Hello".ToSHA256Hash();

// Parse string to bool
bool isTrue = "yes".ParseToBool(); // true

// Check if string is boolean
bool validBool = "on".IsBool(); // true

// Parse string to int
int number = "123".ParseToInt(); // 123

// Convert string to PascalCase
string pascal = "hello world".ToPascalCase(); // "HelloWorld"

// Mask string
string masked = "1234567890".Mask(2, 2); // "12******90"
```

---

## Testing

Automated tests are included in the [OpenCode.Tests](../OpenCode.Tests/README.md) project.  

Run tests using CLI:

```bash
dotnet test
```

Or via Visual Studio Test Explorer.

---

## Contributing

Contributions are welcome! Please submit issues or pull requests via [GitHub](https://github.com/NavneetHegde/OpenCode).

---

## License

See [LICENSE](LICENSE) for details.

