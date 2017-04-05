using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Urho;

namespace CC_X.Model
{
    //Class for the main character in the game
    class MainCharacter : GameObj, Serializer
    {                 
        public enum MainCharOptn { Swat, Mutant }
        public MainCharOptn SelectedCharType { get; set; }
        public Vector3 PositionSinceLastCollide { get; set; }
        public bool IsDead { get; set; }
        public int Strength { get; set; }//An integer value of damage can inflict. High number = high damage. If setting obj, set to 101
        public int Health { get; set; }//Health for MainChar starts at 100. If health <= 0, character dies. If setting obj, set to 101
        public int Experience { get; set; }
        public int Points { get; set; }
        public MainCharacter()
        {           
        }

        //If not dead, sets MainCharacter.Position to position
        public void UpdatePos(Vector3 position)
        {
            throw new NotImplementedException();
        }

        //Returns true if collided with other World objects. If World object type == WorldType.Enemies and MainChar.PositionSinceLastCollide != MainChar.Position, subtracts enemy damage from health
        public bool DetectCollision(Dictionary<int, GameObj> worldObjs)
        {
            throw new NotImplementedException();
        }
        public void ReceiveDamage(int damagePow)
        {

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
        public bool IsMainCharDead()
        {
            throw new NotImplementedException();
        }

        // Store information concerning the Main Character
        public string Serialize()
        {
            string info = string.Format("{0}, {1}, {2}, {3}", this.Position, this.ID, this.Strength, this.Health);
            return info;
        }

        // Load information concerning the Main Character
        public void DeSerialize(string fileinfo)
        {
            string[] info = fileinfo.Split(',');
            string[] tempnums = info[0].Split(',');
            int[] nums = new int[3];
            for (int i = 0; i < 3; ++i)
                nums[i] = Convert.ToInt32(tempnums[i]);
            this.Position = new Vector3(nums[0], nums[1], nums[2]); // cannot implicitly convert string to vector3
            this.ID = Convert.ToUInt32(info[1]);
            this.Strength = Convert.ToInt32(info[2]);
            this.Health = Convert.ToInt32(info[3]);
        }
       
    }
}