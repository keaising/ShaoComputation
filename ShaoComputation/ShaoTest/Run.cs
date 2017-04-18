
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShaoComputation.Computation;
using ShaoComputation.Const;
using ShaoComputation.Helper;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoTest
{
    [TestClass]
    public class Run
    {
        [TestMethod]
        public void Running()
        {
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
        }

        [TestMethod]
        public void GroupRun()
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
            foreach (var group in groups)
            {
                Varias.IsGA = true;
                group.Result = Iteration.Run(group.Ods, group.Luduans, ReadExcel.Nodes(fullUri), uri);
            }
            var mins = new List<double>();
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
                mins.Add(children.Min(c => c.Result));
            }
            #endregion
        }
    }
}
