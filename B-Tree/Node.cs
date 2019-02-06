using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public Node(int maxNodeSize, bool isLeaf) {
            children = new Node<V>[maxNodeSize + 1];
            keys = new V[maxNodeSize];
            keysQty = 0;
            this.isLeaf = isLeaf;
            parent = null;
        }       
        private Node<V> GetLeafNode() {
            if (isLeaf)
                return this;
            return children[0].GetLeafNode();
        }       
        private void ReplaceFromRightNode(Node<V> node, int nodePos) {
            var incompleteNode = node.children[nodePos];
            var donateNode = node.children[nodePos + 1];
            incompleteNode.keys[keysQty] = node.keys[nodePos];
            incompleteNode.keysQty++;

            node.keys[nodePos] = donateNode.keys[0];
            if (!incompleteNode.isLeaf)
            {
                incompleteNode.children[keysQty] = donateNode.children[0];
                donateNode.children[0].parent = this;
                donateNode.NodeDeleteChild(0);
            }
            donateNode.NodeDeleteVal(0);
        }
        private void ReplaceFromLeftNode(Node<V> node, int nodePos) {            
            var donateNode = node.children[nodePos - 1];
            FillFromDonateNode(donateNode, nodePos);
            node.keys[nodePos - 1] = donateNode.keys[donateNode.keysQty - 1];
            donateNode.DeleteLastKeyAndChild();
        }
        private void NodeDeleteChild(int pos) {
            for (int i = pos; i < keysQty; i++)
            {
                children[i] = children[i + 1];
            }
            children[keysQty] = null;
        }
        private void FillFromDonateNode(Node<V> donateNode, int pos) {
            for (int i = keysQty + 1; i > 0; i--)
            {
                children[i] = children[i - 1];
                if (i != keysQty + 1)
                    keys[i] = keys[i - 1];
            }
            keysQty++;
            keys[0] = parent.keys[pos - 1];
            children[0] = donateNode.children[donateNode.keysQty];
        }
        private void DeleteLastKeyAndChild() {
            children[keysQty] = null;
            keys[keysQty - 1] = default(V);
            keysQty--;
        }
        public void AddKey(V key) {
            keys[keysQty] = key;
            keysQty++;
        }
        private bool CheckRightSibling(int nodePos) {
            return nodePos != parent.keysQty && parent.children[nodePos + 1].keysQty >= children.Length / 2;
        }
        private bool CheckLeftSibling(int nodePos) {
            return nodePos != 0 && parent.children[nodePos - 1].keysQty >= children.Length / 2;
        }
        private void MergeChildren(Node<V> siblingNode) {
            int childrenQty = keysQty + 1;
            for (int i = 0; i <= siblingNode.keysQty; i++)
            {
                children[i + childrenQty] = siblingNode.children[i];
                siblingNode.children[i].parent = this;
            }
        }
        public bool DeleteInNode(V val) {
            var result = FindInNode(val);
            switch (result.status)
            {
                case Status.Found:
                    if (isLeaf)
                        DeleteInLeaf(result.pos);
                    else
                        DeleteInNonLeaf(val, result.pos);
                    return true;
                case Status.NotFoundLeaf:
                    return false;
                case Status.NotFoundNode:
                    return result.node.DeleteInNode(val);
                default: return false;
            }
        }
        private void DeleteInNonLeaf(V val, int pos) {
            var LeafNode = children[pos + 1].GetLeafNode();
            keys[pos] = LeafNode.keys[0];
            LeafNode.DeleteInLeaf(0);
        }
        private void DeleteInLeaf(int pos) {            
            NodeDeleteVal(pos);
            RebalanceNode();
        }
        private void RebalanceNode() {
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
        private void MergeNodes(int nodePos) {
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
            RebalanceNode();
        }
        private void fillRoot() {
            var child = children[0];
            CopyValuesFromAnotherNode(child);
            isLeaf = child.isLeaf;
            if (!child.isLeaf)
            {
                CopyChildrenFromAnotherNode(child);
            }
        }
        private void CopyValuesFromAnotherNode(Node<V> anotherNode) {
            for (int i = 0; i < anotherNode.keysQty; i++)
            {
                keys[i + keysQty] = anotherNode.keys[i];
            }
            keysQty += anotherNode.keysQty;
        }
        private void CopyChildrenFromAnotherNode(Node<V> anotherNode) {
            for (int i = 0; i <= anotherNode.keysQty; i++)
            {
                children[i] = anotherNode.children[i];
                children[i].parent = this;
            }
        }

        public bool InsertNonFullNode(V val) {
            var result = FindInNode(val);
            switch (result.status)
            {
                case Status.Found:
                    return false;
                case Status.NotFoundLeaf:
                    InsertKey(result.pos, val);
                    return true; 
                case Status.NotFoundNode:
                    return InsertInternalNode(val);
                default: return false;
            }
        }
        private bool InsertInternalNode(V val) {
            var result = FindInNode(val);
            var child = result.node;
            if (child.keysQty == keys.Length)
            {
                child.SplitNode(result.pos);
                if (Match(result.pos, val) > 0)
                    return children[result.pos + 1].InsertNonFullNode(val);
            }
            return child.InsertNonFullNode(val);
        }
        public Node<V> SplitNode(int pos) {
            Node<V> newNode = new Node<V>(keys.Length, isLeaf);
            int minChildrenQty = children.Length / 2;
            int minKeysQty = minChildrenQty - 1;
            FillNewNode(newNode, minChildrenQty);
            if (!isLeaf)
                SplitChildren(newNode, minChildrenQty);
            ReplaceKeyAndChildrenToParent(pos, minKeysQty, newNode);
            return newNode;
        }
        private void ReplaceKeyAndChildrenToParent(int pos, int minKeysQty, Node<V> newNode) {
            parent.InsertKeyAndChildren(pos, keys[minKeysQty], newNode);
            keys[minKeysQty] = default(V);
            keysQty--;
        }
        private void InsertKey(int pos, V val) {
            for (int i = keys.Length - 1; i > pos; i--)
            {               
                keys[i] = keys[i - 1];
            }
            keys[pos] = val;
            keysQty++;
        }
        private void InsertKeyAndChildren (int pos, V val, Node<V> child) {
            for (int i = keys.Length; i >= pos + 1; i--)
            {
                children[i] = children[i - 1];
                if (i == keys.Length || i <= pos)
                {
                    continue;
                }
                keys[i] = keys[i - 1];
            }
            children[pos + 1] = child;
            keys[pos] = val;
            keysQty++;
        }
        private void FillNewNode(Node<V> newNode, int minQty) {
            for (int i = 0; i < minQty - 1; i++)
            {
                newNode.keys[i] = keys[i + minQty];
                keys[i + minQty] = default(V);
                newNode.keysQty++;
                keysQty--;
            }
            newNode.parent = parent;
        }
        private void SplitChildren(Node<V> newNode, int minQty) {
            for (int i = 0; i < minQty; i++)
            {
                newNode.children[i] = children[i + minQty];
                newNode.children[i].parent = newNode;
                children[i + minQty] = null;
            }
        }      
        public bool searchInNode(V val) {
            var result = FindInNode(val);         
            switch (result.status)
            {
                case Status.Found:
                    return true;
                case Status.NotFoundLeaf:
                    return false;
                case Status.NotFoundNode:
                    return result.node.searchInNode(val);
                default: return false;
            }
        }
        private int GetNodePosition(Array arr, Node<V> node) {
            return Array.IndexOf(arr, node);
        }
        private void NodeDeleteVal(int pos) {
            for (int i = pos; i < keysQty - 1; i++)
            {
                keys[i] = keys[i + 1];
            }
            keys[keysQty - 1] = default(V);
            keysQty--;
        }
        public Node<V> TransformToChild() {
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
        private int Match(int pos, V val) {
            return val.CompareTo(keys[pos]);
        }
        public (int pos, Status status, Node<V> node) FindInNode(V val) {
            int pos = 0;
            for (; pos < keysQty; pos++)
            {
                var res = Match(pos, val);
                if (res == 0)
                    return (pos, Status.Found, null);
                if (res < 0) break;                
            }
            return isLeaf ? (pos, Status.NotFoundLeaf, null) : (pos, Status.NotFoundNode, children[pos]);
        }
    }
}
