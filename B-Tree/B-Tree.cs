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
        public bool Delete(V val)
        {
            return DeleteInNode(root, val);
        }
        private bool DeleteInNode(Node<V> node, V val)
        {
            int pos = FindPosition(node, val);
            if (node.keys[pos].CompareTo(val) == 0)
            {
                if (node.isLeaf)
                {
                    DeleteInLeaf(node, val);
                }
                else
                {
                    DeleteInNonLeaf(node, val);
                }
                return true;
            }
            else
            {
                if (node.isLeaf)
                {
                    return false;
                }
                else
                {
                    if (val.CompareTo(node.keys[pos]) > 0)
                    {
                        pos++;
                    }
                    return DeleteInNode(node.children[pos], val);
                }               
            }
        }
        private void DeleteInNonLeaf(Node<V> node, V val)
        {
            Node<V> LeafNode = FindNodeWithClosestVal(node, val);
            node.keys[Array.IndexOf(node.keys, val)] = LeafNode.keys[0];
            DeleteInLeaf(LeafNode, LeafNode.keys[0]);
        }
        private void DeleteInLeaf(Node<V> node, V val)
        {
            SimpleNodeDeleteVal(node, val);
            CheckKeysQty(node);
        }
        void CheckKeysQty(Node<V> node)
        {
            int minQty = (node.parent == null ? 1 : ((maxNodeSize + 1) / 2) - 1);
            if (node.keysQty >= minQty)
            {
                return;
            }
            else
            {
                if (node.parent == null)
                {
                    root = node.children[0];
                    root.parent = null;
                }
                else
                {
                    int nodePos = Array.IndexOf(node.parent.children, node);
                    Node<V> DonateNode = CheckDonator(node, nodePos);
                    if (DonateNode == null)
                    {
                        if ((nodePos + 1) < maxNodeSize && node.parent.children[nodePos + 1] != null)
                        {
                            MergeNodes(node, node.parent.children[nodePos + 1]);
                        }
                        else
                        {
                            MergeNodes(node.parent.children[nodePos - 1], node);
                        }
                    }
                    else
                    {
                        ReplaceFromDonateNode(node, DonateNode);
                    }
                }
               
            }
        }
        void MergeNodes(Node<V> node, Node<V> rightSiblingNode)
        {
            node.keys[node.keysQty] = node.parent.keys[Array.IndexOf(node.parent.children, node)];
            node.keysQty++;
            for (int i = node.keysQty, j = 0; j < rightSiblingNode.keysQty; i++, j++)
            {
                node.keys[i] = rightSiblingNode.keys[j];
                node.keysQty++;
            }
            for (int i = Array.IndexOf(node.parent.children, node); i < node.parent.keysQty - 1; i++)
            {
                node.parent.keys[i] = node.parent.keys[i + 1];
            }
            node.parent.keys[node.parent.keysQty - 1] = default(V);
            for (int i = Array.IndexOf(node.parent.children, node) + 1; i < node.parent.keysQty; i++)
            {
                node.parent.children[i] = node.parent.children[i + 1];
            }
            node.parent.children[node.parent.keysQty] = null;
            node.parent.keysQty--;
            // TBD replace children if they exist
        }
        void ReplaceFromDonateNode(Node<V> node, Node<V> nodeDonateNode)
        {
            if (Array.IndexOf(node.parent.children, nodeDonateNode) > Array.IndexOf(node.parent.children, node))
            {
                node.keys[node.keysQty] = node.parent.keys[Array.IndexOf(node.parent.children, node)];               
                node.parent.keys[Array.IndexOf(node.parent.children, node)] = nodeDonateNode.keys[0];
                SimpleNodeDeleteVal(nodeDonateNode, nodeDonateNode.keys[0]);
                // TBD replace children if they exist
            }
            else
            {
                for (int i = 0; i <= node.keysQty - 1; i++)
                {
                    node.keys[i + 1] = node.keys[i];
                }
                node.keys[0] = node.parent.keys[Array.IndexOf(node.parent.children, node) - 1];
                node.parent.keys[Array.IndexOf(node.parent.children, node) - 1] = nodeDonateNode.keys[nodeDonateNode.keysQty - 1];
                nodeDonateNode.keys[nodeDonateNode.keysQty - 1] = default(V);
                nodeDonateNode.keysQty--;
                // TBD replace children if they exist
            }
            node.keysQty++;
        }
        private void SimpleNodeDeleteVal(Node<V> node, V val)
        {
            int pos = Array.IndexOf(node.keys, val);
            for (int i = pos; i < node.keysQty - 1; i++)
            {
                node.keys[i] = node.keys[i + 1];
            }
            node.keys[node.keysQty - 1] = default(V);
            node.keysQty--;
        }
        Node<V> CheckDonator(Node<V> node, int pos)
        {
            Node<V> LeftSibling = pos != 0 ? node.parent.children[pos - 1] : null;
            Node<V> RightSibling = (pos + 1) != maxNodeSize ? node.parent.children[pos + 1] : null;

            if (RightSibling != null && RightSibling.keysQty >= (maxNodeSize + 1) / 2 &&
                (LeftSibling == null || LeftSibling.keysQty < (maxNodeSize + 1) / 2))
            {
                return RightSibling;
            }
            else if (LeftSibling != null && LeftSibling.keysQty >= (maxNodeSize + 1) / 2 && 
                    (RightSibling == null || RightSibling.keysQty < (maxNodeSize + 1) / 2))
            {
                return LeftSibling;
            }
            else if (RightSibling != null && RightSibling.keysQty >= (maxNodeSize + 1) / 2 &&
                    LeftSibling != null && LeftSibling.keysQty >= (maxNodeSize + 1) / 2)
            {
                return RightSibling.keysQty > LeftSibling.keysQty ? RightSibling : LeftSibling;
            }
            else
            {
                return null;
            }
        }
        Node<V> FindNodeWithClosestVal(Node<V> node, V val)
        {
            int pos = Array.IndexOf(node.keys, val);
            Node<V> foundNode = node.children[pos + 1];           
            return GetFirstLowestNode(foundNode);
        }
        private Node<V> GetFirstLowestNode(Node<V> node)
        {
            if (node.isLeaf)
            {
                return node;
            }
            return GetFirstLowestNode(node.children[0]);
        }
        private int FindPosition(Node<V> node, V val)
        {
            int pos = 0;
            while( pos < node.keysQty - 1 && node.keys[pos].CompareTo(val) < 0)
            {
                pos++;
            }
            return pos;
        }
    }
}
