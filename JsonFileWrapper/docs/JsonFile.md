# Jsonfile

A generic class that works like a wrapper that loads and saves a given object.

## Usage
The whole idea is an object wrapper to handle saving and loading of an object, so the coder using the class don't need to think about that.

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
string playerName="LeetMan";
var PlayerState = new JsonFile<GameState>(playerName);
// PlayerState is loaded when the object is created, 
// if no file is found an empty object is created.

// Change data values
PlayerState.Data.XP+=10;

// When the gaming session is over
// Save the player state
PlayerState.Save();
```

## Disclaimer
This project is meant to be simple, it was written to explain to my students how to use generic classes.

##  [Back to readme](https://github.com/MarcusMedinaPro/JsonFileWrapper/ReadMe.md)    
