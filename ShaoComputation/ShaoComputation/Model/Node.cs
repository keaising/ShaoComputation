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
        /// <summary>
        /// 后继节点
        /// </summary>
        public List<Node> Next { get; set; }
        /// <summary>
        /// 当前路径使用过了
        /// </summary>
        public bool StackUsed { get; set; }
        public bool NextUsed { get; set; }
    }
}
