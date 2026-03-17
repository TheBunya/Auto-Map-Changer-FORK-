# Auto Map Changer
 Changes the map to default when not active

# Installation
1. Install [CounterStrike Sharp](https://github.com/roflmuffin/CounterStrikeSharp) and [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master)
2. Download [Auto-Map-Changer](https://github.com/TheBunya/Auto-Map-Changer-FORK-/releases/latest)
3. Unzip the archive and upload it to the game server

# Config
The config is created automatically in the same place where the dll is located
```
{
  "Delay": 180,
  "DefaultMap": "de_dust2",
  "ChangeMap": false // Change the map even if there are no players
  "Debug": false // Output of checks to the log file
}
```
> [!NOTE]
> For Workshop maps needs prefix `ws:`

# Commands
`css_acm_reload` - Reload config AutoChangeMap
