using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;
using System.Drawing;

namespace CC_X.Model
{
    //Class for the main character in the game
    class MainCharacter : GameObj, Serializer
    {                 
        public enum MainCharOptn { Swat, Mutant, Ninja }
        public MainCharOptn SelectedCharType { get; set; }
        public int TimeSinceLastCollide { get; set; }
        public bool IsDead { get; set; }
        public int Strength { get; set; }//An integer value of damage can inflict. High number = high damage. If setting obj, set to 101
        public int Health { get; set; }//Health for MainChar starts at 100. If health <= 0, character dies. If setting obj, set to 101
        public int Experience { get; set; }
        public int Points { get; set; }
        public Rectangle persnlBubble;
        public MainCharacter()
        {
            IsDead = false;
            Health = 100;
            persnlBubble = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Z), Convert.ToInt32(0.6), Convert.ToInt32(0.6));
        }               
        
        public void ReceiveDamage(int damagePow)
        {
            Health = Health - damagePow;
            if(Health <= 0)
            {
                IsDead = true;
            }
        }
        public void UpdatePos(Vector3 pos)
        {            
            Position = pos;
        }
        public void MoveLeft(float howMuch)
        {
            var newPos = new Vector3(-howMuch,Position.Y,Position.Z);
            Position += newPos;
        }

        public void MoveRight(float howMuch)
        {
            var newPos = new Vector3(howMuch, Position.Y, Position.Z);
            Position += newPos;
        }
        public void MoveForward(float howMuch)
        {
            var newPos = new Vector3(Position.X, Position.Y, howMuch);
            Position += newPos;
        }
        public void MoveBackward(float howMuch)
        {
            var newPos = new Vector3(Position.X, Position.Y, -howMuch);
            Position += newPos;
        }
        public string GetStatus()
        {
            if (IsDead) { return "Dead"; }
            else { return "Living"; }
        }

        // Store information concerning the Main Character
        public string Serialize()
        {
            string info = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", this.Position, this.ID, this.SelectedCharType, this.Strength, this.Health, this.TimeSinceLastCollide, this.IsDead);
            return info;
        }

        // Load information concerning the Main Character
        public void DeSerialize(string fileinfo)
        {
            string[] info = fileinfo.Split(',');
            string[] tempnums1 = info[0].Split(',');
            int[] nums1 = new int[3];
            for (int i = 0; i < 3; ++i)
                nums1[i] = Convert.ToInt32(tempnums1[i]);
            this.Position = new Vector3(nums1[0], nums1[1], nums1[2]);
            this.ID = Convert.ToUInt32(info[1]);
            string tempChar = info[2].ToString();
            switch (tempChar)
            {
                case "Swat":
                    this.SelectedCharType = MainCharOptn.Swat;
                    break;
                case "Mutant":
                    this.SelectedCharType = MainCharOptn.Mutant;
                    break;
                default:
                    this.SelectedCharType = MainCharOptn.Swat;
                    break;
            }
            this.Strength = Convert.ToInt32(info[3]);
            this.Health = Convert.ToInt32(info[4]);
            string[] tempnums2 = info[5].Split(',');
            int[] nums2 = new int[3];
            for (int j = 0; j < 3; ++j)
                nums2[j] = Convert.ToInt32(tempnums2[j]);
            //this.TimeSinceLastCollide = new Vector3(nums2[0], nums2[1], nums2[2]);
            string tempBool = info[6];
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