using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC_X.Model
{
    [TestClass]
    class HighScoreUnitTests
    {
        [TestMethod]
        public void AddHighScore_FirstScoreAdded_Success()
        {
            HighScore cmdHS = new HighScore();
            cmdHS.AddHighScore("Fred", 32);
            Assert.IsTrue(cmdHS.collectionScoreObj[0].ToString() == "PlayerOne 32");
        }

        [TestMethod]
        public void AddHighScore_5thScoreAdded_Success()
        {
            HighScore cmdHS = new HighScore();
            //not implemented yet
        }

        [TestMethod]
        public void AddHighScore_10thScoreAdded_Success()
        {
            HighScore cmdHS = new HighScore();
            //not implemented yet
        }
    }
}
