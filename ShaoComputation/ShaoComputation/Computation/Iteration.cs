using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ShaoComputation.Const;
using ShaoComputation.Helper;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Computation
{
    /// <summary>
    /// 迭代
    /// </summary>
    public class Iteration
    {
        public static double Run(List<OD> ods, List<LuDuan> luduans, List<Node> nodes, String uri)
        {
            ods.initOD();
            var i = 0.0;
            var finals = new List<double>();
            while (i < Varias.Count)
            {
                i++;
                foreach (var od in ods)
                {
                    foreach (var lujing in od.LuJings)
                    {
                        foreach (var luduan in lujing.LuDuans)
                        {
                            luduans.NumOf(luduan.No).LD_fac_fab();
                            luduans.NumOf(luduan.No).LD_tac_tab();
                        }
                        lujing.Fee(luduans);
                    }
                }
                foreach (var od in ods)
                {
                    var flag1 = true;
                    var flag2 = true;
                    if (od.Q_rs_c <= od.Q_rs / (1 + Math.Pow(Math.E, Varias.Theta * (od.Ec_min - od.Eb_min - Varias.Phy)))) //判断往QRSC还是QRSB加载
                    {
                        foreach (var lujing in od.LuJings)
                        {
                            //flag保证在多个最小值的情况下最多只分配一次
                            if (lujing.ec == od.Ec_min && flag1)
                            {
                                lujing.Fpc = (1 - 1 / i) * lujing.Fpc + 1 / i * od.Q_rs;
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb;
                                flag1 = false;
                            }
                            else
                            {
                                lujing.Fpc = (1 - 1 / i) * lujing.Fpc;
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb;
                            }
                        }
                        od.Q_rs_c = od.LuJings.Sum(lj => lj.Fpc);
                        od.Q_rs_b = od.Q_rs - od.Q_rs_c;
                    }
                    else
                    {
                        foreach (var lujing in od.LuJings)
                        {
                            if (lujing.eb == od.Eb_min && flag2)
                            {
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb + 1 / i * od.Q_rs;
                                lujing.Fpc = (1 - 1 / i) * lujing.Fpc;
                                flag2 = false;
                            }
                            else
                            {
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb;
                                lujing.Fpc = (1 - 1 / i) * lujing.Fpc;
                            }
                        }
                        od.Q_rs_b = od.LuJings.Sum(lj => lj.Fpb);
                        od.Q_rs_c = od.Q_rs - od.Q_rs_b;
                    }
                }
                foreach (var od in ods)
                {
                    foreach (var lujing in od.LuJings)
                    {
                        foreach (var luduan in lujing.LuDuans)
                        {
                            luduans.NumOf(luduan.No).LD_fac_fab();
                            luduans.NumOf(luduan.No).LD_tac_tab();
                        }
                        //lujing.Fee(luduans);
                    }
                }
                var v1 = new List<double>();
                var v2 = new List<double>();
                var v3 = new List<double>();
                var v4 = new List<double>();
                var v5 = new List<double>();
                foreach (var od in ods)
                {
                    v1.Add(od.LuJings.Sum(lj => (lj.ec - od.Ec_min) * lj.Fpc));
                    v2.Add(od.Ec_min * od.Q_rs_c);
                    v3.Add(od.LuJings.Sum(lj => (lj.eb - od.Eb_min) * lj.Fpc));
                    v4.Add(od.Eb_min * od.Q_rs_b);
                    v5.Add(od.Q_rs_c - od.Q_rs / (1 + Math.Pow(Math.E, (od.Ec_min - od.Eb_min))));
                }
                //todo: 待议
                var final = v1.Sum() / v2.Sum() + v3.Sum() / v4.Sum() + Math.Sqrt(v5.Sum(v => Math.Pow(v, 2))) / ods.Sum(od => od.Q_rs);
                finals.Add(final);
            }
            if (Varias.IsGA)
            {
                var E1 = ods.Sum(od => od.Ec_min * od.Q_rs_c) + ods.Sum(od => od.Eb_min * od.Q_rs_b);
                var E2 = Varias.Gamma_tb * luduans.Sum(ld => ld.F * ld.tb) - Varias.Gamma_m * ods.Sum(od => od.Q_rs_b * Varias.Money);
                return E1 + E2;
            }
            else
            {
                #region 导出到Excel
                //OD
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet1 = workbook.CreateSheet("OD");
                IRow row0 = sheet1.CreateRow(0);
                row0.CreateCell(0).SetCellValue("Start");
                row0.CreateCell(1).SetCellValue("End");
                row0.CreateCell(2).SetCellValue("q_rs_c");
                row0.CreateCell(3).SetCellValue("q_rs_b");
                row0.CreateCell(4).SetCellValue("ec_min");
                row0.CreateCell(5).SetCellValue("eb_min");
                row0.CreateCell(6).SetCellValue("value1");
                row0.CreateCell(7).SetCellValue("value2");
                row0.CreateCell(8).SetCellValue("value3");
                row0.CreateCell(9).SetCellValue("value4");

                var rowCount = 0;
                foreach (var od in ods)
                {
                    rowCount = rowCount + 1;
                    IRow row = sheet1.CreateRow(rowCount);
                    row.CreateCell(0).SetCellValue(od.Start);
                    row.CreateCell(1).SetCellValue(od.End);
                    row.CreateCell(2).SetCellValue(od.Q_rs_c);
                    row.CreateCell(3).SetCellValue(od.Q_rs_b);
                    row.CreateCell(4).SetCellValue(od.Ec_min);
                    row.CreateCell(5).SetCellValue(od.Eb_min);
                    if (rowCount == 1)
                    {
                        var value1 = ods.Sum(o => o.Q_rs_c) / (ods.Sum(o => o.Q_rs));
                        var value2 = ods.Sum(o => o.Ec_min * o.Q_rs_c) / ods.Sum(o => o.Q_rs_c);
                        var value3 = ods.Sum(o => o.Eb_min * o.Q_rs_b) / ods.Sum(o => o.Q_rs_b);
                        var value4 = (ods.Sum(o => o.Ec_min * o.Q_rs_c) + ods.Sum(o => o.Eb_min * o.Q_rs_b)) / (ods.Sum(o => o.Q_rs));
                        row.CreateCell(6).SetCellValue(value1);
                        row.CreateCell(7).SetCellValue(value2);
                        row.CreateCell(8).SetCellValue(value3);
                        row.CreateCell(9).SetCellValue(value4);
                    }
                }
                //LuDuan
                ISheet sheet2 = workbook.CreateSheet("路段信息");
                IRow row0LuDuan = sheet2.CreateRow(0);
                row0LuDuan.CreateCell(0).SetCellValue("编号");
                row0LuDuan.CreateCell(1).SetCellValue("Start");
                row0LuDuan.CreateCell(2).SetCellValue("End");
                row0LuDuan.CreateCell(3).SetCellValue("Fac");
                row0LuDuan.CreateCell(4).SetCellValue("Fab");
                row0LuDuan.CreateCell(5).SetCellValue("Xac");
                row0LuDuan.CreateCell(6).SetCellValue("Xab");
                row0LuDuan.CreateCell(7).SetCellValue("Tac");
                row0LuDuan.CreateCell(8).SetCellValue("Tab");
                var rowCount2 = 0;
                foreach (var luduan in luduans)
                {
                    rowCount2 = rowCount2 + 1;
                    IRow row = sheet2.CreateRow(rowCount2);
                    row.CreateCell(0).SetCellValue(luduan.No);
                    row.CreateCell(1).SetCellValue(luduan.start);
                    row.CreateCell(2).SetCellValue(luduan.end);
                    row.CreateCell(3).SetCellValue(luduan.Fac);
                    row.CreateCell(4).SetCellValue(luduan.Fab);
                    row.CreateCell(5).SetCellValue(luduan.Xac);
                    row.CreateCell(6).SetCellValue(luduan.Xab);
                    row.CreateCell(7).SetCellValue(luduan.tc);
                    row.CreateCell(8).SetCellValue(luduan.tb);
                }
                //LuJing
                ISheet sheet3 = workbook.CreateSheet("路径信息");
                IRow row0LuJing = sheet3.CreateRow(0);
                row0LuJing.CreateCell(0).SetCellValue("Start");
                row0LuJing.CreateCell(1).SetCellValue("End");
                row0LuJing.CreateCell(2).SetCellValue("Fpc");
                row0LuJing.CreateCell(3).SetCellValue("Fpb");
                row0LuJing.CreateCell(4).SetCellValue("Ec");
                row0LuJing.CreateCell(5).SetCellValue("Eb");
                row0LuJing.CreateCell(6).SetCellValue("Nodes");
                var rowCount3 = 0;
                foreach (var od in ods)
                {
                    foreach (var lujing in od.LuJings)
                    {
                        rowCount3 = rowCount3 + 1;
                        IRow row = sheet3.CreateRow(rowCount3);
                        row.CreateCell(0).SetCellValue(lujing.start.No);
                        row.CreateCell(1).SetCellValue(lujing.end.No);
                        row.CreateCell(2).SetCellValue(lujing.Fpc);
                        row.CreateCell(3).SetCellValue(lujing.Fpb);
                        row.CreateCell(4).SetCellValue(lujing.ec);
                        row.CreateCell(5).SetCellValue(lujing.eb);
                        row.CreateCell(6).SetCellValue(string.Format(string.Join(",", lujing.Nodes.Select(n => n.No))));
                    }
                }
                //Final
                ISheet sheet4 = workbook.CreateSheet("Final");
                IRow rowFinal = sheet4.CreateRow(0);
                rowFinal.CreateCell(0).SetCellValue("次数");
                rowFinal.CreateCell(1).SetCellValue("值");
                var rowCount4 = 0;
                foreach (var final in finals)
                {
                    rowCount4 = rowCount4 + 1;
                    IRow row = sheet4.CreateRow(rowCount4);
                    row.CreateCell(0).SetCellValue(rowCount4);
                    row.CreateCell(1).SetCellValue(final);
                }

                var newFile = string.Format($"{uri}\\Data\\{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-计算结果.xlsx");
                FileStream sw = File.Create(newFile);
                workbook.Write(sw);
                GC.Collect();
                GC.WaitForPendingFinalizers();
                //Marshal.ReleaseComObject(sheet1);
                //Marshal.ReleaseComObject(sheet2);
                //Marshal.ReleaseComObject(sheet3);
                //Marshal.ReleaseComObject(sheet4);
                workbook.Close();
                //Marshal.ReleaseComObject(workbook);
                sw.Close();
                #endregion
                #region 导出所有数据到Json
                var newfile = string.Format($"{uri}\\Data\\{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-od.json");
                using (StreamWriter file = new StreamWriter(newfile, false))
                {
                    ods = ods.OrderBy(c => c.Start).ThenBy(c => c.End).ToList();
                    foreach (var od in ods)
                    {
                        foreach (var lujing in od.LuJings)
                        {
                            foreach (var ld in lujing.LuDuans)
                            {
                                ld.At = new List<LuJing>();
                            }
                            foreach (var node in lujing.Nodes)
                            {
                                node.Neighbor = new List<Node>();
                                node.NextUsed = new List<Node>();
                            }
                        }
                    }
                    var json = JsonConvert.SerializeObject(ods);
                    file.WriteLine(json);
                }
                #endregion
                return 0;
            }
        }
    }
}
