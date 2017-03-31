using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_X.Model
{
    enum Difficulty { Easy, Medium, Hard }
    enum Level { One, Two, Three}
    class GameController
    {        
        public Dictionary<int, World> SettingCollection { get; set; }
        public Dictionary<int, World> EnemiesCollection { get; set; }
        public World MainChar = new MainCharacter();
        public Difficulty DifficutlySelected { get; set; }
       
        public Level HighestLevelReached = Level.One;
        public HighScore highscore = new HighScore();

        //View will set this to user input name
        public string MainCharName = "";

        //Receives selected difficulty from view and generates level according to difficulty
        public GameController(Difficulty difficulty)
        {
            
        }        

        //Populate WorldCollection with level 1 world objects/coordinates according to difficutly
        private void SetUpLevel1(Difficulty difficulty)
        {

        }

        //Populate WorldCollection with level 2 world objects/coordinates according to difficutly
        private void SetUpLevel2(Difficulty difficulty)
        {

        }

        //Populate WorldCollection with level 3 world objects/coordinates according to difficutly
        private void SetUpLevel3(Difficulty difficulty)
        {

        }

        //Populate WorldCollection according to level and difficulty.
        private void SetUpLevel(Level level, Difficulty difficulty)
        {
            switch(level)
            {
                case Level.One:
                    {
                        break;
                    }
                case Level.Two:
                    {
                        break;
                    }
                case Level.Three:
                    {
                        break;
                    }
            }
        }

        // Load/save mechanism
        private void Load(string filename)
        {
            throw new NotImplementedException();
        }

        private void Save(string info)
        {
            throw new NotImplementedException();
        }
    }
}
