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
        public override void UpdatePos(int X, int Y, int Z)
        {
            throw new NotImplementedException();
        }

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
