using FParsec;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
            if (index < 0) return 0;
            if (index > Items.Count) return Items.Count - 1;
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

        public List<int> CountPerTime = new List<int>();

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
            CountPerTime.Clear();
            CountPerTime.Add(Count);
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
        public Interval dPhiInterval = new Interval() { From = -Math.PI / 4, To = +Math.PI / 4, Value = 0 };


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
        public Interval01 BornOrDieInterval = new Interval01() { From = 0.05, To = 0.94, Value = 0.5 };

        /// <summary>
        /// interval, whre aged dead is started
        /// </summary>
        Interval AgeOfDead = new Interval() { From = 30, To = 100, Value = 80 };


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
                Status.Add(0);
                /// get random value and test it against a borders
                var res = BornOrDieInterval.TestValue(RandGenBorn, true, true);
                /// die
                if (res == Interval01.TestResult.LesserThanFrom) Status[i] = -1;
                else
                {
                    /// born
                    if (res == Interval01.TestResult.BiggerThanTo) Status[i] = 1;
                }

                /// aged dead
                /// 
                if (Items[i].Age > AgeOfDead.UniformValue(RandGenBorn)) Status[i] = -2;
            }


            /// copy all livin things to new List
            /// 
            List<Item> items = new List<Item>();
            /// new born
            List<Item> items1 = new List<Item>();
            for (int i = 0; i < Items.Count; i++)
            {
                /// add existing living things
                if (Status[i] >= 0)
                {
                    items.Add(Items[i]);
                    /// if born
                    if (Status[i] == 1)
                    {
                        /// add new to new list
                        var item1 = ConstructItem(i);
                        items1.Add(item1);
                    }

                }
            }

            /// replace old
            /// 
            items.AddRange(items1.ToArray()); /// add born 
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

                /// increment Age
                item.Age += 1;
            }
        }

        /// <summary>
        /// radius to find near
        /// </summary>
        public double R { get; set; } = 10;

        /// <summary>
        /// if lesser than from - nothing
        /// between - others die
        /// bigger than to - this items got new born
        /// </summary>
        public Interval01 EatInterval = new Interval01() { From = 0.25, To = 0.8, Value = 0.6 };


        /// <summary>
        /// list of first near other things
        /// </summary>
        public List<int> Near = new List<int>();

        /// <summary>
        /// rand gen for eat
        /// </summary>
        public RandGen RandGenEat { get; set; } = new RandGen();

        /// eat other
        /// <summary>
        /// eat others
        /// try to find nearest others from things
        /// place its index to list of nearest
        /// try to eat other (and therefore exclude other by index)
        /// try to born (and add new item)
        /// </summary>
        /// <param name="things">others</param>
        /// 
        public void Eat04(Things things)
        {
            Near.Clear();
            /// find near others
            /// 
            for (int i = 0; i < Count; i++)
            {
                var item = Items[i];
                Near.Add(-1);
                for (int j = 0; j < things.Count; j++)
                {
                    var other = things[j];
                    if (item.IsNear(other, R))
                    {
                        Near[i] = j;
                        // Trace.WriteLine($"For i={i} find near j={j}");
                        break;
                    }
                }
            }

            /// Trace.WriteLine($"{Count} {Near.Count}");

            /// try to eat and born new
            ///
            List<int> indexToDelete = new List<int>();
            /// new born
            List<Item> items1 = new List<Item>();

            for (int i = 0; i < Near.Count; i++)
            {
                if (Near[i] == -1) continue;
                var res = EatInterval.TestValue(RandGenEat, true, true);
                if (res == Interval.TestResult.LesserThanFrom) continue;

                if (res == Interval.TestResult.BetweenFromAndTo)
                {
                    /// Trace.WriteLine($"{i} -> Eat {Near[i]}");
                    /// mark others to delete
                    indexToDelete.Add(Near[i]);
                }
                else
                {
                    /// Trace.WriteLine($"{i} -> Eat {Near[i]} TryToBorn new");
                    /// mark others to delete
                    indexToDelete.Add(Near[i]);
                    /// add newborn
                    var item = ConstructItem(i);
                    items1.Add(item);
                }
            }

            /// try to eat and delete others
            /// 
            Trace.WriteLine($"------ Add newborn ({items1.Count})");
            this.Items.AddRange(items1.ToArray()); /// add newborn

            /// remove all, that marked for delete
            Trace.WriteLine($"------ Die from others ({indexToDelete.Count})");
            indexToDelete.Sort();
            indexToDelete.Reverse();
            var itd = indexToDelete.Distinct();
            foreach (var index in itd)
            {
                things.Items.RemoveAt(index);
            }
        }

        public void Update()
        {
            Move02();
            BornOrDie03();
            CountPerTime.Add(Count);
        }

        public int UpdateCountsPerTime()
        {
            CountPerTime[CountPerTime.Count - 1] = Count;
            return CountPerTime.Count; 
        }

        private List<double> _x = new List<double>();
        public List<double> X
        { get
            {
                _x.Clear();
                foreach (var item in Items)
                    _x.Add(item.X);
                return _x;
            }
        }
        private List<double> _y = new List<double>();
        public List<double> Y
        {
            get
            {
                _y.Clear();
                foreach (var item in Items)
                    _y.Add(item.Y);
                return _y;
            }
        }
    }
}
