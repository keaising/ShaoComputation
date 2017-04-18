using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoTest
{
    public class Person
    {
        public string Name { get; set; }
        public int No { get; set; }
    }
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void Method()
        {
            var persons = new List<Person>();
            for (int i = 0; i < 3; i++)
            {
                persons.Add(new Person
                {
                    Name = "Person" + i,
                    No = i
                });
            }
            var person2 = new List<Person>();
            foreach (var person in persons)
            {
                person2.Add(new Person
                {
                    Name = person.Name,
                    No = person.No
                });
            }
            person2[0].Name = "wsx";

            var int1 = new List<int> { 1, 2, 3, 4, 5 };
            var int2 = new List<int>(int1);

            int2[0] = 10;
            //Assert.AreEqual(int1[0], 1);
            Assert.AreEqual("Person0", persons[0].Name);
        }
    }

}
