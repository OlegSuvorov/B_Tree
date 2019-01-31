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
            bool valueExistInNode = node.CheckValExistence(val);
            if (valueExistInNode)
            {
                DeleteInsideNode(node, val);
                return true;
            }
            return DeleteDeeperInNode(node, val);
        }
        private void DeleteInsideNode(Node<V> node, V val)
        {
            if (node.isLeaf) DeleteInLeaf(node, val);
            else DeleteInNonLeaf(node, val);
        }
        private bool DeleteDeeperInNode(Node<V> node, V val)
        {
            if (node.isLeaf) return false;
            int valuePosition = node.FindPosition(val);
            if (val.CompareTo(node.keys[valuePosition]) > 0)
                valuePosition++;
            return DeleteInNode(node.children[valuePosition], val);
        }
        private void DeleteInNonLeaf(Node<V> node, V val)
        {
            Node<V> LeafNode = node.FindNodeWithClosestVal(val);
            node.keys[Array.IndexOf(node.keys, val)] = LeafNode.keys[0];
            DeleteInLeaf(LeafNode, LeafNode.keys[0]);
        }
        private void DeleteInLeaf(Node<V> node, V val)
        {
            int pos = Array.IndexOf(node.keys, val);
            node.NodeDeleteVal(pos);
            CheckKeysQty(node);
        }
        void CheckKeysQty(Node<V> node)
        {
            int minQty = (node.parent == null ? 1 : ((maxNodeSize + 1) / 2) - 1);
            if (node.keysQty >= minQty)
                return;
            
            if (node.parent == null)
            {
                if (node.children[0] == null)
                    return;
                root = node.children[0];
                root.parent = null;
                return;
            }
            int nodePos = Array.IndexOf(node.parent.children, node);
            bool isFullRightSibling = node.CheckRightSibling(nodePos);
            bool isFullLeftSibling = node.CheckLeftSibling(nodePos); ;

            if (isFullRightSibling)
                node.ReplaceFromRightNode(node.parent, nodePos);
            else if (isFullLeftSibling)
                node.ReplaceFromLeftNode(node.parent, nodePos);
            else
                MergeNodes(node.parent, nodePos);
        }
        void MergeNodes(Node<V> node, int nodePos)
        {
            if (nodePos == node.keysQty)
                nodePos--;
            var leftNode = node.children[nodePos];
            var rightNode = node.children[nodePos + 1];
            if (!leftNode.isLeaf)
            {
                leftNode.MergeChildren(rightNode);
            }
            leftNode.AddKey(node.keys[nodePos]);
            for (int i = leftNode.keysQty, j = 0; j < rightNode.keysQty; i++, j++)
            {
                leftNode.AddKey(rightNode.keys[j]);
            }
            node.NodeDeleteChild(nodePos + 1);
            node.NodeDeleteVal(nodePos);
            CheckKeysQty(node);            
        }
    }
}
