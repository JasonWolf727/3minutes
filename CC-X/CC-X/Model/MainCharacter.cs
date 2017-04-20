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
        public bool Invisible = false;
        public Rectangle persnlBubble;
        public MainCharacter(Vector3 position)
        {
            Experience = 1;
            Position = position;
            IsDead = false;
            Health = 100;
            persnlBubble = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Z), Convert.ToInt32(0.6), Convert.ToInt32(0.6));
        }               
        
        public void ReceiveDamage(int damagePow)
        {
            if(!Invisible)
            {
                Health = Health - damagePow / (Experience * (1 / 1000));
            }
            if (Health <= 0)
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
            string info = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}", this.Position.X, this.Position.Y, this.Position.Z, this.ID, this.SelectedCharType, this.Strength, this.Health, this.TimeSinceLastCollide, this.IsDead, this.persnlBubble.X, this.persnlBubble.Y, this.persnlBubble.Width, this.persnlBubble.Height);
            return info;
        }

        // Load information concerning the Main Character
        public void DeSerialize(string fileinfo)
        {
            string[] info = fileinfo.Split(',');
            this.Position = new Vector3(Convert.ToInt32(info[0]), Convert.ToInt32(info[1]), Convert.ToInt32(info[2]));
            this.ID = Convert.ToUInt32(info[3]);
            string tempChar = info[4].ToString();

            // For some reason, Mutant is stored with leading white space; adding the white space passes the unit test.
            if (tempChar == " Mutant")
                this.SelectedCharType = MainCharOptn.Mutant;
            else if (tempChar == " Ninja")
                this.SelectedCharType = MainCharOptn.Ninja;
            else
                this.SelectedCharType = MainCharOptn.Swat;

            this.Strength = Convert.ToInt32(info[5]);
            this.Health = Convert.ToInt32(info[6]);
            this.TimeSinceLastCollide = Convert.ToInt32(info[7]);
            string tempBool = info[8].ToLower();
            if (tempBool == " true")
                this.IsDead = true;
            else
                this.IsDead = false;
            this.persnlBubble = new Rectangle(Convert.ToInt32(info[9]), Convert.ToInt32(info[10]), Convert.ToInt32(info[11]), Convert.ToInt32(info[12]));
        }
    }
}