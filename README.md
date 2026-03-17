# JsonFileWrapper

Simple generic JSON file persistence for .NET 10 and C# 14.

## Install

```bash
dotnet add package JsonFileWrapper
```

## Usage

```csharp
using MarcusMedinaPro.JsonFileWrapper;

var file = new JsonFile<List<string>>("actors")
{
    Data = ["Allison", "Vera"],
};

file.Save();
```

This writes `actors.json`, creates `.bak` backups on overwrite, and can reload through `Load()` or the implicit conversion operator.

## Sample

```bash
dotnet run --project samples/JsonFileWrapper.Demo/JsonFileWrapper.Demo.csproj
```
