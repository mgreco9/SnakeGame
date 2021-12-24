using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Util.Container
{
    public class SingleNode<T>
    {
        public T Value { get; set; }
        public SingleNode<T> Next { get; set; }
        public SingleNode<T> Previous { get; set; }

        public SingleNode(T value)
        {
            Value = value;
            Next = this;
            Previous = this;
        }

    }

    public class CircularLinkedList<T>
    {
        private int count;

        public SingleNode<T> Head { get; set; }
        public int Count
        {
            get
            {
                return count;
            }
        }

        public CircularLinkedList()
        {
        }

        public void Add(T value)
        {
            SingleNode<T> newNode = new SingleNode<T>(value);
            Add(newNode);
        }

        public void Add(SingleNode<T> newNode)
        {
            if (Head == null)
            {
                Head = newNode;
                for (SingleNode<T> nodeItr = newNode; nodeItr != newNode.Previous; nodeItr = nodeItr.Next)
                    count++;
                count++;
            }
            else
                AddBefore(Head, newNode);
        }

        public void AddBefore(SingleNode<T> node, T value)
        {
            SingleNode<T> newNode = new SingleNode<T>(value);
            AddBefore(node, newNode);
        }

        public void AddBefore(SingleNode<T> node, SingleNode<T> newNode)
        {
            SingleNode<T> startCurrentList = node;
            SingleNode<T> endCurrentList = startCurrentList.Previous;

            SingleNode<T> startImportList = newNode;
            SingleNode<T> endImportList = startImportList.Previous;

            startCurrentList.Previous = endImportList;
            endImportList.Next = startCurrentList;

            startImportList.Previous = endCurrentList;
            endCurrentList.Next = startImportList;

            for (SingleNode<T> nodeItr = startImportList; nodeItr != endImportList; nodeItr = nodeItr.Next)
                count++;
            count++;
        }

        public void AddAfter(SingleNode<T> node, T value)
        {
            SingleNode<T> newNode = new SingleNode<T>(value);
            AddAfter(node, newNode);
        }

        public void AddAfter(SingleNode<T> node, SingleNode<T> newNode)
        {
            SingleNode<T> startCurrentList = node.Next;
            SingleNode<T> endCurrentList = node;

            SingleNode<T> startImportList = newNode;
            SingleNode<T> endImportList = startImportList.Previous;

            startCurrentList.Previous = endImportList;
            endImportList.Next = startCurrentList;

            startImportList.Previous = endCurrentList;
            endCurrentList.Next = startImportList;

            for (SingleNode<T> nodeItr = startImportList; nodeItr != endImportList; nodeItr = nodeItr.Next)
                count++;
            count++;
        }

        public void Remove(SingleNode<T> node)
        {
            if (node == Head && node.Next == Head)
                Head = null;
            if (node == Head && node.Next != Head)
                Head = Head.Next;

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;

            count--;
        }

        public void RemoveFirst()
        {
            Remove(Head);
        }

        public void RemoveLast()
        {
            Remove(Head.Previous);
        }

        public void Reverse()
        {
            SingleNode<T> itrNode = Head;
            for(int i = 0; i<Count; i++)
            {
                SingleNode<T> prevNode = itrNode.Previous;
                SingleNode<T> nextNode = itrNode.Next;

                itrNode.Next = prevNode;
                itrNode.Previous = nextNode;

                itrNode = itrNode.Next;
            }
        }

        public CircularLinkedList<T> Divide(SingleNode<T> node1, SingleNode<T> node2)
        {
            SingleNode<T> newListStart = node1.Next;
            SingleNode<T> newListEnd = node2.Previous;

            node1.Next = node2;
            node2.Previous = node1;
            Head = node2;

            newListEnd.Next = newListStart;
            newListStart.Previous = newListEnd;

            CircularLinkedList<T> newList = new CircularLinkedList<T>();
            newList.Add(newListStart);

            count = count - newList.Count;

            return newList;
        }

        public bool Contains(T value)
        {
            SingleNode<T> startCurrentList = Head;
            SingleNode<T> endCurrentList = Head.Previous;

            for (SingleNode<T> nodeItr = startCurrentList; nodeItr != endCurrentList; nodeItr = nodeItr.Next)
            {
                if (nodeItr.Value.Equals(value))
                    return true;
            }
            if (endCurrentList.Value.Equals(value))
                return true;

            return false;
        }

        public SingleNode<T> Find(T value)
        {
            SingleNode<T> startCurrentList = Head;
            SingleNode<T> endCurrentList = Head.Previous;

            for (SingleNode<T> nodeItr = startCurrentList; nodeItr != endCurrentList; nodeItr = nodeItr.Next)
            {
                if (nodeItr.Value.Equals(value))
                    return nodeItr;
            }
            if (endCurrentList.Value.Equals(value))
                return endCurrentList;

            return null;
        }
    }
}
