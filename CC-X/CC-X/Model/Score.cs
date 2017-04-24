/*
 * File: Score.cs
 * Author: Joshua Case
 * Desc: contains properties for the scoring mechanism
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_X.Model
{
    // Score class, contains properties for scoring
    class Score
    {
        //The name of the score to save
        public string Name { get; set; }

        //The value of the score to save
        public int PlayerScore { get; set; }

        //Converts the contents of a Score Object into a string.
        public string ToString()
        {
            string toReturn = this.Name + " " + this.PlayerScore;
            return toReturn;
        }
    }
}
