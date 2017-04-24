/*
 * File: SerializationTest.cs
 * Author: Carlos Santana
 * Desc: Contains unit tests for the (De)Serialization logic.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CC_X.Model
{
    // Class for the (De)Serialization Unit Tests
    [TestClass]
    public class SerializationTest
    {

        // Tests the power of MainCharacter's Serializer method
        [TestMethod]
        public void MainCharSerializer()
        {
            Urho.Vector3 pos = new Urho.Vector3(1, 2, 4); // Position for use in the instance of MainCharacter
            MainCharacter chara = new MainCharacter(pos); // Instance of MainCharacter for use in the Unit Test
            chara.Position = pos;
            chara.ID = 42;
            chara.SelectedCharType = MainCharacter.MainCharOptn.Mutant;
            chara.Strength = 64;
            chara.Health = 90;
            chara.TimeSinceLastCollide = 1337;
            chara.IsDead = false;
            chara.persnlBubble = new System.Drawing.Rectangle(1, 2, 3, 4); // Set the instance's persnlBubble
            string tempInfo = chara.Serialize();
            Assert.IsTrue(tempInfo == "1, 2, 4, 42, Mutant, 64, 90, 1337, False, 1, 2, 3, 4");
        }

        // Tests the power of MainCharacter's DeSerializer method
        [TestMethod]
        public void MainCharDeSerializer()
        {
            string tempInfo = "1, 2, 4, 42, Mutant, 64, 90, 1337, False, 1, 2, 3, 4";
            Urho.Vector3 pos = new Urho.Vector3(1, 2, 4); // Position for use in the instance of MainCharacter
            MainCharacter chara = new MainCharacter(pos); // Instance of MainCharacter for use in the Unit Test
            chara.DeSerialize(tempInfo);
            Assert.IsTrue(chara.Position == pos);
            Assert.IsTrue(chara.ID == 42);
            Assert.IsTrue(chara.SelectedCharType == MainCharacter.MainCharOptn.Mutant);
            Assert.IsTrue(chara.Strength == 64);
            Assert.IsTrue(chara.Health == 90);
            Assert.IsTrue(chara.TimeSinceLastCollide == 1337);
            Assert.IsTrue(chara.IsDead == false);
            System.Drawing.Rectangle space = new System.Drawing.Rectangle(1, 2, 3, 4); // Set the instance's persnlBubble
            Assert.IsTrue(chara.persnlBubble == space);
        }

        // Tests the power of Nature's Serializer method
        [TestMethod]
        public void NatureSerializer()
        {
            Urho.Vector3 pos = new Urho.Vector3(5, 12, 13); // Position for use in the instance of Nature
            Nature flowey = new Nature(Nature.NatureType.Grass, pos); // Instance of Nature for use in the Unit Test
            flowey.SelectedNatureType = Nature.NatureType.Grass;
            flowey.Position = pos;
            flowey.ID = 24;
            string tempInfo = flowey.Serialize();
            Assert.IsTrue(tempInfo == "5, 12, 13, 24, Grass");
        }

        // // Tests the power of Nature's DeSerializer method
        [TestMethod]
        public void NatureDeSerializer()
        {
            string tempInfo = "5, 12, 13, 24, Grass";
            Urho.Vector3 pos = new Urho.Vector3(5, 12, 13); // Position for use in the instance of Nature
            Nature flowey = new Nature(Nature.NatureType.Grass, pos); // Instance of Nature for use in the Unit Test
            flowey.DeSerialize(tempInfo);
            Assert.IsTrue(flowey.Position == pos);
            Assert.IsTrue(flowey.ID == 24);
            Assert.IsTrue(flowey.SelectedNatureType == Nature.NatureType.Grass);
        }

        // Tests the power of Enemy's Serializer method
        [TestMethod]
        public void EnemySerializer()
        {
            Urho.Vector3 pos = new Urho.Vector3(3, 4, 5); // Position for use in the instance of Enemy
            Enemy froggit = new Enemy(pos); // Instance of Enemy for use in the Unit Test
            froggit.Position = pos;
            froggit.ID = 65;
            froggit.ObjType = Enemy.EnemyType.Car;
            froggit.Strength = 2;
            froggit.Health = 30;
            froggit.IsDead = false;
            froggit.persnlBubble = new System.Drawing.Rectangle(2, 5, 9, 7); // Set the instance's persnlBubble
            froggit.CarMovingDirection = Enemy.CarDir.Right;
            string tempInfo = froggit.Serialize();
            Assert.IsTrue(tempInfo == "3, 4, 5, 65, Car, 2, 30, False, 2, 5, 9, 7, Right");
        }

        // Tests the power of Enemy's DeSerializer method
        [TestMethod]
        public void EnemyDeSerializer()
        {
            string tempInfo = "3, 4, 5, 65, Car, 2, 30, False, 2, 5, 9, 7, Right";
            Urho.Vector3 pos = new Urho.Vector3(3, 4, 5); // Position for use in the instance of Enemy
            System.Drawing.Rectangle space = new System.Drawing.Rectangle(2, 5, 9, 7); // Set the instance's persnlBubble
            Enemy froggit = new Enemy(pos); // Instance of Enemy for use in the Unit Test
            froggit.DeSerialize(tempInfo);
            Assert.IsTrue(froggit.Position == pos);
            Assert.IsTrue(froggit.ID == 65);
            Assert.IsTrue(froggit.ObjType == Enemy.EnemyType.Car);
            Assert.IsTrue(froggit.Strength == 2);
            Assert.IsTrue(froggit.Health == 30);
            Assert.IsTrue(froggit.IsDead == false);
            Assert.IsTrue(froggit.persnlBubble == space);
            Assert.IsTrue(froggit.CarMovingDirection == Enemy.CarDir.Right);
        }
    }
}
