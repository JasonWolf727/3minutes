using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Urho;



namespace CC_X.Model
{
    [TestClass]
    public class GameObjTest
    {
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

        [TestMethod]
        public void DetectCollision_zombieMainCharClosePos_Collision()
        {
            Enemy zombie = new Enemy(new Urho.Vector3(1, 2, 3));
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(0.96f, 4, 3));
            GameController game = new GameController(Difficulty.Easy);
            game.MainChar = MainChar;

            game.GameObjCollection[0] = zombie;

            Assert.IsTrue((bool)(game.DetectCollision()[0]) == true);
        }

        [TestMethod]
        public void DetectCollision_zombieMainCharEqualPosX_Collision()
        {
            Enemy zombie = new Enemy(new Urho.Vector3(1, 2, 8));
            MainCharacter MainChar = new MainCharacter(new Urho.Vector3(1, 4, 8));
            GameController game = new GameController(Difficulty.Easy);
            game.MainChar = MainChar;
            
            game.GameObjCollection[0] = zombie;

            Assert.IsTrue((bool)(game.DetectCollision()[0]) == true);
        }

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
