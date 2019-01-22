using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    class B_Tree
    {
        public Node root { get; private set; }
        public int t { get; private set; }
        public B_Tree(int t)
        {
            if (t < 2)
            {
                Console.WriteLine(" Размерность должна быть целым положительным числом не менее 2");
                return;
            }
            root = new Node(t, true);
            this.t = t;
        }        
        
        public bool Insert(int val)
        {    
            if (root.keysQty == 0)
            {
                root.keys[0] = val;
                root.keysQty++;
                return true;
            }

            if (root.keysQty == 2 * t - 1)
            {
                Node newRoot = new Node(t, false);
                newRoot.children[0] = root;
                root.parent = newRoot;
                Node newNode = SplitNode(root, 0);
                int i = 0;
                if (val > newNode.keys[0])
                {
                    i++;
                }
                root = newRoot;
                return InsertNonFullNode(newRoot.children[i], val);
            }

            return InsertNonFullNode(root, val);
        }        
        private bool InsertNonFullNode(Node node, int val)
        {           
            int pos = node.keysQty - 1;
            
            if (node.isLeaf)
            {
                while (pos >= 0 && node.keys[pos] > val)
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
                while (pos >= 0 && node.keys[pos] > val)
                {
                    pos--;
                }
                    
                if (node.children[pos + 1].keysQty == 2 * t - 1)
                { 
                    SplitNode(node.children[pos + 1], pos + 1);
                    if (node.keys[pos + 1] < val)
                    {
                        pos++;
                    } 
                }
                return InsertNonFullNode(node.children[pos + 1], val);
            }
        }        
        private Node SplitNode(Node node, int pos)
        {
            Node newNode = new Node(t, node.isLeaf);
            newNode.keysQty = t - 1;
            newNode.parent = node.parent;

            for (int i = 0; i < t - 1; i++)
            {
                newNode.keys[i] = node.keys[i + t];
                node.keys[i + t] = 0;
            }                

            if (!node.isLeaf)
            {
                for (int i = 0; i < t; i++)
                {
                    newNode.children[i] = node.children[i + t];
                    newNode.children[i].parent = newNode;
                    node.children[i + t] = null;
                }
            }

            node.keysQty = t - 1;

            for (int i = node.parent.keysQty; i >= pos + 1; i--)
            {
                node.parent.children[i + 1] = node.parent.children[i];
            }                

            node.parent.children[pos + 1] = newNode;

            for (int i = node.parent.keysQty - 1; i >= pos; i--)
            {
                node.parent.keys[i + 1] = node.parent.keys[i];
            }                
           
            node.parent.keys[pos] = node.keys[t - 1];
            node.keys[t - 1] = 0;
            node.parent.keysQty++;

            return newNode;
        }
        public bool Search(int val)
        {
            return searchInNode(root, val);
        }
        private bool searchInNode(Node node, int val)
        {
            int pos = 0;
            int checkPos = 0;
            while (pos < node.keysQty && val > node.keys[pos])
            {
                pos++;
            }

            checkPos = pos;

            if (pos == 2 * t - 1)
            {
                checkPos--;
            }

            if (node.keys[checkPos] == val)
            {
                return true;
            }

            if (node.isLeaf)
            {
                return false;
            }                

            return searchInNode(node.children[pos], val);
        }
        public void Delete(int value)
        {
            // TBD метод для удаления ключей
        }
    }
}
