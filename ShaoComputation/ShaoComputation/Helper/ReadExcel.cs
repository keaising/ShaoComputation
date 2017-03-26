using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace ShaoComputation.Helper
{
    public class ReadExcel
    {
        public static List<OD> OD(String fullUri)
        {
            var ODs = new List<OD>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fullUri);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            Parallel.For(2, rowCount + 1, (i) =>
            {
                var od = new OD();
                for (int j = 1; j <= colCount; j++)
                {
                    if (j == 1)
                    {
                        od.start = Convert.ToInt32(xlRange.Cells[i, j].Value2.ToString());
                        od.end = i - 1;
                    }
                    else
                    {
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        {
                            var value = xlRange.Cells[i, j].Value2.ToString();
                            var value2 = Convert.ToDouble(value);
                            od.Q_rs = (double)value2;
                        }

                    }
                }
                ODs.Add(od);
            });

            #region release
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            #endregion
            return ODs;
        }

        public static List<OD> FromJson(String fullUri)
        {
            var ods = new List<OD>();
            String reader = File.ReadAllText(fullUri);
            var result = JArray.Parse(reader).Children().ToList();
            foreach (var item in result)
            {
                var city = JsonConvert.DeserializeObject<OD>(item.ToString());
                ods.Add(city);
            }
            return ods;
        }
    }
}
