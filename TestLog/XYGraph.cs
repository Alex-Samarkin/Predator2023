using OxyPlot.Series;
using OxyPlot;
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
    public partial class XYGraph : Form
    {
        public XYGraph()
        {
            InitializeComponent();
        }
        public List<double> X = new List<double>();
        public List<double> Y = new List<double>();
        public string Title = nameof(Title);

        private void XYGraph_Load(object sender, EventArgs e)
        {
            if (X == null || Y == null) return;

            if (Y.Count == 0) return;
            if (X.Count == 0) return;
            if (X.Count != Y.Count) return;

            /*
                var model = new PlotModel { Title = "ScatterSeries" };
                var scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
                var r = new Random(314);
                for (int i = 0; i < 100; i++)
                {
                    var x = r.NextDouble();
                    var y = r.NextDouble();
                    var size = r.Next(5, 15);
                    var colorValue = r.Next(100, 1000);
                    scatterSeries.Points.Add(new ScatterPoint(x, y, size, colorValue));
                 }

                model.Series.Add(scatterSeries);
                model.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
             */
            plot_scatter();
        }

        public void plot_scatter()
        {
            var s = new ScatterSeries();
            s.MarkerType = MarkerType.Circle;
            s.MarkerSize = 2.5;
            for (int i = 0; i < X.Count; i++)
            {
                s.Points.Add(new ScatterPoint(X[i], Y[i]));
            }
            var Model = new PlotModel { Title = this.Title };
            Model.Series.Add(s);
            this.plotView1.Model = Model;
        }
    }
}
