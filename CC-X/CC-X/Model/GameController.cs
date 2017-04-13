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
        public Dictionary<uint, GameObj> GameObjCollection { get; set; } // Contains Nature and Enemy objects      
        public MainCharacter MainChar = new MainCharacter();
        public Enemy foe = new Enemy();
        public Difficulty DifficutlySelected { get; set; }
        public Vector3 EndGameZone { get; set; }
        public bool GameOver { get; set; }
        public int CurrentTime { get; set; }

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
            if(Math.Abs(MainChar.Position.X - EndGameZone.X) <= 5 && Math.Abs(MainChar.Position.Z - EndGameZone.Z) <= 1)
            {
                GameOver = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Returns true if collided with other GameObj objects. If GameObj object is Enemy and MainChar.PositionSinceLastCollide != MainChar.Position, subtracts enemy damage from health
        public List<object> DetectCollision()
        {
            //if(MainChar != null)
            //{
            //    foreach (GameObj obj in GameObjCollection.Values)
            //    {
            //        if(obj is Enemy)
            //        {
            //            if (Math.Abs(MainChar.Position.X - obj.Position.X) <= 0.05f && Math.Abs(MainChar.Position.Z - obj.Position.Z) <= 0.05f)
            //            {
            //                MainChar.ReceiveDamage(((Enemy)obj).Strength);                            
            //                return new List<object>(){ true,obj.Position };
            //            }
            //        }   
            //        if(obj is Nature && ((Nature)(obj)).SelectedNatureType != Nature.NatureType.Plane)
            //        {
            //            return new List<object>() { true, obj.Position };
            //        }
            //    }
            //    return new List<object>() { false, new Vector3(-1000000, -1000000, -1000000) };
            //}
            //else
            //{
            //    return new List<object>() { false, new Vector3(-1000000, -1000000, -1000000) };
            //}
            if (MainChar != null)
            {
                UpdateCurrentTime();
                foreach (GameObj obj in GameObjCollection.Values)
                {
                    if (obj is Enemy)
                    {
                        if (MainChar.persnlBubble.IntersectsWith(((Enemy)(obj)).persnlBubble) && MainChar.TimeSinceLastCollide != CurrentTime)
                        {
                            MainChar.TimeSinceLastCollide = CurrentTime;
                            MainChar.ReceiveDamage(((Enemy)obj).Strength);
                            return new List<object>() { true, obj.Position };
                        }
                    }
                    //if (obj is Nature && ((Nature)(obj)).SelectedNatureType != Nature.NatureType.Plane)
                    //{
                    //    return new List<object>() { true, obj.Position };
                    //}
                }
                return new List<object>() { false, new Vector3(-1000000, -1000000, -1000000) };
            }
            else
            {
                return new List<object>() { false, new Vector3(-1000000, -1000000, -1000000) };
            }
        }
        public void UpdateCurrentTime()
        {
            gui.UpdateCurrentTime();
        }
        //Calculates player's experience
        public void CalcExperience(int points)
        {
            throw new NotImplementedException();
        }

        public void CalcPoints(Level level, Difficulty difficulty, int time)
        {
            throw new NotImplementedException();
        }


        //Populate WorldCollection with level 1 world objects/coordinates according to difficutly
        private void SetUpLevel1(Difficulty difficulty)
        {
            EndGameZone = new Vector3(75, 0, 124);
            MainChar.Position = new Vector3(75, -0.50523f, 1.62f);
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
        // Save() writes the game state (previously serialized into a string) to the provided file (CSV).
        // Load() uploads the file, parsing its contents into a string array for deserialization.

        public void Load(string filepath)
        {
            string[] temp = File.ReadAllLines(filepath);
            MainChar.DeSerialize(temp[0]);

            for (int i = 1; i < temp.Count(); ++i) // Aid found at: http://stackoverflow.com/questions/3878820/c-how-to-get-first-char-of-a-string
            {
                string TempString = temp[i];
                if (TempString[0] == '+')
                {
                    var tempEnemy = new Enemy();
                    tempEnemy.DeSerialize(TempString);
                }
                else if (TempString[0] == '-')
                {
                    Nature.NatureType tempType = new Nature.NatureType();
                    tempType = Nature.NatureType.Plane;
                    Vector3 num = new Vector3(0, 0, 0);
                    var tempNature = new Nature(tempType, num);
                    tempNature.DeSerialize(TempString);
                }

            }
        }

        public void Save(string filepath) // Aid found at: http://stackoverflow.com/questions/18757097/writing-data-into-csv-file
        {
            // Use a StringBuilder
            var csv = new StringBuilder();
            csv.AppendLine(MainChar.Serialize());
            foreach (KeyValuePair<uint, GameObj> entry in GameObjCollection) // Aid found at: http://stackoverflow.com/questions/141088/what-is-the-best-way-to-iterate-over-a-dictionary-in-c
            {
                if (entry.Value is Enemy)
                {
                    var tempEnemy = (Enemy)entry.Value;
                    csv.AppendLine('+' + tempEnemy.Serialize());
                }
                else if (entry.Value is Nature)
                {
                    var tempNature = (Nature)entry.Value;
                    csv.AppendLine('-' + tempNature.Serialize());
                }
                else
                    continue;
            }
            File.WriteAllText(filepath, csv.ToString());
        }
    }
}
