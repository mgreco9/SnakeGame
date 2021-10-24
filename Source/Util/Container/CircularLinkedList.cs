using Snake.Source.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Source.Util.Container
{
    public static class CircularLinkedList
    {
        public static LinkedListNode<T> NextOrFirst<T>(this LinkedListNode<T> current)
        {
            if (current.Next == null)
                return current.List.First;
            return current.Next;
        }

        public static LinkedListNode<T> PreviousOrLast<T>(this LinkedListNode<T> current)
        {
            if (current.Previous == null)
                return current.List.Last;
            return current.Previous;
        }

        public static LinkedList<T> ReverseList<T>(this LinkedList<T> baseList)
        {
            return (LinkedList<T>)baseList.Reverse();
        }

        public static void InsertListAfter<T>(this LinkedList<T> baseList, LinkedListNode<T> node, LinkedListNode<T> firstNodeToInsert)
        {
            int listToInsertCount = firstNodeToInsert.List.Count;

            LinkedListNode<T> currNode = node;
            LinkedListNode<T> nodeToInsert = firstNodeToInsert;
            for (int i = 0; i < listToInsertCount; i++)
            {
                currNode = baseList.AddAfter(currNode, nodeToInsert.Value);
                nodeToInsert = nodeToInsert.NextOrFirst();
            }
        }

        public static void InsertListAfterInReverseOrder<T>(this LinkedList<T> baseList, LinkedListNode<T> node, LinkedListNode<T> firstNodeToInsert)
        {
            int listToInsertCount = firstNodeToInsert.List.Count;

            LinkedListNode<T> currNode = node;
            LinkedListNode<T> nodeToInsert = firstNodeToInsert;
            for (int i = 0; i < listToInsertCount; i++)
            {
                currNode = baseList.AddAfter(currNode, nodeToInsert.Value);
                nodeToInsert = nodeToInsert.PreviousOrLast();
            }
        }

        public static void InsertListBefore<T>(this LinkedList<T> baseList, LinkedListNode<T> node, LinkedListNode<T> firstNodeToInsert)
        {
            int listToInsertCount = firstNodeToInsert.List.Count;

            LinkedListNode<T> currNode = node;
            LinkedListNode<T> nodeToInsert = firstNodeToInsert;
            for (int i = 0; i < listToInsertCount; i++)
            {
                currNode = baseList.AddBefore(currNode, nodeToInsert.Value);
                nodeToInsert = nodeToInsert.PreviousOrLast();
            }
        }

        public static LinkedList<T> Divide<T>(this LinkedList<T> baseList, LinkedListNode<T> startNode, LinkedListNode<T> endNode)
        {
            LinkedList<T> newList = new LinkedList<T>();

            while(startNode.NextOrFirst() != endNode)
            {
                newList.AddLast(startNode.NextOrFirst().Value);
                baseList.Remove(startNode.NextOrFirst());
            }

            return newList;
        }
    }
}
