using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShaoComputation.Const;
using ShaoComputation.Model;

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
        /// <summary>
        /// 轮盘赌选择种群
        /// </summary>
        public static List<Group> Roulette(List<Group> groups)
        {
            var MaxResult = groups.Max(g => g.Result);
            foreach (var group in groups)
            {
                group.Fitness = MaxResult - group.Result;
            }
            var SumFitness = groups.Sum(g => g.Fitness);
            var seed = new Random();
            var result = new List<Group>();
            for (int i = 0; i < Varias.M; i++)
            {
                var mark = seed.NextDouble();
                var init = 0.0;
                for (int j = 0; j < groups.Count; j++)
                {
                    init += groups[j].Fitness / SumFitness;
                    if (init > mark)
                    {
                        result.Add(groups[j]);
                        break;
                    }
                }
            }
            return result;
        }
    }
}
