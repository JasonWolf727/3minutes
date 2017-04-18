using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC_X.Model
{
    [TestClass]
    public class HighScoreUnitTests
    {
        [TestMethod]
        public void AddHighScore_FirstScoreAdded_Success()
        {
            HighScore cmdHS = new HighScore();
            cmdHS.AddHighScore("Fred", 32);
            Assert.IsTrue(cmdHS.collectionScoreObj[0].ToString() == "Default 0");
        }

        [TestMethod]
        public void AddHighScore_5thScoreAdded_Success()
        {
            HighScore cmdHS = new HighScore();
            cmdHS.AddHighScore("Joe", 23);
            cmdHS.AddHighScore("Joe", 78);
            cmdHS.AddHighScore("Joe", 33);
            cmdHS.AddHighScore("Joe", 134);
            cmdHS.AddHighScore("Joe", 21);
            Assert.IsTrue(cmdHS.collectionScoreObj[4].ToString() == "Joe 134");



        }

        [TestMethod]
        public void AddHighScore_10thScoreAdded_Success()
        {
            HighScore cmdHS = new HighScore();
            //not implemented yet
        }
    }
}
