using NPOI.SS.UserModel;
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
            var fullUri = string.Empty;
            if (string.IsNullOrWhiteSpace(PathBox.Text))
            {
                fullUri = string.Format($"{Environment.CurrentDirectory}\\Data\\OD.xlsx");
            }
            else
            {
                fullUri = PathBox.Text;
            }
            messageBox.Text += string.Format($"开始计算，文件路径为{fullUri}\r\n");
            var result = ReadExcel.LuDuan(fullUri);
            result = result.OrderBy(l => l.No).ToList();
            var luduans = ReadExcel.LuduanAndPoint(result, fullUri);
            messageBox.Text += string.Format($"导入路段完成，路段数{luduans.Count}\r\n");
            var nodes = ReadExcel.Nodes(fullUri);
            messageBox.Text += string.Format($"导入节点完成，节点数{nodes.Count}\r\n");
            var ods = ReadExcel.OD(fullUri);
            messageBox.Text += string.Format($"导入OD完成，OD对数{ods.Count}\r\n");
            ReadExcel.Varia(fullUri);
            messageBox.Text += string.Format($"导入参数完成\t\n");
            messageBox.Text += string.Format($"{nameof(Varias.LuJingCount)}:{Varias.LuJingCount}\r\n");
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
            messageBox.Text += string.Format($"任务完成！耗时{sw.ElapsedMilliseconds / 1000}秒");
        }

        private void GA_Button_Click(object sender, EventArgs e)
        {
            messageBox.Text = string.Empty;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var uri = string.Format($"{Environment.CurrentDirectory}");
            var fullUri = string.Empty;
            if (string.IsNullOrWhiteSpace(PathBox.Text))
            {
                fullUri = string.Format($"{Environment.CurrentDirectory}\\Data\\OD.xlsx");
            }
            else
            {
                fullUri = PathBox.Text;
            }
            var groups = new List<Group>();
            Varias.GroupNo = 0;
            ReadExcel.Excel2Json(fullUri);
            var nodesOrigin = ReadExcel.Nodes(fullUri);
            var odsOrigin = ReadExcel.OD(fullUri);
            var result = ReadExcel.LuDuan(fullUri);
            result = result.OrderBy(l => l.No).ToList();
            var luduansOrigin = ReadExcel.LuduanAndPoint(result, fullUri);
            messageBox.Text += string.Format($"数据读入成功！\r\n{nameof(nodesOrigin)}：{nodesOrigin.Count}\r\n");
            messageBox.Text += string.Format($"{nameof(odsOrigin)}：{odsOrigin.Count}\r\n");
            messageBox.Text += string.Format($"{nameof(luduansOrigin)}：{luduansOrigin.Count}\r\n");
            #region 产生种群
            for (int i = 0; i < Varias.M; i++)
            {
                var luduans = CopyHelper.DeepClone(luduansOrigin);
                var ods = CopyHelper.DeepClone(odsOrigin);
                var nodes = CopyHelper.DeepClone(nodesOrigin);
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
            messageBox.Text += string.Format($"产生种群：{groups.Count}\r\n");
            #endregion
            sw.Stop();
            messageBox.Text += string.Format(($"数据读取完成，耗时{sw.ElapsedMilliseconds / 1000}秒，开始数据初始化\r\n"));
            #region 循环
            sw.Restart();
            Varias.IsGA = true;
            foreach (var group in groups)
            {
                group.Result = Iteration.Run(group.Ods, group.Luduans, ReadExcel.Nodes(fullUri), uri);
            }
            sw.Stop();
            messageBox.Text += string.Format(($"数据初始化完成，{groups.Count}个种群共耗时{sw.ElapsedMilliseconds / 1000}秒，开始遗传算法迭代\r\n"));
            var minResults = new List<double>();
            var minFs = new List<List<int>>();
            sw.Restart();
            var results = new Dictionary<int, List<double>>();
            for (int i = 0; i < Varias.T; i++)
            {
                var sw2 = new Stopwatch();
                sw2.Start();
                var chosenGroup = CopyHelper.DeepClone(Randam.Roulette(groups));
                var children = GeneticAlgorithm.Children(chosenGroup, CopyHelper.DeepClone(odsOrigin), CopyHelper.DeepClone(luduansOrigin), CopyHelper.DeepClone(nodesOrigin));
                #region 原有串行代码
                //foreach (var child in children)
                //{
                //    child.Result = Iteration.Run(child.Ods, child.Luduans, ReadExcel.Nodes(fullUri), uri);
                //}
                #endregion
                #region 并行
                Parallel.ForEach<Group>(children, child =>
                {
                    child.Result = Iteration.Run(child.Ods, child.Luduans, ReadExcel.Nodes(fullUri), uri);
                });
                #endregion
                var minGroup = groups.OrderBy(g => g.Result).Take(Varias.M - (int)Math.Round(Varias.M * Varias.Pc)).ToList();
                children.AddRange(minGroup);
                children = GeneticAlgorithm.CalculateFitness(children);
                var minResult = children.Min(c => c.Result);
                minResults.Add(minResult);
                minFs.Add(CopyHelper.DeepClone(children.FirstOrDefault(g => g.Result == minResult).Fs));
                results.Add(i, children.Select(c => c.Result).ToList());
                sw2.Stop();
                groups = children;
                messageBox.Text += string.Format(($"第{i + 1}次迭代完成，耗时{sw2.ElapsedMilliseconds / 1000}秒\r\n"));
            }

            sw.Stop();
            messageBox.Text += string.Format(($"遗传算法完成，{Varias.T}次迭代共耗时{sw.ElapsedMilliseconds / 1000}秒"));
            #endregion
            #region 输出到Excel
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet1 = workbook.CreateSheet("ResultFs");
            IRow row0 = sheet1.CreateRow(0);
            row0.CreateCell(0).SetCellValue("Result");
            row0.CreateCell(1).SetCellValue("Fs");
            row0.CreateCell(1).SetCellValue(Varias.MaxResult);
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
            ISheet sheet2 = workbook.CreateSheet("AllResult");
            IRow row1 = sheet2.CreateRow(0);
            row1.CreateCell(0).SetCellValue("No");
            row1.CreateCell(1).SetCellValue("Result");
            rowCount = 0;
            foreach (var list in results)
            {
                rowCount = rowCount + 1;
                IRow row = sheet2.CreateRow(rowCount);
                row1.CreateCell(0).SetCellValue(string.Format($"{list.Key}"));
                for (int i = 0; i < list.Value.Count; i++)
                {
                    row.CreateCell(i + 1).SetCellValue(list.Value[i]);
                }
            }
            ISheet sheet3 = workbook.CreateSheet("AllGroups");
            IRow row3 = sheet3.CreateRow(0);
            rowCount = 0;
            foreach (var list in groups)
            {
                rowCount = rowCount + 1;
                IRow row = sheet3.CreateRow(rowCount);
                row.CreateCell(0).SetCellValue(string.Format($"{list.Fitness}"));
                for (int i = 0; i < list.Fs.Count; i++)
                {
                    row.CreateCell(i + 1).SetCellValue(list.Fs[i]);
                }
            }


            var newFile = string.Format($"{uri}\\Data\\{DateTime.Now.Day}-{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-遗传算法结果.xlsx");
            FileStream sw1 = File.Create(newFile);
            workbook.Write(sw1);
            workbook.Close();
            sw1.Close();
            #endregion
        }

        private void ChooseBtn_Click(object sender, EventArgs e)
        {
            //初始化一个OpenFileDialog类 
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = $"{Application.StartupPath}\\Data";
            //判断用户是否正确的选择了文件 
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择文件的后缀名 
                string fullUri = fileDialog.FileName;
                PathBox.Text = fullUri;
            }
        }
    }
}
