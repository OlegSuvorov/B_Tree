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
            Console.WriteLine(" Максимально допустимый размер массива значений в узлах равен 5");
            Console.WriteLine(" Минимально допустимый размер массива значений в узлах равен 2");

            B_Tree TestTree = new B_Tree(3);

            for (int i = 1; i < 10000; i++)
            {
                TestTree.Insert(i);
            }

            Console.WriteLine(" В тестовое дерево добавлены значения от 1 до 9999");

            Console.WriteLine($" \n Тесты");
            Console.WriteLine($" 1) Проверяем наличие уже добавленного значения 5555 в дереве");
            Console.WriteLine($" Ожидаем: True. Результат: {TestTree.Search(5555)}");
            Console.WriteLine($" 2) Проверяем наличие отсутствующего значения 10000 в дереве");
            Console.WriteLine($" Ожидаем: False. Результат: {TestTree.Search(10000)}");
            Console.WriteLine($" 3) Проверяем невозможность добавить уже существующее значение 5555");
            Console.WriteLine($" Ожидаем: False. Результат: {TestTree.Insert(5555)}");
            Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            Console.WriteLine($" допустимый размер массива значений в узлах");
            Console.WriteLine($" Ожидаем: True. Результат: {TestTree.CheckAllowedSize()}");
            Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            Console.WriteLine($" элементов по возрастанию");
            Console.WriteLine($" Ожидаем: True. Результат: {TestTree.CheckOrder()}");
            Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (1)");
            Console.WriteLine($" на начальной позиции первого листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {TestTree.CheckMinValuePosition(1)}");
            Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (9999)");
            Console.WriteLine($" на последней позиции последнего листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {TestTree.CheckMaxValuePosition(9999)}");
            Console.Read();                                     
        }
    }
}
