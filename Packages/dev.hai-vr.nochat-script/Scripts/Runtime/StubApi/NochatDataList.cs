using System.Collections.Generic;

namespace VRC.SDK3.Data
{
    public class DataList
    {
        private readonly List<object> _list;

        public DataList(List<object> objects)
        {
            _list = new List<object>(objects);
        }

        public DataList()
        {
            _list = new List<object>();
        }

        public int Count => _list.Count;

        public void Add(object obj)
        {
            _list.Add(obj);
        }

        public void Remove(object obj)
        {
            _list.Remove(obj);
        }

        public DataToken this[int i]
        {
            get { return new DataToken(_list[i]); }
        }

        public DataList ShallowClone()
        {
            return new DataList(_list);
        }

        public void Clear()
        {
            _list.Clear();
        }
    }

    public class DataToken
    {
        private readonly object obj;

        public DataToken(object obj)
        {
            this.obj = obj;
        }

        public object Reference => obj;
        public DataDictionary DataDictionary { get => (DataDictionary)obj; }
    }
}