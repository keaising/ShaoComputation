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
        /// <summary>
        /// 小汽车路径最小值
        /// </summary>
        public double ec_min
        {
            get
            {
                return LuJings.Min(l => l.ec);
            }
        }
        /// <summary>
        /// 公交车路径最小值
        /// </summary>
        public double eb_min
        {
            get
            { 
                return LuJings.Min(l => l.eb);
            }
        }
        public List<LuJing> LuJings { get; set; }
    }
}
