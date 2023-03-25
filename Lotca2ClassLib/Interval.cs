using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotca2ClassLib
{
    public class Interval
    {
        public double From { get; set; } = 0;
        public double To { get; set; } = 1;
        public double Value { get; set; } = 0.5;

        public virtual double UniformValue(RandGen randGen)
        {
            Value = randGen.NextUniform(From,To);
            return Value;
        }
        public virtual double NormalValue(RandGen randGen)
        {
            Value = randGen.NextNormalFromTo(From, To);
            return Value;
        }
    }

    public class Interval01 : Interval 
    { 
        public override double UniformValue(RandGen randGen)
        {
            Value = randGen.NextUniform(0.0, 1.0);
            return Value;
        }
        public override double NormalValue(RandGen randGen)
        {
            Value = randGen.NextNormalFromTo(0.0, 1.0);
            return Value;
        }

        public enum TestResult
        {
            LesserThanFrom = -1,
            BetweenFromAndTo = 0,
            BiggerThanTo =1
        }

        public TestResult TestValue(RandGen randGen,bool GenerateNew = false, bool GetUniform = false)
        {
            if (GenerateNew) 
            { 
                if(GetUniform)
                {
                    UniformValue(randGen);
                }
                else
                {
                    NormalValue(randGen);
                }
            }
            if ( Value<From ) return TestResult.LesserThanFrom;
            if ( Value>To ) return TestResult.BiggerThanTo;
            return TestResult.BetweenFromAndTo;
        }


    }
}
