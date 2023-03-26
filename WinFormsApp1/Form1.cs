using Lotca2ClassLib;
using System.Diagnostics;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var a = new AboutBox1();
            a.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Things things = new Things();
            things.RandGenCoord = new RandGen(-1);

            Things things2 = new Things();
            things2.RandGenCoord = new RandGen(1);

            things.StartCount = new Interval() { From = 1000, To = 50000, Value = 25000 };
            things2.StartCount = new Interval() { From = 1000, To = 10000, Value = 5000 };

            DateTime dateTime = DateTime.Now;
            Trace.WriteLine(dateTime);

            things.SetupItems01();
            things.BornOrDieInterval.To = 0.75;

            things2.SetupItems01();

            for (int i = 0; i < 5; i++)
            {
                Trace.WriteLine(i);
                Trace.WriteLine("------- Move ");
                things.Move02();
                things2.Move02();
                Trace.WriteLine("------- BornOrDie ");
                things.BornOrDie03();
                things2.BornOrDie03();
                Trace.WriteLine("------- Eat ");
                things2.Eat04(things);
                Trace.WriteLine("------- Count ");
                Trace.WriteLine(things.Count);
                Trace.WriteLine(things2.Count);
                Trace.WriteLine("------- ");
            }
            DateTime dateTime2 = DateTime.Now;
            Trace.WriteLine(dateTime2);
            var span = dateTime2 - dateTime;
            Trace.WriteLine(dateTime2);
            Trace.WriteLine(span);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AreaOfThings areaOfThings = new AreaOfThings();
            areaOfThings.Setup2();
            areaOfThings.Update1();
            areaOfThings.UpdateN(10);

            ChartXY chartXY = new ChartXY();
            chartXY.MakeModel(this.plotView1, areaOfThings.Wolfs1.X, areaOfThings.Wolfs1.Y);
            chartXY.AppendSeries(areaOfThings.Rabbits1.X, areaOfThings.Rabbits1.Y);
            this.plotView1.Refresh();

            areaOfThings.UpdateN(10);
            chartXY.MakeModel(this.plotView1, areaOfThings.Wolfs1.X, areaOfThings.Wolfs1.Y);
            chartXY.AppendSeries(areaOfThings.Rabbits1.X, areaOfThings.Rabbits1.Y);
            this.plotView1.Refresh();

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            var f2 = new Form2();
            f2.ShowDialog();
        }
    }
}