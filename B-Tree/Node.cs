using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    public class Node<V>
    {
        public Node<V>[] children { get; set; }
        public int keysQty { get; set; }
        public Node<V> parent { get; set; }
        public V[] keys { get; set; }
        public bool isLeaf { get; set; }
        public Node(int maxNodeSize, bool isLeaf)
        {
            children = new Node<V>[maxNodeSize + 1];
            keys = new V[maxNodeSize];
            keysQty = 0;
            this.isLeaf = isLeaf;
            parent = null;
        }
    }
}

