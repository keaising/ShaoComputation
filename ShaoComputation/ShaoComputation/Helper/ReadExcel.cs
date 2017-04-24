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
using System.Windows.Forms;
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
            fullUri = String.Format($"{Environment.CurrentDirectory}\\Data\\0-sheet1.json");
            String reader = File.ReadAllText(fullUri);
            var result = JArray.Parse(reader).Children().ToList();
            foreach (var item in result)
            {
                var city = JsonConvert.DeserializeObject<OD>(item.ToString());
                ODs.Add(city);
            }
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
            var luduans = new List<LuDuan>();
            fullUri = String.Format($"{Environment.CurrentDirectory}\\Data\\0-sheet2.json");
            String reader = File.ReadAllText(fullUri);
            var result = JArray.Parse(reader).Children().ToList();
            foreach (var item in result)
            {
                var ld = JsonConvert.DeserializeObject<LuDuan>(item.ToString());
                luduans.Add(ld);
            }
            return luduans;
        }

        /// <summary>
        /// 读取参数，只有配流还需要用
        /// </summary>
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
                            var value1 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Mu = Convert.ToInt32(value1);
                            break;
                        case 2:
                            var value2 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Alpha_b = Convert.ToDouble(value2);
                            break;
                        case 3:
                            var value3 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Alpha_c = Convert.ToDouble(value3);
                            break;
                        case 4:
                            var value4 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Beta_b = Convert.ToDouble(value4);
                            break;
                        case 5:
                            var value5 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Beta_c = Convert.ToDouble(value5);
                            break;
                        case 6:
                            var value6 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Gamma_b = Convert.ToDouble(value6);
                            break;
                        case 7:
                            var value7 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Gamma_c = Convert.ToDouble(value7);
                            break;
                        case 8:
                            var value8 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Gamma_a = Convert.ToDouble(value8);
                            break;
                        case 9:
                            var value9 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Gamma_m = Convert.ToDouble(value9);
                            break;
                        case 10:
                            var value10 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Gamma_tc = Convert.ToDouble(value10);
                            break;
                        case 11:
                            var value11 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Gamma_tb = Convert.ToDouble(value11);
                            break;
                        case 12:
                            var value12 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Bc = Convert.ToDouble(value12);
                            break;
                        case 13:
                            var value13 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Bb = Convert.ToInt32(value13);
                            break;
                        case 14:
                            var value14 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Money = Convert.ToDouble(value14);
                            break;
                        case 15:
                            var value15 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.MaxValue = Convert.ToDouble(value15);
                            break;
                        case 16:
                            var value16 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.F_low = Convert.ToInt32(value16);
                            break;
                        case 17:
                            var value17 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.F_up = Convert.ToInt32(value17);
                            break;
                        case 18:
                            var value18 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Count = Convert.ToInt32(value18);
                            break;
                        case 19:
                            var value19 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Theta = Convert.ToDouble(value19);
                            break;
                        case 20:
                            var value20 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Phy = Convert.ToDouble(value20);
                            break;
                        case 21:
                            var value21 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.M = Convert.ToInt32(value21);
                            break;
                        case 22:
                            var value22 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Pc = Convert.ToDouble(value22);
                            break;
                        case 23:
                            var value23 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Pm = Convert.ToDouble(value23);
                            break;
                        case 24:
                            var value24 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.T = Convert.ToInt32(value24);
                            break;
                        case 25:
                            var value25 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.Tmax = Convert.ToInt32(value25);
                            break;
                        case 26:
                            var value26 = xlRange.Cells[2, j].Value2.ToString();
                            Varias.LuJingCount = Convert.ToInt32(value26);
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
            fullUri = String.Format($"{Environment.CurrentDirectory}\\Data\\0-sheet4.json");
            String reader = File.ReadAllText(fullUri);
            var result = JArray.Parse(reader).Children().ToList();
            foreach (var item in result)
            {
                var ld = JsonConvert.DeserializeObject<LuduanLite>(item.ToString());
                luDuans.FirstOrDefault(l => l.No == ld.No).start = ld.Start;
                luDuans.FirstOrDefault(l => l.No == ld.No).end = ld.End;
            }
            return luDuans;
        }

        public static List<Node> Nodes(String fullUri)
        {
            var nodes = new List<Node>();
            fullUri = String.Format($"{Environment.CurrentDirectory}\\Data\\0-sheet4.json");
            String reader = File.ReadAllText(fullUri);
            var result = JArray.Parse(reader).Children().ToList();
            var nodesCount = 0;
            foreach (var item in result)
            {
                var ld = JsonConvert.DeserializeObject<LuduanLite>(item.ToString());
                nodesCount = ld.Start > nodesCount ? ld.Start : ld.End > nodesCount ? ld.End : nodesCount;
            }
            for (int i = 1; i <= nodesCount; i++)
            {
                nodes.Add(new Node
                {
                    Neighbor = new List<Node>(),
                    NextUsed = new List<Node>(),
                    No = i
                });
            }
            foreach (var item in result)
            {
                var ld = JsonConvert.DeserializeObject<LuduanLite>(item.ToString());
                nodes.NumOf(ld.Start).Neighbor.Add(nodes.NumOf(ld.End));
            }
            return nodes;
        }

        public static void Excel2Json(string fullUri)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fullUri);
            #region sheet1
            var ODs = new List<OD>();
            Excel._Worksheet sheet1 = xlWorkbook.Sheets[1];
            Excel.Range range1 = sheet1.UsedRange;
            int rowCount1 = range1.Rows.Count;
            int colCount1 = range1.Columns.Count;
            for (int i = 2; i <= rowCount1; i++)
            {
                for (int j = 2; j <= colCount1; j++)
                {
                    if (range1.Cells[i, j] != null && range1.Cells[i, j].Value2 != null)
                    {
                        var od = new OD()
                        {
                            Start = i - 1,
                            End = j - 1
                        };
                        var value = range1.Cells[i, j].Value2.ToString();
                        od.Q_rs = Convert.ToDouble(value);
                        ODs.Add(od);
                    }
                }
            }
            var newfile1 = string.Format($"{Environment.CurrentDirectory}\\Data\\0-sheet1.json");
            using (StreamWriter file = new StreamWriter(newfile1, false))
            {
                ODs = ODs.OrderBy(c => c.No).ToList();
                var json = JsonConvert.SerializeObject(ODs);
                file.WriteLine(json);
            }
            #endregion
            #region sheet2
            Excel._Worksheet sheet2 = xlWorkbook.Sheets[2];
            Excel.Range range2 = sheet2.UsedRange;
            int rowCount2 = range2.Rows.Count;
            int colCount2 = range2.Columns.Count;
            var LuDuans = new List<LuDuan>();
            Parallel.For(3, rowCount2 + 1, (i) =>
            {
                var luduan = new LuDuan();
                for (int j = 1; j <= colCount2; j++)
                {
                    if (range2.Cells[i, j] != null && range2.Cells[i, j].Value2 != null)
                    {

                        switch (j)
                        {
                            case 1:
                                var value1 = range2.Cells[i, j].Value2.ToString();
                                luduan.No = Convert.ToInt32(value1);
                                break;
                            case 2:
                                var value2 = range2.Cells[i, j].Value2.ToString();
                                luduan.N = Convert.ToDouble(value2);
                                break;
                            case 3:
                                var value3 = range2.Cells[i, j].Value2.ToString();
                                luduan.C = Convert.ToInt32(value3);
                                break;
                            case 4:
                                var value4 = range2.Cells[i, j].Value2.ToString();
                                luduan.tc0 = Convert.ToDouble(value4);
                                //luduan.ltc = Convert.ToDouble(value4);
                                break;
                            case 5:
                                var value5 = range2.Cells[i, j].Value2.ToString();
                                luduan.tb0 = Convert.ToDouble(value5);
                                //luduan.ltb = Convert.ToDouble(value5);
                                break;
                            case 6:
                                var value6 = range2.Cells[i, j].Value2.ToString();
                                luduan.Lambda = Convert.ToInt32(value6);
                                break;
                            case 7:
                                var value7 = range2.Cells[i, j].Value2.ToString();
                                luduan.Yita = Convert.ToInt32(value7);
                                break;
                            case 8:
                                var value8 = range2.Cells[i, j].Value2.ToString();
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
            var newfile2 = string.Format($"{Environment.CurrentDirectory}\\Data\\0-sheet2.json");
            using (StreamWriter file = new StreamWriter(newfile2, false))
            {
                LuDuans = LuDuans.OrderBy(c => c.No).ToList();
                var json = JsonConvert.SerializeObject(LuDuans);
                file.WriteLine(json);
            }

            #endregion
            #region sheet3
            Excel._Worksheet sheet3 = xlWorkbook.Sheets[3];
            Excel.Range range3 = sheet3.UsedRange;
            int colCount3 = range3.Columns.Count;
            for (int j = 1; j <= colCount3; j++)
            {
                if (range3.Cells[1, j] != null && range3.Cells[1, j].Value2 != null)
                {
                    switch (j)
                    {
                        case 1:
                            var value1 = range3.Cells[2, j].Value2.ToString();
                            Varias.Mu = Convert.ToInt32(value1);
                            break;
                        case 2:
                            var value2 = range3.Cells[2, j].Value2.ToString();
                            Varias.Alpha_b = Convert.ToDouble(value2);
                            break;
                        case 3:
                            var value3 = range3.Cells[2, j].Value2.ToString();
                            Varias.Alpha_c = Convert.ToDouble(value3);
                            break;
                        case 4:
                            var value4 = range3.Cells[2, j].Value2.ToString();
                            Varias.Beta_b = Convert.ToDouble(value4);
                            break;
                        case 5:
                            var value5 = range3.Cells[2, j].Value2.ToString();
                            Varias.Beta_c = Convert.ToDouble(value5);
                            break;
                        case 6:
                            var value6 = range3.Cells[2, j].Value2.ToString();
                            Varias.Gamma_b = Convert.ToDouble(value6);
                            break;
                        case 7:
                            var value7 = range3.Cells[2, j].Value2.ToString();
                            Varias.Gamma_c = Convert.ToDouble(value7);
                            break;
                        case 8:
                            var value8 = range3.Cells[2, j].Value2.ToString();
                            Varias.Gamma_a = Convert.ToDouble(value8);
                            break;
                        case 9:
                            var value9 = range3.Cells[2, j].Value2.ToString();
                            Varias.Gamma_m = Convert.ToDouble(value9);
                            break;
                        case 10:
                            var value10 = range3.Cells[2, j].Value2.ToString();
                            Varias.Gamma_tc = Convert.ToDouble(value10);
                            break;
                        case 11:
                            var value11 = range3.Cells[2, j].Value2.ToString();
                            Varias.Gamma_tb = Convert.ToDouble(value11);
                            break;
                        case 12:
                            var value12 = range3.Cells[2, j].Value2.ToString();
                            Varias.Bc = Convert.ToDouble(value12);
                            break;
                        case 13:
                            var value13 = range3.Cells[2, j].Value2.ToString();
                            Varias.Bb = Convert.ToInt32(value13);
                            break;
                        case 14:
                            var value14 = range3.Cells[2, j].Value2.ToString();
                            Varias.Money = Convert.ToDouble(value14);
                            break;
                        case 15:
                            var value15 = range3.Cells[2, j].Value2.ToString();
                            Varias.MaxValue = Convert.ToDouble(value15);
                            break;
                        case 16:
                            var value16 = range3.Cells[2, j].Value2.ToString();
                            Varias.F_low = Convert.ToInt32(value16);
                            break;
                        case 17:
                            var value17 = range3.Cells[2, j].Value2.ToString();
                            Varias.F_up = Convert.ToInt32(value17);
                            break;
                        case 18:
                            var value18 = range3.Cells[2, j].Value2.ToString();
                            Varias.Count = Convert.ToInt32(value18);
                            break;
                        case 19:
                            var value19 = range3.Cells[2, j].Value2.ToString();
                            Varias.Theta = Convert.ToDouble(value19);
                            break;
                        case 20:
                            var value20 = range3.Cells[2, j].Value2.ToString();
                            Varias.Phy = Convert.ToDouble(value20);
                            break;
                        case 21:
                            var value21 = range3.Cells[2, j].Value2.ToString();
                            Varias.M = Convert.ToInt32(value21);
                            break;
                        case 22:
                            var value22 = range3.Cells[2, j].Value2.ToString();
                            Varias.Pc = Convert.ToDouble(value22);
                            break;
                        case 23:
                            var value23 = range3.Cells[2, j].Value2.ToString();
                            Varias.Pm = Convert.ToDouble(value23);
                            break;
                        case 24:
                            var value24 = range3.Cells[2, j].Value2.ToString();
                            Varias.T = Convert.ToInt32(value24);
                            break;
                        case 25:
                            var value25 = range3.Cells[2, j].Value2.ToString();
                            Varias.Tmax = Convert.ToInt32(value25);
                            break;
                        case 26:
                            var value26 = range3.Cells[2, j].Value2.ToString();
                            Varias.LuJingCount = Convert.ToInt32(value26);
                            break;
                        default:
                            break;
                    }
                }
            }
            #endregion
            #region sheet4
            var luduanLites = new List<LuduanLite>();
            Excel._Worksheet sheet4 = xlWorkbook.Sheets[4];
            Excel.Range range4 = sheet4.UsedRange;
            int rowCount4 = range4.Rows.Count;
            int colCount4 = range4.Columns.Count;
            for (int i = 2; i <= rowCount4; i++)
            {
                for (int j = 2; j <= colCount4; j++)
                {
                    if (range4.Cells[i, j] != null && range4.Cells[i, j].Value2 != null)
                    {
                        var value = range4.Cells[i, j].Value2.ToString();
                        var value2 = Convert.ToInt32(value);
                        if (value2 != 0)
                        {
                            luduanLites.Add(new LuduanLite
                            {
                                Start = i - 1,
                                End = j - 1,
                                No = value2
                            });
                        }
                    }
                }
            }
            var newfile4 = string.Format($"{Environment.CurrentDirectory}\\Data\\0-sheet4.json");
            using (StreamWriter file = new StreamWriter(newfile4, false))
            {
                luduanLites = luduanLites.OrderBy(c => c.No).ToList();
                var json = JsonConvert.SerializeObject(luduanLites);
                file.WriteLine(json);
            }
            #endregion
            #region release
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(range1);
            Marshal.ReleaseComObject(range2);
            Marshal.ReleaseComObject(range3);
            Marshal.ReleaseComObject(range4);
            Marshal.ReleaseComObject(sheet1);
            Marshal.ReleaseComObject(sheet2);
            Marshal.ReleaseComObject(sheet3);
            Marshal.ReleaseComObject(sheet4);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            #endregion
        }
    }
}
