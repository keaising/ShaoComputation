
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
    public class Run
    {
        [TestMethod]
        public void R()
        {
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var result = ReadExcel.LuDuan(fullUri);
            result = result.OrderBy(l => l.No).ToList();
            var luduans = ReadExcel.LuduanAndPoint(result, fullUri);
            var nodes = ReadExcel.Nodes(fullUri);
            var ods = ReadExcel.OD(fullUri);
            foreach (var od in ods)
            {
                od.LuJings = GenarateLuJing.GetAllPath(od, luduans, nodes);
            }
            Iteration.Run(ods, luduans, nodes);
        }
    }
}
