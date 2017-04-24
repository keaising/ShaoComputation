using ShaoComputation.Const;
using ShaoComputation.Helper;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Computation
{
    public class GeneticAlgorithm
    {
        public static List<Group> Children(List<Group> groups, List<OD> ODsOrigin, List<LuDuan> luduansOrigin, List<Node> nodesOrigin)
        {
            var seed = new Random();
            var children = new List<Group>();
            for (int i = 0; i < Varias.M * Varias.Pc; i++)
            {
                var id1 = seed.Next(0, Varias.M);
                var id2 = 0;
                var father = groups[id1];
                var mother = new Group();
                while (true)
                {
                    id2 = seed.Next(0, Varias.M);
                    mother = groups[id2];
                    if (groups[id2].No != father.No) break;
                }
                children.Add(CreateChild(father, mother, ODsOrigin, luduansOrigin, nodesOrigin));
            }
            children = Variation(children);
            return children;
        }

        /// <summary>
        /// 交叉
        /// </summary>
        /// <param name="father"></param>
        /// <param name="mother"></param>
        /// <param name="originOD"></param>
        /// <returns></returns>
        static Group CreateChild(Group father, Group mother, List<OD> ODsOrigin, List<LuDuan> luduansOrigin, List<Node> nodesOrigin)
        {
            var ods = CopyHelper.DeepClone(ODsOrigin);
            var luduans = CopyHelper.DeepClone(luduansOrigin);
            var nodes = CopyHelper.DeepClone(nodesOrigin);
            var child = new Group()
            {
                No = Varias.GroupNo,
                Luduans = new List<LuDuan>(),
                Ods = ods
            };
            Varias.GroupNo += 1;
            //交叉
            var seed = new Random();
            var point = seed.Next(0, father.Luduans.Count);
            child.Luduans = luduans;
            var fatherLd = father.Luduans.Take(point);
            var motherLd = mother.Luduans.Skip(point);
            foreach (var fld in fatherLd)
            {
                child.Luduans.NumOf(fld.No).F = fld.F;
            }
            foreach (var mld in motherLd)
            {
                child.Luduans.NumOf(mld.No).F = mld.F;
            }
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
            return child;
        }

        /// <summary>
        /// 变异
        /// </summary>
        /// <param name="groups"></param>
        static List<Group> Variation(List<Group> groups)
        {
            var geneCount = groups.Count * groups.First().Fs.Count;
            for (int i = 0; i < geneCount * Varias.Pm; i++)
            {
                var seed = new Random();
                var num = seed.Next(0, geneCount);
                var groupNum = (int)Math.Round(num / (double)groups.First().Fs.Count);
                var geneNum = num % groups.First().Fs.Count;
                if (groups.Count > groupNum && groups[groupNum].Fs != null && groups[groupNum].Fs.Count > (geneNum))
                {
                    groups[groupNum].Fs[geneNum] = Randam.F;
                }
                else
                {
                    i -= 1;
                }
            }
            return groups;
        }

        /// <summary>
        /// 计算适应度
        /// </summary>
        /// <param name="groups"></param>
        /// <returns></returns>
        public static List<Group> CalculateFitness(List<Group> groups)
        {
            //保留所有代中最大的目标值
            if (Varias.MaxResult < groups.Max(g => g.Result))
            {
                Varias.MaxResult = groups.Max(g => g.Result);
            }
            foreach (var group in groups)
            {
                group.Fitness = Varias.MaxResult - group.Result;
            }
            return groups;
        }
    }
}
