using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Const
{
    public class Varias
    {
        /// <summary>
        /// 公交车转换为小汽车的比例系数
        /// </summary>
        public static double mu { get; set; }
        /// <summary>
        /// 公交票价
        /// </summary>
        public static double money { get; set; }
        /// <summary>
        /// 小汽车平均载客量
        /// </summary>
        public static double Bc { get; set; }
        /// <summary>
        /// 公交车最大载客量
        /// </summary>
        public static double Bb { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double alpha_b { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double alpha_c { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double beta_b { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double beta_c { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double gamma_b { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double gamma_c { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double gamma_a { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double gamma_m { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double gamma_tc { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double gamma_tb { get; set; }
        public static int F_low { get; set; }
        public static int F_up { get; set; }
        public static double MaxValue { get; set; }
    }
}
