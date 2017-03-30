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
    }
}
