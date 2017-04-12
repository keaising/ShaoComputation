using ShaoComputation.Const;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoComputation.Computation
{
    public class GeneticAlgorithm
    {
        public static List<Group> Children(List<Group> groups, List<OD> originOD)
        {
            var seed = new Random();
            var children = new List<Group>();
            for (int i = 0; i < Varias.M; i++)
            {
                var id1 = seed.Next(0, Varias.M);
                var id2 = 0;
                var father = groups[id1];
                var mother = new Group();
                while (true)
                {
                    id2 = seed.Next(0, Varias.M);
                    mother = groups[id2];
                    if (groups[id2].No != father.No) break;
                }
                children.Add(Child(father, mother, originOD));
            }
            return children;
        }

        static Group Child(Group father, Group mother, List<OD> originOD)
        {
            var ods = new OD[originOD.Count];
            originOD.CopyTo(ods);
            var child = new Group()
            {
                No = Varias.GroupNo,
                Luduans = new List<LuDuan>(),
                Ods = ods.ToList()
            };
            Varias.GroupNo += 1;
            var seed = new Random();
            var point = seed.Next(0, father.Luduans.Count);
            child.Luduans.AddRange(father.Luduans.Take(point));
            child.Luduans.AddRange(mother.Luduans.Skip(point));
            return child;
        }
    }
}
