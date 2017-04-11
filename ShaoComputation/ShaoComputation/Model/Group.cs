using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Model
{
    public class Group
    {
        public List<int> Fs
        {
            get
            {
                return Luduans.Select(ld => ld.F).ToList();
            }
        }
        public List<LuDuan> Luduans { get; set; }
        public List<OD> Ods { get; set; }
        public double Fitness { get; set; }
        public double Result { get; set; }
    }
}
