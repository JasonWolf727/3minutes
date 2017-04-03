using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    class Enemy : World, Serializer
    {        
        public Enemy()
        {
            type = WorldType.Enemy;
            IsDead = false;
        }
        //If not dead, sets Enemies.Position to position
        public override void UpdatePos(Vector3 position)
        {
            throw new NotImplementedException();
        }

        // Store information concerning enemies
        public string Serialize()
        {
            //string info = string.Format("{0}, {1}, {2}, {3}", pos, id, pow, health);
            //return info;
            throw new NotImplementedException();
        }

        // Load information concerning enemies
        public void DeSerialize(string fileinfo)
        {
            //string[] info = fileinfo.Split(',');
            //string[] tempnums = info[0].Split(',');
            //int[] nums = new int[3];
            //for (int i = 0; i < 3; ++i)
            //    nums[i] = Convert.ToInt32(tempnums[i]);
            //this.Position = new Vector3(nums[0], nums[1], nums[2]);
            //this.ID = Convert.ToInt32(info[1]);
            //this.Damage = Convert.ToInt32(info[2]);
            //this.Health = Convert.ToInt32(info[3]);
            throw new NotImplementedException();
        }

        public override bool DetectCollision(Dictionary<int, World> worldObjs)
        {
            throw new NotImplementedException();
        }
    }
}
