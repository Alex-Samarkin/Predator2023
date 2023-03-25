using LotcaClassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestLog
{
    public partial class FiveGraphForm : Form
    {
        public FiveGraphForm()
        {
            InitializeComponent();
        }

        private SpecsPack sp1 = new SpecsPack();
        private SpecsPack sp2 = new SpecsPack();
        private string endl = " "+Environment.NewLine;

        private void FiveGraphForm_Load(object sender, EventArgs e)
        {
            var t1 = DateTime.Now;
            // set speck 1
            sp1.Setup_BaseConst();
            sp1.StartCount = 350;
            // set speck 2
            sp2.Setup_BaseConst();
            sp2.StartCount = 100;

            sp1.Start();
            sp2.Start();

            textBox1.Clear();
            textBox1.Text += DateTime.Now.ToString()+" Start 2 pack"+endl;

            sp1.UpdateXY();
            sp1.BornOrDie();
            textBox1.Text += "Update SP1"+endl;

            sp2.UpdateXY();
            sp2.BornOrDie();
            textBox1.Text += (DateTime.Now - t1).ToString() + " | Update SP2" + endl;

            SpecsPack.Interaction(sp1, sp2);

            textBox1.Text += (DateTime.Now - t1).ToString() + " | Find enemy" + endl;

            // plot s1 s2 

            // plot s1 and s2

            // plot count

            // plot s1 vs s2
        }
    }
}
