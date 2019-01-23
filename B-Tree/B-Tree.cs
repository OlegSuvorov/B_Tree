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
                root.keys[0] = val;
                root.keysQty++;
                return true;
            }

            if (root.keysQty == maxNodeSize)
            {
                Node<V> newRoot = new Node<V>(maxNodeSize, false);
                newRoot.children[0] = root;
                root.parent = newRoot;
                Node<V> newNode = SplitNode(root, 0);
                int i = 0;
                if (val.CompareTo(newNode.keys[0]) > 0)
                {
                    i++;
                }
                root = newRoot;
                return InsertNonFullNode(newRoot.children[i], val);
            }

            return InsertNonFullNode(root, val);
        }        
        private bool InsertNonFullNode(Node<V> node, V val)
        {
            if (CheckDuplicate(node, val))
            {
                return false;
            }

            int pos = node.keysQty - 1;            
            
            if (node.isLeaf)
            {
                while (pos >= 0 && val.CompareTo(node.keys[pos]) < 0)
                {
                    node.keys[pos + 1] = node.keys[pos];
                    pos--;
                }
                node.keys[pos + 1] = val;
                node.keysQty++;
                return true;
            }
            else
            {
                while (pos >= 0 && val.CompareTo(node.keys[pos]) < 0)
                {
                    pos--;
                }               

                if (node.children[pos + 1].keysQty == maxNodeSize)
                { 
                    SplitNode(node.children[pos + 1], pos + 1);
                    if (val.CompareTo(node.keys[pos + 1]) > 0)
                    {
                        pos++;
                    } 
                }
                return InsertNonFullNode(node.children[pos + 1], val);
            }
        }        
        private Node<V> SplitNode(Node<V> node, int pos)
        {
            Node<V> newNode = new Node<V>(maxNodeSize, node.isLeaf);
            newNode.keysQty = ((maxNodeSize + 1) / 2) - 1;
            newNode.parent = node.parent;

            for (int i = 0; i < ((maxNodeSize + 1) / 2) - 1; i++)
            {
                newNode.keys[i] = node.keys[i + ((maxNodeSize + 1) / 2)];
                node.keys[i + ((maxNodeSize + 1) / 2)] = default(V);
            }                

            if (!node.isLeaf)
            {
                for (int i = 0; i < ((maxNodeSize + 1) / 2); i++)
                {
                    newNode.children[i] = node.children[i + ((maxNodeSize + 1) / 2)];
                    newNode.children[i].parent = newNode;
                    node.children[i + ((maxNodeSize + 1) / 2)] = null;
                }
            }

            node.keysQty = ((maxNodeSize + 1) / 2) - 1;

            for (int i = node.parent.keysQty; i >= pos + 1; i--)
            {
                node.parent.children[i + 1] = node.parent.children[i];
            }                

            node.parent.children[pos + 1] = newNode;

            for (int i = node.parent.keysQty - 1; i >= pos; i--)
            {
                node.parent.keys[i + 1] = node.parent.keys[i];
            }                
           
            node.parent.keys[pos] = node.keys[((maxNodeSize + 1) / 2) - 1];
            node.keys[((maxNodeSize + 1) / 2) - 1] = default(V);
            node.parent.keysQty++;

            return newNode;
        }
        private bool CheckDuplicate(Node<V> node, V val)
        {
            for (int i = 0; i < node.keysQty; i++)
            {
                if (val.CompareTo(node.keys[i]) == 0)
                {
                    return true;
                }
            }
            return false;
        }      
        public bool Search(V val)
        {
            return searchInNode(root, val);
        }
        private bool searchInNode(Node<V> node, V val)
        {
            int pos = 0;
            int checkPos = 0;
            while (pos < node.keysQty && val.CompareTo(node.keys[pos]) > 0)
            {
                pos++;
            }

            checkPos = pos;

            if (pos == maxNodeSize)
            {
                checkPos--;
            }

            if (val.CompareTo(node.keys[checkPos]) == 0)
            {
                return true;
            }

            if (node.isLeaf)
            {
                return false;
            }                

            return searchInNode(node.children[pos], val);
        }
    }
}
