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
        public static double Mu { get; set; }
        /// <summary>
        /// 公交票价
        /// </summary>
        public static double Money { get; set; }
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
        public static double Alpha_b { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double Alpha_c { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double Beta_b { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double Beta_c { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double Gamma_b { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double Gamma_c { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double Gamma_a { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double Gamma_m { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double Gamma_tc { get; set; }
        /// <summary>
        /// BPR参数
        /// </summary>
        public static double Gamma_tb { get; set; }
        public static int F_low { get; set; }
        public static int F_up { get; set; }
        public static double MaxValue { get; set; }
    }
}
