using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    class Enemy : GameObj, Serializer
    {
        public bool IsDead { get; set; }
        public int Damage { get; set; }//An integer value of damage can inflict. High number = high damage.
        public int Health { get; set; }//Health for MainChar starts at 100. If health <= 0, character dies. 
        public EnemyType ObjType { get; set; }      
        public enum EnemyType { Zombie, Car, None }

        public Enemy()
        {

        }
        //If not dead, sets Enemies.Position to position
        public void UpdatePos(Vector3 position)
        {
            throw new NotImplementedException();
        }

        // Store information concerning enemies
        public string Serialize()
        {
            string info = string.Format("{0}, {1}, {2}, {3}", this.Position, this.ID, this.Damage, this.Health);
            return info;
        }

        // Load information concerning enemies
        public void DeSerialize(string fileinfo)
        {
            string[] info = fileinfo.Split(',');
            string[] tempnums = info[0].Split(',');
            int[] nums = new int[3];
            for (int i = 0; i < 3; ++i)
                nums[i] = Convert.ToInt32(tempnums[i]);
            this.Position = new Vector3(nums[0], nums[1], nums[2]);
            this.ID = Convert.ToUInt32(info[1]);
            this.Damage = Convert.ToInt32(info[2]);
            this.Health = Convert.ToInt32(info[3]);
        }

        public bool DetectCollision(Dictionary<int, GameObj> worldObjs)
        {
            throw new NotImplementedException();
        }
    }
}
