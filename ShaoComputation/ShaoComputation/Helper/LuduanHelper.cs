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
    }
}
