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
            int pos = Array.IndexOf(keys, val);
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
            {
                return this;
            }
            return children[0].GetFirstLowestNode();
        }       
        public int FindPosition(V val)
        {
            int pos = 0;
            while (pos < keysQty - 1 && keys[pos].CompareTo(val) < 0)
            {
                pos++;
            }
            return pos;
        }
        public bool CheckValExistence(V val)
        {            
            return Array.IndexOf(keys, val) >= 0;
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
    }
}

