/*
 * File: MainCharacter.cs
 * Author: Joshua Case, Michael Johannes, and Carlos Santana
 * Description: Attributes and abilities of the character the player controls.
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
    // Class that creates the main character in the game. Inherits the GameObj model, and implements the Serialization interface.
    class MainCharacter : GameObj, Serializer
    {                 
        public enum MainCharOptn { Swat, Mutant, Ninja } // Lists the possible skins for the main character
        public MainCharOptn SelectedCharType { get; set; } // Retrieves & sets the skin to each main character option
        public int TimeSinceLastCollide { get; set; } // Records time elapsed since the character got hit; keeps the character from losing all health with every collision frame
        public bool IsDead { get; set; } // Identifies the character as dead or alive, for Save/Load purposes
        public int Strength { get; set; } // An integer value of damage the character can inflict. High number = high damage.
        public int Health { get; set; } // Health for MainChar starts at 100. If health <= 0, character dies.
        public int Experience { get; set; } // Number of experience the player has
        public int Points { get; set; } // Records the number of points the MainCharacter receives at the end of the level
        public bool Invinsible = false; // Activates invincibility for the character. Used in Cheat mode
        public Rectangle persnlBubble; // Acts as a field of contact around the MainCharacter

        // Instantiates the Character with a provided position.
        public MainCharacter(Vector3 position)
        {
            Position = position;
            IsDead = false;
            Health = 100;
            persnlBubble = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Z), Convert.ToInt32(0.6), Convert.ToInt32(0.6));
        }

        // Calculates the damage the Character receives in the event of a collision and adjusts health accordingly.
        // Also sets the character to dead once health is depleted
        public void ReceiveDamage(int damagePow)
        {
            if(!Invinsible)
            {
                Health = Health - (damagePow / ((Experience + 1000)/1000));
            }
            if (Health <= 0)
            {
                IsDead = true;
            }
        }

        // Updates the Character's position on the tick of the clock
        public void UpdatePos(Vector3 pos)
        {            
            Position = pos;
        }

        // Calculates the distance moved to the left and updates the position accordingly.
        public void MoveLeft(float howMuch)
        {
            var newPos = new Vector3(-howMuch,Position.Y,Position.Z);
            Position += newPos;
        }

        // Calculates the distance moved to the right and updates the position accordingly.
        public void MoveRight(float howMuch)
        {
            var newPos = new Vector3(howMuch, Position.Y, Position.Z);
            Position += newPos;
        }

        // Calculates the distance moved forwards and updates the position accordingly.
        public void MoveForward(float howMuch)
        {
            var newPos = new Vector3(Position.X, Position.Y, howMuch);
            Position += newPos;
        }

        // Calculates the distance moved backwards and updates the position accordingly.
        public void MoveBackward(float howMuch)
        {
            var newPos = new Vector3(Position.X, Position.Y, -howMuch);
            Position += newPos;
        }

        // Returns the text that matches the Character's life status
        public string GetStatus()
        {
            if (IsDead) { return "Dead"; }
            else { return "Living"; }
        }

        // Store information concerning the Main Character in a single comma-delimited string
        public string Serialize()
        {
            string info = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}", this.Position.X, this.Position.Y, this.Position.Z, this.ID, this.SelectedCharType, this.Strength, this.Health, this.TimeSinceLastCollide, this.IsDead, this.persnlBubble.X, this.persnlBubble.Y, this.persnlBubble.Width, this.persnlBubble.Height);
            return info;
        }

        // Parses information concerning the Main Character to all of its attributes
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