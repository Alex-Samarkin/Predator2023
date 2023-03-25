using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet;
using MathNet.Numerics;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using System.Numerics;

namespace Lotca2ClassLib
{
    public class RandGen
    {
        public int Seed { get; set; }
        public Random Rnd { get; set; } = new MathNet.Numerics.Random.MersenneTwister(0);
        public RandGen(int seed = 0) { Seed = seed; Rnd = new MathNet.Numerics.Random.MersenneTwister(Seed); }

        public double NextUniform(double from = 0, double to = 1) => ContinuousUniform.Sample(Rnd, from, to);
        public int NextUniform(int from = 0, int to = 100) => DiscreteUniform.Sample(Rnd, from, to);
        public double NextNormal(double mx=0, double sd=0) => Normal.Sample(Rnd, mx, sd);
        public double NextNormalFromTo(double from=0, double to=1) => NextNormal2Sigma((from + to) / 2.0, (to - from) / 4.0);
        public double Correct(double value, double from, double to)
        {
            if (value < from) return from;
            if (value > to) return to;
            return value;
        }
        public double NextNormal2Sigma(double mx, double sd)
        {
            var value = Normal.Sample(Rnd, mx, sd);
            return Correct(value, mx - 2.0 * sd, mx + 2.0 * sd);
        }
        public double NextExponential(double lambda = 1) => Exponential.Sample(Rnd, lambda);

        ///
        public double[] SetDoubles(int Count) => new double[Count];
        public double[] Uniforms (int count, double from = 0, double to = 1)
        {
            var res = SetDoubles(count);
            ContinuousUniform.Samples(Rnd,res, from, to);
            return res;
        }
        public int[] Uniforms(int count, int from = 0, int to = 100)
        {
            var res = new int[count];
            DiscreteUniform.Samples(Rnd, res, from, to);
            return res;
        }
        public double[] Normals(int count, double mx = 0, double sd = 1)
        {
            var res = SetDoubles(count);
            Normal.Samples(Rnd, res, mx, sd);
            return res;
        }
        public double[] Normals2Sigma(int count, double mx = 0, double sd = 1)
        {
            var res = SetDoubles(count);
            Normal.Samples(Rnd, res, mx, sd);
            for (int i = 0; i < res.Length; i++) res[i] = Correct(res[i], mx - 2.0 * sd, mx + 2.0 * sd);
            return res;
        }
        public double[] NormalsFromTo(int count, double from = 0, double to = 1)
        {
            return Normals2Sigma(count, (from+to)/2.0, (to-from)/4.0);
        }
        public double[] Exponentials(int count, double lambda = 1)
        {
            var res = SetDoubles(count);
            Exponential.Samples(Rnd, res, lambda);
            return res;
        }

    }
}
