using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    class B_Tree<V> where V : IComparable<V>
    {
        public Node<V> root { get; private set; }
        public int maxNodeSize { get; private set; }
        public B_Tree(int maxNodeSize)
        {
            if (maxNodeSize < 3)
            {
                Console.WriteLine(" Максимальный размер узла должен быть целым положительным числом не менее 3");
                return;
            }
            root = new Node<V>(maxNodeSize, true);
            this.maxNodeSize = maxNodeSize;
        }

        public bool Insert(V val)
        {
            if (root.keysQty == 0)
            {
                root.AddKey(val);              
                return true;
            }
            if (root.keysQty == maxNodeSize)
            {               
                Node<V> oldRoot = root.TransformToChild();
                Node<V> newRootChild = oldRoot.SplitNode(0);
                Node<V> NextNodeForInsert = root.GetNextNode(val);
                return NextNodeForInsert.InsertNonFullNode(val);
            }
            return root.InsertNonFullNode(val);
        }
        public bool Search(V val)
        {
            return root.searchInNode(val);
        }        
        public bool Delete(V val)
        {
            return root.DeleteInNode(val);
        }
    }
}
