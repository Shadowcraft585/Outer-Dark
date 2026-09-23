# 🌌 Outer Dark  
# A turn-based strategy game inspired by Conquest of Elysium 5, set in space.  



# 📖 About
Outer Dark is a hobby project that takes the core ideas of Conquest of Elysium 5 and puts them into a sci-fi setting. You pick a faction, fight for control of a solar system, claim planets and asteroids, and build up armies to defeat your rivals.

It's being made in Unity as a 2D tile-based game.  

# ✨ Vision
The goal is to make a game that feels like CoE5 — fast, chaotic, with very different factions — but in space. Inspirations include:  

## 🚀 Sci-fi like StarCraft and Warhammer 40k  
## 👁️ Eldritch cosmic horror (huge boss monsters)  
## 🪐 A mix of futuristic and primitive weapons depending on the faction  

The game works on two scales:  

A solar system map where you move between planets  
Planet maps that work like CoE5 maps, where multiple factions can fight on the same world  

# 🎮 Core Ideas  

## 🪐 Two Map Scales  
You start stuck on your home planet for the first couple dozen turns  
Once you can travel through space, you explore the rest of the system  
Every planet is its own map; the solar system is the bigger picture  

## 🧬 Different Factions  
Several human factions with different styles  
Alien races that play very differently from each other and from humans  
Multiple factions can be on the same planet at once  

## ⚡ Resources  
Resources only come from planets and asteroids — empty space gives you nothing  
Each faction may care about different resources  

## 👁️ Eldritch Bosses  
Massive boss monsters that can threaten the whole system  
Late-game threats that everyone has to deal with  

## 🛸 Ships and Combat  
Different ships for different factions  
Mix of high-tech and low-tech weapons  
Battles are auto-resolved like in CoE5 — what you bring matters more than how you control it  

#🛠️ What Works Right Now  
The project is in very early development. So far:  

## ✅ Solar System Generation  
Seed-based random generator  
A sun in the middle  
Randomly placed planets and asteroids of different sizes  
Checks to make sure things don't overlap  
Round shapes built using distance from a center tile  

## ✅ Rendering  
Tiles are drawn as sprites on a 2D grid  
Different sprites for space, sun, planets, and asteroids  
Generation and rendering are separated in the code  

## ✅ Camera  
WASD to pan, mouse wheel to zoom  
Pan speed scales with zoom level  
Uses Unity's new Input System  

## ✅ Clicking on Tiles  
Left-click selects a tile and highlights it  
Right-click deselects  
A panel shows info about the selected tile (position, type, ID, size)  

## ✅ Commander spawning and movement
A test commander is spawned into the middle of the generated map.  
Left-click on the tile with the commander opens the info panel for the selected tile.  
The panel includes a picture of the commander with their corresponding stats and stamina.  
Clicking the commander entry in the sidebar selects the commander.  
Clicking another tile moves the commander toward that tile, spending movement points for each tile.  
An End Turn button restores commanders' movement for the next turn.  

# 🗺️ Roadmap  

## 🔜 Next Up  

 Better-looking planet generation (biomes, names)  
 Cleaner sprites for tiles  
 UI improvements  
 Turn system  
 Basic faction data  
## 🔭 Later  

 Planet-level maps (zoom into a planet for tactical combat)  
 Units you can move around  
 Resource production  
 First two playable factions  
 Auto-battle combat  
 Fog of war  
 
## 🌠 Long-Term  
 Special actions / abilities per faction (like CoE5 rituals)  
 Eldritch boss fights  
 AI opponents  
 Multiplayer (way down the line)  
 
# 🧱 Project Structure  
```text
Outer Dark/
├── Assets/
│   ├── Data/                         # Game data assets
│   ├── Material/                     # Materials for space, planets, asteroids, and the sun
│   ├── Prefabs/
│   │   ├── CommanderPrefab.prefab    # Commander world-unit prefab
│   │   ├── GameTile.png              # Tile asset
│   │   └── UI/
│   │       └── CommanderEntry.prefab # Commander sidebar entry prefab
│   ├── Scenes/
│   │   ├── StartScene.unity          # Start/menu scene
│   │   └── GameScene.unity           # Main gameplay scene
│   ├── Scripts/
│   │   ├── Map/
│   │   │   ├── CameraInputActions.*      # Camera input bindings
│   │   │   ├── CameraMovement.cs         # Camera pan and zoom
│   │   │   ├── GameManager.cs            # Main game bootstrap and turn handling
│   │   │   ├── MapInteract.cs             # Tile selection and movement input
│   │   │   ├── MovementCostCalculator.cs  # Terrain movement costs
│   │   │   └── TerrainType.cs              # Terrain definitions
│   │   ├── Menu/
│   │   │   └── StartManager.cs            # Start scene/menu logic
│   │   ├── Units/
│   │   │   ├── CommanderController.cs    # Commander selection and movement
│   │   │   ├── CommanderEntryUI.cs        # Commander sidebar entry UI
│   │   │   ├── CommanderSidebar.cs        # Displays commanders on a tile
│   │   │   ├── CommanderSpawner.cs        # Creates and tracks commanders
│   │   │   ├── CommanderView.cs           # Commander presentation and sync
│   │   │   ├── Gameplay.cs                # Units, factions, and gameplay data
│   │   │   ├── Spell.cs                   # Spell definitions
│   │   │   └── SpellProjectile.cs         # Spell projectile behaviour
│   │   └── WorldGen/
│   │       ├── SolarSystemGenerator.cs   # Random solar-system generation
│   │       ├── SolarSystemRenderer.cs     # Draws generated tiles
│   │       └── TileType.cs                # Solar-system tile types
│   ├── Settings/                     # URP and scene template settings
│   ├── Sprites/                      # Space, planet, asteroid, sun, and unit sprites
│   └── TextMesh Pro/                 # TextMesh Pro resources
├── Packages/                         # Unity package manifest and lock file
└── ProjectSettings/                  # Unity project and build settings
```

## Code Notes
- World generation, rendering, interaction, units, and menu logic are organized into separate script folders.
- `GameManager` coordinates the main gameplay setup and turn handling.
- Commander data and presentation are separated between `Gameplay.cs`, `CommanderController.cs`, and `CommanderView.cs`.
- Commander selection is displayed through `CommanderSidebar` and `CommanderEntryUI` using the `CommanderEntry.prefab` UI template.
- Faction and unit data can be moved to ScriptableObjects as the project grows.

# 🚀 Getting Started  
Requirements  
Unity 6 (or later) with 2D URP  
Unity Input System package  
Setup  
Clone the repo  
bash  
git clone https://github.com/yourname/outer-dark.git  
Open it in Unity Hub  
Open the main scene in Assets/Scenes/  
Press Play — a new solar system is generated each time  
Controls  
Action\tInput  
Move camera\tW A S D  
Zoom\tMouse scroll  
Select tile\tLeft click  
Deselect\tRight click  

# 🙏 Inspiration  
Heavily inspired by Conquest of Elysium 5 by Illwinter Game Design. All mechanics are rebuilt from scratch — no assets or code from CoE5 are used. Outer Dark is an independent fan project and is not affiliated with Illwinter Game Design.
