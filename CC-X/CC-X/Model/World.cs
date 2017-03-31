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
        public Vector3 Position { get; set; }
        public int ID { get; set; }
        abstract public void UpdatePos(Vector3 position);                      

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
