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
            cmdHS.AddHighScore(32, "PlayerOne");
            Assert.IsTrue(cmdHS.data[0] == "PlayerOne 32");
        }
    }
}
