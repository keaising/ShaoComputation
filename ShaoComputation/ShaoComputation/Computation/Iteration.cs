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
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Computation
{
    /// <summary>
    /// 迭代
    /// </summary>
    public class Iteration
    {
        public static void Run(List<OD> ods, List<LuDuan> luduans, List<Node> nodes)
        {
            ods.initOD();
            var i = 0;

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
                    if (od.Q_rs_c <= od.Q_rs / (1 + Math.Pow(Math.E, (od.Ec_min - od.Eb_min)))) //判断往QRSC还是QRSB加载
                    {
                        foreach (var lujing in od.LuJings)
                        {
                            if (lujing.ec == od.Ec_min)
                            {
                                lujing.Fpc = (1 - 1 / i) * lujing.Fpc + 1 / i * od.Q_rs;
                            }
                            else
                            {
                                lujing.Fpc = (1 - 1 / i) * lujing.Fpc;
                            }
                        }
                    }
                    else
                    {
                        foreach (var lujing in od.LuJings)
                        {
                            if (lujing.eb == od.Eb_min)
                            {
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb + 1 / i * od.Q_rs;
                            }
                            else
                            {
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb;
                            }
                        }
                    }
                    od.Q_rs_c = od.LuJings.Sum(lj => lj.Fpc);
                    od.Q_rs_b = od.Q_rs - od.Q_rs_c;
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
            }
            #region 导出到Excel
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("Start");
            row0.CreateCell(1).SetCellValue("End");
            row0.CreateCell(2).SetCellValue("q_rs_c");
            row0.CreateCell(3).SetCellValue("q_rs_b");
            row0.CreateCell(4).SetCellValue("ec_min");
            row0.CreateCell(5).SetCellValue("eb_min");
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
            }
            var newFile = string.Format(@"D:\Data\1.xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            #endregion
            #region
            var newfile = string.Format($"D:\\Data\\{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-od.json");
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
        }
    }
}
