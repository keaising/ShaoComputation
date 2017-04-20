using ShaoComputation.Const;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Helper
{
    public static class LuduanHelper
    {
        public static LuDuan NumOf(this List<LuDuan> luduans, int no)
        {
            return luduans.FirstOrDefault(l => l.No == no);
        }

        public static LuDuan StartEnd(this List<LuDuan> luduans, int start, int end)
        {
            var luduan = luduans.Where(l => l.start == start && l.end == end);
            if (luduan != null && luduan.Count() > 0)
            {
                return luduan.First();
            }
            else
            {
                return new LuDuan
                {
                    start = start,
                    end = end
                };
            }
        }

        public static void ChangeLtcLtb(this List<LuDuan> luduans)
        {
            foreach (var l in luduans)
            {
                if (l.N == 0)
                {
                    l.ltc = Varias.MaxValue;
                    l.ltb = Varias.MaxValue;
                }
                else
                {
                    if (l.Lambda == 0)
                    {
                        l.ltc = l.tc0;
                        l.ltb = l.tb0;
                    }
                    else
                    {
                        if (l.Yita == 0)
                        {
                            l.ltc = l.tc0;
                            l.ltb = Varias.MaxValue;
                        }
                        else if (l.N == l.Yita)
                        {
                            l.ltc = Varias.MaxValue;
                            l.ltb = l.tb0;
                        }
                        else
                        {
                            l.ltc = l.tc0;
                            l.ltb = l.tb0;
                        }
                    }
                }
            }
        }
    }
}
