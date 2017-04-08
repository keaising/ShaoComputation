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
                    if (od.q_rs_c <= od.Q_rs / (1 + Math.Pow(Math.E, (od.ec_min - od.eb_min))))
                    {
                        foreach (var lujing in od.LuJings)
                        {
                            if (lujing.ec == od.ec_min)
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
                            if (lujing.eb == od.eb_min)
                            {
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb + 1 / i * od.Q_rs;
                            }
                            else
                            {
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb;
                            }
                        }
                    }
                    od.q_rs_c = od.LuJings.Sum(lj => lj.Fpc);
                    od.q_rs_b = od.Q_rs - od.q_rs_c;
                }
                var v1 = new List<double>();
                var v2 = new List<double>();
                var v3 = new List<double>();
                var v4 = new List<double>();
                var v5 = new List<double>();
                foreach (var od in ods)
                {
                    v1.Add(od.LuJings.Sum(lj => (lj.ec - od.ec_min) * lj.Fpc));
                    v2.Add(od.ec_min * od.q_rs_c);
                    v3.Add(od.LuJings.Sum(lj => (lj.eb - od.eb_min) * lj.Fpc));
                    v4.Add(od.eb_min * od.q_rs_b);
                    v5.Add(od.q_rs_c - od.Q_rs / (1 + Math.Pow(Math.E, (od.ec_min - od.eb_min))));
                }
                //todo: 待议
                var final = v1.Sum() / v2.Sum() + v3.Sum() / v4.Sum() + Math.Sqrt(v5.Sum(v => Math.Pow(v, 2))) / ods.Sum(od => od.Q_rs);
            }
            #region
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("Sheet1");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("OD-Start");
            row0.CreateCell(1).SetCellValue("OD-End");
            row0.CreateCell(2).SetCellValue("od.q_rs_c");
            row0.CreateCell(3).SetCellValue("od.q_rs_b");
            row0.CreateCell(4).SetCellValue("od.ec_min");
            row0.CreateCell(5).SetCellValue("od.eb_min");
            var rowCount = 0;
            foreach (var od in ods)
            {
                rowCount = rowCount + 1;
                IRow row = sheet1.CreateRow(rowCount);
                row.CreateCell(0).SetCellValue(od.start);
                row.CreateCell(1).SetCellValue(od.end);
                row.CreateCell(2).SetCellValue(od.q_rs_c);
                row.CreateCell(3).SetCellValue(od.q_rs_b);
                row.CreateCell(4).SetCellValue(od.ec_min);
                row.CreateCell(5).SetCellValue(od.eb_min);
            }
            var newFile = string.Format(@"D:\Files\1.xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            #endregion
            #region
            //var newfile = string.Format("C:\\Data\\od.json");
            //using (StreamWriter file = new StreamWriter(newfile, false))
            //{
            //    ods = ods.OrderBy(c => c.start).ThenBy(c => c.end).ToList();
            //    var json = JsonConvert.SerializeObject(ods);
            //    file.WriteLine(json);
            //}
            #endregion
        }
    }
}
