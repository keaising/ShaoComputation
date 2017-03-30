using ShaoComputation.Helper;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Computation
{
    /// <summary>
    /// 迭代
    /// </summary>
    public class Iteration
    {
        public static void Run()
        {
            var ods = new List<OD>();
            var luduans = new List<LuDuan>();
            var nodes = new List<Node>();

            ods.initOD();
            var i = 1;
            while (i > 0)
            {
                foreach (var od in ods)
                {
                    foreach (var lujing in od.LuJings)
                    {
                        foreach (var luduan in lujing.LuDuans)
                        {
                            luduans.NumOf(luduan.No).LD_fac_fab();
                            luduans.NumOf(luduan.No).LD_tac_tab();
                        }
                        lujing.Fee(luduans);
                    }
                    if (od.q_rs_c <= od.Q_rs / (1 + Math.Pow(Math.E, (od.ec_min - od.eb_min))))
                    {
                        foreach (var lujing in od.LuJings)
                        {
                            if (lujing.ec == od.ec_min)
                            {
                                lujing.Fpc = (1 - 1 / i) * lujing.Fpc + 1 / i * od.Q_rs;
                            }
                            else
                            {
                                lujing.Fpc = (1 - 1 / i) * lujing.Fpc;
                            }
                        }
                    }
                    else
                    {
                        foreach (var lujing in od.LuJings)
                        {
                            if (lujing.eb == od.eb_min)
                            {
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb + 1 / i * od.Q_rs;
                            }
                            else
                            {
                                lujing.Fpb = (1 - 1 / i) * lujing.Fpb;
                            }
                        }
                    }
                    od.q_rs_c = od.LuJings.Sum(lj => lj.Fpc);
                    od.q_rs_b = od.Q_rs - od.q_rs_c;
                }
            }
        }
    }
}
