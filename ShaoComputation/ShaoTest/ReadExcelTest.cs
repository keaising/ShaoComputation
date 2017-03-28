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
            result = result.OrderBy(od => od.start).ThenBy(od => od.end).ToList();

            Assert.AreEqual(16, result.Count);
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
    }
}
