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
            Console.WriteLine(" Дерево типа int");
            B_Tree<int> intTree = new B_Tree<int>(3);
            
            try
            {
                intTree.Insert(6);
                intTree.Insert(6);
                intTree.Insert(2);
                intTree.Insert(4);
                intTree.Insert(1);
                intTree.Insert(3);
                intTree.Insert(5);
                intTree.Insert(5000);

                Console.WriteLine($" Корень: {intTree.root.keys[0]}");
                Console.WriteLine($" Первый потомок: " +
                    $"{intTree.root.children[0].keys[0]}" + ", " +
                    $"{intTree.root.children[0].keys[1]}");
                Console.WriteLine($" Второй потомок: " +
                    $"{intTree.root.children[1].keys[0]}" + ", " +
                    $"{intTree.root.children[1].keys[1]}" + ", " +
                    $"{intTree.root.children[1].keys[2]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            intTree.Search(5);
            intTree.Search(10);

            Console.WriteLine("\n Дерево типа string");
            B_Tree<string> stringTree = new B_Tree<string>(3);

            try
            {
                stringTree.Insert("b");
                stringTree.Insert("e");
                stringTree.Insert("e");
                stringTree.Insert("d");
                stringTree.Insert("a");
                stringTree.Insert("c");
                stringTree.Insert("f");

                Console.WriteLine($" Корень: {stringTree.root.keys[0]}");
                Console.WriteLine($" Первый потомок: " +
                    $"{stringTree.root.children[0].keys[0]}" + ", " +
                    $"{stringTree.root.children[0].keys[1]}");
                Console.WriteLine($" Второй потомок: " +
                    $"{stringTree.root.children[1].keys[0]}" + ", " +
                    $"{stringTree.root.children[1].keys[1]}" + ", " +
                    $"{stringTree.root.children[1].keys[2]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            stringTree.Search("a");
            stringTree.Search("h");

            Console.WriteLine("\n Большое дерево типа int");
            B_Tree<int> bigIntTree = new B_Tree<int>(3);
            for (int i = 0; i < 10000; i++)
            {
                bigIntTree.Insert(i);
            }

            bigIntTree.Search(9999);
            bigIntTree.Search(111111);
            Console.Read();
        }
    }
}
