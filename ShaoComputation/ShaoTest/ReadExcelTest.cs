using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShaoComputation;
using ShaoComputation.Const;
using ShaoComputation.Helper;
using ShaoComputation.Model;
using System.Linq;

namespace ShaoTest
{
    [TestClass]
    public class ReadExcelTest
    {
        [TestMethod]
        [TestCategory("ReadExcel")]
        public void ODs()
        {
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var result = ReadExcel.OD(fullUri);
            result = result.OrderBy(od => od.Start).ThenBy(od => od.End).ToList();

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        [TestCategory("ReadExcel")]
        public void LuDuans()
        {
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var result = ReadExcel.LuDuan(fullUri);
            result = result.OrderBy(od => od.No).ToList();

            Assert.AreEqual(50, result.Count);
        }

        [TestMethod]
        [TestCategory("ReadExcel")]
        public void LuduanAndPoint()
        {
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var result = ReadExcel.LuDuan(fullUri);
            result = result.OrderBy(od => od.No).ToList();
            var result2 = ReadExcel.LuduanAndPoint(result, fullUri);

            Assert.AreEqual(50, result.Count);
        }

        [TestMethod]
        [TestCategory("ReadExcel")]
        public void Nodes()
        {
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var result = ReadExcel.Nodes(fullUri);
            result = result.OrderBy(od => od.No).ToList();

            Assert.AreEqual(50, result.Count);
        }


        [TestMethod]
        public void Excel2JsonTest()
        {
            var fullUri = String.Format($"{Environment.CurrentDirectory}\\OD.xlsx");

            ReadExcel.Excel2Json(fullUri);
        }
    }
}
