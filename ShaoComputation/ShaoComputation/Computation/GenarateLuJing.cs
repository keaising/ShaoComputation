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
    public class GenarateLuJing
    {
        /// <summary>
        /// 获取所有OD的可行路径
        /// </summary>
        /// <param name="ods"></param>
        /// <param name="luDuans"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static List<OD> Run(List<OD> ods, List<LuDuan> luDuans, List<Node> nodes)
        {
            foreach (var od in ods)
            {
                od.LuJings = GetAllPath(od, luDuans, nodes);
                foreach (var lujing in od.LuJings) //添加路段所在路径信息
                {
                    foreach (var luduan in lujing.LuDuans)
                    {
                        if (luduan.No != 0)
                        {
                            if (!luDuans.NumOf(luduan.No).At.Any(l => l.start == lujing.start && l.end == lujing.end))
                            {
                                luDuans.NumOf(luduan.No).At.Add(lujing);
                            }
                        }
                    }
                }
            }
            return ods;
        }

        /// <summary>
        /// 获取一个OD的所有可行路径
        /// </summary>
        /// <param name="od"></param>
        /// <param name="luDuans"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static List<LuJing> GetAllPath(OD od, List<LuDuan> luDuans, List<Node> nodes)
        {
            var lujings = new List<LuJing>();
            var stack = new Stack<Node>();
            var lujingNos = new List<List<int>>();
            stack.Push(nodes.NumOf(od.Start));
            while (stack.Count > 0)
            {
                var neighbor = GetNeighbor(stack, luDuans, nodes);
                if (neighbor != null)
                {
                    nodes.NumOf(stack.Peek().No).NextUsed.Add(neighbor);
                    stack.Push(neighbor);
                    if (neighbor.No == od.End)
                    {
                        var lujing = OutPutLuJing(stack);
                        lujings.Add(lujing);
                        lujingNos.Add(lujing.Nodes.Select(p => p.No).ToList());
                        nodes.NumOf(stack.Peek().No).NextUsed = new List<Node>();
                        stack.Pop();
                    }
                    else
                        continue;
                }
                else
                {
                    var peekNode = stack.Peek().No;
                    nodes.NumOf(stack.Peek().No).NextUsed = new List<Node>();
                    var show = stack.Select(n => n.No).ToList();
                    stack.Pop();
                    if (stack.Count > 0)
                    {
                        nodes.NumOf(stack.Peek().No).NextUsed.Add(nodes.NumOf(peekNode));
                    }
                }
            }
            GetLuduansByNode(luDuans, lujings);
            #region 筛选路阻函数最小的几条路径
            luDuans.ChangeLtcLtb();
            var busLujing = lujings.OrderBy(e => e.LuDuans.Sum(ld => ld.ltb)).Take(Varias.LuJingCount).ToList();
            var carLujing = lujings.OrderBy(e => e.LuDuans.Sum(ld => ld.ltc)).Take(Varias.LuJingCount).ToList();
            var result = new List<LuJing>();
            result.AddRange(busLujing);
            foreach (var clj in carLujing)
            {
                //去重
                if (!result.Any(lj => string.Join(",", lj.Nodes.Select(n => n.No)) == string.Join(",", clj.Nodes.Select(n => n.No))))
                {
                    result.Add(clj);
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 找到可用的邻居
        /// </summary>
        static Node GetNeighbor(Stack<Node> stack, List<LuDuan> luDuans, List<Node> allNodes)
        {
            var ends = luDuans.Where(l => l.start == stack.Peek().No).Select(l => l.end).ToList();
            if (ends != null && ends.Count > 0)
            {
                foreach (var end in ends)
                {
                    if (!stack.Peek().NextUsed.Select(n => n.No).Contains(end) && !stack.Select(n => n.No).Contains(end))
                    {
                        return allNodes.NumOf(end);
                    }
                }
                return null;
            }
            else return null;
        }

        /// <summary>
        /// 根据路径节点获取构成的路段
        /// </summary>
        public static void GetLuduansByNode(List<LuDuan> luduans, List<LuJing> lujings)
        {
            foreach (var lujing in lujings)
            {
                lujing.LuDuans = new List<LuDuan>();
                if (lujing.Nodes != null && lujing.Nodes.Count > 0)
                {
                    for (int i = 0; i < lujing.Nodes.Count - 1; i++)
                    {
                        var luduan = luduans.StartEnd(lujing.Nodes[i].No, lujing.Nodes[i + 1].No);
                        lujing.LuDuans.Add(luduan);
                    }
                }
            }
        }

        /// <summary>
        /// 从Stack转换为路径
        /// </summary>
        public static LuJing OutPutLuJing(Stack<Node> stack)
        {
            var lujing = new LuJing()
            {
                Nodes = new List<Node>()
            };

            foreach (var item in stack)
            {
                lujing.Nodes.Add(item);
            }
            lujing.Nodes.Reverse();
            lujing.start = stack.Last();
            lujing.end = stack.Peek();
            return lujing;
        }
    }
}
