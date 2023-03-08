using OxyPlot;
using OxyPlot.Series;
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
    public partial class XY2Form : Form
    {
        public XY2Form()
        {
            InitializeComponent();
        }

        public List<double> X = new List<double>();
        public List<double> Y = new List<double>();
        public string Title = nameof(Title);
        public List<double> X2 = new List<double>();
        public List<double> Y2 = new List<double>();
        public string Title2 = nameof(Title);
        public string Title3 = nameof(Title);
        public List<int> Count1 = new List<int>();
        public List<int> Count2 = new List<int>();
        public string Title4 = nameof(Title);

        PlotModel model = new PlotModel { Title = "1" };
        PlotModel model2 = new PlotModel { Title = "2" };
        PlotModel model3 = new PlotModel { Title = "3" };
        PlotModel model4 = new PlotModel { Title = "4" };

        ScatterSeries scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };
        ScatterSeries scatterSeries_ = new ScatterSeries { MarkerType = MarkerType.Circle };
        ScatterSeries scatterSeries2 = new ScatterSeries { MarkerType = MarkerType.Circle };
        ScatterSeries scatterSeries2_ = new ScatterSeries { MarkerType = MarkerType.Circle };
        ScatterSeries scatterSeries3 = new ScatterSeries { MarkerType = MarkerType.Circle };
        ScatterSeries scatterSeries4 = new ScatterSeries { MarkerType = MarkerType.Circle };

        public void SetXY(double[] x, double[] y)
        {
            X.Clear();
            Y.Clear();
            X.AddRange(x);
            Y.AddRange(y);
            scatterSeries.Points.Clear();
            scatterSeries_.Points.Clear();
            for (int i = 0; i < X.Count; i++)
            {
                scatterSeries.Points.Add(new ScatterPoint(X[i], Y[i]));
                scatterSeries_.Points.Add(new ScatterPoint(X[i], Y[i]));
            }
            model.Series.Clear();
            model.Series.Add(scatterSeries);
            
            model3.Series.Clear();
            model3.Series.Add(scatterSeries_);
        }
        public void SetXY2(double[] x, double[] y)
        {
            X2.Clear();
            Y2.Clear();
            X2.AddRange(x);
            Y2.AddRange(y);

            scatterSeries2.Points.Clear();
            scatterSeries2_.Points.Clear();
            for (int i = 0; i < X2.Count; i++)
            {
                scatterSeries2.Points.Add(new ScatterPoint(X2[i], Y2[i]));
                scatterSeries2_.Points.Add(new ScatterPoint(X2[i], Y2[i]));
            }
            model2.Series.Clear();
            model2.Series.Add(scatterSeries2);

            //model3.Series.Clear();
            model3.Series.Add(scatterSeries2_);

            Invalidate();
        }
        public void SetCount(int[] c1, int[] c2)
        {
            Count1.Clear();
            Count2.Clear();
            Count1.AddRange(c1);
            Count2.AddRange(c2);

            scatterSeries3.Points.Clear();
            scatterSeries4.Points.Clear();

            for(int i = 0;i < c1.Length;i++)
            {
                scatterSeries3.Points.Add(new ScatterPoint(i, c1[i]));
                scatterSeries4.Points.Add(new ScatterPoint(i, c2[i]));
            }
            model4.Series.Clear();
            model4.Series.Add(scatterSeries3);
            model4.Series.Add(scatterSeries4);
        }

        public void plot(double[] x, double[] y, 
            double[] x2, double[] y2,
            int[] c1, int[] c2)
        {
            SetXY(x, y);
            SetXY2(x2, y2);
            SetCount(c1, c2);

            plotView1.Model = model;
            plotView2.Model = model2;
            plotView3.Model = model3;
            plotView4.Model = model4;
        }




    }
}
