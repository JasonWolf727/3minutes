using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_X.Model
{
    enum Difficulty { Easy, Medium, Hard }
    enum Level { One, Two, Three}
    class GameController
    {        
        public Dictionary<int, World> WorldCollection { get; set; } //Contains Setting and Enemy objects      
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
        // Writes the game state (previously serialized into a string) to the provided file (CSV).
        // Uploads the file, parsing its contents into a single string for deserialization.

        private void Load(string filepath)
        {
            //string[] temp = File.ReadAllLines(filepath);
            //var sett = new Setting(this.setting, Urho.Vector3);
            //sett.DeSerialize();
            //var chara = new MainCharacter();
            //chara.DeSerialize();
            //var foes = new Enemies();
            //foes.DeSerialize();
            throw new NotImplementedException();
        }

        private void Save(Setting sett, string filepath) // Aid found at: http://stackoverflow.com/questions/18757097/writing-data-into-csv-file
        {
            // Use a StringBuilder
            var csv = new StringBuilder();
            var setting = sett;
            csv.AppendLine(setting.Serialize());
            var chara = new MainCharacter();
            csv.AppendLine(chara.Serialize());
            var foes = new Enemies();
            csv.AppendLine(foes.Serialize());
            File.WriteAllText(filepath, csv.ToString());
            //throw new NotImplementedException();
        }
    }
}
