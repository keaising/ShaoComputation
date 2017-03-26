using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShaoComputation.Const;

namespace ShaoComputation.Helper
{
    /// <summary>
    /// 随机生成
    /// </summary>
    public class Randam
    {
        /// <summary>
        /// 随机生成公交发车频率
        /// </summary>
        public static int F
        {
            get
            {
                Random rnd = new Random();
                return rnd.Next(Varias.F_low, Varias.F_up);  
            }
        }
    }
}
