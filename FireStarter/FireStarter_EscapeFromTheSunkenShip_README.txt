No special installation requirements necessary. All necessary components are included.

Instructions:
 - When On land: 
	- WASD to move the character
	- SPACE to Jump
	- C to crouch
 - When underwater: 
	- WASD to swim
	- C to sink
	- SPACE to rise
 - Left mouse button to pick up objects
 - Right mouse button to use the object
 - E to drop the objects that is currently being carried.

The goal is to find all the keys needed to open the door at the end of each level before the room floods with water. 
The player will need to move different objects, break glass cases or glass walls, 
and sometimes kill fish and avoid sharks to achieve this goal.

Rubric requirements: 
3D Game Feel game:
 - Environment and all interactions are in 3D space. 
 - Defined objective that player can achieve in each level (unlocking the final door of each room).
 - Game over and death jingle for communicating failure.
 - Victory screen and jingle for communicating success.
 - Title menu supports starting action.
 - Game over screen included retry button. Upon success, player can move back to level select and re-select level.
 - Game is in third person.

Precursors to fun gameplay:
 - Goals and sub-goals communicated through tutorial pop-ups in initial levels.
 - Open environments allow player to explore options to figure out how to tackle puzzles. 
 - Levels are designed such that taking certain options will result in easier progression through the level than other options, largely imposed by the time constraint induced by the rising water.
 - Player choices involve picking up objects, breaking containers, killing fish, and opening doors.
 - Lack of micromanagement, stagnation, impossible obstacles, and arbitrary events.
 - Players can move on to next level with success, and player has to retry entire level if they fail.
 - Tutorial level with relaxed constraints allows players to become familiar with mechanics before trying other levels.
 - Difficulty rises as levels progress.
 - Levels are void of cheesy strategies.

3D Character with real-time control:
 - Character designed to move with root motion.
 - Character movement and interaction is a focal part of the game's design.
 - Character used isn't a tutorial character or one used in the Milestones.
 - Character animations and models have been tweaked to suit the game's purpose.
 - Smooth transition between walking and swimming animations and from either state to taking a turn.
 - Player has continuous, dynamic, analog control of character movement.
 - Controls mapped to small, single-handed area of keyboard and mouse buttons.
 - Animation is fluid and polished.
 - Root motion implemented.
 - Low latency in control inputs.
 - Camera is fairly smooth. 
 - Sound effects result in auditory feedback on movement and environmental interactions.

Physics and Spatial Simulation in 3D World:
 - All environments newly synthesized, such as walls, water, and all objects.
 - All interactions are visually supported and have audio sound effects tied to them.
 - Most visuals aligned with physical implementation.
 - All boundaries confine player to playable space.
 - Physical environmental interactions include using tools to break glass and using spears to kill fish.
   Additionally, lifting heavy boxes and uncovering keys that subsequently float in water.
 - 3D Simulation in X, Y, and Z axes with movement in all directions.
 - Environment is interactive. 
 - Consistent spatial simulation throughout.

AI:
 - Uses skills learned from AI Milestone, including navmeshes and waypoints.
 - Assets are configured specifically for game.
 - AI has three states of behavior, including idle, fish-pursuit, and player-pursuit states. 
 - AI locomotion is relatively smooth.
 - AI is believable and reasonably effective.
 - Animation is fairly fluid, including when changing states. 
 - Music changes to indicate change of states.
 - Difficulty of player engaging enemy is balanced per level.
 - AI is not too hard, but also not too easy.
 - AI interacts with dead fish. 

Polish: 
 - Start menu GUI
 - In game pause menu, which includes a quit button.
 - Game feels like a game throughout each scene.
 - No debug output. 
 - Can quit from game on title screen, which is easily accessible from every other screen.
 - No test mode or Konami-code-esque dev mode settings.
 - GUI themed after setting of game.
 - Rippling water, waterfalls, and swimming fish are in environment. Lightweight buoyant objects get pushed away by player. 
 - Water shimmers.
 - Glass spawns shards when breaking.
 - Every observable game event has a sound tied to it.
 - All textures and UI themed after scenario consistently.
 - Shading and lighting appropriate for setting.
 - Color palette is consistent throughout game.
 - Sound design is consistent and tailored to game's setting of water and puzzle-solving. 
 - Few glitches, no ways to escape levels. 
 - All seams hidden to the player's eye. 
 
Development Team and Roles:
 - Jeevitesh Juneja: Shark and Fish AI, level design, puzzle design
 - Jiaxi Liu: Inventory system, weapon using logic and animation, UI for tutorial system and interaction, UI and scene polishing
 - Junyu Hu: Character control script and animation, Swimming logic, level design
 - Jianan Le: Water effect, animations of character, fish and sharks, playtesting and fine tuning the game, building scene layout
 - Nishit Undale: Sound design, sound effects, UI implementation, scene transition scripting.



Start Scene: Title Screen
Other scenes used in final product: Level Select Screen, level 0, Level 1, Level 2, Level_3