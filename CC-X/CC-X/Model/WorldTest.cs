using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC_X.Model
{
    [TestClass]
    public class WorldTest
    {
        [TestMethod]
        public void DetectCollision_zombieMainChar_NoCollision()
        {
            GameObj zombie = new Enemy();
            zombie.Position = new Urho.Vector3(1, 2, 1);
            GameObj MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(5, 4, 1);

            Dictionary<int, GameObj> world = new Dictionary<int, GameObj>();
            world[1] = zombie;

            Assert.IsFalse(MainChar.DetectCollision(world) == false);
        }

        [TestMethod]
        public void DetectCollision_zombieMainCharClosePos_Collision()
        {
            GameObj zombie = new Enemy();
            zombie.Position = new Urho.Vector3(1, 2, 3);
            GameObj MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(0.95f, 4, 3);

            Dictionary<int, GameObj> world = new Dictionary<int, GameObj>();
            world[1] = zombie;

            Assert.IsTrue(MainChar.DetectCollision(world) == true);
        }

        [TestMethod]
        public void DetectCollision_zombieMainCharEqualPosX_Collision()
        {
            GameObj zombie = new Enemy();
            zombie.Position = new Urho.Vector3(1, 2, 8);
            GameObj MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(1, 4, 8);

            Dictionary<int, GameObj> world = new Dictionary<int, GameObj>();
            world[1] = zombie;

            Assert.IsTrue(MainChar.DetectCollision(world) == true);
        }

        [TestMethod]
        public void DetectCollision_zombieMainCharEqualPosY_Collision()
        {
            GameObj zombie = new Enemy();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            GameObj MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(3, 4, 8);

            Dictionary<int, GameObj> world = new Dictionary<int, GameObj>();
            world[1] = zombie;

            Assert.IsTrue(MainChar.DetectCollision(world) == true);
        }

        [TestMethod]
        public void DetectCollision_AssessDamage_DamageTaken()
        {
            GameObj zombie = new Enemy();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            zombie.Damage = 20;
            GameObj MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(3, 4, 8);
            MainChar.Health = 100;

            Dictionary<int, GameObj> world = new Dictionary<int, GameObj>();
            world[1] = zombie;

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 80);
        }
        [TestMethod]
        public void DetectCollision_AssessDamage_NoDamageTaken()
        {
            GameObj zombie = new Enemy();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            zombie.Damage = 20;
            GameObj MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(3, 2, 8);
            MainChar.Health = 100;

            Dictionary<int, GameObj> world = new Dictionary<int, GameObj>();
            world[1] = zombie;

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 100);
        }
        [TestMethod]
        public void DetectCollision_AssessDamageClosePos_DamageTaken()
        {
            GameObj zombie = new Enemy();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            zombie.Damage = 20;
            GameObj MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(0.95f, 3.95f, 8);
            MainChar.Health = 100;

            Dictionary<int, GameObj> world = new Dictionary<int, GameObj>();
            world[1] = zombie;

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 80);
        }
        [TestMethod]
        public void DetectCollision_AssessDamageAndDeath_NoException()
        {
            GameObj zombie = new Enemy();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            zombie.Damage = 20;
            GameObj MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(3, 4, 8);
            MainChar.Health = 100;

            Dictionary<int, GameObj> world = new Dictionary<int, GameObj>();
            world[1] = zombie;

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 80);
            Assert.IsFalse(MainChar.IsDead == false);

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 60);
            Assert.IsFalse(MainChar.IsDead == false);

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 40);
            Assert.IsFalse(MainChar.IsDead == false);

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 20);
            Assert.IsFalse(MainChar.IsDead == false);

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 0);
            Assert.IsTrue(MainChar.IsDead == true);
        }
        [TestMethod]
        public void UpdatePos_MainChar_NoException()
        {            
            GameObj MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(0.95f, 3.95f, 8);
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
            GameObj enemy = new Enemy();
            enemy.Position = new Urho.Vector3(0.95f, 3.95f, 8);
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
