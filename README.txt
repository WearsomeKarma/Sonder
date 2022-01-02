

###                 #
#                   #
#                   #    
###   ###   ##    ###   ###   ###
  #   # #   # #   # #   #=#   #
  #   # #   # #   # #   #     #
###   ###   # #   ####  ###   #



Sonder is a 2.5D Isometric game with a 3D world space of the roguelike genre.
It's world is generated on negative curvature such that it is finite in size
but loops - like Earth in real life.

The story is also generated - an ambitious goal of the project among other things.


Features:
-- Randomly Generated Finite Looping World with Negative Curvature.
-- Randomly Generated History / Plotlines
-- Randomly Generated Magic system and Creation lore. (A world might not have magic.)
-- My Hypothetical AI


Gameplay Features:
-- Inventory
-- Party Management (Emergent through AI!)
-- Type -> Parse -> Suggest dialog.


Gamespace exists in two states - World Travel, and Site Travel.

World Travel:
	Here the spherical world is projected into a local 2.5D space. It uses the same
	overworld generation for Site Travel, except it unloads and loads world data as
	the player moves. So for World Travel, the player is effective stationary, and 
	the world moves.
	
Site Travel:
	Here the gamespace is fixed in size and loaded in once. More elaborate spaces
	are shown here. Common areas like civilizations/ruins/etc are done here.
