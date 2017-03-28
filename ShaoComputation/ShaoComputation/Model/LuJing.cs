using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Model
{
    public class LuJing
    {
        public int start { get; set; }
        public int end { get; set; }
        /// <summary>
        /// 路径构成
        /// </summary>
        public List<LuDuan> Contain { get; set; }
        public List<int> Points { get; set; }
        /// <summary>
        /// 小汽车费用
        /// </summary>
        public double ec { get; set; }
        /// <summary>
        /// 公交车费用
        /// </summary>
        public double eb { get; set; }
        /// <summary>
        /// 小汽车路径人数
        /// </summary>
        public double RenShu_c { get; set; }
        /// <summary>
        /// 公交车路径人数
        /// </summary>
        public double RenShu_b { get; set; }
    }
}
