﻿using ShaoComputation.Helper;
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
            stack.Push(nodes.NumOf(od.start));
            while (stack.Count > 0)
            {
                var neighbor = GetNeighbor(stack, luDuans, nodes);
                if (neighbor != null)
                {
                    nodes.NumOf(stack.Peek().No).NextUsed.Add(neighbor);
                    stack.Push(neighbor);
                    if (neighbor.No == od.end)
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
            return lujings;
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
        /// 从Stack转换为路径
        /// </summary>
        public static LuJing OutPutLuJing(Stack<Node> stack)
        {
            var lujing = new LuJing();
            lujing.Nodes = new List<Node>();
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
