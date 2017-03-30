using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShaoComputation.Const;
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

            for (int i = 2; i <= rowCount; i++)
            {
                for (int j = 2; j <= colCount; j++)
                {
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        var od = new OD();
                        od.start = i - 1;
                        od.end = j - 1;
                        var value = xlRange.Cells[i, j].Value2.ToString();
                        od.Q_rs = Convert.ToDouble(value);
                        ODs.Add(od);
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


        public static List<LuDuan> Varia(String fullUri)
        {
            var LuDuans = new List<LuDuan>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fullUri);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[3];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int colCount = xlRange.Columns.Count;

            for (int j = 1; j <= colCount; j++)
            {
                if (xlRange.Cells[1, j] != null && xlRange.Cells[1, j].Value2 != null)
                {
                    switch (j)
                    {
                        case 1:
                            var value1 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.mu = Convert.ToInt32(value1);
                            break;
                        case 2:
                            var value2 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.alpha_b = Convert.ToDouble(value2);
                            break;
                        case 3:
                            var value3 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.alpha_c = Convert.ToDouble(value3);
                            break;
                        case 4:
                            var value4 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.beta_b = Convert.ToDouble(value4);
                            break;
                        case 5:
                            var value5 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.beta_c = Convert.ToDouble(value5);
                            break;
                        case 6:
                            var value6 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.gamma_b = Convert.ToDouble(value6);
                            break;
                        case 7:
                            var value7 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.gamma_c = Convert.ToDouble(value7);
                            break;
                        case 8:
                            var value8 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.gamma_a = Convert.ToDouble(value8);
                            break;
                        case 9:
                            var value9 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.gamma_m = Convert.ToDouble(value9);
                            break;
                        case 10:
                            var value10 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.gamma_tc = Convert.ToDouble(value10);
                            break;
                        case 11:
                            var value11 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.gamma_tb = Convert.ToDouble(value11);
                            break;
                        case 12:
                            var value12 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.Bc = Convert.ToDouble(value12);
                            break;
                        case 13:
                            var value13 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.Bb = Convert.ToInt32(value13);
                            break;
                        case 14:
                            var value14 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.money = Convert.ToDouble(value14);
                            break;
                        case 15:
                            var value15 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.MaxValue = Convert.ToDouble(value15);
                            break;
                        case 16:
                            var value16 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.F_low = Convert.ToInt32(value16);
                            break;
                        case 17:
                            var value17 = xlRange.Cells[1, j].Value2.ToString();
                            Varias.F_up = Convert.ToInt32(value17);
                            break;
                        default:
                            break;
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
