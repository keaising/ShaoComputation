﻿using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using ShaoComputation.Computation;
using ShaoComputation.Const;
using ShaoComputation.Helper;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShaoComputation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Begin_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var result = ReadExcel.LuDuan(fullUri);
            result = result.OrderBy(l => l.No).ToList();
            var luduans = ReadExcel.LuduanAndPoint(result, fullUri);
            var nodes = ReadExcel.Nodes(fullUri);
            var ods = ReadExcel.OD(fullUri);
            ReadExcel.Varia(fullUri);
            foreach (var od in ods)
            {
                od.LuJings = GenarateLuJing.GetAllPath(od, luduans, nodes);
                foreach (var lujing in od.LuJings) //添加路段所在路径信息
                {
                    foreach (var luduan in lujing.LuDuans)
                    {
                        if (luduan.No != 0)
                        {
                            if (luduans.NumOf(luduan.No).At == null)
                            {
                                luduans.NumOf(luduan.No).At = new List<LuJing>();
                            }

                            if (!luduans.NumOf(luduan.No).At.Any(l => l.start.No == lujing.Nodes.First().No && l.end.No == lujing.Nodes.Last().No))
                            {
                                luduans.NumOf(luduan.No).At.Add(lujing);
                            }
                        }
                    }
                }
            }
            var uri = string.Format($"{Environment.CurrentDirectory}");
            Iteration.Run(ods, luduans, nodes, uri);
            sw.Stop();
            MessageBox.Show($"任务完成！耗时{sw.ElapsedMilliseconds / 1000}秒");
        }

        private void GA_Button_Click(object sender, EventArgs e)
        {
            var uri = string.Format($"{Environment.CurrentDirectory}");
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var groups = new List<Group>();
            Varias.GroupNo = 0;
            ReadExcel.Varia(fullUri);
            #region 产生种群
            for (int i = 0; i < Varias.M; i++)
            {
                var result = ReadExcel.LuDuan(fullUri);
                result = result.OrderBy(l => l.No).ToList();
                var luduans = ReadExcel.LuduanAndPoint(result, fullUri);
                var nodes = ReadExcel.Nodes(fullUri);
                var ods = ReadExcel.OD(fullUri);
                foreach (var od in ods)
                {
                    od.LuJings = GenarateLuJing.GetAllPath(od, luduans, nodes);
                    foreach (var lujing in od.LuJings) //添加路段所在路径信息
                    {
                        foreach (var luduan in lujing.LuDuans)
                        {
                            if (luduan.No != 0)
                            {
                                if (luduans.NumOf(luduan.No).At == null)
                                {
                                    luduans.NumOf(luduan.No).At = new List<LuJing>();
                                }

                                if (!luduans.NumOf(luduan.No).At.Any(l => l.start.No == lujing.Nodes.First().No && l.end.No == lujing.Nodes.Last().No))
                                {
                                    luduans.NumOf(luduan.No).At.Add(lujing);
                                }
                            }
                        }
                    }
                }
                var group = new Group
                {
                    No = Varias.GroupNo,
                    Luduans = luduans,
                    Ods = ods
                };
                foreach (var ld in group.Luduans)
                {
                    ld.F = Randam.F;
                }
                groups.Add(group);
                Varias.GroupNo += 1;
            }
            #endregion
            #region 循环
            Varias.IsGA = true;
            foreach (var group in groups)
            {
                group.Result = Iteration.Run(group.Ods, group.Luduans, ReadExcel.Nodes(fullUri), uri);
            }
            var minResults = new List<double>();
            var minFs = new List<List<int>>();
            for (int i = 0; i < Varias.T; i++)
            {
                var chosenGroup = Randam.Roulette(groups);
                var ODs = ReadExcel.OD(fullUri);
                var children = GeneticAlgorithm.Children(groups, ODs);
                foreach (var child in children)
                {
                    child.Result = Iteration.Run(child.Ods, child.Luduans, ReadExcel.Nodes(fullUri), uri);
                }
                var maxGroup = groups.OrderBy(g => g.Result).Take(Varias.M - (int)Math.Round(Varias.M * Varias.Pc)).ToList();
                children.AddRange(maxGroup);
                children = GeneticAlgorithm.CalculateFitness(groups);
                var minResult = children.Min(c => c.Result);
                minResults.Add(minResult);
                minFs.Add(children.FirstOrDefault(g => g.Result == minResult).Fs);
            }
            #endregion
            #region 输出到Excel
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("ResultFs");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("Result");
            row0.CreateCell(1).SetCellValue("Fs");
            var rowCount = 0;
            foreach (var Fs in minFs)
            {
                rowCount = rowCount + 1;
                IRow row = sheet1.CreateRow(rowCount);
                row.CreateCell(0).SetCellValue(minResults[rowCount - 1]);
                for (int i = 0; i < Fs.Count; i++)
                {
                    row.CreateCell(i + 1).SetCellValue(Fs[i]);
                }
            }
            var newFile = string.Format($"{uri}\\Data\\{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-遗传算法结果.xlsx");
            FileStream sw = File.Create(newFile);
            workbook.Write(sw);
            sw.Close();
            #endregion
        }
    }
}
