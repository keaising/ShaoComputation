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
    }
}
