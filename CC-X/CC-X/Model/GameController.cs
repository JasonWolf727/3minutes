using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    public enum Difficulty { Easy, Medium, Hard }
    public enum Level { One, Two, Three}
    //Controls game play
    class GameController
    {
        public Dictionary<uint, GameObj> GameObjCollection { get; set; } //Contains Setting and Enemy objects      
        public MainCharacter MainChar = new MainCharacter();
        public Enemy foe = new Enemy();
        public Difficulty DifficutlySelected { get; set; }
        public Vector3 EndGameZone { get; set; }
        public bool GameOver { get; set; }

        public Level CurrentLevel = Level.One;
        public HighScore highscore = new HighScore();

        //View will set this to user input name
        public string MainCharName = "";

        //IObserver object
        public IObserver gui { get; set; }

        //Receives selected difficulty from view and generates level according to difficulty
        public GameController(Difficulty difficulty)
        {
            GameOver = false;
            GameObjCollection = new Dictionary<uint, GameObj>();
        }

        //Returns true when level is over
        public bool EndLevel()
        {
            if(Math.Abs(MainChar.Position.X - EndGameZone.X) <= 1.5f && Math.Abs(MainChar.Position.Z - EndGameZone.Z) <= 1)
            {
                GameOver = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Populate WorldCollection with level 1 world objects/coordinates according to difficutly
        private void SetUpLevel1(Difficulty difficulty)
        {
            EndGameZone = new Vector3(75, 0, 124);
            gui.SetUpLevel(Level.One, difficulty);
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
        public void SetUpLevel(Level level, Difficulty difficulty)
        {
            switch(level)
            {
                case Level.One:
                    {
                        SetUpLevel1(difficulty);
                        break;
                    }
                case Level.Two:
                    {
                        SetUpLevel2(difficulty);
                        break;
                    }
                case Level.Three:
                    {
                        SetUpLevel3(difficulty);
                        break;
                    }
            }
        }

        // Load/save mechanism
        // Writes the game state (previously serialized into a string) to the provided file (CSV).
        // Uploads the file, parsing its contents into a single string for deserialization.

        public void Load(string filepath)
        {
            string[] temp = File.ReadAllLines(filepath);
            //var sett = new Setting(this.setting, Urho.Vector3);
            //sett.DeSerialize(temp[0]);
            MainChar.DeSerialize(temp[1]);
            foe.DeSerialize(temp[2]);
            //throw new NotImplementedException();
        }

        public void Save(string filepath) // Aid found at: http://stackoverflow.com/questions/18757097/writing-data-into-csv-file
        {
            // Use a StringBuilder
            var csv = new StringBuilder();
            //var setting = sett;
            //csv.AppendLine(setting.Serialize());
            csv.AppendLine(MainChar.Serialize());
            csv.AppendLine(foe.Serialize());
            File.WriteAllText(filepath, csv.ToString());
        }
    }
}
