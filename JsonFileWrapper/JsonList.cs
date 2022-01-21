// -----------------------------------------------------------------------------------------------
//  JsonFile.cs by Marcus Medina, Copyright (C) 2021, http://MarcusMedina.Pro.
//  Published under Apache License 2.0 (Apache-2.0)
//  https://www.tldrlegal.com/l/apache2
// -----------------------------------------------------------------------------------------------

namespace MarcusMedinaPro.JsonFileWrapper;

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

/// <summary>
/// Defines the <see cref="JsonList{T}" />.
/// </summary>
/// <typeparam name="T">Any object that can be instansiated will be added to a list.</typeparam>
public class JsonList<T> : ICloneable, IEnumerable, IEnumerable<T>, IDisposable where T : new()
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JsonFile{T}"/> class.
    /// </summary>
    /// <param name="filename">The filename<see cref="string"/>.</param>
    public JsonList(string filename)
    {
        Filename = filename;
        Format = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
        _ = Load();
    }

    /// <summary>
    /// Gets the current object in the list.
    /// </summary>
    /// <value>
    /// The current.
    /// </value>
    public T? Current => TheList[ CurrentPosition ];

    /// <summary>
    /// Gets or sets the current position.
    /// </summary>
    /// <value>
    /// The current position.
    /// </value>
    public int CurrentPosition { get; set; } = 0;

    /// <summary>
    /// Gets or sets the Filename, just the name, without suffix. <see cref="Suffix"/> will be added automatically.
    /// </summary>
    public string Filename { get; } = "";

    /// <summary>
    /// Gets the first item in the list.
    /// </summary>
    /// <value>
    /// The first item.
    /// </value>
    public T? First => TheList[ 0 ];

    /// <summary>
    /// Gets or Sets the serialisation format settings.
    /// </summary>
    public JsonSerializerSettings? Format { get; set; }

    /// <summary>
    /// Gets the last item in the list.
    /// </summary>
    /// <value>
    /// The last item.
    /// </value>
    public T? Last => TheList[ ^1 ];

    /// <summary>
    /// Gets the length of the list.
    /// </summary>
    /// <value>
    /// The length of the list.
    /// </value>
    public int Length => TheList.Count;

    /// <summary>
    /// Gets the next item in the list.
    /// </summary>
    /// <value>
    /// The next item.
    /// </value>
    public T? Next => CurrentPosition < TheList.Count ? TheList[ ++CurrentPosition ] : default;

    /// <summary>
    /// Gets the previous item in the list.
    /// </summary>
    /// <value>
    /// The previous item in the list.
    /// </value>
    public T? Previous => CurrentPosition > 0 ? TheList[ --CurrentPosition ] : default;

    /// <summary>
    /// Gets or Sets the suffix (default is .json).
    /// </summary>
    public string Suffix { get; set; } = "json";

    /// <summary>
    /// Gets or sets the list.
    /// </summary>
    public List<T> TheList { get; set; } = new();

    /// <summary>
    /// Gets or sets the <see cref="T"/> at the specified index.
    /// </summary>
    /// <value>
    /// The <see cref="T"/>.
    /// </value>
    /// <param name="index">The index.</param>
    /// <returns>A <see cref="T"/></returns>
    public T this[ int index ]
    {
        get => TheList[ index ];
        set => Addnew(index, value);
    }

    /// <summary>
    /// Adds the specified value at the end of the list.
    /// </summary>
    /// <param name="value">The value.</param>
    public void Add(T value) => TheList.Add(value);

    /// <summary>
    /// Adds the specified values to the list (same as AddRange).
    /// </summary>
    /// <param name="value">The values.</param>
    public void Add(T[] values) => TheList.AddRange(values);

    /// <summary>
    /// Adds the specified values to the list.
    /// </summary>
    /// <param name="values">The values.</param>
    public void AddRange(T[] values) => TheList.AddRange(values);

    /// <summary>
    /// Clears the list.
    /// </summary>
    public void Clear() => TheList.Clear();

    /// <summary>
    /// Deletes the specified value at the given index (Same as Remove at).
    /// </summary>
    /// <param name="index">The index.</param>
    public void Delete(int index) => RemoveAt(index);

    /// <summary>
    /// Deletes the specified value (Same as Remove).
    /// </summary>
    /// <param name="value">The value.</param>
    public void Delete(T value) => Remove(value);

    /// <summary>
    /// Deletes the specified match.
    /// </summary>
    /// <param name="match">The match.</param>
    public void Delete(Predicate<T> match) => TheList.RemoveAll(match);

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Save();
        TheList.Clear();

        //This will prevent derived types that introduce a finalizer from needing to re-implement 'IDisposable' to call it.
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Loops through the items in the list
    /// </summary>
    /// <param name="action">The action.</param>
    public void ForEach(Action<T> action)
    {
        foreach (var element in TheList)
            action(element);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// An enumerator that can be used to iterate through the collection.
    /// </returns>
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)TheList).GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator GetEnumerator() => TheList.GetEnumerator();

    /// <summary>
    /// Determines whether this list is empty.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
    /// </returns>
    public bool IsEmpty() => TheList.Count == 0;

    /// <summary>
    /// Loads the file.
    /// </summary>
    /// <returns>A list of <see cref="T"/></returns>
    public List<T>? Load()
    {
        var data = "[]";
        if (File.Exists($"{Filename}.{Suffix}"))
        {
            try
            {
                data = File.ReadAllText($"{Filename}.{Suffix}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error reading file:");
                Debug.WriteLine(ex.Message);
                data = "[]";
            }
        }

        try
        {
            TheList = JsonConvert.DeserializeObject<List<T>>(data) ?? new();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error deserializing file:");
            Debug.WriteLine(ex.Message);
            TheList = new List<T>();
        }

        return TheList;
    }

    /// <summary>
    /// Imports a Json file to the list and ads the values at the end.
    /// </summary>
    /// <returns>The imported list</returns>
    public List<T>? Import(string filenameWithSuffix)
    {
        var data = "[]";
        var tmpList = new List<T>();
        if (File.Exists($"{filenameWithSuffix}"))
        {
            try
            {
                data = File.ReadAllText($"{filenameWithSuffix}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error reading file:");
                Debug.WriteLine(ex.Message);
                data = "[]";
            }
        }

        try
        {
            tmpList = JsonConvert.DeserializeObject<List<T>>(data) ?? new();
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error deserializing file:");
            Debug.WriteLine(ex.Message);
            tmpList = new List<T>();
        }

        tmpList.AddRange(tmpList);

        return tmpList;
    }
    /// <summary>
    /// Removes the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    public void Remove(T value) => TheList.Remove(value);

    /// <summary>
    /// Removes the specified value at the given index.
    /// </summary>
    /// <param name="index">The index.</param>
    public void RemoveAt(int index) => TheList.RemoveAt(index);

    /// <summary>
    /// Saves the list.
    /// </summary>
    public void Save()
    {
        var json = "";
        if (TheList == null)
            TheList = new List<T>();
        try
        {
            json = JsonConvert.SerializeObject(TheList, null, Format);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error serializing data:");
            Debug.WriteLine(ex.Message);
        }

        if (File.Exists($"{Filename}.{Suffix}.bak"))
            File.Delete($"{Filename}.{Suffix}.bak");

        if (File.Exists($"{Filename}.{Suffix}"))
            File.Move($"{Filename}.{Suffix}", $"{Filename}.{Suffix}.bak");

        try
        {
            File.WriteAllText($"{Filename}.{Suffix}", json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error saving data:");
            Debug.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Sorts using the given lambda expression.
    /// </summary>
    /// <param name="p">The lambda expression</param>
    public void Sort(Func<T, object> p) => TheList = TheList.OrderBy(p).ToList();

    /// <summary>
    /// Sorts the list.
    /// </summary>
    public void Sort() => TheList.Sort();

    /// <summary>
    /// Sorts the specified comparer.
    /// </summary>
    /// <param name="comparer">The comparer.</param>
    public void Sort(IComparer<T>? comparer) => TheList.Sort(comparer);

    /// <summary>
    /// Sorts the specified comparison.
    /// </summary>
    /// <param name="comparison">The comparison.</param>
    public void Sort(Comparison<T> comparison) => TheList.Sort(comparison);

    /// <summary>
    /// Sorts at the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="count">The count.</param>
    /// <param name="comparer">The comparer.</param>
    public void Sort(int index, int count, IComparer<T>? comparer) => TheList.Sort(index, count, comparer);

    /// <summary>
    /// Addnews the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <param name="value">The value.</param>
    private void Addnew(int index, T value)
    {
        if (TheList is null)
            TheList = new List<T>();

        if (index < TheList.Count - 1)
        {
            TheList[ index ] = value;
        }
        else
        {
            while (index < TheList.Count - 1)
            {
                TheList.Add(new T());
            }
        }

        TheList.Add(value);
    }
    /// <summary>
    /// Clones the specified lambda function e.g Clone(c=>new Person{Name=c.Name});.
    /// </summary>
    /// <param name="p">The p.</param>
    /// <returns>A list of cloned objects</returns>
    public List<T> Clone(Func<T, T> p)
    {
        var tmpList = new List<T>();
        foreach (var item in TheList)
        {
            tmpList.Add(p.Invoke(item));
        }

        return tmpList;
    }
    /// <summary>
    /// Clones this instance and the contents, if the content is cloneable it will be cloned, otherwise only the reference is copied.
    /// </summary>
    /// <returns>A list of cloned objects</returns>
    public List<T> Clone()
    {
        var tmpList = new List<T>();
        foreach (var item in TheList)
        {
            if (item is ICloneable)
            {
                var clone = (ICloneable)item;
                tmpList.Add((T)clone.Clone());
            }
            else
            {
                tmpList.Add(item);
            }
        }

        return tmpList;
    }

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>
    /// A new object that is a copy of this instance.
    /// </returns>
    object ICloneable.Clone() => Clone();
}