using ShaoComputation.Const;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Computation
{

    public static class EPC
    {
        /// <summary>
        /// 每条路径上小汽车和公交车的费用
        /// </summary>
        public static void Fee(this LuJing l, List<LuDuan> Allluduans)
        {
            var luDuanNo = l.LuDuans.Select(ld => ld.No).ToList();
            var luduans = Allluduans.Where(ld => luDuanNo.Contains(ld.No));
            l.ec = luduans.Select(ld => ld.tc).Sum() * (Varias.Gamma_c + Varias.Gamma_tc);
            var comfort = luduans.Select(ld => ld.tb * ld.Fab / (Varias.Bb * ld.F)).Sum() * Varias.Gamma_a;
            l.eb = luduans.Select(ld => ld.tb).Sum() * (Varias.Gamma_b) + Varias.Gamma_m * Varias.Money + comfort;
        }
    }
}
