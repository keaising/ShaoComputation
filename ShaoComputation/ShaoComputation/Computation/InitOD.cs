using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Computation
{
    public static class InitOD
    {
        /// <summary>
        /// 初始化OD
        /// </summary>
        public static void initOD(this List<OD> ods)
        {
            foreach (var od in ods)
            {
                od.q_rs_c = 0;
                od.q_rs_b = 0;
                foreach (var lujing in od.LuJings)
                {
                    lujing.Fpc = 0;
                    lujing.Fpb = 0;
                }
            }
        }
    }
}
