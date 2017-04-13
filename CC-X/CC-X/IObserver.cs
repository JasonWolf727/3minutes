using CC_X.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IObserver
{
    void UpdateCurrentTime();

    void SetUpLevel1(Difficulty difficulty);

    //Populate WorldCollection with level 2 world objects/coordinates according to difficutly
    void SetUpLevel2(Difficulty difficulty);

    //Populate WorldCollection with level 3 world objects/coordinates according to difficutly
    void SetUpLevel3(Difficulty difficulty);

    //Populate WorldCollection according to level and difficulty.
    void SetUpLevel(Level level, Difficulty difficulty);   
}
