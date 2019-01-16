using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    public class Key<V> : IEquatable<Key<V>>
    {
        // Значение ключа
        public V value { get; set; }
       
        // Сравниваем два ключа
        public bool Equals(Key<V> otherKey)
        {
            return this.value.Equals(otherKey.value);
        }
    }
}
