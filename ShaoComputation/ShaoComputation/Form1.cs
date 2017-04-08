using ShaoComputation.Computation;
using ShaoComputation.Helper;
using ShaoComputation.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShaoComputation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Begin_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var fullUri = string.Format($"{Environment.CurrentDirectory}\\OD.xlsx");
            var result = ReadExcel.LuDuan(fullUri);
            result = result.OrderBy(l => l.No).ToList();
            var luduans = ReadExcel.LuduanAndPoint(result, fullUri);
            var nodes = ReadExcel.Nodes(fullUri);
            var ods = ReadExcel.OD(fullUri);
            ReadExcel.Varia(fullUri);
            foreach (var od in ods)
            {
                od.LuJings = GenarateLuJing.GetAllPath(od, luduans, nodes);
                foreach (var lujing in od.LuJings) //添加路段所在路径信息
                {
                    foreach (var luduan in lujing.LuDuans)
                    {
                        if (luduan.No != 0)
                        {
                            if (luduans.NumOf(luduan.No).At == null)
                            {
                                luduans.NumOf(luduan.No).At = new List<LuJing>();
                            }

                            if (!luduans.NumOf(luduan.No).At.Any(l => l.start.No == lujing.Nodes.First().No && l.end.No == lujing.Nodes.Last().No))
                            {
                                luduans.NumOf(luduan.No).At.Add(lujing);
                            }
                        }
                    }
                }
            }
            var uri = string.Format($"{Environment.CurrentDirectory}");
            Iteration.Run(ods, luduans, nodes, uri);
            sw.Stop();
            MessageBox.Show($"任务完成！耗时{sw.ElapsedMilliseconds / 1000}秒");
        }
    }
}
