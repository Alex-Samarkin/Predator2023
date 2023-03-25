using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotca2ClassLib
{
    /// <summary>
    /// координаты точки
    /// </summary>
    public class XYCoord
    {
        /// <summary>
        /// X
        /// </summary>
        public double X { get; set; } = 0;
        /// <summary>
        /// Y
        /// </summary>
        public double Y { get; set; } = 0;

        /// <summary>
        /// delta by X
        /// </summary>
        /// <param name="xy">other point</param>
        /// <returns>delta X</returns>
        public double dx (XYCoord xy) => xy.X - X;
        /// <summary>
        /// delta by Y
        /// </summary>
        /// <param name="xy">other point</param>
        /// <returns>delta Y</returns>
        public double dy (XYCoord xy) => xy.Y - Y;
        /// <summary>
        /// squared distance
        /// </summary>
        /// <param name="xy">other point</param>
        /// <returns>squared distance</returns>
        public double Dist2(XYCoord xy) => dx(xy) * dx(xy) + dy(xy) * dy(xy);
        /// <summary>
        /// distance
        /// </summary>
        /// <param name="xy">other point</param>
        /// <returns>distance</returns>
        public double Dist(XYCoord xy) => Math.Sqrt(Dist2(xy));
        /// <summary>
        /// angle to <paramref name="xy"/>
        /// </summary>
        /// <param name="xy">other point</param>
        /// <returns>angle in rad from X axis</returns>
        public double Angle(XYCoord xy) => Math.Atan2(dy(xy),dx(xy));
        /// <summary>
        /// check if <paramref name="xy"/> is inside circle with <paramref name="R"/>
        /// i.e. near from current point
        /// </summary>
        /// <param name="xy">other point</param>
        /// <param name="R">radius</param>
        /// <returns>true if dist is lesser than R and false otherwise</returns>
        public bool IsNear(XYCoord xy, double R)
        {
            var dx1 = Math.Abs(dx(xy));
            var dy1 = Math.Abs(dy(xy));
            /// exclude point with bigger than R dist by X and Y
            if(dx1 > R) return false;
            if(dy1 > R) return false;
            /// if a point inside square +/-R find distance
            /// its a long-time operation
            if( (dx1*dx1+dy1*dy1) > R*R ) return false;
            return true;
        }
    }
}
