/*
 * File: Enemy.cs
 * Author: Michael Johannes and Carlos Santana
 * Desc: Contains the attributes and abilities of an enemy
 */


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
        public bool IsDead { get; set; } // Sets the enemy's life status
        public int Strength { get; set; }// An integer value of damage can inflict. High number = high damage.
        public int Health { get; set; } // Health for MainChar starts at 100. If health <= 0, character dies. 
        public EnemyType ObjType { get; set; } // Retrieves & sets the enemy's EnemyType
        public enum EnemyType { Zombie, Car, None } // Lists the possible types this enemy can be
        public enum CarDir { Left, Right } // Lists the possible directions a car can go
        public CarDir CarMovingDirection { get; set; } // Retrieves & sets the car's direction
        public float CarSpeed { get; set; } // Records the car's speed
        public Rectangle persnlBubble; // Acts as a field of contact around the enemy

        // Instantiates the enemy with the given values
        public Enemy(Vector3 position)
        {
            Strength = 1;
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
            string info = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}", this.Position.X, this.Position.Y, this.Position.Z, this.ID, this.ObjType, this.Strength, this.Health, this.IsDead, this.persnlBubble.X, this.persnlBubble.Y, this.persnlBubble.Width, this.persnlBubble.Height, this.CarMovingDirection);
            return info;
        }

        // Parse information concerning enemies
        public void DeSerialize(string fileinfo)
        {
            string[] info = fileinfo.Split(',');
            this.Position = new Vector3(Convert.ToInt32(info[0]), Convert.ToInt32(info[1]), Convert.ToInt32(info[2]));
            this.ID = Convert.ToUInt32(info[3]);
            string tempChar = info[4].ToString();
            switch (tempChar)
            {
                case " Zombie":
                    this.ObjType = EnemyType.Zombie;
                    break;
                case " Car":
                    this.ObjType = EnemyType.Car;
                    break;
                case " None":
                    this.ObjType = EnemyType.None;
                    break;
                default:
                    this.ObjType = EnemyType.None;
                    break;
            }
            this.Strength = Convert.ToInt32(info[5]);
            this.Health = Convert.ToInt32(info[6]);
            string tempBool = info[7];
            switch (tempBool)
            {
                case " true":
                    this.IsDead = true;
                    break;
                case " false":
                    this.IsDead = false;
                    break;
                default:
                    this.IsDead = false;
                    break;
            }
            this.persnlBubble = new Rectangle(Convert.ToInt32(info[8]), Convert.ToInt32(info[9]), Convert.ToInt32(info[10]), Convert.ToInt32(info[11]));
            string tempDir = info[12].ToLower();
            switch (tempDir)
            {
                case " left":
                    this.CarMovingDirection = CarDir.Left;
                    break;
                case " right":
                    this.CarMovingDirection = CarDir.Right;
                    break;
                default:
                    this.CarMovingDirection = CarDir.Left;
                    break;
            }
        }
    }
}
