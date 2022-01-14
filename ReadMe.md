# JsonFileWrapper

This is another file I created to explain generic classes for my students. So I created this generic class that loads and saves json objects. All you need to do is to give it the object to use and the filename. Then you can load and save the object while you work on it.

This project is losely based on the Bridge design pattern.

## Installation

Use the package manager in Visual Studio to install JsonFileWrapper or use the package-manager window.
```
install-package JsonFileWrapper
```

## Usage

```cs
// Example object
class Person
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
}
```

```cs
using MarcusMedinaPro.JsonFileWrapper;
// preparing the object
var file = new JsonFile<List<Person>>("Friends")
{
    Data = new List<Person>(),
};
file.Load();
var People = file.Data;

// Now you can use your list as you want
People.Add(new Person { Name = "Adam", LastName = "West" });
People.Add(new Person { Name = "Bob", LastName = "Hoskins" });
People.Add(new Person { Name = "James", LastName = "Woods" });

// Save the list
file.Save();
```
## Source code
You can find the code on my [Github](https://github.com/MarcusMedinaPro/JsonFileWrapper) account.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[Apache 2](https://tldrlegal.com/license/apache-license-2.0-%28apache-2.0%29)

## Thanks list
[Json file Icon](https://iconscout.com/icons/json-file) by [First Styles](https://iconscout.com/contributors/first-styles) on [Iconscout](https://iconscout.com)
