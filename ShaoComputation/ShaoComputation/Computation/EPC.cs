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
            var luDuanNo = l.LuDuans.Select(ld => ld.tc).ToList();
            var luduans = Allluduans.Where(ld => luDuanNo.Contains(ld.No));
            l.ec = luduans.Select(ld => ld.tc).Sum() * (Varias.gamma_c + Varias.gamma_tc);
            var comfort = luduans.Select(ld => ld.tb * ld.Fab / (Varias.Bb * ld.F)).Sum() * Varias.gamma_a;
            l.eb = luduans.Select(ld => ld.tb).Sum() * (Varias.gamma_b) + Varias.gamma_m * Varias.money + comfort;
        }
    }
}
