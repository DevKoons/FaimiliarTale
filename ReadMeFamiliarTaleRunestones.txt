Runestones System

The Runestones system was built to give players collectible items that can be slotted or used to trigger effects, buffs, or passive abilities. Think of them like magical power-ups, but modular and customizable.

 What It Does
- Lets players collect Runestones throughout the game
- Each Runestone holds a unique effect (stat buff, skill modifier, passive, etc.)
- Designed to be modular — new Runestones can be added without changing the core system

 System Breakdown
- `RunestoneSO.cs`: Holds data for each Runestone (name, icon, description, effect type, etc.)
- `RunestoneManager.cs`: Handles equipping, activating, and tracking Runestones
- `IEffect.cs`: Interface for custom effect logic (buffs, damage mods, etc.)
- `RunestoneSlot.cs`: Tracks what’s equipped and handles UI updates

 Features
- Passive and active effects supported
- Simple integration into existing inventory and UI
- Designed to be scalable
- Effects include stat boosts, cooldown reduction, visual effects, etc.

 Built With
- Unity (2021+)
- C#
- ScriptableObjects
- Custom event system for triggering effects

 Used In
This system is used in Familiar Tale as a way to reward exploration and give players build variety without deep menus or overcomplication.
