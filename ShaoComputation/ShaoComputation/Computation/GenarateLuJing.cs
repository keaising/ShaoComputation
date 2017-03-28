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

        public static List<LuJing> GetAllPath(OD od, List<LuDuan> luDuans)
        {
            var lujings = new List<LuJing>();
            Stack<int> stack = new Stack<int>();
            var flag = false;
            var lujingUsed = new List<int>();
            var nextUsed = new List<int>();
            nextUsed.Add(od.start);
            stack.Push(od.start);
            lujingUsed.Add(od.start);

            while (stack.Count > 0)
            {
                var neighbor = GetNeighbor(stack, luDuans, lujingUsed, nextUsed);
                if (neighbor != 0)
                {
                    if (!nextUsed.Contains(neighbor))
                    {
                        nextUsed.Add(neighbor);
                    }
                    if (!lujingUsed.Contains(neighbor))
                    {
                        lujingUsed.Add(neighbor);
                    }
                    stack.Push(neighbor);

                    if (neighbor == od.end)
                    {
                        lujings.Add(OutPutLuJing(stack));
                        stack.Pop();
                        if (flag)
                        {
                            lujingUsed.RemoveAll(u => u == neighbor);
                            nextUsed.RemoveAll(u => u == neighbor);
                            flag = true;
                        }
                    }
                    else
                        continue;
                }
                else
                {
                    var last = nextUsed.Last();
                    nextUsed.RemoveAll(u => u == last);
                    //nextUsed.Add(stack.Peek());
                    //var frontPoint = stack.Count == 1 ? 0 : stack.ElementAt(1);
                    //nextUsed = ReleaseFollowPoint(nextUsed, stack.First(), frontPoint, luDuans, lujingUsed);
                    lujingUsed.RemoveAll(u => u == last);
                    stack.Pop();
                }
            }
            return lujings;
        }

        /// <summary>
        /// 找到可用的邻居
        /// </summary>
        static int GetNeighbor(Stack<int> stack, List<LuDuan> luDuans, List<int> lujingUsed, List<int> nextUsed)
        {
            var ends = luDuans.Where(l => l.start == stack.Peek()).Select(l => l.end).ToList();
            if (ends != null && ends.Count > 0)
            {
                foreach (var end in ends)
                {
                    if (!lujingUsed.Contains(end) && !nextUsed.Contains(end))
                    //if (!nextUsed.Contains(end))
                    {
                        return end;
                    }
                }
                return 0;
            }
            else return 0;
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
        public static LuJing OutPutLuJing(Stack<int> stack)
        {
            var lujing = new LuJing();
            lujing.Points = new List<int>();
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
