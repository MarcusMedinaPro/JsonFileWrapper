# JsonFileWrapper

This is another file I created to explain generic classes for my students. So I created this generic class that loads and saves json objects. All you need to do is to give it the object to use and the filename. Then you can load and save the object while you work on it.

This project is losely based on the Bridge design pattern.

## Installation

Use the package manager in Visual Studio to install JsonFileWrapper or use the [package-manager](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio) window.
```
install-package JsonFileWrapper
```

## Usage
Nothing fancy but it saves you the time of writing the serialization and deserialization code.
Check the document for each file in the [docs](https://github.com/MarcusMedinaPro/JsonFileWrapper/docs/) folder.

### JsonFile
For information about [JsonFile](https://github.com/MarcusMedinaPro/JsonFileWrapper/docs/JsonFile.md)  check the readme file in the [docs](https://github.com/MarcusMedinaPro/JsonFileWrapper/docs/) folder.
```cs
// Add using to your code
using MarcusMedinaPro.JsonFileWrapper;

// Initiate the object
var model = new JsonFile<MyModel>("Model");

// The object is initiated and if the file "model" exists, 
// it will be loaded to the object.
model.Name="Mr Magoo";

model.Save();
```

### JsonList
For information about [JsonList](https://github.com/MarcusMedinaPro/JsonFileWrapper/docs/JsonList.md)     check the readme file in the [docs](https://github.com/MarcusMedinaPro/JsonFileWrapper/docs/) folder.
```cs
// Add using to your code
using MarcusMedinaPro.JsonFileWrapper;

// Initiate the object
var model = new JsonFile<MyModel>("Model");

// The object is initiated and if the file "model" exists, 
// it will be loaded to the object.
model.Add(new MyModel{Name="Mr Magoo"});

model.Save();
```


## Source code
You can find the code on my [Github](https://github.com/MarcusMedinaPro/JsonFileWrapper) account.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## Disclaimer
This project is meant to be simple, it was written to explain to my students how to use generic classes.

## Thanks list
[Json file Icon](https://iconscout.com/icons/json-file) by [First Styles](https://iconscout.com/contributors/first-styles) on [Iconscout](https://iconscout.com)

## License 

Copyright 2012 Marcus Medina

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
