using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Model
{
    public class Node
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int No { get; set; }
        public List<Node> Neighbor { get; set; }
        /// <summary>
        /// 已经访问过的点
        /// </summary>
        public List<Node> NextUsed { get; set; }
    }
}
