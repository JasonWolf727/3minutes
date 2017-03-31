using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    class MainCharacter : World
    {
        public int Damage { get; set; }
        public int Health { get; set; }
        public MainCharacter()
        {

        }
        public override void UpdatePos(Vector3 position)
        {
            throw new NotImplementedException();
        }

        // Store information concerning the Main Character
        public override string Serialize(SettingType setting, EnemyType enemy, Vector3 pos, int id, int pow, int health)
        {
            string info = string.Format("{0}, {1}, {2}, {3}", pos, id, pow, health);
            return info;
        }

        // Load information concerning the Main Character
        public override void DeSerialize(string fileinfo)
        {
            string[] info = fileinfo.Split(',');
            string[] tempnums = info[0].Split(',');
            int[] nums = new int[3];
            for (int i = 0; i < 3; ++i)
                nums[i] = Convert.ToInt32(tempnums[i]);
            this.Position = new Vector3(nums[0], nums[1], nums[2]); // cannot implicitly convert string to vector3
            this.ID = Convert.ToInt32(info[1]);
            this.Damage = Convert.ToInt32(info[2]);
            this.Health = Convert.ToInt32(info[3]);
        }
    }
}
