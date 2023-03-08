using LotcaClassLib;

namespace TestLog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddComment(About.AboutString);
            AddComment(About.Author);
        }
        private string endl = Environment.NewLine;
        public void ClearText() { richTextBox1.Clear(); }
        public void CopyText() { richTextBox1.Copy(); }
        public void CutText() { richTextBox1.Cut(); }
        public void PasteText() { richTextBox1.Paste(); }

        public string Hr(char ch = '-', int w = 80) => new string(ch, w);
        public string Hr(string info, char ch = '-', int w = 80) => info + new string(ch, w - info.Length);
        public void AddTimeStamp()
        {
            richTextBox1.AppendText(Hr() + endl);
            richTextBox1.AppendText(DateTime.Now.ToString() + endl);
            richTextBox1.AppendText(Hr() + endl);

            richTextBox1.Invalidate();
        }
        public void AddComment(string comment)
        {
            richTextBox1.AppendText(Hr() + endl);
            richTextBox1.AppendText(comment + endl);
            richTextBox1.AppendText(Hr() + endl);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            CopyText();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            CutText();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            AddTimeStamp();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            AddComment(toolStripTextBox1.Text);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            PasteText();
        }

        public void AddString(string str) { richTextBox1.AppendText(str + endl); }
        public void AddText(string str, bool clear = false)
        {
            if (clear) ClearText();
            AddTimeStamp();
            richTextBox1.AppendText(str + endl);
            AddComment("End of added text");
        }
        private Spec spc1 = new Spec();
        private Spec spc2 = new Spec();
        private SpecsPack SpecsPack = new SpecsPack();
        private SpecsPack SpecsPack2 = new SpecsPack();
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            AddComment(spc1.ToString());
            spc2.Update();
            AddComment(spc2.ToString());
            AddComment(spc1.Dist(spc2).ToString());
            AddComment(spc1.IsNear(spc2, 5).ToString());

            SpecsPack.Setup_BaseConst();
            SpecsPack.Start();
            AddComment(SpecsPack.Info());
            SpecsPack.UpdateXY();
            AddComment(SpecsPack.Info());
            SpecsPack.UpdateXY();
            SpecsPack.BornOrDie();
            AddComment(SpecsPack.Info());
            /* Output coords
            var x = SpecsPack.X();
            var y = SpecsPack.Y();
            for (int i = 0; i < x.Length; i++)
            {
                AddString($"{i} {x[i]} {y[i]}");
            }
            */
            SpecsPack2.Setup_BaseConst();
            SpecsPack2.StartCount = 150;
            SpecsPack2.Start();
            SpecsPack2.UpdateXY();
            SpecsPack2.BornOrDie();
            AddComment(SpecsPack2.Info());
            var d2 = SpecsPack.Distance2(SpecsPack2);
            var n2 = SpecsPack.IsNear(SpecsPack2);
            var count_near = 0;
            foreach (var item in n2)
            {
                if (item) count_near++;
            }
            AddComment($"Count of near {count_near}");

        }
        private XYGraph xy_form = new XYGraph();
        private XYGraph xy_form2 = new XYGraph();

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

            xy_form.X.Clear();
            xy_form.X.AddRange(SpecsPack.X());
            xy_form.Y.Clear();
            xy_form.Y.AddRange(SpecsPack.Y());
            xy_form.Title = "SpecsPack1";

            xy_form.Show();
            xy_form.plot_scatter();

            xy_form2.X.Clear();
            xy_form2.X.AddRange(SpecsPack2.X());
            xy_form2.Y.Clear();
            xy_form2.Y.AddRange(SpecsPack2.Y());
            xy_form2.Title = "SpecsPack2";

            xy_form2.Show();
            xy_form2.plot_scatter();

        }
        private XY2Form xy_form3 = new XY2Form();
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            xy_form3.Hide();
            xy_form3.Show();
            xy_form3.plot(SpecsPack.X(),SpecsPack.Y(),SpecsPack2.X(),SpecsPack2.Y(), 
                SpecsPack.CountOfSpecs.ToArray(),SpecsPack2.CountOfSpecs.ToArray());
            xy_form3.Invalidate();
        }
    }
}