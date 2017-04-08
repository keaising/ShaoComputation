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
        public int Start { get; set; }
        public int End { get; set; }
        public double Q_rs { get; set; }
        public double Q_rs_c { get; set; }
        public double Q_rs_b { get; set; }
        /// <summary>
        /// 小汽车路径最小值
        /// </summary>
        public double Ec_min
        {
            get
            {
                return LuJings.Min(l => l.ec);
            }
        }
        /// <summary>
        /// 公交车路径最小值
        /// </summary>
        public double Eb_min
        {
            get
            { 
                return LuJings.Min(l => l.eb);
            }
        }
        public List<LuJing> LuJings { get; set; }
    }
}
