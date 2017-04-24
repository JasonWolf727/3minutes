/*
 * File: GameObjTest.cs
 * Author: Michael Johannes
 * Desc: Features unit tests for the Game Objects
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Urho;



namespace CC_X.Model
{
    // Unit tests using an instance of the Game Object Class
    [TestClass]
    public class GameObjTest
    {
        Dictionary<uint, GameObj> GameObjCollection { get; set; } // Contains Nature and Enemy objects      
        MainCharacter MainChar = new MainCharacter(new Vector3(75, -0.50523f, 1.62f)); // Instance of the Main Character class

        // Logic to calculate if a collision has taken place. If so, do the damage thing. If not, reset the object.
        List<object> DetectCollision()
        {
            if (MainChar != null)
            {
                foreach (GameObj obj in GameObjCollection.Values)
                {
                    if (obj is Enemy)
                    {
                        if (MainChar.persnlBubble.IntersectsWith(((Enemy)(obj)).persnlBubble))
                        {
                            MainChar.ReceiveDamage(((Enemy)obj).Strength);
                            return new List<object>() { true, obj.Position };
                        }
                    }
                }
                return new List<object>() { false, new Vector3(-1000000, -1000000, -1000000) };
            }
            else
            {
                return new List<object>() { false, new Vector3(-1000000, -1000000, -1000000) };
            }
        }

        // Test to make sure the two objects are not colliding while being rendered.
        [TestMethod]
        public void DetectCollision_zombieMainChar_NoCollision()
        {
            Enemy zombie = new Enemy(new Urho.Vector3(1, 2, 1));
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(5, 4, 1));
            GameController game = new GameController(Difficulty.Easy);
            game.MainChar = MainChar;
            
            game.GameObjCollection[0] = zombie;

            Assert.IsTrue((bool)(game.DetectCollision()[0]) != true);
        }

        // Test to see if the Zombie collided with the Main Character
        [TestMethod]
        public void DetectCollision_zombieMainCharClosePos_Collision()
        {
            Enemy zombie = new Enemy(new Urho.Vector3(1, 2, 3));
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(0.96f, 4, 3));
            GameController game = new GameController(Difficulty.Easy);
            game.MainChar = MainChar;

            game.GameObjCollection[0] = zombie;

            Assert.IsTrue((bool)(DetectCollision()[0]) == true);
        }

        // Make sure that two objects with equal X's collide
        [TestMethod]
        public void DetectCollision_zombieMainCharEqualPosX_Collision()
        {
            Enemy zombie = new Enemy(new Urho.Vector3(1, 2, 8));
            MainChar = new MainCharacter(new Urho.Vector3(1, 4, 8));
            
            GameObjCollection[0] = zombie;

            Assert.IsTrue((bool)(DetectCollision()[0]) == true);
        }

        // Make sure that two objects with equal Y's collide
        [TestMethod]
        public void DetectCollision_zombieMainCharEqualPosY_Collision()
        {
            Enemy zombie = new Enemy(new Urho.Vector3(1, 4, 8));
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(1.05f, 4, 8));
            GameController game = new GameController(Difficulty.Easy);
            game.MainChar = MainChar;
            
            game.GameObjCollection[0] = zombie;

            Assert.IsTrue((bool)(game.DetectCollision()[0]) == true);
        }

        // Make sure the character's health was deducted correctly
        [TestMethod]
        public void DetectCollision_AssessDamage_DamageTaken()
        {            
            Enemy zombie = new Enemy(new Urho.Vector3(1, 4, 8));
            zombie.Strength = 20;
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(0.96f, 4, 8));
            MainChar.Health = 100;
            GameController game = new GameController(Difficulty.Easy);
            game.MainChar = MainChar;
            
            game.GameObjCollection[0] = zombie;

            game.DetectCollision();

            Assert.IsTrue(MainChar.Health == 80);
        }

        // Make sure the character's health didn't go down when the collision doesn't happen
        [TestMethod]
        public void DetectCollision_AssessDamage_NoDamageTaken()
        {
            Enemy zombie = new Enemy(new Urho.Vector3(1, 4, 8));
            zombie.Strength = 20;
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(3, 2, 8));
            MainChar.Health = 100;
            GameController game = new GameController(Difficulty.Easy);
            game.MainChar = MainChar;

            
            game.GameObjCollection[0] = zombie;

            game.DetectCollision();

            Assert.IsTrue(MainChar.Health == 100);
        }

        // Make sure the damage deducts correctly in the event of proximity
        [TestMethod]
        public void DetectCollision_AssessDamageClosePos_DamageTaken()
        {
            Enemy zombie = new Enemy(new Urho.Vector3(1, 4, 8));
            zombie.Strength = 20;
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(0.96f, 3.95f, 8));
            MainChar.Health = 100;
            GameController game = new GameController(Difficulty.Easy);
            game.MainChar = MainChar;
            
            game.GameObjCollection[0] = zombie;

            game.DetectCollision();

            Assert.IsTrue(game.MainChar.Health == 80);
        }

        // Check for continuing damage until death occurs
        [TestMethod]
        public void DetectCollision_AssessDamageAndDeath_NoException()
        {
            Enemy zombie = new Enemy(new Urho.Vector3(1, 4, 8));
            zombie.Strength = 20;
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(0.96f, 4, 8));
            MainChar.Health = 100;

            GameController game = new GameController(Difficulty.Easy);
            game.MainChar = MainChar;
            
            game.GameObjCollection[0] = zombie;

            game.DetectCollision();

            Assert.IsTrue(game.MainChar.Health == 80);
            Assert.IsTrue(game.MainChar.IsDead != true);

            game.DetectCollision();

            Assert.IsTrue(game.MainChar.Health == 60);
            Assert.IsTrue(game.MainChar.IsDead != true);

            game.DetectCollision();

            Assert.IsTrue(game.MainChar.Health == 40);
            Assert.IsTrue(game.MainChar.IsDead != true);

            game.DetectCollision();

            Assert.IsTrue(game.MainChar.Health == 20);
            Assert.IsTrue(game.MainChar.IsDead != true);

            game.DetectCollision();

            Assert.IsTrue(game.MainChar.Health == 0);
            Assert.IsTrue(game.MainChar.IsDead == true);
        }

        // Check for a correct update of position.
        [TestMethod]
        public void UpdatePos_MainChar_NoException()
        {
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(0.95f, 3.95f, 8));
            Urho.Vector3 pos = new Urho.Vector3(1, 2, 3);
            MainChar.UpdatePos(pos);

            Assert.IsTrue(MainChar.Position.X == 1);
            Assert.IsTrue(MainChar.Position.Y == 2);
            Assert.IsTrue(MainChar.Position.Z == 3);

            pos = new Urho.Vector3(0, 0, 0);
            MainChar.UpdatePos(pos);

            Assert.IsTrue(MainChar.Position.X == 0);
            Assert.IsTrue(MainChar.Position.Y == 0);
            Assert.IsTrue(MainChar.Position.Z == 0);
        }

        // Check for a correct update of Enemy position
        [TestMethod]
        public void UpdatePos_Enemies_NoException()
        {
            Enemy enemy = new Enemy(new Urho.Vector3(0.95f, 3.95f, 8));
            Urho.Vector3 pos = new Urho.Vector3(1, 2, 3);
            enemy.UpdatePos(pos);

            Assert.IsTrue(enemy.Position.X == 1);
            Assert.IsTrue(enemy.Position.Y == 2);
            Assert.IsTrue(enemy.Position.Z == 3);

            pos = new Urho.Vector3(0, 0, 0);
            enemy.UpdatePos(pos);

            Assert.IsTrue(enemy.Position.X == 0);
            Assert.IsTrue(enemy.Position.Y == 0);
            Assert.IsTrue(enemy.Position.Z == 0);
        }
    }
}
