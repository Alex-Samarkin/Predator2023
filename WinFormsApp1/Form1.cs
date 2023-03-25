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
            Things things2 = new Things();
            things2.RandGenCoord = new RandGen(1);
            things.StartCount = new Interval() { From = 10000, To = 100000, Value = 50000 };
            things2.StartCount = new Interval() { From = 10000, To = 100000, Value = 50000 };
            DateTime dateTime = DateTime.Now;
            Trace.WriteLine(dateTime);
            
            things.SetupItems01();
            things2.SetupItems01();

            for (int i = 0; i < 5; i++)
            {
                things.Move02();
                things2.Move02();
                things.BornOrDie03();
                things2.BornOrDie03();
                Trace.WriteLine(things.Count);
                Trace.WriteLine(things2.Count);
            }
            DateTime dateTime2 = DateTime.Now;
            Trace.WriteLine(dateTime2);
            var span = dateTime2 - dateTime;
            Trace.WriteLine(dateTime2);
            Trace.WriteLine(span);
        }
    }
}