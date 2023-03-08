using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotcaClassLib
{
    internal interface IListOperations<T>
    {
        List<LotcaClassLib.Spec> Items { get; set; }

        public int Count();
        public void Add(LotcaClassLib.Spec spec) { Items.Add(spec); }
        public void Clear() { Items.Clear(); }
        public bool Contains(LotcaClassLib.Spec spec) { return Items.Contains(spec); }
        public int IndexOf(LotcaClassLib.Spec spec) { return Items.IndexOf(spec); }
        public void Insert(int index, LotcaClassLib.Spec spec) { Items.Insert(index, spec); }
        public void RemoveAt(int index) { Items.RemoveAt(index); }
        public bool Remove(LotcaClassLib.Spec spec) { return Items.Remove(spec); }
    }
}
