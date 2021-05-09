# Centipede
Centipede game made in C# with unity.
# Overview
Welcome to Centipede2d! This is a game I created in unity based on the old arcade shooter.
This project was meant to be a challenge as modeling a Centipede through code requires 
proper application of various data structures.
### Gameplay
This game employs many of the same rules as the original Centipede:
1. Mushrooms have 3 stages, and shrink each time they make contact with an enemy that can deal
damage to them. (Laser and Spiders)
2. Spiders may deal damage to mushrooms and the player ship, but not to the centipede.
They will track the player if they player moves within a certain distance.
3. The centipede will being at the back of the map and advance towards the player, mushrooms
and spiders are free to spawn at any location on the map.
4. Only one laser (fired from the ship) may be on screen at a time. This is a mechanic from the
original game designed to punish missed shots.
5. The player has one life, losing it results in a loss. Killing the centipede results in a win.
### Controls
1. Arrow keys move the player.
2. Space bar to fire laser.

### Gameplay Video
[Software Demo Video](https://youtu.be/vn1miejtqYM)

# Development Environment
Developed in Unity 2021.1.5f1 using C#. If you would like to create a copy of this for your own usage,
it is recommended to use the exact version of unity mentioned here. Unity is not backwards compatible,
and some forwards compatibility can lose some associations existing between the editor and the inspector view.

The IntelliJ product Rider Studio was used for this project due to its excellent integration
with Unity.
# Useful Websites

{Make a list of websites that you found helpful in this project}
* [Unity Documentation](https://docs.unity3d.com/Manual/index.html)
* [Using Rider Studio with Unity](https://blog.jetbrains.com/dotnet/2017/08/30/getting-started-rider-unity/)

# Future Work
* Add a flea enemy to the game.
* Add levels with increasing Centipede difficulty.
* Add animations to all enemies.
