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
        public NatureType SelectedNatureType { get; set; }
        public Nature(NatureType natureObj, Vector3 position) { } 
                       
        // Store information concerning the environment
        public string Serialize()
        {
            string info = string.Format("{0}, {1}, {2}", this.Position, this.ID, this.SelectedNatureType);
            return info;
        }

        // Load the environment
        // NOT EDITED YET
        public void DeSerialize(string fileinfo)
        {
            string[] info = fileinfo.Split(',');
            string[] tempnums = info[0].Split(',');
            int[] nums = new int[3];
            for (int i = 0; i < 3; ++i)
                nums[i] = Convert.ToInt32(tempnums[i]);
            this.Position = new Vector3(nums[0], nums[1], nums[2]);
            this.ID = Convert.ToUInt32(info[1]);
            string tempType = info[2];
            switch (tempType)
            {
                case "Plane":
                    this.SelectedNatureType = NatureType.Plane;
                    break;
                case "Tree":
                    this.SelectedNatureType = NatureType.Tree;
                    break;
                case "Rock":
                    this.SelectedNatureType = NatureType.Rock;
                    break;
                case "Grass":
                    this.SelectedNatureType = NatureType.Grass;
                    break;
                case "None":
                    this.SelectedNatureType = NatureType.None;
                    break;
                default:
                    this.SelectedNatureType = NatureType.Plane;
                    break;
            }
        }
    }
}
