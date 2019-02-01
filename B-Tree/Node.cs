using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    public class Node<V> where V : IComparable<V>
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
        public Node<V> FindNodeWithClosestVal(V val)
        {
            int pos = GetValPosition(keys, val);
            Node<V> foundNode = children[pos + 1];
            return foundNode.GetFirstLowestNode();
        }
        public void NodeDeleteVal(int valPosition)
        {            
            for (int i = valPosition; i < keysQty - 1; i++)
            {
                keys[i] = keys[i + 1];
            }
            keys[keysQty - 1] = default(V);
            keysQty--;
        }
        public Node<V> GetFirstLowestNode()
        {
            if (isLeaf)
                return this;
            return children[0].GetFirstLowestNode();
        }       
        public int FindPosition(V val)
        {
            int pos = 0;
            while (pos < keysQty - 1 && keys[pos].CompareTo(val) < 0)
                pos++;
            return pos;
        }
        public bool CheckValExistence(V val)
        {            
            return GetValPosition(keys, val) > -1;
        }
        public void ReplaceFromRightNode(Node<V> node, int nodePos)
        {
            var incompleteNode = node.children[nodePos];
            var donateNode = node.children[nodePos + 1];
            incompleteNode.keys[keysQty] = node.keys[nodePos];
            node.keys[nodePos] = donateNode.keys[0];
            incompleteNode.keysQty++;
            if (!incompleteNode.isLeaf)
            {
                incompleteNode.children[keysQty] = donateNode.children[0];
                donateNode.children[0].parent = this;
                donateNode.NodeDeleteChild(0);
            }
            donateNode.NodeDeleteVal(0);
        }
        public void ReplaceFromLeftNode(Node<V> node, int nodePos)
        {
            var incompleteNode = node.children[nodePos];
            var donateNode = node.children[nodePos - 1];
            incompleteNode.NodeFreeChildrenPosition(0);
            incompleteNode.NodeFreeValPosition(0);            
            incompleteNode.keys[0] = node.keys[nodePos - 1];
            node.keys[nodePos - 1] = donateNode.keys[donateNode.keysQty - 1];
            incompleteNode.children[0] = donateNode.children[donateNode.keysQty];            
            donateNode.children[donateNode.keysQty] = null;
            donateNode.keys[donateNode.keysQty - 1] = default(V);
            donateNode.keysQty--;            
        }
        public void NodeDeleteChild(int pos)
        {            
            for (int i = pos; i < keysQty; i++)
            {
                children[i] = children[i + 1];
            }
            children[keysQty] = null;           
        }
        public void NodeFreeValPosition(int pos)
        {
            for (int i = keysQty; i > pos; i--)
            {
                keys[i] = keys[i - 1];
            }
            keysQty++;
        }
        public void NodeFreeChildrenPosition(int pos)
        {
            for (int i = keysQty + 1; i > pos; i--)
            {
                children[i] = children[i - 1];
            }
        }
        public void AddKey(V key)
        {
            keys[keysQty] = key;
            keysQty++;
        }
        public bool CheckRightSibling(int nodePos)
        {
            return nodePos != parent.keysQty && parent.children[nodePos + 1].keysQty >= children.Length / 2;
        }
        public bool CheckLeftSibling(int nodePos)
        {
            return nodePos != 0 && parent.children[nodePos - 1].keysQty >= children.Length / 2;
        }
        public void MergeChildren(Node<V> siblingNode)
        {
            for (int i = keysQty + 1, j = 0; j < siblingNode.keysQty + 1; i++, j++)
            {
                children[i] = siblingNode.children[j];
                siblingNode.children[j].parent = this;
            }
        }
        public bool DeleteInNode(V val)
        {
            bool valueExistInNode = CheckValExistence(val);
            if (valueExistInNode)
            {
                if (isLeaf)
                    DeleteInLeaf(val);
                else
                    DeleteInNonLeaf(val);
                return true;
            }
            return DeleteDeeperInNode(val);
        }
        private bool DeleteDeeperInNode(V val)
        {
            if (isLeaf)
                return false;
            int valuePosition = FindPosition(val);
            if (val.CompareTo(keys[valuePosition]) > 0)
                valuePosition++;
            return children[valuePosition].DeleteInNode(val);
        }
        private void DeleteInNonLeaf(V val)
        {
            Node<V> LeafNode = FindNodeWithClosestVal(val);
            keys[GetValPosition(keys, val)] = LeafNode.keys[0];
            LeafNode.DeleteInLeaf(LeafNode.keys[0]);
        }
        private void DeleteInLeaf(V val)
        {
            int pos = GetValPosition(keys, val);
            NodeDeleteVal(pos);
            CheckKeysQty();
        }
        private void CheckKeysQty()
        {
            int minQty = (parent == null ? 1 : (children.Length / 2) - 1);
            if (keysQty >= minQty)
                return;

            if (parent == null)
            {
                if (children[0] == null)
                {
                    return;
                }                    
                fillRoot();
                return;
            }
            int nodePos = GetNodePosition(parent.children, this);
            bool isFullRightSibling = CheckRightSibling(nodePos);
            if (isFullRightSibling)
            {
                ReplaceFromRightNode(parent, nodePos);
                return;
            }
            bool isFullLeftSibling = CheckLeftSibling(nodePos);
            if (isFullLeftSibling)
            {
                ReplaceFromLeftNode(parent, nodePos);
                return;
            }
            parent.MergeNodes(nodePos);
        }
        void MergeNodes(int nodePos)
        {
            if (nodePos == keysQty)
                nodePos--;
            var leftNode = children[nodePos];
            var rightNode = children[nodePos + 1];
            if (!leftNode.isLeaf)
            {
                leftNode.MergeChildren(rightNode);
            }
            leftNode.AddKey(keys[nodePos]);
            for (int j = 0; j < rightNode.keysQty; j++)
            {
                leftNode.AddKey(rightNode.keys[j]);
            }
            NodeDeleteChild(nodePos + 1);
            NodeDeleteVal(nodePos);
            CheckKeysQty();
        }
        private void fillRoot()
        {
            var child = children[0];
            for (int i= 0; i < child.keysQty; i++)
            {
                keys[i] = child.keys[i];
            }
            keysQty = child.keysQty;
            isLeaf = child.isLeaf;
            if (!child.isLeaf)
            {
                for (int i= 0; i < child.keysQty + 1; i++)
                {                
                    children[i] = child.children[i];
                    children[i].parent = this;
                }
            }            
        }
        public Node<V> GetNextNode(V val)
        {
            int i = 0;
            if (val.CompareTo(keys[0]) > 0)
                i++;
            return children[i];
        }
        public bool InsertNonFullNode(V val)
        {
            if (CheckDuplicate(val))
            {
                return false;
            }

            int pos = keysQty - 1;

            if (isLeaf)
            {
                while (pos >= 0 && val.CompareTo(keys[pos]) < 0)
                {
                    keys[pos + 1] = keys[pos];
                    pos--;
                }
                keys[pos + 1] = val;
                keysQty++;
                return true;
            }
            else
            {
                while (pos >= 0 && val.CompareTo(keys[pos]) < 0)
                {
                    pos--;
                }

                if (children[pos + 1].keysQty == keys.Length)
                {
                    children[pos + 1].SplitNode(pos + 1);
                    if (val.CompareTo(keys[pos + 1]) > 0)
                    {
                        pos++;
                    }
                }
                return children[pos + 1].InsertNonFullNode(val);
            }
        }
        private bool CheckDuplicate(V val)
        {
            for (int i = 0; i < keysQty; i++)
            {
                if (val.CompareTo(keys[i]) == 0)
                    return true;
            }
            return false;
        }
        public Node<V> SplitNode(int pos)
        {
            Node<V> newNode = new Node<V>(keys.Length, isLeaf);
            newNode.keysQty = (children.Length / 2) - 1;
            newNode.parent = parent;

            for (int i = 0; i < (children.Length / 2) - 1; i++)
            {
                newNode.keys[i] = keys[i + (children.Length / 2)];
                keys[i + (children.Length / 2)] = default(V);
            }

            if (!isLeaf)
            {
                for (int i = 0; i < (children.Length / 2); i++)
                {
                    newNode.children[i] = children[i + (children.Length / 2)];
                    newNode.children[i].parent = newNode;
                    children[i + (children.Length / 2)] = null;
                }
            }

            keysQty = (children.Length / 2) - 1;

            for (int i = parent.keysQty; i >= pos + 1; i--)
            {
                parent.children[i + 1] = parent.children[i];
            }
            parent.children[pos + 1] = newNode;
            for (int i = parent.keysQty - 1; i >= pos; i--)
            {
                parent.keys[i + 1] = parent.keys[i];
            }
            parent.keys[pos] = keys[(children.Length / 2) - 1];
            keys[(children.Length / 2) - 1] = default(V);
            parent.keysQty++;
            return newNode;
        }
        public bool searchInNode(V val)
        {
            int pos = 0;
            int checkPos = 0;
            while (pos < keysQty && val.CompareTo(keys[pos]) > 0)
                pos++;
            checkPos = pos;
            if (pos == keys.Length)
                checkPos--;
            if (val.CompareTo(keys[checkPos]) == 0)
                return true;
            if (isLeaf)
                return false;
            return children[pos].searchInNode(val);
        }
        private int GetValPosition(Array arr, V val)
        {
            return Array.IndexOf(arr, val);
        }
        private int GetNodePosition(Array arr, Node<V> node)
        {
            return Array.IndexOf(arr, node);
        }
    }
}

