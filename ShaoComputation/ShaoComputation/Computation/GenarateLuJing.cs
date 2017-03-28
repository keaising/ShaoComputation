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
        public static List<OD> Run(List<OD> ods, List<LuDuan> luDuans)
        {
            foreach (var od in ods)
            {

            }
            return new List<OD>();
        }

        public static List<LuJing> GetAllPath(OD od, List<LuDuan> luDuans, List<Node> nodes)
        {
            var lujings = new List<LuJing>();
            var stack = new Stack<Node>();
            var lujingUsed = new List<Node>();
            var lujingNos = new List<List<int>>();

            stack.Push(nodes.NumOf(od.start));
            lujingUsed.Add(nodes.NumOf(od.start));

            while (stack.Count > 0)
            {
                var neighbor = GetNeighbor(stack, luDuans, lujingUsed, nodes);
                if (neighbor != null)
                {
                    if (!lujingUsed.Any(n => n.No == neighbor.No))
                    {
                        lujingUsed.Add(neighbor);
                    }
                    stack.Push(neighbor);

                    if (neighbor.No == od.end)
                    {
                        var lujing = OutPutLuJing(stack);
                        lujings.Add(lujing);
                        lujingNos.Add(lujing.Points.Select(p => p.No).ToList());
                        stack.Peek().NextUsed = new List<Node>();
                        stack.Pop();
                        //lujingUsed.RemoveAll(n => n.No == neighbor.No);
                    }
                    else
                        continue;
                }
                else
                {
                    var peekNode = stack.Peek().No;
                    stack.Peek().NextUsed = new List<Node>();
                    lujingUsed.RemoveAll(n => n.No == stack.Peek().No);
                    var show = stack.Select(n => n.No).ToList();
                    stack.Pop();
                    stack.Peek().NextUsed.Add(nodes.NumOf(peekNode));
                }
            }
            return lujings;
        }

        /// <summary>
        /// 找到可用的邻居
        /// </summary>
        static Node GetNeighbor(Stack<Node> stack, List<LuDuan> luDuans, List<Node> lujingUsed, List<Node> allNodes)
        {
            var ends = luDuans.Where(l => l.start == stack.Peek().No).Select(l => l.end).ToList();
            if (ends != null && ends.Count > 0)
            {
                foreach (var end in ends)
                {
                    var endNode = allNodes.NumOf(end);
                    if (!lujingUsed.Contains(endNode) && !stack.Peek().NextUsed.Contains(endNode))
                    {
                        return endNode;
                    }
                }
                return null;
            }
            else return null;
        }

        static List<int> ReleaseFollowPoint(List<int> backup, int pointBack, int pointFront, List<LuDuan> luduans, List<int> lujingUsed)
        {
            var pointBack_back = luduans.Where(l => l.start == pointBack).ToList();
            var pointFront_back = luduans.Where(l => l.start == pointFront).ToList();
            if (pointFront_back != null && pointFront_back.Count > 0)
            {
                var pointFront_Points = pointFront_back.Select(l => l.end).ToList();
                if (pointBack_back != null && pointBack_back.Count > 0)
                {
                    foreach (var line in pointBack_back)
                    {
                        if (backup.Contains(line.end) && !lujingUsed.Contains(line.end) && !pointFront_Points.Contains(line.end))
                        {
                            backup.RemoveAll(b => b == line.end);
                        }
                    }
                }
            }
            else
            {
                if (pointBack_back != null && pointBack_back.Count > 0)
                {
                    foreach (var line in pointBack_back)
                    {
                        if (backup.Contains(line.end) && !lujingUsed.Contains(line.end))
                        {
                            backup.RemoveAll(b => b == line.end);
                        }
                    }
                }
            }
            return backup;
        }

        /// <summary>
        /// 从Stack转换为路径
        /// </summary>
        public static LuJing OutPutLuJing(Stack<Node> stack)
        {
            var lujing = new LuJing();
            lujing.Points = new List<Node>();
            foreach (var item in stack)
            {
                lujing.Points.Add(item);
            }
            lujing.Points.Reverse();
            lujing.start = stack.Last();
            lujing.end = stack.Peek();
            return lujing;
        }
    }
}
