using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using System.Drawing;

namespace CC_X.Model
{
    //Class for objects that reduce main character's health
    class Enemy : GameObj, Serializer
    {
        public bool IsDead { get; set; }
        public int Strength { get; set; }//An integer value of damage can inflict. High number = high damage.
        public int Health { get; set; }//Health for MainChar starts at 100. If health <= 0, character dies. 
        public EnemyType ObjType { get; set; }      
        public enum EnemyType { Zombie, Car, None }
        public enum CarDir { Left, Right }
        public CarDir CarMovingDirection { get; set; }
        public float CarSpeed { get; set; }
        public Rectangle persnlBubble;
        public Enemy(Vector3 position)
        {
            Position = position;
            persnlBubble = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Z), Convert.ToInt32(0.6), Convert.ToInt32(0.6));
        }
        //If not dead, sets Enemies.Position to position
        public void UpdatePos(Vector3 position)
        {
            throw new NotImplementedException();
        }

        // Store information concerning enemies
        public string Serialize()
        {
            string info = string.Format("{0}, {1}, {2}, {3}, {4}, {5}", this.Position, this.ID, this.ObjType, this.Strength, this.Health, this.IsDead);
            return info;
        }

        // Load information concerning enemies
        public void DeSerialize(string fileinfo)
        {
            string[] info = fileinfo.Split(',');
            string[] tempnums = info[0].Split(',');
            int[] nums = new int[3];
            for (int i = 0; i < 3; ++i)
                nums[i] = Convert.ToInt32(tempnums[i]);
            this.Position = new Vector3(nums[0], nums[1], nums[2]);
            this.ID = Convert.ToUInt32(info[1]);
            string tempChar = info[3].ToString();
            switch (tempChar)
            {
                case "Zombie":
                    this.ObjType = EnemyType.Zombie;
                    break;
                case "Car":
                    this.ObjType = EnemyType.Car;
                    break;
                case "None":
                    this.ObjType = EnemyType.None;
                    break;
                default:
                    this.ObjType = EnemyType.None;
                    break;
            }
            this.Strength = Convert.ToInt32(info[3]);
            this.Health = Convert.ToInt32(info[4]);
            string tempBool = info[5];
            switch (tempBool)
            {
                case "true":
                    this.IsDead = true;
                    break;
                case "false":
                    this.IsDead = false;
                    break;
                default:
                    this.IsDead = false;
                    break;
            }
        }        
    }
}
