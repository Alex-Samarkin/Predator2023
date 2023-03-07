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

namespace LotcaClassLib
{
    public class SpecsPack
    {
        public List<Spec> SpecList = new List<Spec>();
        #region List
        public int Count => SpecList.Count;
        public SpecsPack Add(Spec spec) { SpecList.Add(spec); return this; }
        public void Clear() { SpecList.Clear(); }
        public bool Contains(Spec spec) {  return SpecList.Contains(spec); }
        public int IndexOf(Spec spec) { return SpecList.IndexOf(spec); }
        public void Insert(int index, Spec spec) {  SpecList.Insert(index, spec); }
        public void RemoveAt(int index) {  SpecList.RemoveAt(index); }
        public bool Remove(Spec spec) { return SpecList.Remove(spec); }
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
        public double DVMin { get; set; } = 0;

        public double DPhiMax { get; set; } = Math.PI * 2.0 / 4;
        public double DPhiMin { get; set; } = -Math.PI * 2.0 / 4;

        #endregion

        #region Randoms

        public Random rnd = new MersenneTwister();
        public double[] UniformSample(double from, double to, int size) 
        {
            double[] res = new double[size];
            ContinuousUniform.Samples(rnd,res,from,to);
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
            Normal.Samples(rnd,res,mu,sigma);
            return res;
        }
        public double[] NormalSampleFromTo(double from, double to, int size)
        {
            double[] res = new double[size];
            Normal.Samples(rnd, res, (to+from)/2.0, (from-to));
            return res;
        }
        public double Clip(double value, double from, double to)
        {
            if (value < from ) return from; if (value > to) return to; return value;
        }
        public double Bend(double value, double from, double to)
        { 
            if (value > to ) return value - from; 
            if (value < from ) return to - value;
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


        #endregion

    }
}
