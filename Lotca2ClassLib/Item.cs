using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotca2ClassLib
{
    public class Item
    {
        public int Id { get; set; } = 0;
        public int Age { get; set; } = 0;
        public double Mass { get; set; } = 1;
        public string Name { get; set; } = "item";

        /// 
        public XYCoord Point { get; set; } = new XYCoord();

        public double Correct(double value, double from = 0, double to = 100)
        {
            if (value < from) return from;
            if (value > to) return to;
            return value;
        }
        public double CorrectAngle(double value)
        {
            return Math.Asin(Math.Sin(value));
        }

        ///
        public double V { get; set; } = 0;
        public double dV { get; set; } = 0;

        ///
        public double Phi { get; set; } = 0;
        public double dPhi { get; set; }

        /// 
        public void SetV(double dV)
        {
            this.dV = dV;
            V = V+dV;
        }
        public void SetPhi(double dPhi)
        {
            this.dPhi = dPhi;
            Phi = Phi+dPhi;
        }
        /// 
        public void UpdateV(double dV, double dPhi)
        {
            SetPhi(dPhi);
            SetV(dV);
        }
        public void UpdateXY()
        {
            Point.X = Point.X + V*Math.Cos(Phi);
            Point.Y = Point.Y + V*Math.Sin(Phi);
        }
        /// <summary>
        /// set new dv and dphi and update 
        /// 1 - phi and V
        /// 2 - coords based on new phi and V
        /// </summary>
        /// <param name="dV"></param>
        /// <param name="dPhi"></param>
        public void Update(double dV, double dPhi)
        { 
            UpdateV(dV, dPhi);
            UpdateXY();
        }

        ///
        public double X { get => Point.X; set => Point.X = value; }
        public double Y { get => Point.Y; set => Point.Y = value; }
        ///
        public double Vx => V*Math.Cos(Phi);
        public double Vy => V*Math.Sin(Phi);
        ///
        public double dVx => dV*Math.Cos(Phi);
        public double dVy => dV*Math.Sin(Phi);


    }
}
