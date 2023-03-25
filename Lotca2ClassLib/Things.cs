using FParsec;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotca2ClassLib
{
    public class Things
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "Items";
        public string Description { get; set; } = "Популяция";

        public List<Item> Items { get; set; } = new List<Item>();

        public int Count => Items.Count;

        public int Check(int index)
        {
            if (Items.Count == 0) return 0;
            if (Items.Count == 1) return 0;
            if(index<0) return 0;
            if(index>Items.Count) return Items.Count-1;
            return index;
        }

        public Item this[int index]
        {
            get
            {
                return Items[Check(index)];
            }
            set 
            { 
                Items[Check(index)] = value; 
            }
        }

        public Things() { }

        /// setup
        /// 

        public void SetupItems01()
        {
            /// generate items, fill coordinates, fill v, phi, dv, dpfi, mass and ages
            /// you need before set constant for it
            /// see intervals section

            StartCount.NormalValue(RandGenCoord);
            Items.Clear();

            for (int i = 0; i < StartCount.Value; i++) 
            { 
                /*
                var item = new Item();
                item.Id = i;
                item.Name = i.ToString();
                item.Mass = Masses.NormalValue(RandGenCoord);
                item.Age = (int)Ages.NormalValue(RandGenCoord);

                item.X = XInterval.UniformValue(RandGenCoord);
                item.Y = YInterval.UniformValue(RandGenCoord);

                item.V = VInterval.NormalValue(RandGenCoord);
                item.Phi = PhiInterval.UniformValue(RandGenCoord);

                item.dV = dVInterval.NormalValue(RandGenCoord);
                item.dPhi = dPhiInterval.NormalValue(RandGenCoord);
                */
                var item = ConstructItem(i);
                Items.Add(item);
            }

        }

        public Item ConstructItem(int i)
        {
            var item = new Item();
            item.Id = i;
            item.Name = i.ToString();
            item.Mass = Masses.NormalValue(RandGenCoord);
            item.Age = (int)Ages.NormalValue(RandGenCoord);

            item.X = XInterval.UniformValue(RandGenCoord);
            item.Y = YInterval.UniformValue(RandGenCoord);

            item.V = VInterval.NormalValue(RandGenCoord);
            item.Phi = PhiInterval.UniformValue(RandGenCoord);

            item.dV = dVInterval.NormalValue(RandGenCoord);
            item.dPhi = dPhiInterval.NormalValue(RandGenCoord);

            return item;
        }

        /// <summary>
        /// randgen for coordinates
        /// </summary>
        public RandGen RandGenCoord = new RandGen();
        /// start counts
        public Interval StartCount = new Interval() { From = 200, To = 1000, Value = 500 };
        /// coordinates
        public Interval XInterval = new Interval() { From = 0, To = 1000, Value = 500 };
        public Interval YInterval = new Interval() { From = 0, To = 1000, Value = 500 };
        /// mass
        public Interval Masses = new Interval() { From = 0.1, To = 10, Value = 5 };
        /// age
        public Interval Ages = new Interval() { From = 0, To = 30, Value = 25 };

        public Interval VInterval = new Interval() { From = 0, To = 10, Value = 3 };
        public Interval PhiInterval = new Interval() { From = -Math.PI, To = +Math.PI, Value = 0 };

        public Interval dVInterval = new Interval() { From = -0.5, To = 0.5, Value = 0 };
        public Interval dPhiInterval = new Interval() { From = -Math.PI/4, To = +Math.PI/4, Value = 0 };


        /// born or die
        ///
        public List<int> Status { get; set; } = new List<int>();

        /// <summary>
        /// normal (fully random) born and die level
        ///  from - prob of die (if generated value is lesser than) 
        ///  to - prob of got new born (if generated value is bigger than)
        ///  so by default
        ///    beings die at 100 steps
        ///    beings got new born every 50 steps (2/100%)
        /// </summary>
        public Interval01 BornOrDieInterval = new Interval01() { From = 0.005, To = 0.94, Value = 0.5};
        /// <summary>
        /// maximum Age for beings
        /// die ratio according by age is: 
        /// (MaxAge-Age)/MaxAxe - criterio
        /// if Age is bigger Crit goin near to 1
        /// we test it against uniform random from 0.5 to 1
        /// </summary>
        public int MaxAge = 300;

        /// <summary>
        /// randgen for coordinates
        /// </summary>
        public RandGen RandGenBorn = new RandGen();


        public void BornOrDie03()
        {
            Status.Clear();
            /// born or die
            /// 
            for (int i = 0; i < Items.Count; i++)
            {
                /// random dead 
                /// 

                /// get random value and test it against a borders
                var res = BornOrDieInterval.TestValue(RandGenBorn, true, true);
                /// die
                if (res == Interval01.TestResult.LesserThanFrom) Status.Add(-1);
                else
                {
                    /// born
                    if (res == Interval01.TestResult.BiggerThanTo) Status.Add(1);
                    else
                    /// still the same
                    Status.Add(0);
                }

                /// aged dead
                /// 
                var age_dead = RandGenBorn.NextUniform(0.0, 1.0);
                if(age_dead > (double)(MaxAge - Items[i].Age)/(double)MaxAge ) Status[i] = -2;

            }


            /// copy all livin things to new List
            /// 
            List<Item> items = new List<Item>();
            /// new born
            List<Item> items1 = new List<Item>();
            for (int i = 0;i < Items.Count;i++)
            {
                /// add existing living things
                if (Status[i]==0)
                { 
                    items.Add(Items[i]); 
                }
                /// if born
                if (Status[i] > 0)
                {
                    /// add parent
                    items.Add(Items[i]);
                    /// add new to new list
                    var item1 = ConstructItem(i);
                    items1.Add(item1);
                }
            }

            /// replace old
            /// 
            items.AddRange(items1);
            Items = items;

        }


        /// move
        ///
        public void Move02()
        {
            /// get dv, dpfi, set it to every item, update item
            /// 
            foreach (var item in Items)
            {
                var dv = dVInterval.NormalValue(RandGenCoord);
                var dpfi = dPhiInterval.NormalValue(RandGenCoord);
                item.Update(dv, dpfi);
                item.Age += 1;
            }
        }


        /// eat other
        /// 
    }
}
