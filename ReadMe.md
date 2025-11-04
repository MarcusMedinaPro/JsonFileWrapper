# JsonFileWrapper

[![NuGet](https://img.shields.io/nuget/v/JsonFileWrapper.svg)](https://www.nuget.org/packages/JsonFileWrapper/)
[![.NET](https://img.shields.io/badge/.NET-8.0+-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

A simple, educational generic wrapper for JSON file operations in .NET. Simplifies 
loading and saving strongly-typed objects to JSON files with automatic backup support.

## Overview

JsonFileWrapper was created as an educational tool to demonstrate generic classes 
and the Bridge design pattern. It provides a clean API for persisting objects to 
JSON files without writing repetitive serialization code.

## Features

- ✅ **Generic Type Support** - Works with any serializable .NET type
- ✅ **Automatic Backup** - Creates `.bak` files before overwriting
- ✅ **Zero External Dependencies** - Uses built-in `System.Text.Json`
- ✅ **Simple API** - Load, modify, and save in just a few lines
- ✅ **Null-Safe** - Handles missing files and null data gracefully
- ✅ **IDisposable Support** - Auto-saves on disposal

## Requirements

- .NET 8.0 or higher

## Installation

### Package Manager Console
```powershell
Install-Package JsonFileWrapper
```

### .NET CLI
```bash
dotnet add package JsonFileWrapper
```

### Package Reference
```xml
<PackageReference Include="JsonFileWrapper" Version="1.1.0" />
```

## Quick Start

### Define Your Model

```csharp
public class Person
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Movie { get; set; } = string.Empty; // Film de medverkat i
    public int Year { get; set; } // Årtal för filmen
}
```

### Load, Modify, and Save

```csharp
using MarcusMedinaPro.JsonFileWrapper;

// Create or load existing file
var file = new JsonFile<List<Person>>("actors")
{
    Data = new List<Person>()
};

// Add data
file.Data.Add(new Person { Name = "Allison", LastName = "Williams", Age = 35, Movie = "M3GAN", Year = 2022 });
file.Data.Add(new Person { Name = "Violet", LastName = "McGraw", Age = 13, Movie = "M3GAN", Year = 2022 });
file.Data.Add(new Person { Name = "Vera", LastName = "Farmiga", Age = 50, Movie = "The Conjuring", Year = 2013 });
file.Data.Add(new Person { Name = "Patrick", LastName = "Wilson", Age = 50, Movie = "The Conjuring", Year = 2013 });

// Save to actors.json
file.Save();
```

### Output (`actors.json`)

```json
[
  {
    "Name": "Allison",
    "LastName": "Williams",
    "Age": 35,
    "Movie": "M3GAN",
    "Year": 2022
  },
  {
    "Name": "Violet",
    "LastName": "McGraw",
    "Age": 13,
    "Movie": "M3GAN",
    "Year": 2022
  },
  {
    "Name": "Vera",
    "LastName": "Farmiga",
    "Age": 50,
    "Movie": "The Conjuring",
    "Year": 2013
  },
  {
    "Name": "Patrick",
    "LastName": "Wilson",
    "Age": 50,
    "Movie": "The Conjuring",
    "Year": 2013
  }
]
```

## Advanced Usage

### Custom JSON Formatting

```csharp
var file = new JsonFile<List<Person>>("data")
{
    Format = new JsonSerializerOptions
    {
        WriteIndented = false,  // Compact JSON
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    }
};
```

### Implicit Conversion

```csharp
var file = new JsonFile<List<Person>>("actors");
List<Person> people = file;  // Implicit conversion
```

### Auto-Save with IDisposable

```csharp
using (var file = new JsonFile<List<Person>>("actors"))
{
    file.Data.Add(new Person { Name = "New", LastName = "Actor" });
    // Automatically saves on dispose
}
```

## API Reference

### Constructor

```csharp
public JsonFile(string filename)
```

Creates a new instance and attempts to load existing data from `{filename}.json`.

### Properties

| Property | Type | Description |
|----------|------|-------------|
| `Data` | `T?` | The data object being managed |
| `Filename` | `string` | Filename without extension |
| `Suffix` | `string` | File extension (default: "json") |
| `Format` | `JsonSerializerOptions?` | JSON serialization options |

### Methods

| Method | Returns | Description |
|--------|---------|-------------|
| `Load()` | `T?` | Loads data from file |
| `Save()` | `void` | Saves data to file with backup |
| `Dispose()` | `void` | Saves and disposes resources |

## How It Works

1. **Load**: Reads JSON file if it exists, deserializes to type `T`
2. **Modify**: Work with the strongly-typed `Data` property
3. **Save**:
   - Creates backup (`.json.bak`) of existing file
   - Writes new data to `.json` file

## Error Handling

JsonFileWrapper handles common errors gracefully:

- **Missing files**: Returns new instance of `T`
- **Corrupt JSON**: Logs error and returns new instance
- **File access errors**: Logs error using `Debug.WriteLine`

## Educational Purpose

This library demonstrates several C# concepts:

- **Generic Classes**: `JsonFile<T>` works with any type
- **Design Patterns**: Loosely based on the Bridge pattern
- **Serialization**: Using modern `System.Text.Json`
- **Resource Management**: Implementing `IDisposable`

## Migration from v1.0.x

Version 1.1.0 introduces breaking changes:

- **Newtonsoft.Json → System.Text.Json**: Update your `Format` settings
- **.NET 6 → .NET 8**: Update project target framework
- **MSTest → xUnit**: If using tests, update test framework

### Updating JsonSerializerSettings

**Before (v1.0.x with Newtonsoft.Json)**:
```csharp
Format = new JsonSerializerSettings
{
    Formatting = Formatting.Indented,
    NullValueHandling = NullValueHandling.Ignore
};
```

**After (v1.1.0 with System.Text.Json)**:
```csharp
Format = new JsonSerializerOptions
{
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};
```

## Contributing

Contributions are welcome! Please:

1. Open an issue to discuss major changes
2. Fork the repository
3. Create a feature branch
4. Add/update tests as appropriate
5. Submit a pull request

## Source Code

Full source available on [GitHub](https://github.com/MarcusMedinaPro/JsonFileWrapper)

## License

Licensed under [MIT License](https://opensource.org/licenses/MIT)

## Credits

- **Author**: Marcus Medina
- **Icon**: [Json file Icon](https://iconscout.com/icons/json-file) by [First Styles](https://iconscout.com/contributors/first-styles) on [Iconscout](https://iconscout.com)

---

**Made with ❤️ for educational purposes**

---
_For metadata and SEO keywords, see [SEO.md](SEO.md)._