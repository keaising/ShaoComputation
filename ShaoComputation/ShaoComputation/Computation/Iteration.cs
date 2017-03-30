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
                var v1 = new List<double>();
                var v2 = new List<double>();
                var v3 = new List<double>();
                var v4 = new List<double>();
                var v5 = new List<double>();
                foreach (var od in ods)
                {
                    v1.Add(od.LuJings.Sum(lj => (lj.ec - od.ec_min) * lj.Fpc));
                    v2.Add(od.ec_min * od.q_rs_c);
                    v3.Add(od.LuJings.Sum(lj => (lj.eb - od.eb_min) * lj.Fpc));
                    v4.Add(od.eb_min * od.q_rs_b);
                    v5.Add(od.q_rs_c - od.Q_rs / (1 + Math.Pow(Math.E, (od.ec_min - od.eb_min))));
                }
                //todo: 待议
                var final = v1.Sum() / v2.Sum() + v3.Sum() / v4.Sum() + Math.Sqrt(v5.Sum(v => Math.Pow(v, 2))) / ods.Sum(od => od.Q_rs);
            }
        }
    }
}
