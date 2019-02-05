﻿using System;
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
            //B_Tree<int> TestIntTree = new B_Tree<int>(5);

            //for (int i = 1; i < 21; i++)
            //{
            //    TestIntTree.Insert(i);
            //}            
            //Console.WriteLine($" Ожидаем: False. Результат: {TestIntTree.Insert(1)}");
            //Console.Read();
            Console.WriteLine(" Тестовое числовое дерево");
            Console.WriteLine(" Максимально допустимый размер массива значений в узлах равен 5");
            Console.WriteLine(" Минимально допустимый размер массива значений в узлах равен 2");

            B_Tree<int> TestIntTree = new B_Tree<int>(5);
            int[] intArr = new int[] {1, 15, 8, 9, 13, 10, 3, 16, 19, 14, 12, 17, 5, 18, 6, 2, 7, 20, 4, 11};
            //int[] intArr = new int[] { 1, 3, 5, 4, 2 };
            //int[] intArr = new int[] { 1, 8, 9, 3, 5, 6, 2, 7, 9 };
            //int[] intArr = new int[] { 1, 8, 9, 3, 10, 5, 6, 11, 2, 7, 9 };
            //int[] intArr = new int[20];
            //for (int i = 0; i < 20; i++)
            //{
            //    intArr[i] = i + 1;
            //}
            //Random rand = new Random();

            //for (int i = intArr.Length - 1; i >= 1; i--)
            //{
            //    int j = rand.Next(i + 1);

            //    int tmp = intArr[j];
            //    intArr[j] = intArr[i];
            //    intArr[i] = tmp;
            //}
            for (int i = 0; i < 20; i++)
            {
                TestIntTree.Insert(intArr[i]);
            }
            Console.WriteLine($" 1) Проверяем наличие уже добавленного значения 8 в дереве");
            Console.WriteLine($" Ожидаем: True. Результат: {TestIntTree.Search(20)}");

            Console.Read();
            //Console.WriteLine(" В тестовое дерево добавлены значения от 1 до 499");

            //Console.WriteLine($" \n Тесты");
            //Console.WriteLine($" 1) Проверяем наличие уже добавленного значения 255 в дереве");
            //Console.WriteLine($" Ожидаем: True. Результат: {TestIntTree.Search(255)}");
            //Console.WriteLine($" 2) Проверяем наличие отсутствующего значения 500 в дереве");
            //Console.WriteLine($" Ожидаем: False. Результат: {TestIntTree.Search(500)}");
            //Console.WriteLine($" 3) Проверяем невозможность добавить уже существующее значение 255");
            //Console.WriteLine($" Ожидаем: False. Результат: {TestIntTree.Insert(255)}");
            //Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            //Console.WriteLine($" допустимый размер массива значений в узлах");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckAllowedSize(TestIntTree)}");
            //Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            //Console.WriteLine($" элементов по возрастанию");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckOrder(TestIntTree)}");
            //Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (1)");
            //Console.WriteLine($" на начальной позиции первого листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckMinValuePosition(TestIntTree, 1)}");
            //Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (499)");
            //Console.WriteLine($" на конечной позиции последнего листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckMaxValuePosition(TestIntTree, 499)}");

            //Console.WriteLine("\n Тестовое символьное дерево");
            //Console.WriteLine(" Максимально допустимый размер массива значений в узлах равен 5");
            //Console.WriteLine(" Минимально допустимый размер массива значений в узлах равен 2");

            //B_Tree<char> TestCharTree = new B_Tree<char>(5);

            //string baseStr = "abcdefghijklmnopqrstuvwxyz";
            //for (int i = 0; i < baseStr.Length; i++)
            //{
            //    TestCharTree.Insert(baseStr[i]);
            //}

            //Console.WriteLine(" В символьное дерево добавлены символы от a до z");

            //Console.WriteLine($" \n Тесты");
            //Console.WriteLine($" 1) Проверяем наличие уже добавленного значения a в дереве");
            //Console.WriteLine($" Ожидаем: True. Результат: {TestCharTree.Search('a')}");
            //Console.WriteLine($" 2) Проверяем наличие отсутствующего значения ф в дереве");
            //Console.WriteLine($" Ожидаем: False. Результат: {TestCharTree.Search('ф')}");
            //Console.WriteLine($" 3) Проверяем невозможность добавить уже существующее значение k");
            //Console.WriteLine($" Ожидаем: False. Результат: {TestCharTree.Insert('k')}");
            //Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            //Console.WriteLine($" допустимый размер массива значений в узлах");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckAllowedSize(TestCharTree)}");
            //Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            //Console.WriteLine($" элементов по возрастанию");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckOrder(TestCharTree)}");
            //Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (a)");
            //Console.WriteLine($" на начальной позиции первого листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckMinValuePosition(TestCharTree, 'a')}");
            //Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (z)");
            //Console.WriteLine($" на конечной позиции последнего листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckMaxValuePosition(TestCharTree, 'z')}");

            //Console.WriteLine("\n Тестовое строчное дерево");
            //Console.WriteLine(" Максимально допустимый размер массива значений в узлах равен 5");
            //Console.WriteLine(" Минимально допустимый размер массива значений в узлах равен 2");

            //B_Tree<string> TestStrTree = new B_Tree<string>(5);

            //string baseString = "abcdefghijklmnopqrstuvwxyz";

            //for (int i = 0; i < baseString.Length; i++)
            //{
            //    TestStrTree.Insert(baseString[i].ToString());
            //}

            //Console.WriteLine(" В строчное дерево добавлены строки от a до z");

            //Console.WriteLine($" \n Тесты");
            //Console.WriteLine($" 1) Проверяем наличие уже добавленного значения a в дереве");
            //Console.WriteLine($" Ожидаем: True. Результат: {TestStrTree.Search("a")}");
            //Console.WriteLine($" 2) Проверяем наличие отсутствующего значения ф в дереве");
            //Console.WriteLine($" Ожидаем: False. Результат: {TestStrTree.Search("ф")}");
            //Console.WriteLine($" 3) Проверяем невозможность добавить уже существующее значение k");
            //Console.WriteLine($" Ожидаем: False. Результат: {TestStrTree.Insert("k")}");
            //Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            //Console.WriteLine($" допустимый размер массива значений в узлах");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<string>.CheckAllowedSize(TestStrTree)}");
            //Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            //Console.WriteLine($" элементов по возрастанию");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<string>.CheckOrder(TestStrTree)}");
            //Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (a)");
            //Console.WriteLine($" на начальной позиции первого листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<string>.CheckMinValuePosition(TestStrTree, "a")}");
            //Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (z)");
            //Console.WriteLine($" на конечной позиции последнего листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<string>.CheckMaxValuePosition(TestStrTree, "z")}");

            //Console.WriteLine(" \nТест удаления из числового дерева");
            //Console.WriteLine($" 1) Удаляем значение 255");
            //TestIntTree.Delete(255);
            //Console.WriteLine($" 2) Проверяем наличие отсутствующего значения 255 в дереве");
            //Console.WriteLine($" Ожидаем: False. Результат: {TestIntTree.Search(255)}");
            //Console.WriteLine($" 3) Проверяем наличие остальных значений в дереве");
            //bool isRestExist = true;
            //for (int i = 1; i < 500; i++)
            //{
            //    if (i == 255) continue;
            //    if (!TestIntTree.Search(i))
            //        isRestExist = false;
            //}
            //Console.WriteLine($" Ожидаем: True. Результат: {isRestExist}");
            //Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            //Console.WriteLine($" допустимый размер массива значений в узлах");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckAllowedSize(TestIntTree)}");
            //Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            //Console.WriteLine($" элементов по возрастанию");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckOrder(TestIntTree)}");
            //Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (1)");
            //Console.WriteLine($" на начальной позиции первого листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckMinValuePosition(TestIntTree, 1)}");
            //Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (499)");
            //Console.WriteLine($" на конечной позиции последнего листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<int>.CheckMaxValuePosition(TestIntTree, 499)}");

            //Console.WriteLine(" \nТест удаления из символьного дерева");
            //Console.WriteLine($" 1) Удаляем значение k");
            //TestCharTree.Delete('k');
            //Console.WriteLine($" 2) Проверяем наличие отсутствующего значения k в дереве");
            //Console.WriteLine($" Ожидаем: False. Результат: {TestCharTree.Search('k')}");
            //Console.WriteLine($" 3) Проверяем наличие остальных значений в дереве");
            //bool isCharRestExist = true;
            //for (int i = 0; i < baseStr.Length; i++)
            //{
            //    if (baseStr[i] == 'k') continue;
            //    if (!TestCharTree.Search(baseStr[i]))
            //        isCharRestExist = false;
            //}
            //Console.WriteLine($" Ожидаем: True. Результат: {isCharRestExist}");
            //Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            //Console.WriteLine($" допустимый размер массива значений в узлах");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckAllowedSize(TestCharTree)}");
            //Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            //Console.WriteLine($" элементов по возрастанию");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckOrder(TestCharTree)}");
            //Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (a)");
            //Console.WriteLine($" на начальной позиции первого листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckMinValuePosition(TestCharTree, 'a')}");
            //Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (z)");
            //Console.WriteLine($" на конечной позиции последнего листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<char>.CheckMaxValuePosition(TestCharTree, 'z')}");

            //Console.WriteLine(" \nТест удаления из строчного дерева");
            //Console.WriteLine($" 1) Удаляем значение k");
            //TestStrTree.Delete("k");
            //Console.WriteLine($" 2) Проверяем наличие отсутствующего значения k в дереве");
            //Console.WriteLine($" Ожидаем: False. Результат: {TestStrTree.Search("k")}");
            //Console.WriteLine($" 3) Проверяем наличие остальных значений в дереве");
            //bool isStrRestExist = true;
            //for (int i = 0; i < baseStr.Length; i++)
            //{
            //    if (baseString[i].ToString() == "k") continue;
            //    if (!TestStrTree.Search(baseString[i].ToString()))
            //        isStrRestExist = false;
            //}
            //Console.WriteLine($" Ожидаем: True. Результат: {isStrRestExist}");
            //Console.WriteLine($" 4) Проверяем дерево на максимально и минимально");
            //Console.WriteLine($" допустимый размер массива значений в узлах");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<string>.CheckAllowedSize(TestStrTree)}");
            //Console.WriteLine($" 5) Проверяем узлы дерева на упорядоченность ");
            //Console.WriteLine($" элементов по возрастанию");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<string>.CheckOrder(TestStrTree)}");
            //Console.WriteLine($" 6) Проверяем наличие наименьшего введенного значения (a)");
            //Console.WriteLine($" на начальной позиции первого листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<string>.CheckMinValuePosition(TestStrTree, "a")}");
            //Console.WriteLine($" 7) Проверяем наличие наибольшего введенного значения (z)");
            //Console.WriteLine($" на конечной позиции последнего листа дерева");
            //Console.WriteLine($" Ожидаем: True. Результат: {Test<string>.CheckMaxValuePosition(TestStrTree, "z")}");
            //Console.Read();
        }
    }
}
