using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    abstract class World
    {
        public enum SettingType { Plane, Tree, Rock, Grass, None }
        public enum EnemyType { Zombie, Car, None }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int ID { get; set; }
        abstract public void UpdatePos(int X, int Y, int Z);

        public void Serialize()
        {
            // Take the stuff in World and put it into a single comma-delimited string.
        }

        public void DeSerialize()
        {
            // Take the stuff in a CSV file and put it into a single comma-delimited string; then distribute among all the values.
        }

    }
}
