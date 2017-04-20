using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CC_X.Model
{
    [TestClass]
    public class HighScoreUnitTests
    {
        [TestMethod]
        public void AddHighScore_FirstScoreAdded_Success()
        {
            if (File.Exists(@"C:highScore.txt"))    
            {
                File.Delete(@"C:highScore.txt");
            }
            HighScore cmdHS = new HighScore();
            cmdHS.AddHighScore("Fred", 32);
            Assert.IsTrue(cmdHS.collectionScoreObj[0].ToString() == "Fred 32");
        }

        [TestMethod]
        public void AddHighScore_5thScoreAdded_Success()
        {
            if (File.Exists(@"C:highScore.txt"))
            {
                File.Delete(@"C:highScore.txt");
            }
            HighScore cmdHS = new HighScore();
            cmdHS.AddHighScore("Joe", 23);
            cmdHS.AddHighScore("Joe", 78);
            cmdHS.AddHighScore("Joe", 33);
            cmdHS.AddHighScore("Joe", 134);
            cmdHS.AddHighScore("Joe", 21);
            Assert.IsTrue(cmdHS.collectionScoreObj[4].ToString() == "Joe 134");

            //changed to commit

        }

        [TestMethod]
        public void AddHighScore_10thScoreAdded_Success()
        {
            if (File.Exists(@"C:highScore.txt"))
            {
                File.Delete(@"C:highScore.txt");
            }
            HighScore cmdHS = new HighScore();
            cmdHS.AddHighScore("Joe", 23);
            cmdHS.AddHighScore("Joe", 78);
            cmdHS.AddHighScore("Joe", 33);
            cmdHS.AddHighScore("Joe", 134);
            cmdHS.AddHighScore("Joe", 21);
            cmdHS.AddHighScore("Fred", 114);
            cmdHS.AddHighScore("Bob", 25);
            cmdHS.AddHighScore("Fred", 128);
            cmdHS.AddHighScore("Bob", 52);
            cmdHS.AddHighScore("Garry", 131);
            cmdHS.AddHighScore("Batman", 43);
            Assert.IsTrue(cmdHS.collectionScoreObj[9].ToString() == "Garry 131");
        }
    }
}
