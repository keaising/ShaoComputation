using ShaoComputation.Model;
using ShaoComputation.Const;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Computation
{
    public static class BPR
    {
        /// <summary>
        /// 路段行驶时间，路阻函数
        /// </summary>
        /// <param name="l"></param>
        public static void LD_tac_tab(this LuDuan l)
        {
            if (l.N == 0)
            {
                l.tc = Varias.MaxValue;
                l.tb = Varias.MaxValue;
            }
            else
            {
                if (l.Lambda == 0)
                {
                    l.tc = l.tc0 * (1 + Varias.Alpha_c * (Math.Pow(((l.Xac + l.Xab * Varias.Mu) / (l.N * l.C)), Varias.Beta_c)));
                    l.tb = l.tb0 * (1 + Varias.Alpha_b * (Math.Pow(((l.Xac + l.Xab * Varias.Mu) / (l.N * l.C)), Varias.Beta_b)));
                }
                else
                {
                    if (l.N == l.Yita)
                    {
                        l.tc = Varias.MaxValue;
                        l.tb = l.tb0 * (1 + Varias.Alpha_b * (Math.Pow(((l.Xab * Varias.Mu) / (l.Yita * l.C)), Varias.Beta_b)));
                    }
                    else
                    {
                        l.tc = l.tc0 * (1 + Varias.Alpha_c * (Math.Pow((l.Xac / ((l.N - l.Yita) * l.C)), Varias.Beta_c)));
                        l.tb = l.tb0 * (1 + Varias.Alpha_b * (Math.Pow(((l.Xab * Varias.Mu) / (l.Yita * l.C)), Varias.Beta_b)));
                    }
                }
            }
        }

        /// <summary>
        /// 路段所有信息
        /// </summary>
        /// <param name="ld"></param>
        public static void LD_fac_fab(this LuDuan ld)
        {
            ld.Fac = ld.At.Sum(l => l.Fpc);
            ld.Fab = ld.At.Sum(l => l.Fpb);
            ld.Xab = ld.F;
            ld.Xac = ld.Fac / Varias.Bc;
        }
    }
}
