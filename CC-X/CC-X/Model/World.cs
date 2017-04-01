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
        public int Damage { get; set; }//An integer value of damage can inflict. High number = high damage. If setting obj, set to 101
        public int Health { get; set; }//Health for MainChar starts at 100. If health <= 0, character dies. If setting obj, set to 101
        abstract public void UpdatePos(Vector3 position);
        abstract public bool DetectCollision(Dictionary<int, World> worldObjs);

    }
}
