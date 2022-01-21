// -----------------------------------------------------------------------------------------------
//  JsonFileTests.cs by Marcus Medina, Copyright (C) 2021, http://MarcusMedina.Pro.
//  Published under Apache License 2.0 (Apache-2.0)
//  https://tldrlegal.com/license/apache-license-2.0-%28apache-2.0%29
// -----------------------------------------------------------------------------------------------

namespace JsonFileWrapperTests;

using System.Collections.Generic;
using System.IO;
using MarcusMedinaPro.JsonFileWrapper;
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
    /// The test message
    /// </summary>
    private const string message = "Hello Crazy World";

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
    /// Test to see if implicit conversion works.
    /// </summary>
    [TestMethod()]
    public void ImplicitGetDataTest()
    {
        // Arrange
        var file = new JsonFile<List<string>>(filename)
        {
            Data = new List<string> { "One", "Two", "Three" }
        };
        var expected = file.Data.Count;
        // act
        List<string> actual = file;

        Assert.AreEqual(expected, actual.Count);
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
        var file = new JsonFile<List<string>>("test")
        {
            Data = new List<string>() { message },
            Format = null
        };
        Assert.IsNotNull(file);
    }

    /// <summary>
    /// Loads a file that does not exist test.
    /// </summary>
    [TestMethod()]
    public void LoadEmptyFileTest()
    {
        // Arrange
        const int expected = 0;
        var file = new JsonFile<List<string>>("FileDoesNotExist");

        // Act
        _ = file.Load();

        // Assert
        Assert.IsNotNull(file.Data);
        Assert.AreEqual(expected, file.Data.Count);
    }

    /// <summary>
    /// Loads a test.
    /// </summary>
    [TestMethod()]
    public void LoadTest()
    {
        // Arrange
        var expected = new List<string>() { message };
        File.WriteAllText(filename, message);
        var file = new JsonFile<List<string>>(filename);

        // Act
        _ = file.Load();

        // Assert
        Assert.AreEqual(string.Join(',', expected), message);
    }

    /// <summary>
    /// Loads a file that does not exist test.
    /// </summary>
    [TestMethod()]
    public void SavesEvenIfDataIsNull()
    {
        // Arrange
        const int expected = 0;
        var file = new JsonFile<List<string>>("DataNull");

        // Act
        file.Save();

        // Assert
        Assert.IsNotNull(file.Data);
        Assert.AreEqual(expected, file.Data.Count);
    }

    /// <summary>
    /// Saves a test.
    /// </summary>
    [TestMethod()]
    public void SaveTest()
    {
        //Arrange
        var file = new JsonFile<List<string>>(filename)
        {
            Data = new List<string>() { "Hello Crazy World" },
            Format = null,
        };

        // Act
        file.Save();

        // Assert
        Assert.IsNotNull(file);
    }

    /// <summary>
    /// Saves a test.
    /// </summary>
    [TestMethod()]
    public void SaveTestBackup()
    {
        //Arrange
        var file = new JsonFile<List<string>>(filename)
        {
            Data = new List<string>() { "Hello Crazy World" },
            Format = null,
        };

        if (File.Exists(filename + ".json.bak"))
            File.Delete(filename + ".json.bak");

        // Act
        file.Save();
        file.Save();

        // Assert
        Assert.IsTrue(File.Exists(filename+".json.bak"));
    }
}