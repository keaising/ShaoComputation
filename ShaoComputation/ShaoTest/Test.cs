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
    public class Test
    {
        [TestMethod]
        [TestCategory("ReadExcel")]
        public void ODs()
        {
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var result = ReadExcel.OD(fullUri);
            result = result.OrderBy(od => od.start).ThenBy(od => od.end).ToList();
        }
    }
}
