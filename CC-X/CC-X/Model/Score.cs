using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_X.Model
{
    class Score
    {
        //The name of the score to save
        public string Name { get; set; }

        //The value of the score to save
        public int PlayerScore { get; set; }

        public string ToString()
        {
            string toReturn = this.Name + " " + this.PlayerScore;
            return toReturn;
        }
    }
}
