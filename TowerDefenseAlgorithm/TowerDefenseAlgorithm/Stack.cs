using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefenseAlgorithm
{
    class Stack<T>
    {
        ListNode<T>[] stack;
        ListNode<T>[] temp;
        int numbersOfObjects = 0;
        int arraySize = 10;

        public Stack()
        {
            stack = new ListNode<T>[arraySize];  //Skapar en array med 10 platser
            
        }

        public void Push(T data)
        {
            if (numbersOfObjects == arraySize-1)
            {
                AddArraySize();
                
            }
            if (numbersOfObjects == 0)  //Tittar om stacken är tom
            {
                ListNode<T> firstNode = new ListNode<T>(data, null);    // Sätter next till null eftersom stacken är tom
                stack[numbersOfObjects] = firstNode;
                numbersOfObjects++; 
            }
            else    // Har vi något redan i arrayen läggs en ny nod till
            {
                ListNode<T> newNode = new ListNode<T>(data, stack[numbersOfObjects - 1]);
                stack[numbersOfObjects] = newNode;
                numbersOfObjects++;
            }
        }

        public T Pop()
        {
            if (numbersOfObjects > 0)   // Tittar så vi har nått i arrayen, har vi det tar vi bort det och returnerar det värdet
            {
                T value = stack[numbersOfObjects - 1].data;
                numbersOfObjects--;
                return value;
            }
            else    //Har vi inget i arrayen returnerar vi "Null" för T
            {
                return default(T);
            }
            
        }

        public T Peak()
        {
            T value;
            value = stack[numbersOfObjects - 1].data;
            return value;   //Returnerar vi värdet för det som ligger överst på stacken
        }

        public int Count()
        {
            return numbersOfObjects;    // Returnerar hur många objekt vi har i arrayen
        }

        public void AddArraySize()
        {
            arraySize += 10;
            temp = new ListNode<T>[arraySize];
            for (int i = 0; i < numbersOfObjects; i++)
            {
                temp[i] = stack[i];
            }
            stack = temp;
        }
    }
}
