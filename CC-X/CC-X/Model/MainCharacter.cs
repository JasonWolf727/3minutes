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

        public override void DeSerialize()
        {
            // Take the stuff in a CSV file and put it into a single comma-delimited string; then distribute among all the values.
        }
    }
}
