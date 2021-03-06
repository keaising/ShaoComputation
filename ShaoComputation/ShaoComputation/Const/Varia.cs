﻿using System;
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
        /// <summary>
        /// 迭代次数
        /// </summary>
        public static int Count { get; set; }
        /// <summary>
        /// 费用变量
        /// </summary>
        public static double Theta { get; set; }
        /// <summary>
        /// 用户随机变量
        /// </summary>
        public static double Phy { get; set; }
        /// <summary>
        /// 是否处于遗传算法迭代
        /// </summary>
        public static bool IsGA = false;
        /// <summary>
        /// 种群数量
        /// </summary>
        public static int M { get; set; }
        /// <summary>
        /// 交叉概率
        /// </summary>
        public static double Pc { get; set; }
        /// <summary>
        /// 变异概率
        /// </summary>
        public static double Pm { get; set; }
        /// <summary>
        /// 迭代次数
        /// </summary>
        public static int T { get; set; }
        /// <summary>
        /// 最大迭代次数
        /// </summary>
        public static int Tmax { get; set; }
        /// <summary>
        /// 种群编号
        /// </summary>
        public static int GroupNo { get; set; }
        /// <summary>
        /// 最大适应度
        /// </summary>
        public static double MaxResult { get; set; }
        /// <summary>
        /// OD中路阻最小的N条路径
        /// </summary>
        public static int LuJingCount { get; set; }
        public static double Epsilon { get; set; }    
    }
}
