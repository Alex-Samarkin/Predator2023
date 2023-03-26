using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotca2ClassLib
{
    public class AreaOfThings
    {
        public double MaxX { get; set; } = 1000;
        public double MaxY { get; set; } = 1000;

        public List<Things> BiomOfThings { get; set; } = new List<Things>();
        public Things Rabbits1 => BiomOfThings[1];
        public Things Wolfs1 => BiomOfThings[0];

        public void Setup2()
        {
            Things Rabbits = new Things();
            Things Wolfs = new Things();

            /// setup Rabbits
            /// 
            Rabbits.RandGenCoord = new RandGen(-1);
            Rabbits.RandGenBorn = new RandGen(-1);
            Rabbits.RandGenEat = new RandGen(-1);
            Rabbits.StartCount = new Interval() { From = 1000, To = 5000, Value = 3000 };
            DateTime dateTime = DateTime.Now;
            Trace.WriteLine(dateTime);
            Rabbits.SetupItems01();
            Rabbits.BornOrDieInterval.To = 0.8;
            Rabbits.BornOrDieInterval.From = 0.15;

            /// setup Wolf
            /// 
            Wolfs.RandGenCoord = new RandGen(1);
            Wolfs.RandGenBorn = new RandGen(1);
            Wolfs.RandGenEat = new RandGen(1);
            Wolfs.StartCount = new Interval() { From = 100, To = 1000, Value = 500 };
            Wolfs.BornOrDieInterval.To = 0.92;
            Wolfs.BornOrDieInterval.From = 0.12;
            Wolfs.R = 5.35;

            Trace.WriteLine(dateTime);
            Wolfs.SetupItems01();

            /// add to Biom
            /// 
            BiomOfThings.Clear();
            BiomOfThings.Add(Wolfs);
            BiomOfThings.Add(Rabbits);
        }

        public int ModellingTime { get; set; } = 0;

        public void Update1()
        {
            ModellingTime +=1;
            foreach (var things in BiomOfThings)
            {
                if (things.Count == 0) continue;
                things.Update();
            }

            Wolfs1.Eat04(Rabbits1);
            foreach (var things in BiomOfThings)
            {
                things.UpdateCountsPerTime();
            }
        }

        public void UpdateN(int N)
        {
            for (int i = 0; i < N; i++)
            {
                Update1();
                Trace.WriteLine($"{i} {N} Wolfs {Wolfs1.Count} Rabbits {Rabbits1.Count}");
                if ( Wolfs1.Count == 0) return;
                if ( Rabbits1.Count == 0) return;
            }
        }
    }
}
