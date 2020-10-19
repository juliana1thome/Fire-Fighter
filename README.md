# FireFighter

# Story 1
Description: As a player, I want a game manager to handle fire, so we can win the game and move to the next level.

• If the number of fires is equal to 0, we need to trigger a victory end screen to display results. If we hit any key, we load the next level (or the same level again if we only have one) 
- [X] 

• We define how many fires are lit per level (1 to 6) 
- [X] 

# Story 2
Description: As a player I want fire to start randomly after a while, so I will try to finish the level quickly. (Require story 1)

• The game starts with a specific number of fire (1 to 6), after a delay (20 seconds or less) a new fire will start at a random window (that did not have a fire before)
- [X] 

• After we end the level, we give points (between 50 to 150 pts) based on the windows that did not caught on fire. ((14 – totalFires) * preventionScore)
- [X] 

• We need to display the number of active fires in the UI
- [X] 

# Story 3
Description: As a player I want a timer to count how much time pass before we finish the level, so we can create a score. (Require story 1)

• We need to measure the time from the beginning of the level and stop the clock when we finish
- [X] 

• We need to define an average time for each level (manually set, based on previous attempt)
- [] 

• We need to define a base score to grade the player (between 150 to 500 pts)
- [] 

• When we finish a level, we display a summary and a score based on how long it took to
- [] 

complete the level. ((avgTime / timeElapsed) * timeScore)
- [] 

• We need to display the results with details in the victory end screen
- [] 

• We need to display the elapse time in the UI
- [] 

# Story 4
Description: As a player, I want structure damage, so we can lose. (Require story 1) & (Require story 2 and/or 3)

• We need to measure the damage amount based on how many fires are lit, and for how long
(between 0.5 to 5% / second). damage += (damageSpeed * activeFire) * Time.deltaTime
- [] 

• When we complete a level, we need to give a score penalty (-10 pts/ 1%). Final score cannot be
less than 0! (damage * penaltyScore)
- [] 

• If we reach 100% damage, the game ends with a Game Over screen
- [] 

• When Game Over, we can restart the game from level 1 or exit game
- [] 

• We need to display the damage in the UI
- [] 

# Story 5
Description: As a player, I want water limit, so we can lose. (Require story 1 and 4)

• We need to specify an amount of water consumed per level (between 0.05 and 1%/second active). The level only goes down when we use it
- [] 

• If we ran out of water, the game ends with Game Over
- [] 

• We need to specify different amount per level, to make the game more intense
- [] 

• Every level must be beatable! – Test your late levels to make sure it is playable
- [] 

• When we complete a level, we need to display a score based on the amount of water remaining (1000 pts/1%). (waterLeft * scoreWater)
- [] 

• We need to display the amount of water remaining in the UI
- [] 

# Story 6
Description: As a master figure I want the fire to intensify back if I did not completely extinguish it, so the game will be more challenging.

• Slowly increase the intensity back when the value is below the original intensity (3) if we are not trying to extinguish it
- [] 

• Invoke the increase only after a delay (1 second) when we previously tried to extinguish the fire
- [] 

# Story 7
Description: As a master figure I want to see the highest score for each level, so I can train to beat the best score.

• Add a high score in PlayerPrefs for each level
- [] 

• Display the best score in the victory screen
- [] 

# Story 8
Description: As a game designer, I want a main menu and loading screen in between scenes, so the game look more professional.

• Add a main menu with 2 buttons: Start Game, Exit game
- [] 

• When we are Game Over, we need to replace the button Exit game by Go to Main menu
- [] 

• Add loading screen between scenes in the Build Settings
- [] 

• The UI must always look good on different resolutions screen and aspect ratio
- [] 

# Story 9
Description: As a player, I want multiples levels with different difficulty settings, so the game progress over time.

• Create between 5 to 10 levels with increasingly more difficulty in the game manager
- [] 

• Create an ending screen to say congratulations to winning player and then add a button to return to the main menu (or quit the game if you don’t have any)
- [] 

# Story 10
Description: As a game designer, I want my game to have an icon, the college’s logo during the splash images, so the game look professional.

• Add an icon for the build
- [] 

• Add the LaSalle logo in the splash image section
- [] 
