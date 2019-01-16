using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    public class Node<V>
    {       
        public int t { get; set; }       
        public List<Node<V>> children { get; set; }       
        public List<V> keys { get; set; }
        public Node(int t)
        {
            this.t = t;
            this.children = new List<Node<V>>(t);
            this.keys = new List<V>();
        }
    }
}

