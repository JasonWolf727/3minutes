using System;
using System.IO; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_X.Model
{
    class HighScore
    {
        const string highScoreFile = "highscore.txt";
        //static string fileLocation = @"c:\Users\Joshua\Desktop\TeamProjectShared\3minutes\CC-X\CC-X"
        int timesTest = 0;
        public void WriteToFile()
        {
            using (StreamWriter writer = new StreamWriter(highScoreFile))
            {
                writer.Write("test" + timesTest);
            }
            timesTest += 1;
        }
    }
}
