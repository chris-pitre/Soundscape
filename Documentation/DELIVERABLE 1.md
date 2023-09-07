# Description of the Game
A player is working for a company as an undercover agent trying to crack down on a huge mafia boss.
The player will go though various different floors and levels, and progress through a story until you reach the CEO of the company and take them down.
You will find items that will help you on your journey, solve puzzles, and fight/ sneak throuhg various enemies.
# Mechanics 
Some of the main mechanics that will be implemented in the game will be a vision and sound system for both players and enemies.
The vision system will essentially be the main component that dictates stealth as a gameplay mechanic, with the sound system being supplementary to vision for stealth.
The vision system will work through having a cone of vision emitted by enemies which can detect the player inside its bounds, and when detected, it will activate the enemy's AI.
The sound system will also activate enemy AI, but will work through a radius based system where different actions emit a different radius of sound, which can trigger the enemy AI.
The player can stealthily move through levels through lethal or non-lethal means (not encouraged). 
The player will be able to avoid enemies, and they can also takedown enemies with weapons.
The main goal of the game is to progress though the levels obtaining key items to fight the boss at the top.
# AI Component
The main AI component will utilize a combination of A* pathfinding and a state machine to manage alert levels. 
Most of the AI will start off in a "Patrol" state where they follow a designated path. 
If the player is detected through either their cone of vision, or through sound, then the enemy will enter an "Alert" state and will use A* to chase the player.
If the player is able to escape their immediate line of sight, then the enemy will enter the "Search" state and will attempt to search for the player around their last known position.

