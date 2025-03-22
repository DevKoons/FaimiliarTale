## Enemy State Machine Modular (ESMM)

This is a flexible enemy AI system I built for *Familiar Tale*. It’s based on a modular state machine setup using ScriptableObjects in Unity, so it’s easy to create and swap behaviors without rewriting code.

## What It Does
- Lets you build enemies out of modular states like Move, Attack, Patrol, etc.
- Uses ScriptableObjects to make the states reusable and easy to edit
- Makes it simple to create new enemy types or behaviors without touching the core logic

## What's in Here
- `EnemyStateMachineModular.cs`: Runs the state machine and handles transitions
- `EnemyStateSO.cs`: Base class for states
- `AttackSO.cs`, `MoveSO.cs`, `PathSO.cs`: Example behaviors using the system


##  Used In
This system is used in *Familiar Tale* to handle AI for different enemy types. It made testing and tuning way easier since each state could be worked on independently. Instead of focusing on restructuring obsolete code.

##  Built With
- Unity (2021+)
- C#
- ScriptableObjects

-------------

## Runestones System

The Runestones system was built to give players collectible items that can be slotted or used to trigger effects, buffs, or passive abilities. Think of them like magical power-ups, but modular and customizable.

 ## What It Does
- Lets players collect Runestones throughout the game
- Each Runestone holds a unique effect (stat buff, skill modifier, passive, etc.)
- Designed to be modular — new Runestones can be added without changing the core system

 ## System Breakdown
- `RunestoneSO.cs`: Holds data for each Runestone (name, icon, description, effect type, etc.)
- `RunestoneManager.cs`: Handles equipping, activating, and tracking Runestones
- `IEffect.cs`: Interface for custom effect logic (buffs, damage mods, etc.)
- `RunestoneSlot.cs`: Tracks what’s equipped and handles UI updates

## Features
- Passive and active effects supported
- Simple integration into existing inventory and UI
- Designed to be scalable
- Effects include stat boosts, cooldown reduction, visual effects, etc.

## Built With
- Unity (2021+)
- C#
- ScriptableObjects
- Custom event system for triggering effects

## Used In
This system is used in Familiar Tale as a way to reward exploration and give players build variety without deep menus or overcomplication.

-------------

## Cooking System

This is a simple but expandable cooking system built for *Familiar Tale*. Players collect ingredients, mix them together, and cook meals that give different effects — like healing, buffs, or temporary stat boosts.

## What It Does
- Lets players select ingredients from their inventory
- Combines ingredients to create a dish based on recipe logic or trial and error
- Applies effects like health regen, defense boosts, speed buffs, and more
- Supports known recipes as well as discovery-based cooking

## System Parts
- `CookingPot.cs` – Handles ingredient checks and triggers the cooking process
- `CookingSlot.cs` – Detects ingredients placed in slots and manages the final output
- `BagInventory.cs` – Stores and provides ingredients to the cooking pot
- `DragDrop.cs` – Allows the player to drag UI ingredients onto cooking slots
- `RecipeBook.cs` – A compendium of known recipes the player can unlock through hidden rewards or experimentation

## Features
- Uses string IDs for ingredients to determine valid combinations
- Integrates with the inventory and player status systems to apply effects

## Built With
- Unity (2021+)
- C#

## Used In
This system is live in Familiar Tale and helps reward exploration by encouraging players to gather rare ingredients and experiment with combinations.

-------------

## Ritual System

The Ritual System is a multi-step interactive mechanic used in Familiar Tale for unlocking rewards, triggering events, or progressing through key parts of the world. It’s designed to feel mysterious, rewarding players for observation, experimentation, or item collection.

## What It Does
- Handles ritual sequences involving multiple interactable pieces
- Players must place items, activate pillars, or complete puzzles in a specific order
- Successfully completing a ritual can unlock rewards, progress storylines, or alter the game world

## System Parts
- `RewardNode.cs` – Grants a reward (item, ability, event) once the ritual is completed
- `RitualBook.cs` – Stores unlocked rituals and keeps track of player discoveries
- `RitualBookUI.cs` – Handles the interface for showing ritual progress, clues, or visual feedback
- `RitualLockManager.cs` – Controls whether the ritual is accessible, locked, or requires specific items
- `RitualPillar.cs` – Interactable object used in the ritual; can be activated or manipulated
- `RitualPiece.cs` – Items or objects players place as part of the ritual sequence

## Features
- Rituals are step-based and can include conditions, item use, or environment interaction
- Tracks progress through the `RitualBook` so players can revisit and complete later
- UI feedback through the Ritual Book and Pillars enhances player immersion
- Designed to be flexible — supports linear and branching rituals

## Built With
- Unity (2021+)
- C#

## Used In
This system is live in Familiar Tale and used in key puzzle areas and narrative progression. It helps deepen worldbuilding and gives players moments of mystery, experimentation, and payoff.

