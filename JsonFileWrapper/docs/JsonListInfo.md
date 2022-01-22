# Jsonfile

A generic class that works like a wrapper that loads and saves a given object. The main difference with the JsonFile class is that this one acts like a list.

## Usage
The whole idea is an object wrapper to handle saving and loading of an object, so the coder using the class don't need to think about that.

The public methods of the (JsonList.cs.md)[../Docs/JsonList.cs.md] are described (here)[../Docs/JsonList.cs.md]

```cs
// Example object
class GameState
{
    public string Name { get; set; } = "Player 1";
    public int Level { get; set; } = 1;
    public int Money { get; set; } = 100;
    public int XP { get; set; } = 100;
    public int HP { get; set; } = 100;
    public int ATK { get; set; } = 100;
    public List<Weapons> Weapons { get; set; } = new();
    public List<Items> Inventory { get; set; } = new();
    public Map CurentMap { get; set; } = new MainMap();
}
```

```cs
// Add using to your code
using MarcusMedinaPro.JsonFileWrapper;

// preparing the object
var PlayerState = new JsonFile<GameState>("Gamers");
// PlayerState is loaded when the object is created, 
// if no file is found an empty object is created.

// Change data values
var gamer = PlayerState.Find(p => p.Name == "Mr Leet");
if (gamer==null) 
{
    gamer= new GameState(){Name = "Mr Leet"};
    PlayerState.Add(gamer);
}
gamer.Xp+=100;

// Works with indexing too
PlayerState[3].ATK=10;

// When the gaming session is over
// Save the player state
PlayerState.Save();
```

## Disclaimer
This project is meant to be simple, it was written to explain to my students how to use generic classes.

##  [Back to readme](https://github.com/MarcusMedinaPro/JsonFileWrapper/blob/main/ReadMe.md)    
