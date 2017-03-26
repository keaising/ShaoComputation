using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public static List<City> FromExcel(String fullUri)
        {
            var cities = new List<City>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fullUri);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            #region mock
            //rowCount = 40;
            //colCount = 40;
            #endregion


            Parallel.For(2, rowCount + 1, (i) =>
            {
                var city = new City();
                for (int j = 1; j <= colCount; j++)
                {
                    if (j == 1)
                    {
                        city.Name = xlRange.Cells[i, j].Value2.ToString();
                        city.No = i - 1;
                    }
                    else
                    {
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        {
                            var value = xlRange.Cells[i, j].Value2.ToString();
                            var value2 = Convert.ToDouble(value);
                            city.Distances.Add(j - 1, value2);
                        }

                    }
                }
                cities.Add(city);
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
            return cities;
        }

        public static List<City> FromJson(String fullUri)
        {
            var cities = new List<City>();
            String reader = File.ReadAllText(fullUri);
            var result = JArray.Parse(reader).Children().ToList();
            foreach (var item in result)
            {
                var city = JsonConvert.DeserializeObject<City>(item.ToString());
                cities.Add(city);
            }
            return cities;
        }
    }
}
