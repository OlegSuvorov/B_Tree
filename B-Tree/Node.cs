using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    public class Node
    {
        public int t { get; set; }
        public Node[] children { get; set; }
        public int keysQty { get; set; }
        public Node parent { get; set; }
        public int[] keys { get; set; }
        public bool isLeaf { get; set; }
        public Node(int t, bool isLeaf)
        {
            this.t = t;
            this.children = new Node[2 * t];
            this.keys = new int[2 * t - 1];
            this.keysQty = 0;
            this.isLeaf = isLeaf;
            this.parent = null;
        }
    }
}

