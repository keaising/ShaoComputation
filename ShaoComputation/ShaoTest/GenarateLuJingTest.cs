using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShaoComputation.Computation;
using ShaoComputation.Helper;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoTest
{
    [TestClass]
    public class GenarateLuJingTest
    {
        [TestMethod]
        [TestCategory("GenarateLuJing")]
        public void OutPutLuJing()
        {
            //var stack = new Stack<int> (new List<int> { 1, 2, 3, 4, 5, 6 });

            //var lujing = GenarateLuJing.OutPutLuJing(stack);

            //Assert.AreEqual(1, lujing.start);
            //Assert.AreEqual(6, lujing.end);
            //Assert.AreEqual(2, lujing.Points[1]);
        }

        [TestMethod]
        [TestCategory("GenarateLuJing")]
        public void GetAllPath()
        {
            var od = new OD();
            od.start = 8;
            od.end = 14;


            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var result = ReadExcel.LuDuan(fullUri);
            result = result.OrderBy(l => l.No).ToList();
            var luduans = ReadExcel.LuduanAndPoint(result, fullUri);
            var nodes = ReadExcel.Nodes(fullUri);

            var result2 = GenarateLuJing.GetAllPath(od, luduans, nodes);
            Assert.AreEqual(1, 1);
        }


    }
}
