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
        public List<string> data = new List<string>();

        //This method will write all of the high scores to the "highScore.txt" file. 
        //It will take in a list of all the data points and overwrite the contents of the file
        //the data will be stored on separate lines separated by a space
        //player_name " " score
        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(highScoreFile))
            {
                string dataToWrite = "";
                for (int i = 0; i < data.Count; i++)
                {
                    dataToWrite = dataToWrite + data[i] + "/n";   
                }
                writer.WriteLine(dataToWrite);
            }

        }

        //Read the data from "highScore.txt"
        //Add the data to a list containing all the highscores. 
        public void ReadFromFile()
        {
            using (StreamReader reader = new StreamReader(highScoreFile))
            {
                data.Clear();
                string line = reader.ReadLine();
                while (line != null)
                {
                    data.Add(line);
                    line = reader.ReadLine();
                }
                
            }
        }

        //Updates the list containing the highscores if the new score is larger than the lowest highscore. 
        public void AddHighScore(int newScore, string playerName)
        {
            ReadFromFile();
            string newScoreData = "";
            newScoreData = newScoreData + newScore + playerName;
            if (data.Count() != 0)
            {
                int loc = -1;
                for (int i = 0; i < data.Count(); i++)
                {
                    var contents = data[i].Split(' ');
                    if (newScore > Convert.ToInt32(contents[1]))
                    {
                        loc = i;
                    }
                }
                if (loc != -1)
                {
                    data.Insert(loc, newScoreData);
                    data.RemoveAt(data.Count() - 1);
                }
            }
            else
            {
                data.Add(newScoreData);
            }        
            
        }

        //Returns the list of highscores
        public List<string> GetHighScores()
        {
            ReadFromFile();
            return data;
        }
    }
}
