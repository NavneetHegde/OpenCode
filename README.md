# OpenCode

OpenCode is a .NET 9 library providing useful string extension methods for common operations such as formatting, parsing, and hashing.

## Features

- **RemoveTrailingZero**: Removes trailing zeros from decimal-formatted strings.
- **TestRemoveTrailingZero**: Similar to `RemoveTrailingZero`, for testing purposes.
- **SHA256tHash**: Computes the SHA256 hash of a string and returns the result as a Base64 string.
- **ParseToBool**: Safely parses a string to a boolean value, with a configurable default.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022 or later

### Installation

Add a reference to the OpenCode project or install via NuGet (if available):

### Usage

Import the namespace and use the extension methods:

```c#
using OpenCode;
// Remove trailing zeros 
string compact = "2.0100".RemoveTrailingZero(); // "2.01"

// SHA256 hash 
bool hashed; 
string hash = "TestMyHash".SHA256tHash(out hashed); // hash string, hashed == true

// Parse to bool 
bool value = "true".ParseToBool(); // true

// Check if string is bool 
bool isBool = "false".IsBool(); // true

// Parse to int 
int number = "123".ParseToInt(); // 123
```

## Testing

Automated tests are available in the [OpenCode.Tests](../OpenCode.Tests/README.md) project. Run tests with:

## Contributing

Contributions are welcome! Please submit issues or pull requests via [GitHub](https://github.com/NavneetHegde/OpenCode).

## License

See [LICENSE](LICENSE) for details.

