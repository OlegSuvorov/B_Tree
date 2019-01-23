using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    static class Test
    {
        public static bool CheckAllowedSize(B_Tree tree)
        {
            return CheckNodeSize(tree.root, tree.maxNodeSize);
        }
        static bool CheckNodeSize(Node node, int maxNodeSize)
        {
            if (node.keysQty < ((maxNodeSize + 1) / 2) - 1 || node.keysQty > maxNodeSize)
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
        public static bool CheckOrder(B_Tree tree)
        {
            return CheckNodeOrder(tree.root);
        }
        private static bool CheckNodeOrder(Node node)
        {
            if (node.keysQty >= 2)
            {
                for (int i = 0; i < node.keysQty - 2; i++)
                {
                    if (node.keys[i] > node.keys[i + 1])
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
        public static bool CheckMinValuePosition(B_Tree tree, int val)
        {
            Node firstLeaf = GetFirstLowestNode(tree.root);
            return val == firstLeaf.keys[0];
        }
        private static Node GetFirstLowestNode(Node node)
        {
            if (node.isLeaf)
            {
                return node;
            }

            return GetFirstLowestNode(node.children[0]);
        }
        public static bool CheckMaxValuePosition(B_Tree tree, int val)
        {
            Node lastLeaf = GetLastLowestNode(tree.root);
            return val == lastLeaf.keys[lastLeaf.keysQty - 1];
        }
        private static Node GetLastLowestNode(Node node)
        {
            if (node.isLeaf)
            {
                return node;
            }

            return GetLastLowestNode(node.children[node.keysQty]);
        }
    }
}
