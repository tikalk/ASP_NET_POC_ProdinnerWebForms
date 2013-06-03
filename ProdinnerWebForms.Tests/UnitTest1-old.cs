using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProdinnerWebForms.Model;

namespace ProdinnerWebForms.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Chef chef = new Chef();
            chef.FirstName = "Gil";
            chef.LastName = "shinar";

            Db.Insert(chef);

            Chef chef2 = Db.Get<Chef>(chef.Id);

            Assert.AreEqual<Chef>(chef, chef2);
        }
    }
}
