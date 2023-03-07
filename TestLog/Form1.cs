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
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            AddComment(spc1.ToString());
            spc2.Update();
            AddComment(spc2.ToString());
            AddComment(spc1.Dist(spc2).ToString());
            AddComment(spc1.IsNear(spc2,5).ToString());
        }
    }
}