/*
 * File: IObserver.cs
 * Author: Michael Johannes
 * Desc: An observer interface that allows the model to access information provided by the view
 */

using CC_X.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Interface between model and view
public interface IObserver
{

    // Updates the current time elapsed in the level
    void UpdateCurrentTime();

    // Populate WorldCollection with level 1 world objects/coordinates according to difficutly
    void SetUpLevel1(Difficulty difficulty);

    // Populate WorldCollection with level 2 world objects/coordinates according to difficutly
    void SetUpLevel2(Difficulty difficulty);

    // Populate WorldCollection with level 3 world objects/coordinates according to difficutly
    void SetUpLevel3(Difficulty difficulty);

    // Populate WorldCollection according to level and difficulty.
    void SetUpLevel(Level level, Difficulty difficulty);

    // Empties the world
    void ResetLevel();
}
