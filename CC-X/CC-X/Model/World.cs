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
        public enum WorldType { MainChar, Enemy, Setting }
        public WorldType type { get; set; }
        public Vector3 Position { get; set; }
        public int ID { get; set; }
        public bool IsDead { get; set; }
        abstract public void UpdatePos(Vector3 position);                

    }
}
