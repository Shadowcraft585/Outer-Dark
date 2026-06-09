# 🌌 Outer Dark  
# A turn-based strategy game inspired by Conquest of Elysium 5, set in space.  



# 📖 About
Outer Dark is a hobby project that takes the core ideas of Conquest of Elysium 5 and puts them into a sci-fi setting. You pick a faction, fight for control of a solar system, claim planets and asteroids for their resources, and build up your forces to take on your enemies.  

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
text  
Assets/  
├── Scripts/  
│   ├── Map/  
│   │   ├── SolarSystemGenerator.cs   # Random generation  
│   │   ├── SolarSystemRenderer.cs    # Drawing tiles  
│   │   ├── SolarSystem.cs            # Data classes  
│   │   ├── MapInteract.cs            # Click handling  
│   │   ├── CameraMovement.cs         # Camera pan/zoom  
│   │   └── CameraInputActions.*      # Input bindings  
│   └── GameManager.cs                # Main bootstrap  
├── Prefabs/  
└── Sprites/  
Code Notes  
Data and rendering are kept separate — the generator only makes data, the renderer just draws it  
GameManager is a singleton that holds the game state  
Plan is to move faction/unit data to ScriptableObjects later  

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
Action	Input  
Move camera	W A S D  
Zoom	Mouse scroll  
Select tile	Left click  
Deselect	Right click  

# 🙏 Inspiration  
Heavily inspired by Conquest of Elysium 5 by Illwinter Game Design. All mechanics are rebuilt from scratch — no assets or code from CoE5 are used. Outer Dark is an independent fan project and is not affiliated with Illwinter.  
