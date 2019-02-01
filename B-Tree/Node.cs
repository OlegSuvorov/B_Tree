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
        public Node<V> GetFirstLowestNode()
        {
            if (isLeaf)
                return this;
            return children[0].GetFirstLowestNode();
        }       
        public int FindPosition(V val)
        {
            int pos = 0;
            while (pos < keysQty - 1 && Match(pos, val) > 0)
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
            incompleteNode.FreePlaceForValAndChildrenInsert(0);                    
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
        public void FreePlaceForValAndChildrenInsert(int pos)
        {
            for (int i = keysQty + 1; i > pos; i--)
            {
                children[i] = children[i - 1];
                if (i != keysQty + 1)
                    keys[i] = keys[i - 1];
            }
            keysQty++;
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
            int childrenQty = keysQty + 1;
            for (int i = 0; i < siblingNode.keysQty + 1; i++)
            {
                children[i + childrenQty] = siblingNode.children[i];
                siblingNode.children[i].parent = this;
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
            if (Match(valuePosition, val) > 0)
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
                if (children[0] != null)   
                    fillRoot();
                return;
            }
            int nodePos = GetNodePosition(parent.children, this);            
            if (CheckRightSibling(nodePos))
            {
                ReplaceFromRightNode(parent, nodePos);
                return;
            }           
            if (CheckLeftSibling(nodePos))
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
            leftNode.CopyValuesFromAnotherNode(rightNode);          
            NodeDeleteChild(nodePos + 1);
            NodeDeleteVal(nodePos);
            CheckKeysQty();
        }
        private void fillRoot()
        {
            var child = children[0];
            CopyValuesFromAnotherNode(child);
            isLeaf = child.isLeaf;
            if (!child.isLeaf)
            {
                CopyChildrenFromAnotherNode(child);
            }            
        }
        private void CopyValuesFromAnotherNode(Node<V> anotherNode)
        {
            for (int i = 0; i < anotherNode.keysQty; i++)
            {
                keys[i + keysQty] = anotherNode.keys[i];                
            }
            keysQty += anotherNode.keysQty;
        }
        private void CopyChildrenFromAnotherNode(Node<V> anotherNode)
        {
            for (int i = 0; i <= anotherNode.keysQty; i++)
            {
                children[i] = anotherNode.children[i];
                children[i].parent = this;
            }
        }
        public Node<V> GetNextNode(V val)
        {
            int i = Match(0, val) > 0 ? 1 : 0;            
            return children[i];
        }
        public bool InsertNonFullNode(V val) // TBD refactoring
        {
            if (CheckDuplicate(val))
                return false;
            int pos = keysQty - 1;
            if (isLeaf)
            {
                while (pos >= 0 && Match(pos, val) < 0)
                {
                    keys[pos + 1] = keys[pos];
                    pos--;
                }
                keys[pos + 1] = val;
                keysQty++;
                return true;
            }
            while (pos >= 0 && Match(pos, val) < 0)
                pos--;
            if (children[pos + 1].keysQty == keys.Length)
            {
                children[pos + 1].SplitNode(pos + 1);
                if (Match(pos + 1, val) > 0)
                    pos++;
            }
            return children[pos + 1].InsertNonFullNode(val);
        }
        private bool CheckDuplicate(V val)
        {
            for (int i = 0; i < keysQty; i++)
            {
                if (Match(i, val) == 0)
                    return true;
            }
            return false;
        }
        public Node<V> SplitNode(int pos)
        {
            Node<V> newNode = new Node<V>(keys.Length, isLeaf);
            int MinAllowedChildrenQty = children.Length / 2;
            int MinAllowedKeysQty = (children.Length / 2) - 1;
            newNode.keysQty = MinAllowedKeysQty;
            newNode.parent = parent;
            SplitKeys(newNode, MinAllowedChildrenQty);           
            if (!isLeaf)
                SplitChildren(newNode, MinAllowedChildrenQty);
            keysQty = MinAllowedKeysQty;
            parent.RemoveKeyAndChildOnPosition(pos);
            parent.children[pos + 1] = newNode;
            parent.keys[pos] = keys[MinAllowedKeysQty];
            keys[MinAllowedKeysQty] = default(V);
            parent.keysQty++;
            return newNode;
        }
        public bool searchInNode(V val)
        {
            int pos = 0;
            int checkPos = 0;
            while (pos < keysQty && Match(pos, val) > 0)
                pos++;
            checkPos = pos;
            if (pos == keys.Length)
                checkPos--;
            if (Match(checkPos, val) == 0)
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
        private void SplitChildren(Node<V> siblingNode, int minQty)
        {
            for (int i = 0; i < minQty; i++)
            {
                siblingNode.children[i] = children[i + minQty];
                siblingNode.children[i].parent = siblingNode;
                children[i + minQty] = null;
            }
        }
        private void SplitKeys(Node<V> siblingNode, int minQty)
        {
            for (int i = 0; i < minQty - 1; i++)
            {
                siblingNode.keys[i] = keys[i + minQty];
                keys[i + minQty] = default(V);
            }
        }
        private void RemoveKeyAndChildOnPosition(int pos)
        {
            for (int i = keysQty; i >= pos + 1; i--)
            {
                children[i + 1] = children[i];
                if(i != pos + 1)
                    keys[i + 1] = keys[i];
            }
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
        public Node<V> TransformToChild()
        {
            Node<V> childRoot = new Node<V>(keys.Length, isLeaf);
            childRoot.CopyValuesFromAnotherNode(this);
            if (!isLeaf)
            {               
                childRoot.CopyChildrenFromAnotherNode(this);
                children = new Node<V>[children.Length];
            }
            keys = new V[keys.Length];
            keysQty = 0;
            children[0] = childRoot;
            childRoot.parent = this;
            isLeaf = false;
            return childRoot;
        }
        private int Match(int pos, V val)
        {
            return val.CompareTo(keys[pos]);
        }
    }
}
