using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_Tree
{
    class B_Tree<V> where V : IComparable<V>
    {
        public Node<V> root { get; private set; }
        public int t { get; private set; }
        public B_Tree(int t)
        {
            if (t < 2)
            {
                Console.WriteLine(" Размерность должна быть целым положительным числом не менее 2");
                return;
            }
            this.root = null;
            this.t = t;
        }
        // Поиск
        public bool Search(V value)
        {
            return this.nodeSearch(this.root, value);
        }

        // Поиск внутри узла
        private bool nodeSearch (Node<V> node, V value)
        {
            if (this.root == null)
            {
                Console.WriteLine($" В нашем дереве еще нет элементов!");
                return false;
            }
            
            int currentPosition = node.keys.TakeWhile(key => value.CompareTo(key) >= 0).Count();
            if (currentPosition - 1 >= 0 && value.CompareTo(node.keys[currentPosition - 1]) == 0)
            {
                Console.WriteLine($" В нашем дереве содержится искомый элемент {value}");
                return true;
            }
            else
            {
                if (node.children.Count == 0)
                {
                    Console.WriteLine($" В нашем дереве не содержится искомый элемент {value}");
                    return false;
                }
                else
                {
                    return this.nodeSearch(node.children[currentPosition], value);
                }                 
            }
        }
        
        public void Insert(V value)
        {    // дерево пустое
            if (this.root == null)
            {
                root = new Node<V>(t);
                root.keys.Add(value);
            }
            else
            {   // дерево не пустое, корень заполнен полностью
                if (root.keys.Count == 2 * this.t - 1)
                {
                    Node<V> newNode = new Node<V>(t);
                    newNode.children.Add(root);
                    this.SplitTwo(newNode, 0, this.root);
                    if (value.CompareTo(newNode.keys[0]) < 0)
                    {
                        AddingValue(newNode.children[0], value);
                    }
                    else
                    {
                        AddingValue(newNode.children[1], value);
                    }
                    root = newNode;
                } // дерево не пустое и корень не заполнен
                else
                {
                    AddingValue(root, value);
                }
            }
        }
        private void AddingValue(Node<V> node, V val)
        {
            int currentPosition = node.keys.TakeWhile(key => val.CompareTo(key) >= 0).Count();
            // Если добавляем одинаковые значения
            if (currentPosition - 1 >= 0 && val.CompareTo(node.keys[currentPosition - 1]) == 0)
            {
                Console.WriteLine($" Нельзя добавлять одинаковые значения: {val}");
                return;
            }
            // Если узел является листом
            if (node.children.Count == 0)
            {
                node.keys.Insert(currentPosition, val);
                return;
            }
            Node<V> currentChild = node.children[currentPosition];
            // Если узел не лист, но заполнен
            if (currentChild.children.Count == 2 * this.t - 1)
            {
                this.SplitTwo(node, currentPosition, currentChild);
                if (val.CompareTo(node.keys[currentPosition]) > 0)
                {
                    currentPosition++;
                }
            }
            this.AddingValue(node.children[currentPosition], val);
        }
        // Метод для ращепления узла
        private void SplitTwo(Node<V> parentNode, int splitPosition, Node<V> baseNode)
        {
            Node<V> newNode = new Node<V>(t);
            parentNode.keys.Insert(splitPosition, baseNode.keys[this.t - 1]);
            parentNode.children.Insert(splitPosition + 1, newNode);
            newNode.keys.AddRange(baseNode.keys.GetRange(this.t, this.t - 1));
            baseNode.keys.RemoveRange(this.t - 1, this.t);
            // Если наш узел не является листом
            if (baseNode.children.Count != 0)
            {
                newNode.children.AddRange(baseNode.children.GetRange(this.t, this.t));
                baseNode.children.RemoveRange(this.t, this.t);
            }
        }
        // Метод для удаления ключа по значению
        public void Delete(V value)
        {
            // TBD метод для удаления ключей
        }
    }
}
