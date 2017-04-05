using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    //Parent for objects in the game
    abstract class GameObj
    {
        public Vector3 Position { get; set; }
        public uint ID { get; set; }                
    }
}
