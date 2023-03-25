using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotcaClassLib
{
    public class Spec : IEquatable<Spec?>
    {
        #region Properties

        /// x y coords
        public double X { get; set; } = 0;

        public double Y { get; set; } = 0;

        /// velocities and change in velocities
        public double v { get; set; } = 0;

        public double dv { get; set; } = 0;

        /// angle of velosity
        public double phi { get; set; } = 0;

        /// angle to change angle of velosity
        public double dphi { get; set; } = 0;

        /// age of being
        public int Age { get; set; } = 0;

        #endregion

        /// clip value to the bounds <from> and <to>
        public double Clip(double value, double from, double to)
        {
            if (value < from) value = from;
            if (value > to) value = to;
            return value;
        }

        /// update velocities and change in velocities
        public Spec Update(double Dv = 0, double Dphi = 0, double VMax = double.MaxValue)
        {
            // update x and y
            X = X + v * Math.Cos(phi);
            Y = Y + v * Math.Sin(phi);
            // update velocities
            v = Clip(v + dv, 0, VMax);
            dv = Dv;

            phi = phi + dphi;
            dphi = Dphi;
            // update age
            Age++;
            return this;
        }

        public Spec(double x, double y, double v, double dv, double phi, double dphi, int age)
        {
            X = x;
            Y = y;
            this.v = v;
            this.dv = dv;
            this.phi = phi;
            this.dphi = dphi;
            Age = age;
        }

        public Spec()
        {
        }

        private string endl = Environment.NewLine;

        public override string ToString()
        {
            return $"{nameof(X)}={X}, {nameof(Y)}={Y}, " + endl +
                   $"{nameof(v)}={v}, {nameof(dv)}={dv.ToString()}, " + endl +
                   $"{nameof(phi)}={phi}, {nameof(dphi)}={dphi}, " + endl +
                   $"{nameof(Age)}={Age}";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Spec);
        }

        public override int GetHashCode()
        {
            /// generate hash code
            return base.GetHashCode()+X.GetHashCode()+Y.GetHashCode()+v.GetHashCode()+dv.GetHashCode()+phi.GetHashCode()+
                dphi.GetHashCode()+Age.GetHashCode();
        }

        public bool Equals(Spec? other)
        {
            return other is not null &&
                   X == other.X &&
                   Y == other.Y &&
                   v == other.v &&
                   dv == other.dv &&
                   phi == other.phi &&
                   dphi == other.dphi &&
                   Age == other.Age;
        }

        public static bool operator ==(Spec? left, Spec? right)
        {
            return EqualityComparer<Spec>.Default.Equals(left, right);
        }

        public static bool operator !=(Spec? left, Spec? right)
        {
            return !(left == right);
        }

        public double Dist2(Spec? other)
        {
            return (X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y);
        }

        public double Dist(Spec? other)
        {
            return Math.Sqrt(Dist2(other));
        }

        public bool IsNear(Spec? other, double R)
        {
            if (other == null) return false;
            if (other == this) return true;
            if (Math.Abs(X - other.X) > R) return false;
            if (Math.Abs(Y - other.Y) > R) return false;
            if (Dist2(other) > R * R) return false;

            return true;
        }
    }
}
