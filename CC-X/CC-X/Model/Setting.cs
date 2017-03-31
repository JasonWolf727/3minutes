using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    class Setting : World
    {
        public Setting(SettingType setting, Vector3 position)
        {

        }
        public override void UpdatePos(Vector3 position)
        {
            throw new NotImplementedException();
        }

        public override string Serialize(SettingType setting, EnemyType enemy, Vector3 pos, int id, int pow, int health)
        {
            string info = string.Format("{0}, {1}", setting, id);
            return info;
        }

        public override void DeSerialize(string fileinfo)
        {
            throw new NotImplementedException();
        }
    }
}
