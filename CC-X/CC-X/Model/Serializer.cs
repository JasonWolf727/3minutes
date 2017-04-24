/* 
 * File: Serializer.cs
 * Author: Carlos Santana (under csant714, prior to GitHub linking)
 * Desc: An interface for the (De)Serialization mechanism
 *       Implemented in MainCharacter, Nature, and Enemy
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_X.Model
{
    // Interface with undefined methods, Serialize and DeSerialize.
    interface Serializer
    {
        string Serialize();
        void DeSerialize(string fileinfo);
    }
}