// -----------------------------------------------------------------------------------------------
//  JsonFileTests.cs by Marcus Medina, Copyright (C) 2021, http://MarcusMedina.Pro.
//  Published under Apache License 2.0 (Apache-2.0)
//  https://tldrlegal.com/license/apache-license-2.0-%28apache-2.0%29
// -----------------------------------------------------------------------------------------------

namespace JsonFileWrapperTests;

using System;
using System.Collections.Generic;
using System.IO;
using MarcusMedinaPro.JsonFileWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

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
}