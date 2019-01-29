using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    static class Test<V> where V : IComparable<V>
    {
        public static bool CheckAllowedSize(B_Tree<V> tree)
        {
            return CheckNodeSize(tree.root, tree.maxNodeSize);
        }
        static bool CheckNodeSize(Node<V> node, int maxNodeSize)
        {
            if (
                node.parent != null && node.keysQty < ((maxNodeSize + 1) / 2) - 1 ||
                node.parent == null && node.keysQty < 1 ||
                node.keysQty > maxNodeSize 
                )
            {
                return false;
            }

            if (!node.isLeaf)
            {
                for (int i = 0; i < node.keysQty + 1; i++)
                {
                    if (!CheckNodeSize(node.children[i], maxNodeSize))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public static bool CheckOrder(B_Tree<V> tree)
        {
            return CheckNodeOrder(tree.root);
        }
        private static bool CheckNodeOrder(Node<V> node)
        {
            if (node.keysQty >= 2)
            {
                for (int i = 0; i < node.keysQty - 2; i++)
                {
                    if (node.keys[i].CompareTo(node.keys[i + 1]) > 0)
                    {
                        return false;
                    }
                }
            }

            if (!node.isLeaf)
            {
                for (int i = 0; i < node.keysQty + 1; i++)
                {
                    if (!CheckNodeOrder(node.children[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public static bool CheckMinValuePosition(B_Tree<V> tree, V val)
        {
            Node<V> firstLeaf = GetFirstLowestNode(tree.root);
            return val.CompareTo(firstLeaf.keys[0]) == 0;
        }
        private static Node<V> GetFirstLowestNode(Node<V> node)
        {
            if (node.isLeaf)
            {
                return node;
            }
            return GetFirstLowestNode(node.children[0]);
        }
        public static bool CheckMaxValuePosition(B_Tree<V> tree, V val)
        {
            Node<V> lastLeaf = GetLastLowestNode(tree.root);
            return val.CompareTo(lastLeaf.keys[lastLeaf.keysQty - 1]) == 0;
        }
        private static Node<V> GetLastLowestNode(Node<V> node)
        {
            if (node.isLeaf)
            {
                return node;
            }
            return GetLastLowestNode(node.children[node.keysQty]);
        }
    }
}
