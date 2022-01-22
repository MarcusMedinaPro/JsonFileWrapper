# JsonFile.cs
Namespace: MarcusMedinaPro.JsonFileWrapper
Path : .

## Usage
Defines the JsonFile{T}.
T : Any object that can be instansiated.

## Public methods
This are the methods that are available for the user of this class

### public JsonFile(string filename)
Initializes a new instance of the JsonFile{T} class.
Parameter: filename : The filename string.

### public T? Data { get; set; }
Gets or sets the Data object.

### public string Filename { get; } = "";
Gets or sets the Filename, just the name, without suffix. Suffix will be added automatically.

### public JsonSerializerSettings? Format { get; set; }
Gets or Sets the serialisation format settings.

### public string Suffix { get; set; } = "json";
Gets or Sets the suffix (default is .json).

### public static implicit operator T(JsonFile<T> file)
There are no comments on this  method


### public T? Load()
Loads the file.

### public void Save()
Saves the file.

### public void Dispose()
Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
