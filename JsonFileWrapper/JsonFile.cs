// -----------------------------------------------------------------------------------------------
//  JsonFile.cs by Marcus Medina, Copyright (C) 2021, http://MarcusMedina.Pro.
//  Published under Apache License 2.0 (Apache-2.0)
//  https://www.tldrlegal.com/l/apache2
// -----------------------------------------------------------------------------------------------

namespace MarcusMedinaPro.JsonFileWrapper;

using System;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

/// <summary>
/// Defines the <see cref="JsonFile{T}" />.
/// </summary>
/// <typeparam name="T">Any object that can be instansiated.</typeparam>
public class JsonFile<T> where T : new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonFile{T}"/> class.
    /// </summary>
    /// <param name="filename">The filename<see cref="string"/>.</param>
    public JsonFile(string filename)
    {
        Filename = filename;
        Format = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
    }

    /// <summary>
    /// Gets or Sets the suffix (default is .json).
    /// </summary>
    public string Suffix { get; set; } = ".json";

    /// <summary>
    /// Gets or sets the Data object.
    /// </summary>
    public T? Data { get; set; } = default;

    /// <summary>
    /// Gets or sets the Filename, just the name, without suffix. <see cref="Suffix"/> will be added automatically.
    /// </summary>
    public string Filename { get; set; } = "";

    /// <summary>
    /// Gets or Sets the serialisation format settings.
    /// </summary>
    public JsonSerializerSettings Format { get; set; }

    /// <summary>
    /// Loads the file.
    /// </summary>
    public T? Load()
    {
        var data = string.Empty;
        if (File.Exists(Filename + Suffix))
        {
            try
            {
                data = File.ReadAllText(Filename + Suffix);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error reading file:");
                Debug.WriteLine(ex.Message);
                data = "{}";
            }
        }

        try
        {
            Data = JsonConvert.DeserializeObject<T>(data);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error deserializing file:");
            Debug.WriteLine(ex.Message);
        }

        return Data;
    }

    /// <summary>
    /// Saves the file.
    /// </summary>
    public void Save()
    {
        var json="";
        try
        {
            json = JsonConvert.SerializeObject(Data, null, Format);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error serializing data:");
            Debug.WriteLine(ex.Message);
        }

        try
        {
            File.WriteAllText(Filename + Suffix, json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error saving data:");
            Debug.WriteLine(ex.Message);
        }
    }
}