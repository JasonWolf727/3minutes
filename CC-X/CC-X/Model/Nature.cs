using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    //Class for harmless objects that populate the scenery
    class Nature : GameObj, Serializer
    {
        public enum NatureType { Plane, Tree, Rock, Grass, None }
        public Nature(NatureType natureObj, Vector3 position) { }                
        // Store information concerning the environment
        public string Serialize()
        {
            string info = string.Format("{0}", this.ID);
            return info;
            throw new NotImplementedException();
        }

        // Load the environment
        public void DeSerialize(string fileinfo)
        {
            string[] info = fileinfo.Split(',');
            // The rest goes here...
        }        
    }
}
