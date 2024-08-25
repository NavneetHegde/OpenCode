# OpenCode

The Custom C# Extension Methods Library is a collection of thoughtfully designed extension methods aimed at enhancing and simplifying common operations on strings and other .NET types. This project provides developers with a set of reusable, intuitive, and efficient methods that can be easily integrated into any C# project. The goal is to extend the functionality of standard .NET classes by adding value through custom methods that are frequently needed but not available in the base class libraries.


## Download/Install

[OpenCode Nuget](https://www.nuget.org/packages/OpenCode/)

## Usage

### Example 1: `ParseToBool`
```csharp
string input = "Hello";
string padded = input.ParseToBool(); // Output: false
