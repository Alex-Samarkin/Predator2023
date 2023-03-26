using Lotca2ClassLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            toolStripProgressBar1.Visible = false;

        }

        public AreaOfThings areaOfThings { get; set; } = new AreaOfThings();

        public ChartXY ch1 { get; set; } = new ChartXY() { Title = "Волки" };
        public ChartXY ch2 { get; set; } = new ChartXY() { Title = "Зайцы" };

        public ChartXY ch3 { get; set; } = new ChartXY() { Title = "Волки и зайцы" };

        public ChartXY ch4 { get; set; } = new ChartXY() { Title = "Количество" };
        public ChartXY ch5 { get; set; } = new ChartXY() { Title = "Количество - фазовая плосткость" };


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            areaOfThings.Setup2();

            //areaOfThings.Rabbits1.Ages.To = 150;

            propertyGrid1.SelectedObject = areaOfThings;

            ch1.MakeModel(plotView1, areaOfThings.Wolfs1.X, areaOfThings.Wolfs1.Y);
            ch1.markerColor = 150;
            ch2.MakeModel(plotView2, areaOfThings.Rabbits1.X, areaOfThings.Rabbits1.Y);

            ch3.MakeModel2(plotView3, areaOfThings.Wolfs1.X, areaOfThings.Wolfs1.Y, areaOfThings.Rabbits1.X, areaOfThings.Rabbits1.Y);

            ch4.MakeCountModel(plotView4, areaOfThings.Wolfs1.CountPerTime, areaOfThings.Rabbits1.CountPerTime);

            ch5.MakeCountModel2(plotView5, areaOfThings.Wolfs1.CountPerTime, areaOfThings.Rabbits1.CountPerTime);


        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = areaOfThings.Wolfs1;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = areaOfThings.Rabbits1;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            areaOfThings.Update1();

            UpdateSeries();
            RefreshViews();
        }
        private void UpdateSeries()
        {
            ch1.UpdateS1(areaOfThings.Wolfs1.X, areaOfThings.Wolfs1.Y);
            ch2.UpdateS1(areaOfThings.Rabbits1.X, areaOfThings.Rabbits1.Y);

            ch3.UpdateS1(areaOfThings.Wolfs1.X, areaOfThings.Wolfs1.Y);
            ch3.UpdateS2(areaOfThings.Rabbits1.X, areaOfThings.Rabbits1.Y);

            ch4.MakeCountModel(plotView4, areaOfThings.Wolfs1.CountPerTime, areaOfThings.Rabbits1.CountPerTime);
            ch5.MakeCountModel2(plotView5, areaOfThings.Wolfs1.CountPerTime, areaOfThings.Rabbits1.CountPerTime);

        }
        private void RefreshViews()
        {
            plotView1.Refresh();
            plotView2.Refresh();
            plotView3.Refresh();

            plotView4.Refresh();
            plotView4.Refresh();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                areaOfThings.Update1();

                UpdateSeries();
                RefreshViews();

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Visible = true;
            for (int i = 0; i < 100; i++)
            {
                areaOfThings.Update1();

                UpdateSeries();
                RefreshViews();

                toolStripProgressBar1.Value = i;

            }
            toolStripProgressBar1.Visible = false;
        }
    }
}
