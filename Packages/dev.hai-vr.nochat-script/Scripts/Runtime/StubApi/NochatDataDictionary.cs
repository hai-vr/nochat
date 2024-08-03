using System.Collections.Generic;
using UnityEngine;

namespace VRC.SDK3.Data
{
    public class DataDictionary
    {
        public Dictionary<string, object> dictionary = new Dictionary<string, object>();
        public int Count => dictionary.Count;

        public DataToken this[string key]
        {
            get { return new DataToken(dictionary[key]); }
        }

        public void Add(string key, object value)
        {
            dictionary[key] = value;
        }
    }
}