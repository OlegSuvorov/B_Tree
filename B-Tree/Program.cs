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
            Console.WriteLine(" Тестовое числовое дерево");
            Console.WriteLine(" Максимально допустимый размер массива значений в узлах равен 5");
            Console.WriteLine(" Минимально допустимый размер массива значений в узлах равен 2");

            B_Tree<int> TestIntTree = new B_Tree<int>(5);

            for (int i = 1; i < 500; i++)
            {
                TestIntTree.Insert(i);
            }

            Console.WriteLine(" В тестовое дерево добавлены значения от 1 до 499");

            Console.WriteLine($" \n Тесты");
            Console.WriteLine($" 1) Проверяем наличие уже добавленного значения 255 в дереве");
            Console.WriteLine($" Ожидаем: True. Результат: {TestIntTree.Search(255)}");
            Console.WriteLine($" 2) Проверяем наличие отсутствующего значения 500 в дереве");
            Console.WriteLine($" Ожидаем: False. Результат: {TestIntTree.Search(500)}");
            Console.WriteLine($" 3) Проверяем невозможность добавить уже существующее значение 255");
            Console.WriteLine($" Ожидаем: False. Результат: {TestIntTree.Insert(255)}");
            Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            Console.WriteLine($" допустимый размер массива значений в узлах");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckAllowedSize(TestIntTree)}");
            Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            Console.WriteLine($" элементов по возрастанию");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckOrder(TestIntTree)}");
            Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (1)");
            Console.WriteLine($" на начальной позиции первого листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckMinValuePosition(TestIntTree, 1)}");
            Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (499)");
            Console.WriteLine($" на конечной позиции последнего листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckMaxValuePosition(TestIntTree, 499)}");

            Console.WriteLine("\n Тестовое символьное дерево");
            Console.WriteLine(" Максимально допустимый размер массива значений в узлах равен 5");
            Console.WriteLine(" Минимально допустимый размер массива значений в узлах равен 2");

            B_Tree<char> TestStrTree = new B_Tree<char>(5);

            string baseStr = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < baseStr.Length; i++)
            {
                TestStrTree.Insert(baseStr[i]);
            }

            Console.WriteLine(" В тестовое дерево добавлены символы от a до z");

            Console.WriteLine($" \n Тесты");
            Console.WriteLine($" 1) Проверяем наличие уже добавленного значения k в дереве");
            Console.WriteLine($" Ожидаем: True. Результат: {TestStrTree.Search('a')}");
            Console.WriteLine($" 2) Проверяем наличие отсутствующего значения ф в дереве");
            Console.WriteLine($" Ожидаем: False. Результат: {TestStrTree.Search('ф')}");
            Console.WriteLine($" 3) Проверяем невозможность добавить уже существующее значение k");
            Console.WriteLine($" Ожидаем: False. Результат: {TestStrTree.Insert('k')}");
            Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            Console.WriteLine($" допустимый размер массива значений в узлах");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckAllowedSize(TestStrTree)}");
            Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            Console.WriteLine($" элементов по возрастанию");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckOrder(TestStrTree)}");
            Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (a)");
            Console.WriteLine($" на начальной позиции первого листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckMinValuePosition(TestStrTree, 'a')}");
            Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (z)");
            Console.WriteLine($" на конечной позиции последнего листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckMaxValuePosition(TestStrTree, 'z')}");

            Console.WriteLine(" \nТест удаления из числового дерева");
            Console.WriteLine($" 1) Удаляем значение 255");
            TestIntTree.Delete(255);
            Console.WriteLine($" 2) Проверяем наличие отсутствующего значения 255 в дереве");
            Console.WriteLine($" Ожидаем: False. Результат: {TestIntTree.Search(255)}");
            Console.WriteLine($" 3) Проверяем наличие остальных значений в дереве");
            bool isRestExist = true;
            for (int i = 1; i < 500; i++)
            {
            if (i == 255) continue;
            if (!TestIntTree.Search(i))
                isRestExist = false;
            }
            Console.WriteLine($" Ожидаем: True. Результат: {isRestExist}");
            Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            Console.WriteLine($" допустимый размер массива значений в узлах");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckAllowedSize(TestIntTree)}");
            Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            Console.WriteLine($" элементов по возрастанию");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckOrder(TestIntTree)}");
            Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (1)");
            Console.WriteLine($" на начальной позиции первого листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckMinValuePosition(TestIntTree, 1)}");
            Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (499)");
            Console.WriteLine($" на конечной позиции последнего листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckMaxValuePosition(TestIntTree, 499)}");

            Console.WriteLine(" \nТест удаления из символьного дерева");
            Console.WriteLine($" 1) Удаляем значение k");
            TestIntTree.Delete('k');
            Console.WriteLine($" 2) Проверяем наличие отсутствующего значения k в дереве");
            Console.WriteLine($" Ожидаем: False. Результат: {TestIntTree.Search('k')}");
            Console.WriteLine($" 3) Проверяем наличие остальных значений в дереве");
            bool isCharRestExist = true;
            for (int i = 1; i < baseStr.Length; i++)
            {
                if (i == 'k') continue;
                if (!TestIntTree.Search(i))
                    isCharRestExist = false;
            }
            Console.WriteLine($" Ожидаем: True. Результат: {isCharRestExist}");
            Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            Console.WriteLine($" допустимый размер массива значений в узлах");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckAllowedSize(TestStrTree)}");
            Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            Console.WriteLine($" элементов по возрастанию");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckOrder(TestStrTree)}");
            Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (a)");
            Console.WriteLine($" на начальной позиции первого листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckMinValuePosition(TestStrTree, 'a')}");
            Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (z)");
            Console.WriteLine($" на конечной позиции последнего листа дерева");
            Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckMaxValuePosition(TestStrTree, 'z')}");
            Console.Read();
        }
    }
}
