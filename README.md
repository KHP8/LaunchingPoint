# Wizard Defense Service

## The Game
Wizard Defense Service is a third-person shooter roguelike where the player must clear rooms filled with enemies to advance to the next room, ultimately ending in the boss room.


## Project Goals
We all entered this project knowing nothing about Unity or C#. We wanted to develop some proficiency with using Unity and create a working program with our new knowledge. 
Our first steps were to become familiar with Unity lingo and capabilities, such as defining GameObjects and Colliders, understanding Scenes, creating an input system, and finally having a basic player object that could move.
We could then begin creating scripts for our abilities, creating 3D model sketches of rooms, adding a main menu and ability selection screen, and implementing a physics-based movement system.

Long term, should project development continue, we aim to add multiplayer (up to 4 players) to our game, more abilities and schools of magic, better 3D models, and a more refined gameplay loop. 


## Features
### Object Oriented Abilities
While we only created a select few abilities, we used an Object Oriented approach to help streamline creating other abilities, should the need arise. C# made this process very easy, allowing us to quickly categorize abilities.
Abilities make use of colliders for impact to determine if they should deal damage, pass through something, be destroyed, etc. 


### UI
Our user interface implements a combination of TextMeshPro and Unity's native UI tools.
The players prefferred settings and equipped abilities are stored in a global JSON dicitonary that updates the visuals.
The settings screen allows for players to edit different audio mixers for game and music volume, as well as change the resolution of the window.


### Physics-based Interactions
We implemented Unity's physics system in two places: player movement and knockback. Both make use of a rigidbody.
The player's movement is determined by forces and implements drag for both air and ground. Slopes were taken into account when developing the player's movement, helping the player to feel as if they slide down steep slopes. 
Some abilities apply knockback to enemies. The knockback affects the enemy as expected, a closer impact will increase the force. It also applies the force directionally from the source of impact, so the direction will vary. 


### Enemy AI
The enemies in our game used Unity's NavMesh for movement. When knockback is applied, the NavMesh would be disabled and a physics-based rigidbody would be enabled, until speed has decreased enough and the enemy is on the ground. 
The enemies make decisions based on line-of-sight to the player. Enemies stay at range from the player, seek them out, and engage in some slightly intelligent movement.
Enemy abilities follow a very similar structure to player abilities.
