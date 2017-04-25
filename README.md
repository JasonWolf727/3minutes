# 3minutes

## History
* March 14: Created README.MD document
* March 31: Design Phase completed
* April 5: Alpha Tag
* April 12: Beta Tag
* April 19: Release Candidate
* April 24: Final Release
* April 26: Presentation

## Instructions as of Alpha Tag:
When the menu appears:
* Click `New Game` to start a new game.
* Click `Exit` to close the window.
* Click `Help` to see instructions.
* Click `About` to see the credits.
* Click `High Score` to see the high score menu.
* Click `Continue Progress` to start off from a later level
* `Load Game` is inactive.

To set up your game:
* Type in a name.
* Select a character: Soldier, Ninja, or Mutant.
* Select a difficulty: Easy, Medium, Hard.

After you start a new game:
* Use the arrow keys to move.
* Up arrow and down arrow move your character forward and back respectively.
* Left arrow and right arrow move your character left and right respectively.
* Don't get hit by a car, or you will lose health.
* If you win, you may proceed to the next level or return to the menu.
* If you lose, you may repeat the level or return to the menu.
* Press `M` to enter Developer Mode.
* Press `C` to enter Cheat Mode.


## Work Completed:

### As of Alpha Tag (April 5):
For GitHub:
* Class Diagram and detailed class/method information has been updated according to notes provided in the Review period.
* Game Specifications have been updated with more current information on how the game will work.
* Journals are up to date. See `Expenses` below for more details.
* Road Map has been updated with a more accurate schedule.

In the code: 
* Serialization/DeSerialization logic has been updated to reflect the current conditions of game objects.
* The main program has been updated with capabilities to launch a basic game.
* The `Settings` class has been renamed `Nature`.
* Load/Save logic has been completed, though it may require some extra work for BETA phase. :-)
* High Score has been redesigned.
* Developer Mode has been created: this is essentially a debugging mode to test object placement before hardcoding it into the final game.

### As of Beta Tag (April 12):
For GitHub:
* Road Map has been updated with completed features. Incomplete features will be pushed back to Release Candidate
* Journals are up to date. See `Expenses` below for more details.
* Issues raised by Dr. Schaub have been addressed.

In the code:
* The main program has been refactored: code for creating the game has been rearranged into methods outside the Start() method.
* Each button now generates a separate screen; however, these screens are empty.
* Health and Time indicators have been added
* Character selection implemented

### As of Release Candidate (April 19):
For GitHub:
* Road Map has been updated with completed features. Incomplete features have been scrapped.
* Journals are up to date

In the code:
* Levels 2 and 3 are active.
* All separate difficulties have been added.
* Experience added.
* High score, about, and help pages added.

### As of Final Release (April 24)
For GitHub:
* Road Map is finalized
* Journals are complete
* All issues addressed/closed

In the code:
* Comments added
* Debugging complete

## Known Issues as of Final Tag:
* No collision with the environment available.

## Recording: 
[Link to Alpha Recording](https://youtu.be/JftPD0mm-N8)

[Link to Beta Recording](https://youtu.be/n0qHjYO-3Fk)

[Link to Release Candidate](https://youtu.be/C0ve-RSEgY0)

[Link to Final Recording](http://youtu.be/f1VEIjY_tsQ?hd=1)

## Final Expenses:
| Developer | Hours invested | Hours left | Link to Journal | 
| ---------- | --------- | ---------- | ------ |
| Case | 36.00 | 14.00 | [Case Journal](https://github.com/runnersQueue/3minutes/wiki/CaseJournal#case-journal) |
| Johannes | 49.6 | 0.4 | [Johannes Journal](https://github.com/runnersQueue/3minutes/wiki/Johannes-Journal) |
| Santana | 30.0 | 20.0 | [Santana Journal](https://github.com/runnersQueue/3minutes/wiki/Santana-Journal) |
