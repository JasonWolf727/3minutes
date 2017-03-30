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
        
    }
}
