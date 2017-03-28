using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Helper
{
    public static class NodeHelper
    {
        public static Node NumOf(this List<Node> nodes, int no)
        {
            return nodes.FirstOrDefault(n => n.No == no);
        }
    }
}
