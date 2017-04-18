using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_X.Model
{
    //Only the top 10 highscores will be saved.
    class HighScore
    {
        const string highScoreFile = "highScore.txt";
        public List<Score> collectionScoreObj = new List<Score>();

        //This method will write all of the high scores to the "highScore.txt" file. 
        //It will take in a list of all the data points and overwrite the contents of the file
        //the data will be stored on separate lines separated by a space
        //player_name " " score
        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(highScoreFile))
            {
                string dataToWrite = "Player 0" + Environment.NewLine;
                for (int i = 0; i < collectionScoreObj.Count; i++)
                {
                    dataToWrite = dataToWrite + collectionScoreObj[i].ToString() + Environment.NewLine;   
                }
                writer.WriteLine(dataToWrite);
            }

        }

        //Read the data from "highScore.txt"
        //Add the data to a list containing all the highscores. 
        public void ReadFromFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(highScoreFile))
                {
                    collectionScoreObj.Clear();
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        string[] contents = new string[2];
                        contents = line.Split(' ');
                        Score scoreObj = new Score();
                        scoreObj.Name = contents[0];
                        scoreObj.PlayerScore = Convert.ToInt32(contents[1]);
                        collectionScoreObj.Add(scoreObj);
                        line = reader.ReadLine();
                    }

                }
            }
            catch(FileNotFoundException e)
            {
                WriteToFile();
                ReadFromFile();
            }
        }

        //Updates the list containing the highscores if the new score is larger than the lowest highscore. 
        public void AddHighScore(string playerName, int newScore)
        {
            ReadFromFile();
            Score newScoreData = new Score();
            newScoreData.Name = playerName;
            newScoreData.PlayerScore = newScore;
            if (collectionScoreObj.Count() != 0)
            {
                int loc = -1;
                for (int i = 0; i < collectionScoreObj.Count(); i++)
                {
                    if (newScore > collectionScoreObj[i].PlayerScore)
                    {
                        loc = i;
                    }
                }
                if (loc != -1)
                {
                    collectionScoreObj.Insert(loc, newScoreData);
                    if (collectionScoreObj.Count() > 10)
                    {
                        collectionScoreObj.RemoveAt(collectionScoreObj.Count() - 1);
                    }
                    
                }
            }
            else
            {
                collectionScoreObj.Add(newScoreData);
            }
            WriteToFile();       
            
        }

        //Returns the list of highscores
        public List<string> GetHighScores()
        {
            ReadFromFile();
            List<string> scores = new List<string>();
            for (int item = 0; item < collectionScoreObj.Count(); item++)
            {
                scores.Add(collectionScoreObj[item].ToString());
            }
            return scores;
        }
    }
}
