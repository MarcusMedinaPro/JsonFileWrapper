// -----------------------------------------------------------------------------------------------
//  JsonFileTests.cs by Marcus Medina, Copyright (C) 2021, http://MarcusMedina.Pro.
//  Published under Apache License 2.0 (Apache-2.0)
//  https://tldrlegal.com/license/apache-license-2.0-%28apache-2.0%29
// -----------------------------------------------------------------------------------------------
namespace JsonFileWrapperTests;
using System.IO;
using JsonFileWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/// <summary>
/// The json file tests.
/// </summary>
[TestClass()]
public class JsonFileTests
{
    /// <summary>
    /// The filename.
    /// </summary>
    private const string filename = "Hello.txt";

    /// <summary>
    /// Cleans up the tests.
    /// </summary>
    [TestCleanup]
    public void CleanUp()
    {
        if (File.Exists(filename))
            File.Delete(filename);
    }

    /// <summary>
    /// Initialize the tests
    /// </summary>
    [TestInitialize]
    public void Init()
    {
        if (File.Exists(filename))
            File.Delete(filename);
    }

    /// <summary>
    /// Jsons the file test.
    /// </summary>
    [TestMethod()]
    public void JsonFileTest()
    {
        var file = new JsonFile<string>("");
        Assert.IsNotNull(file);
    }

    /// <summary>
    /// Loads a test.
    /// </summary>
    [TestMethod()]
    public void LoadTest()
    {
        // Arrange
        const string? expected = "Hello Crazy World";
        File.WriteAllText(filename, expected);
        var file = new JsonFile<string>(filename);

        // Act
        _ = file.Load();

        // Assert
        Assert.AreEqual(expected, file.Data);
    }

    /// <summary>
    /// Saves a test.
    /// </summary>
    [TestMethod()]
    public void SaveTest()
    {
        //Arrange
        var file = new JsonFile<string>(filename)
        {
            Data = "Hello Crazy World"
        };

        // Act
        file.Save();

        // Assert
        Assert.IsNotNull(file);
    }
}
