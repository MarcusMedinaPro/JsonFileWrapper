# JsonList.cs
Namespace: MarcusMedinaPro.JsonFileWrapper
Path : .

## Usage
Defines the JsonList{T}.
T : Any object that can be instansiated will be added to a list.

## Public methods
This are the methods that are available for the user of this class

### public JsonList(string filename)
Initializes a new instance of the JsonFile{T} class.
Parameter: filename : The filename string.

### public T? Current
Gets the current object in the list.
--> The current.

### public int CurrentPosition { get; set; } = 0;
Gets or sets the current position.
--> The current position.

### public string Filename { get; } = "";
Gets or sets the Filename, just the name, without suffix. Suffix will be added automatically.

### public T? First
Gets the first item in the list.
--> The first item.

### public JsonSerializerSettings? Format { get; set; }
Gets or Sets the serialisation format settings.

### public T? Last
Gets the last item in the list.
--> The last item.

### public int Length
Gets the length of the list.
--> The length of the list.

### public T? Next
Gets the next item in the list.
--> The next item.

### public T? Previous
Gets the previous item in the list.
--> The previous item in the list.

### public string Suffix { get; set; } = "json";
Gets or Sets the suffix (default is .json).

### public List<T> TheList { get; set; } = new();
Gets or sets the list.

### public T this[ int index ]
Gets or sets the T at the specified index.
--> The T.

Parameter: index : The index. 
Returns A T

### public void Add(T value)
Adds the specified value at the end of the list.
Parameter: value : The value.

### public void Add(T[] values)
Adds the specified values to the list (same as AddRange).
Parameter: value : The values.

### public void AddRange(T[] values)
Adds the specified values to the list.
Parameter: values : The values.

### public void Clear()
Clears the list.

### public void Delete(int index)
Deletes the specified value at the given index (Same as Remove at).
Parameter: index : The index.

### public void Delete(T value)
Deletes the specified value (Same as Remove).
Parameter: value : The value.

### public void Delete(Predicate<T> match)
Deletes the specified match.
Parameter: match : The match.

### public T? Find(Func<T, bool> match)
Find specified match (wrap of FirstOrDefault).
Parameter: match : The match.

### public void Dispose()
Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.

### public void ForEach(Action<T> action)
Loops through the items in the list
Parameter: action : The action.

### public IEnumerator GetEnumerator()
Returns an enumerator that iterates through the collection.
Returns
An enumerator that can be used to iterate through the collection.
 
Returns an enumerator that iterates through a collection.
Returns
An T:System.Collections.IEnumerator object that can be used to iterate through the collection.

### public bool IsEmpty()
Determines whether this list is empty.
Returns
true if this instance is empty; otherwise, false.

### public List<T>? Load()
Loads the file.
Returns A list of T

### public List<T>? Import(string filenameWithSuffix)
Imports a Json file to the list and ads the values at the end.
Returns The imported list

### public void Remove(T value)
Removes the specified value.
Parameter: value : The value.

### public void RemoveAt(int index)
Removes the specified value at the given index.
Parameter: index : The index.

### public void Save()
Saves the list.

### public void Sort(Func<T, object> p)
Sorts using the given lambda expression.
Parameter: p : The lambda expression

### public void Sort()
Sorts the list.

### public void Sort(IComparer<T>? comparer)
Sorts the specified comparer.
Parameter: comparer : The comparer.

### public void Sort(Comparison<T> comparison)
Sorts the specified comparison.
Parameter: comparison : The comparison.

### public void Sort(int index, int count, IComparer<T>? comparer)
Sorts at the specified index.
Parameter: index : The index. 
Parameter: count : The count. 
Parameter: comparer : The comparer.

### public List<T> Clone(Func<T, T> p)
Clones the specified lambda function e.g Clone(c=>new Person{Name=c.Name});.
Parameter: p : The p. 
Returns A list of cloned objects

### public List<T> Clone()
Clones this instance and the contents, if the content is cloneable it will be cloned, otherwise only the reference is copied.
Returns A list of cloned objects
