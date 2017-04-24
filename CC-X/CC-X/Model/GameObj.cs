/*
 * File: GameObj.cs
 * Author: Michael Johannes
 * Desc: Contains the parent class for all game objects.
 *       MainCharacter, Enemy, and Nature inherit this main class
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    // Parent class for all game objects. Contains non-implemented Position and ID properties
    abstract class GameObj
    {
        public Vector3 Position { get; set; } // Records the current position of an object in the game world
        public uint ID { get; set; } // Records a unique identifying number for each object
    }
}