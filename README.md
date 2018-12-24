# Idle-Miner-Tycoon-Dev-Test
Added a second mine feature to a basic version of Idle Miner Tycoon.
Added save state for both mines.

*************Controls****************
Press b to buy a new mine.
Press n to switch between mines.
Press c to clear all mine save data (note will have to press c in both mines to clear the data)
Press s to write to save data.

********All requirements met**********
1. It’s possible to start a second mine
2. It’s possible to switch back and forth between both mines
3. When switching to a mine, it must be possible to continue from the last state
    a. The same amount of “money” is still available as before in the mine
    b. The amount of mine shafts unlocked is still the same as before
    c. The upgrade levels are still the same as before
   
~~~~~ Code design decisions ~~~~~
    I favored composition over inheritance and tried to make things as decoupled as I could,
    so that I would only have to change things in one file if I were to make change i.e.
    mainly in the GameSaveStateController. 
    Each method is simple and generally does one thing (what it says it does ahah).
    Considering the short time frame of project, I decided to use Unity player prefs with json serialization
    to save the player data but could later implement 
    encryption and/or xml serialization for safer data store.
    
~~~~~~~Did you use any software design pattern? If yes, which one and what are its pros and cons?~~~~~~~~~~
 Yes, I used the MVC (Model-View-Controller) Pattern to map/model the problem of saving the upgrade and capacity levels as
 player button click counts, and using the formulas of the upgrades to rebuild the mines and the shafts with these click counts.
 
~~~~~~~~What are you most proud of?~~~~~~~~~
Mapping the problem of rebuilding the mine from the saved data as player click counts rather than their actual values.
This way you can re-euse code that is already written (but slightly tweaked) to 
rebuild the mine to the state it was when the player last left off.

~~~~~~~What was most challenging in the task~~~~~~~~
Deciding how to save the player data.

~~~~~~~What would you change on the provided code to ensure that many developers can work on it?~~~~~~~~~~~~
I would strongly suggest the making of a 'n' mines feature (where n > 1 ), so that the player can manage n number of mines. That is, 
make the code more generic so that the player can have n mines, rather than hard code with intention of having 2 mines only.
Also use xml to save the data as player prefs is an unsafe way to store sensitive game data and players can thus cheat easilty.

~~~~~~~~~~Anything else? Feedback, comments?~~~~~~~~~~~~
Due to the fact that I am on holiday with family I did not spend much time as I would have liked on this, and thus the
design is not as generic as I would have liked and merely fulfills the requirements.
I got to learn some new things and I enjoyed it. 
I wish the task specs were more specific as I considered making a new mine in the same scene
but decided to use a separate scene instead in order to switch between the mines without having to worry about the camera.
But in the future it could be implemented like the shafts and the 'switch' between them could adjust the camera instead of switching scenes.
Although if many mines with many shafts each are in a single scene this could lag the scene potentially.

