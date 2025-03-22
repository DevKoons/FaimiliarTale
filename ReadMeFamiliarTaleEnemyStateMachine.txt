Enemy State Machine Modular (ESMM)

This is a flexible enemy AI system I built for *Familiar Tale*. It’s based on a modular state machine setup using ScriptableObjects in Unity, so it’s easy to create and swap behaviors without rewriting code.

 What It Does
- Lets you build enemies out of modular states like Move, Attack, Patrol, etc.
- Uses ScriptableObjects to make the states reusable and easy to edit
- Makes it simple to create new enemy types or behaviors without touching the core logic

What's in Here
- `EnemyStateMachineModular.cs`: Runs the state machine and handles transitions
- `EnemyStateSO.cs`: Base class for states
- `AttackSO.cs`, `MoveSO.cs`, `PathSO.cs`: Example behaviors using the system


##  Used In
This system is used in *Familiar Tale* to handle AI for different enemy types. It made testing and tuning way easier since each state could be worked on independently. Instead of focusing on restructuring obsolete code.

##  Built With
- Unity (2021+)
- C#
- ScriptableObjects
