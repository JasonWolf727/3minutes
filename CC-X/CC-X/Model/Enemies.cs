using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    class Enemies : World
    {
        public int Damage { get; set; }
        public int Health { get; set; }
        public Enemies()
        {

        }
        public override void UpdatePos(Vector3 position)
        {
            throw new NotImplementedException();
        }

        public override string Serialize(SettingType setting, EnemyType enemy, Vector3 pos, int id, int pow, int health)
        {
            string info = string.Format("{0}, {1}, {2}, {3}, {4}", enemy, pos, id, pow, health);
            return info;
        }

        public override void DeSerialize()
        {
            // Take the stuff in a CSV file and put it into a single comma-delimited string; then distribute among all the values.
        }
    }
}
