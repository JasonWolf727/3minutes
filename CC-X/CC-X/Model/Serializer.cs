using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC_X.Model
{
    interface Serializer
    {
        string Serialize();
        void DeSerialize(string fileinfo);
    }
}

/*
Notes to self:
    Investigate Node.Save(Urho.IO.File destination);
    Investigate Component.Save(Urho.IO.File destination);
*/