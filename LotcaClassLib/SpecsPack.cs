using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet;
using MathNet.Numerics;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;
using System.Security.Cryptography;
using System.Xml.Schema;
using System.ComponentModel;
using Microsoft.VisualBasic;

namespace LotcaClassLib
{
    public class SpecsPack
    {
        private List<Spec> items = new List<Spec>();
        public List<Spec> Items
        {
            get { return items; }
            set { items = value; }
        }

        public List<int> CountOfSpecs { get; set; } = new List<int>();
        public int Generation => CountOfSpecs.Count;

        public string Name { get; set; } = "Какой-то вид животных";

        public string Info()
        {
            /// string with Base const
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{nameof(Name)} {Name}");
            sb.AppendLine($"X[{XMin} / {XMax}] Y[{YMin} / {YMax}]");
            sb.AppendLine($"V[{VMin}/{VMax}] dV[{DVMin}/{DVMax}]");
            sb.AppendLine($"Phi[{0}/{Math.PI * 2.0}] DPhi[{DPhiMin}{DPhiMax}]");
            sb.AppendLine($"Count of spec {Count}");
            sb.AppendLine($"Generation {Generation}");

            return sb.ToString();
        }

        #region List

        public int Count => Items.Count;
        public SpecsPack Add(Spec spec) { Items.Add(spec); return this; }
        public void Clear() { Items.Clear(); }
        public bool Contains(Spec spec) { return Items.Contains(spec); }
        public int IndexOf(Spec spec) { return Items.IndexOf(spec); }
        public void Insert(int index, Spec spec) { Items.Insert(index, spec); }
        public void RemoveAt(int index) { Items.RemoveAt(index); }
        public bool Remove(Spec spec) { return Items.Remove(spec); }

        #endregion

        #region Base Const

        public int StartCount { get; set; } = 350;

        public double XMax { get; set; } = 800;
        public double YMax { get; set; } = 800;
        public double XMin { get; set; } = 0;
        public double YMin { get; set; } = 0;

        public double VMax { get; set; } = 10;
        public double VMin { get; set; } = 0;

        public double DVMax { get; set; } = 5;
        public double DVMin { get; set; } = -5;

        public double DPhiMax { get; set; } = Math.PI * 2.0 / 4.0;
        public double DPhiMin { get; set; } = -Math.PI * 2.0 / 4.0;

        /// setup Base Const for Spec as method named Setup_BaseConst 
        /// with default values for method parameters
        public void Setup_BaseConst(double XMax = 800, double YMax = 800, double XMin = 0, double YMin = 0,
        double VMax = 10, double VMin = 0,
        double DVMax = 5, double DVMin = -5,
        double DPhiMax = Math.PI * 2.0 / 4.0, double DPhiMin = -Math.PI * 2.0 / 4.0)
        {
            this.XMax = XMax;
            this.YMax = YMax;
            this.XMin = XMin;
            this.YMin = YMin;
            this.VMax = VMax;
            this.VMin = VMin;
            this.DVMax = DVMax;
            this.DVMin = DVMin;
            this.DPhiMax = DPhiMax;
            this.DPhiMin = DPhiMin;

            CountOfSpecs.Clear();
            CountOfSpecs.Append(0);
            Clear();
        }

        public SpecsPack()
        {
            StartCount = 350;
            XMax = 800;
            YMax = 800;
            XMin = 0;
            YMin = 0;
            VMax = 10;
            VMin = 0;
            DVMax = 5;
            DVMin = -5;
            DPhiMax = Math.PI * 2.0 / 4;
            DPhiMin = -Math.PI * 2.0 / 4;

            CountOfSpecs.Clear();
            CountOfSpecs.Append(0);
            Clear();

        }

        #endregion

        #region Randoms

        public Random rnd = new MersenneTwister();
        public double[] UniformSample(double from, double to, int size)
        {
            double[] res = new double[size];
            ContinuousUniform.Samples(rnd, res, from, to);
            return res;
        }
        public double Uniform(double from, double to)
        {
            return ContinuousUniform.Sample(rnd, from, to);
        }

        public int Uniform(int from, int to)
        {
            return DiscreteUniform.Sample(rnd, from, to);
        }

        public double[] NormalSample(double mu, double sigma, int size)
        {
            double[] res = new double[size];
            Normal.Samples(rnd, res, mu, sigma);
            return res;
        }
        public double Normal1(double mu, double sigma)
        {
            return Normal.Sample(rnd, mu, sigma);
        }
        public double[] NormalSampleFromTo(double from, double to, int size)
        {
            double[] res = new double[size];
            Normal.Samples(rnd, res, (to + from) / 2.0, (-from + to) / 6);
            return res;
        }
        public double Normal1FromTo(double from, double to)
        {
            return Normal.Sample(rnd, (to + from) / 2.0, (-from + to) / 6);
        }
        public double Clip(double value, double from, double to)
        {
            if (value < from) return from; if (value > to) return to; return value;
        }
        public double Bend(double value, double from, double to)
        {
            if (value > to) return value - from;
            if (value < from) return to - value;
            return value;
        }
        public double[] NormalSampleClip(double mu, double sigma, int size,
            double from, double to)
        {
            double[] res = new double[size];
            Normal.Samples(rnd, res, mu, sigma);
            for (int i = 0; i < res.Count(); i++) res[i] = Clip(res[i], from, to);
            return res;
        }
        public double Normal1Clip(double mu, double sigma, int size,
        double from, double to)
        {
            double res = Normal.Sample(rnd, mu, sigma);
            return Clip(res, from, to);
        }

        #endregion

        #region Start

        public void Start()
        {
            /// заполняем пакет начальными значениями
            for (int i = 0; i < StartCount; i++)
            {
                Spec spec = new Spec();
                spec.X = Uniform(XMin, XMax);
                spec.Y = Uniform(YMin, YMax);
                spec.v = Uniform(VMin, VMax);
                spec.dv = Uniform(DVMin, DVMax);
                spec.phi = Uniform(DPhiMin, DPhiMax);
                spec.dphi = Uniform(DPhiMin, DPhiMax);

                Items.Add(spec);
            }
            CountOfSpecs.Add(Items.Count);
        }

        // Extract X as double array
        public double[] X()
        {
            double[] res = new double[Count];
            int i = 0;
            foreach (var item in Items)
            {
                res[i] = item.X;
                i++;
            }
            return res;
        }
        // Extract Y as double array
        public double[] Y()
        {
            double[] res = new double[Count];
            int i = 0;
            foreach (var item in Items)
            {
                res[i] = item.Y;
                i++;
            }
            return res;
        }

        #endregion

        #region Update

        /// update all coordinates
        public void UpdateXY()
        {
            double dv = 0;
            double dphi = 0;

            for (int i = 0; i < Count; i++)
            {
                dv = Normal1FromTo(DVMin, DVMax);
                dphi = Normal1FromTo(DPhiMin, DPhiMax);
                Items[i].Update(dv, dphi);
                Items[i].X = Bend(Items[i].X, XMin, XMax);
                Items[i].Y = Bend(Items[i].Y, YMin, YMax);
            }
        }
        /// update all v and generate new dv
        /// update all phi and generate new dfi

        /// <summary>
        /// if random value is lesser then DieRatio - beings is Die 
        /// if random value is bigger then BornRatio there is new spec
        /// otherwise - nothing
        /// </summary>
        public double BornRatio { get; set; } = 0.92;
        public double DieRatio { get; set; } = 0.07;

        public void BornOrDie()
        {
            // index of born or die (-1 - die, +1 born and 0 for nothing
            int[] index = new int[Count];
            double[] d = UniformSample(0, 1, Count);
            for (int i = 0; i < Count; i++)
            {
                if (d[i] < DieRatio) index[i] = -1; // die
                if (d[i] > BornRatio) index[i] = 1; // born
            }
            // copy to new list only living things
            List<Spec> specs = new List<Spec>();
            int born_count = 0;
            for (int i = 0; i < Count; i++)
            {
                if (index[i] > -1) specs.Add(Items[i]);
                if (index[i] == 1) born_count++;
            }
            // born
            for (int i = 0; i < born_count; i++)
            {
                Spec spec = new Spec();
                spec.X = Uniform(XMin, XMax);
                spec.Y = Uniform(YMin, YMax);
                spec.v = Uniform(VMin, VMax);
                spec.dv = Uniform(DVMin, DVMax);
                spec.phi = Uniform(DPhiMin, DPhiMax);
                spec.dphi = Uniform(DPhiMin, DPhiMax);

                specs.Add(spec);
            }
            Items = specs;
        }
        public double[] Distance2(Spec spec)
        {
            double []d2 = new double[Count];
            int i = 0;
            foreach (var item in Items)
            {
                d2[i++] = item.Dist2(spec);
            }
            return d2;
        }
        public double[,] Distance2(SpecsPack Other_specs) 
        {
            double[,] d2 = new double[Count,Other_specs.Count];
            for (int i = 0;i<Count;i++)
            {
                for (int j = 0; j < Other_specs.Count; j++)
                {
                    d2[i, j] = Items[i].Dist2(Other_specs.Items[j]);
                }
            }
            return d2;
        }
        public double R { get; set; } = 8;
        public double R2 => R * R;
        public bool[,] IsNear(SpecsPack Other_specs)
        {
            bool [,] d2 = new bool [Count, Other_specs.Count];
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Other_specs.Count; j++)
                {
                    d2[i, j] = Items[i].IsNear(Other_specs.Items[j],R);
                }
            }
            return d2;
        }

        /// <summary>
        /// взаимодействие между двумя видами
        /// </summary>
        /// <param name="Spec1"> кролики - жертвы </param>
        /// <param name="Spec2"> волки - хищники </param>
        /// <param name="bornRatio"> - вероятность рождения хищника от встречи с жертвой</param>
        /// <param name="dieRatio"> - вероятность смерти жертвы от встречи с хищником</param>
        public static void Interaction(SpecsPack Spec1, SpecsPack Spec2, 
            double bornRatio = 0.2, double dieRatio = 0.25)
        {
            var d2 = Spec1.Distance2(Spec2);
            var n2 = Spec1.IsNear(Spec2);
            var count_near = 0;
            foreach (var item in n2)
            {
                if (item) count_near++;
            }
            // убиваем кроликов
            // рождаем волков
        }

        #endregion
    }
}

