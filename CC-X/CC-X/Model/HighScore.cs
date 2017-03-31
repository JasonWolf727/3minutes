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
        List<string> data = new List<string>();
                       
        //This method will write all of the high scores to the "highScore.txt" file. 
        //It will take in a list of all the data points and overwrite the contents of the file
        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(highScoreFile))
            {
                //the data will be stored on separate lines separated by a space
                //player_name " " score
                writer.Write("data");
            }

        }

        //Read the data from "highScore.txt"
        //Add the data to a list containing all the highscores. 
        public void ReadFromFile()
        {
            //not implemented yet
        }

        //Updates the list containing the highscores if the new score is larger than the lowest highscore. 
        public void AddHighScore(int newScore)
        {
            //not implemented yet
        }

        //Returns the list of highscores
        public List<string> GetHighScores()
        {
            return data;
        }
    }
}
