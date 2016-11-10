using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class ListNode<T>
    {
        public T data { get; private set;}
        public ListNode<T> next;
        public ListNode(T dataValue, ListNode <T> nextNode)
        {
            data = dataValue;
            next = nextNode;
        }
    }
}
