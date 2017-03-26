using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Model
{
    public class OD
    {
        public int No { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public double Q_rs { get; set; }
        public double q_rs_c { get; set; }
        public double q_rs_b { get; set; }
        public double ec_min { get; set; }
        public double eb_min { get; set; }
        public List<LuJing> LuJings { get; set; }
    }
}
