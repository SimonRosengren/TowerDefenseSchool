using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerDefenseAlgorithm
{
    class Entry
    {
        object key;
        public object value;
        public Entry(object key, object value)
        {
            this.key = key;
            this.value = value;
        }
        public override bool Equals(object obj)
        {
            Entry keyValue = (Entry)obj;
            return key.Equals(keyValue.key);
        }
    }
}
