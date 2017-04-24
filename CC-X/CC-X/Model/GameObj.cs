using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

// Parent class for all game objects. Contains non-implemented Position and ID properties

namespace CC_X.Model
{
    abstract class GameObj
    {
        public Vector3 Position { get; set; } // Records the current position of an object in the game world
        public uint ID { get; set; } // Records a unique identifying number for each object
    }
}
