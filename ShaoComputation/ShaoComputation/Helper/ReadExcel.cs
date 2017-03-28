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
        /// <summary>
        /// OD交通需求
        /// </summary>
        /// <param name="fullUri"></param>
        /// <returns></returns>
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
                            od.Q_rs = Convert.ToDouble(value);
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

        /// <summary>
        /// 路段信息
        /// </summary>
        /// <param name="fullUri"></param>
        /// <returns></returns>
        public static List<LuDuan> LuDuan(String fullUri)
        {
            var LuDuans = new List<LuDuan>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fullUri);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[2];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            Parallel.For(3, rowCount + 1, (i) =>
            {
                var luduan = new LuDuan();
                for (int j = 1; j <= colCount; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {

                        switch (j)
                        {
                            case 1:
                                var value1 = xlRange.Cells[i, j].Value2.ToString();
                                luduan.No = Convert.ToInt32(value1);
                                break;
                            case 2:
                                var value2 = xlRange.Cells[i, j].Value2.ToString();
                                luduan.N = Convert.ToDouble(value2);
                                break;
                            case 3:
                                var value3 = xlRange.Cells[i, j].Value2.ToString();
                                luduan.C = Convert.ToInt32(value3);
                                break;
                            case 4:
                                var value4 = xlRange.Cells[i, j].Value2.ToString();
                                luduan.tc0 = Convert.ToDouble(value4);
                                break;
                            case 5:
                                var value5 = xlRange.Cells[i, j].Value2.ToString();
                                luduan.tb0 = Convert.ToDouble(value5);
                                break;
                            case 6:
                                var value6 = xlRange.Cells[i, j].Value2.ToString();
                                luduan.Lambda = Convert.ToInt32(value6);
                                break;
                            case 7:
                                var value7 = xlRange.Cells[i, j].Value2.ToString();
                                luduan.Yita = Convert.ToInt32(value7);
                                break;
                            case 8:
                                var value8 = xlRange.Cells[i, j].Value2.ToString();
                                luduan.F = Convert.ToInt32(value8);
                                if (luduan.F == 0)
                                {
                                    luduan.F = Randam.F;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                LuDuans.Add(luduan);
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
            return LuDuans;
        }

        public static List<LuDuan> LuduanAndPoint(List<LuDuan> luDuans, String fullUri)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fullUri);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[4];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            for (int i = 2; i < rowCount + 1; i++)
            {
                for (int j = 2; j <= colCount; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        var value = xlRange.Cells[i, j].Value2.ToString();
                        var value2 = Convert.ToInt32(value);
                        if (value2 != 0)
                        {
                            luDuans.FirstOrDefault(l => l.No == value2).start = i - 1;
                            luDuans.FirstOrDefault(l => l.No == value2).end = j - 1;
                        }
                    }
                }
            }
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
            return luDuans;
        }

        public static List<Node> Nodes(String fullUri)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fullUri);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[4];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            var nodes = new List<Node>();
            for (int i = 1; i < 17; i++)
            {
                nodes.Add(new Node
                {
                    Neighbor = new List<Node>(),
                    NextUsed = new List<Node>(),
                    No = i
                });
            }

            for (int i = 2; i < rowCount + 1; i++)
            {
                for (int j = 2; j <= colCount; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        var value = xlRange.Cells[i, j].Value2.ToString();
                        var value2 = Convert.ToInt32(value);
                        if (value2 != 0)
                        {
                            nodes.NumOf(i - 1).Neighbor.Add(nodes.NumOf(j - 1));
                        }
                    }
                }
            }
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
            return nodes;
        }
    }
}
