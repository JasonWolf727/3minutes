using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC_X.Model
{
    [TestClass]
    class SerializationTest
    {
        // Serialization logic has not been written yet, so these might fail.
        [TestMethod]
        public void Load_Success()
        {
            try
            {
                GameController c = new GameController(Difficulty.Easy);
                string filepath = "C:\\Users\\csant714\\Desktop\\3minutes\\CC - X\\CC - X\\Model\\LoadSaveTest.csv";
                c.Save(filepath);
                c.Load(filepath);
            }
            catch
            {
                throw new AssertFailedException();
            }
        }
    }
}
