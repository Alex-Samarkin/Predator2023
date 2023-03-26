using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace Lotca2ClassLib
{
    public class ChartXY
    {
        /*
         *  var model = new PlotModel { Title = "ScatterSeries" };
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
         * 
         */
        public PlotModel Model { get; set; } = null;
        public string Title { get; set; } = "Title";
        public ScatterSeries ScatterSeries { get; set; } = null;
        public ScatterSeries ScatterSeries1 { get; set; } = null;
        public MarkerType markerType { get; set; } = MarkerType.Circle;
        public double markerSize { get; set; } = 3;
        public int markerColor { get; set; } = 180;

        public void MakeModel(PlotView oxyPlot,List<double> x, List<double> y)
        {
            Model = new PlotModel { Title = Title };
            ScatterSeries = new ScatterSeries { MarkerType = markerType };
            ScatterSeries.MarkerType = markerType;
            ScatterSeries.MarkerSize = markerSize;

            var Count = Math.Min(x.Count,y.Count);
            for (int i = 0; i < Count; i++)
            {
                ScatterSeries.Points.Add(new ScatterPoint(x[i], y[i],markerSize,1,markerColor));
            }
            Model.Series.Add(ScatterSeries);
            oxyPlot.Model = Model;
            oxyPlot.BackColor = Color.White;
            oxyPlot.Refresh();
        }
        public void MakeModel2(PlotView oxyPlot, List<double> x, List<double> y, List<double> x1, List<double> y1)
        {
            Model = new PlotModel { Title = Title };
            ScatterSeries = new ScatterSeries { MarkerType = markerType };
            ScatterSeries.MarkerType = markerType;
            ScatterSeries.MarkerSize = markerSize;

            var Count = Math.Min(x.Count, y.Count);
            for (int i = 0; i < Count; i++)
            {
                ScatterSeries.Points.Add(new ScatterPoint(x[i], y[i], markerSize, 1, markerColor));
            }
            Model.Series.Add(ScatterSeries);

            ScatterSeries1 = new ScatterSeries { MarkerType = markerType };
            ScatterSeries1.MarkerType = markerType;
            ScatterSeries1.MarkerSize = markerSize;

            var Count1 = Math.Min(x1.Count, y1.Count);
            for (int i = 0; i < Count1; i++)
            {
                ScatterSeries1.Points.Add(new ScatterPoint(x1[i], y1[i], markerSize, 1, markerColor));
            }
            Model.Series.Add(ScatterSeries1);

            oxyPlot.Model = Model;
            oxyPlot.BackColor = Color.White;
            oxyPlot.Refresh();
        }
        public ScatterSeries MakeSeries(List<double> x, List<double> y,MarkerType marker = MarkerType.Diamond)
        {
            var ScatterSeries1 = new ScatterSeries { MarkerType = markerType };
            ScatterSeries1.MarkerType = marker;
            ScatterSeries1.MarkerSize = markerSize;

            var Count = Math.Min(x.Count, y.Count);
            for (int i = 0; i < Count; i++)
            {
                ScatterSeries1.Points.Add(new ScatterPoint(x[i], y[i], markerSize,1,markerColor+50));
            }
            return ScatterSeries1;
        }
        public void AppendSeries(List<double> x, List<double> y)
        {
            Model.Series.Add(MakeSeries(x, y));
        }
        public void UpdateS1(List<double> x, List<double> y)
        {
            ScatterSeries.Points.Clear();
            var Count = Math.Min(x.Count, y.Count);
            for (int i = 0; i < Count; i++)
            {
                ScatterSeries.Points.Add(new ScatterPoint(x[i], y[i], markerSize, 1, markerColor));
            }
        }
        public void UpdateS2(List<double> x, List<double> y)
        {
            ScatterSeries1.Points.Clear();
            var Count = Math.Min(x.Count, y.Count);
            for (int i = 0; i < Count; i++)
            {
                ScatterSeries1.Points.Add(new ScatterPoint(x[i], y[i], markerSize, 1, markerColor));
            }
        }

        public void MakeCountModel(PlotView oxyPlot, List<int> cx, List<int> cy)
        {
            Model = new PlotModel { Title = Title };
            ScatterSeries = new ScatterSeries { MarkerType = markerType };
            ScatterSeries.MarkerType = markerType;
            ScatterSeries.MarkerSize = markerSize;

            ScatterSeries1 = new ScatterSeries { MarkerType = markerType };
            ScatterSeries1.MarkerType = markerType;
            ScatterSeries1.MarkerSize = markerSize;

            var Count = Math.Min(cx.Count, cy.Count);
            for (int i = 0; i < Count; i++)
            {
                ScatterSeries.Points.Add(new ScatterPoint(i, cx[i], markerSize, 1, markerColor));
                ScatterSeries1.Points.Add(new ScatterPoint(i, cy[i], markerSize, 1, markerColor));
            }
            Model.Series.Add(ScatterSeries);
            Model.Series.Add(ScatterSeries1);

            oxyPlot.Model = Model;
            oxyPlot.BackColor = Color.White;
            oxyPlot.Refresh();
        }

        public void UpdateCount(List<int> cx, List<int> cy)
        {
            ScatterSeries.Points.Clear();
            ScatterSeries1.Points.Clear();

            var Count = Math.Min(cx.Count, cy.Count);
            for (int i = 0; i < Count; i++)
            {
                ScatterSeries.Points.Add(new ScatterPoint(i, cx[i], markerSize, 1, markerColor));
                ScatterSeries1.Points.Add(new ScatterPoint(i, cy[i], markerSize, 1, markerColor));
            }

            //Model.DefaultYAxis.;
        }
        public void MakeCountModel2(PlotView oxyPlot, List<int> cx, List<int> cy)
        {
            Model = new PlotModel { Title = Title };
            ScatterSeries = new ScatterSeries { MarkerType = markerType };
            ScatterSeries.MarkerType = markerType;
            ScatterSeries.MarkerSize = markerSize;

            var Count = Math.Min(cx.Count, cy.Count);
            for (int i = 0; i < Count; i++)
            {
                ScatterSeries.Points.Add(new ScatterPoint(cx[i], cy[i], markerSize, 1, markerColor));
            }
            Model.Series.Add(ScatterSeries);

            oxyPlot.Model = Model;
            oxyPlot.BackColor = Color.White;
            oxyPlot.Refresh();
        }

    }

}
