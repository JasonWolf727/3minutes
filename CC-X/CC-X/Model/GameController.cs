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
        public Dictionary<int, World> WorldCollection { get; set; }

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
