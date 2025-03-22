Cooking System

This is a simple but expandable cooking system built for *Familiar Tale*. Players collect ingredients, mix them together, and cook meals that give different effects — like healing, buffs, or temporary stat boosts.

What It Does
- Lets players select ingredients from their inventory
- Combines ingredients to create a dish based on recipe logic or trial and error
- Applies effects like health regen, defense boosts, speed buffs, and more
- Supports known recipes as well as discovery-based cooking

System Parts
- `CookingPot.cs` – Handles ingredient checks and triggers the cooking process
- `CookingSlot.cs` – Detects ingredients placed in slots and manages the final output
- `BagInventory.cs` – Stores and provides ingredients to the cooking pot
- `DragDrop.cs` – Allows the player to drag UI ingredients onto cooking slots
- `RecipeBook.cs` – A compendium of known recipes the player can unlock through hidden rewards or experimentation

Features
- Uses string IDs for ingredients to determine valid combinations
- Integrates with the inventory and player status systems to apply effects

Built With
- Unity (2021+)
- C#

Used In
This system is live in *Familiar Tale* and helps reward exploration by encouraging players to gather rare ingredients and experiment with combinations.
