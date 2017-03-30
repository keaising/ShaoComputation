using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShaoComputation.Const;

namespace ShaoComputation.Model
{
    public class LuDuan
    {
        public int No { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        /// <summary>
        /// 路段车道数
        /// </summary>
        public double N { get; set; }
        /// <summary>
        /// 单车道通行能力
        /// </summary>
        public int C { get; set; }
        /// <summary>
        /// 小汽车路阻 值
        /// </summary>
        public double tc { get; set; }
        /// <summary>
        /// 公交车路阻 值
        /// </summary>
        public double tb { get; set; }
        /// <summary>
        /// 小汽车路阻 定值
        /// </summary>
        public double tc0 { get; set; }
        /// <summary>
        /// 公交车路阻 定值
        /// </summary>
        public double tb0 { get; set; }
        /// <summary>
        /// 不同路径叠加在该路段上小汽车的人数
        /// </summary>
        public double Fac { get; set; }
        /// <summary>
        /// 不同路径叠加在该路段上公交车的人数
        /// </summary>
        public double Fab { get; set; }
        /// <summary>
        /// 不同路径叠加在该路段上公交车数量
        /// </summary>
        public double Xab { get; set; }
        /// <summary>
        /// 不同路径叠加在该路段上小汽车数量
        /// </summary>
        public double Xac { get; set; }
        /// <summary>
        /// 路段公交车发车频率
        /// </summary>
        public int F { get; set; }
        /// <summary>
        /// 是否有公交专用道, 0：否，1：是
        /// </summary>
        public int Lambda { get; set; }
        /// <summary>
        /// 公交专用道数量
        /// </summary>
        public int Yita { get; set; }
        /// <summary>
        /// 路段所在路径
        /// </summary>
        public List<LuJing> At { get; set; }
    }
}
