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
            World zombie = new Enemies();
            zombie.Position = new Urho.Vector3(1, 2, 1);
            World MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(5, 4, 1);

            Dictionary<int, World> world = new Dictionary<int, World>();
            world[1] = zombie;

            Assert.IsFalse(MainChar.DetectCollision(world) == false);
        }

        [TestMethod]
        public void DetectCollision_zombieMainCharClosePos_Collision()
        {
            World zombie = new Enemies();
            zombie.Position = new Urho.Vector3(1, 2, 3);
            World MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(0.95f, 4, 3);

            Dictionary<int, World> world = new Dictionary<int, World>();
            world[1] = zombie;

            Assert.IsTrue(MainChar.DetectCollision(world) == true);
        }

        [TestMethod]
        public void DetectCollision_zombieMainCharEqualPosX_Collision()
        {
            World zombie = new Enemies();
            zombie.Position = new Urho.Vector3(1, 2, 8);
            World MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(1, 4, 8);

            Dictionary<int, World> world = new Dictionary<int, World>();
            world[1] = zombie;

            Assert.IsTrue(MainChar.DetectCollision(world) == true);
        }

        [TestMethod]
        public void DetectCollision_zombieMainCharEqualPosY_Collision()
        {
            World zombie = new Enemies();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            World MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(3, 4, 8);

            Dictionary<int, World> world = new Dictionary<int, World>();
            world[1] = zombie;

            Assert.IsTrue(MainChar.DetectCollision(world) == true);
        }

        [TestMethod]
        public void DetectCollision_AssessDamage_DamageTaken()
        {
            World zombie = new Enemies();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            zombie.Damage = 20;
            World MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(3, 4, 8);
            MainChar.Health = 100;

            Dictionary<int, World> world = new Dictionary<int, World>();
            world[1] = zombie;

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 80);
        }
        [TestMethod]
        public void DetectCollision_AssessDamage_NoDamageTaken()
        {
            World zombie = new Enemies();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            zombie.Damage = 20;
            World MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(3, 2, 8);
            MainChar.Health = 100;

            Dictionary<int, World> world = new Dictionary<int, World>();
            world[1] = zombie;

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 100);
        }
        [TestMethod]
        public void DetectCollision_AssessDamageClosePos_DamageTaken()
        {
            World zombie = new Enemies();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            zombie.Damage = 20;
            World MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(0.95f, 3.95f, 8);
            MainChar.Health = 100;

            Dictionary<int, World> world = new Dictionary<int, World>();
            world[1] = zombie;

            MainChar.DetectCollision(world);

            Assert.IsTrue(MainChar.Health == 80);
        }
        [TestMethod]
        public void DetectCollision_AssessDamageAndDeath_NoException()
        {
            World zombie = new Enemies();
            zombie.Position = new Urho.Vector3(1, 4, 8);
            zombie.Damage = 20;
            World MainChar = new MainCharacter();
            MainChar.Position = new Urho.Vector3(3, 4, 8);
            MainChar.Health = 100;

            Dictionary<int, World> world = new Dictionary<int, World>();
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

    }
}
