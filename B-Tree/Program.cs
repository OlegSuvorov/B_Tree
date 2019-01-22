using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Тестовое дерево");

            B_Tree TestTree = new B_Tree(3);

            for (int i = 1; i < 10000; i++)
            {
                TestTree.Insert(i);
            }            
            
            Console.Read();                                     
        }
    }
}
