using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using ProdinnerWebForms.Model;

namespace ProdinnerWebForms.Tests
{
     [TestFixture]
     public class DatabaseTest
    {
        [Test] public void TestInsert()
        {
            Chef chef = new Chef();
            chef.FirstName = "Gil";
            chef.LastName = "shinar";

            Db.Insert(chef);

            Chef chef2 = Db.Get<Chef>(chef.Id);

            Assert.AreEqual(chef, chef2);
        }
        [Test]
        public void TestUpdate()
        {
            Chef chef = new Chef();
            chef.FirstName = "Haggai";
            chef.LastName = "Zagury";

            Db.Insert(chef);

            Chef haggai = Db.Get<Chef>(chef.Id);
            haggai.LastName = "Philip Zagury";
            Db.Update(haggai);
            Assert.AreEqual("Philip Zagury", haggai.LastName);
        }
    }
}
