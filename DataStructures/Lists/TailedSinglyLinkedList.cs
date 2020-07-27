namespace DataStructures.Lists
{
    using System;

    // Singly linked list with tail pointer
    public class TailedSinglyLinkedList<T>
    {
        public int Size { get; private set; }
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }

        // Get a node at the specified index
        public Node<T> GetAt(int index)
        {
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            var counter = 0;
            var cur = Head;
            while (counter != index)
            {
                cur = cur.Next;
                counter++;
            }

            return cur;
        }

        // Set a node at the specified index
        public void SetAt(int index, T value)
        {
            var node = GetAt(index);
            node.Value = value;
        }

        // Add a node to the specified index
        public void AddAt(int index, T value)
        {
            if (index == 0)
            {
                AddFirst(value);
            }
            else if (index == Size)
            {
                AddLast(value);
            }
            else
            {
                var prev = GetAt(index - 1);
                var cur = prev.Next;
                prev.Next = new Node<T>(value, cur);
                Size++;
            }
        }

        // Add a node to the head of the list
        public void AddFirst(T value)
        {
            var head = new Node<T>(value, Head);
            Head = head;
            if (Size == 0)
            {
                Tail = head;
            }
            Size++;
        }

        // Add a node to the tail of the list
        public void AddLast(T value)
        {
            var tail = new Node<T>(value);
            if (Size == 0)
            {
                Head = tail;
                Tail = tail;
            }
            else
            {
                Tail.Next = tail;
                Tail = tail;
            }
            Size++;
        }

        // Remove a node from the specified index
        public Node<T> RemoveAt(int index)
        {
            if (index == 0)
            {
                return RemoveFirst();
            }
            else if (index == Math.Max(0, Size - 1))
            {
                return RemoveLast();
            }
            else
            {
                var prev = GetAt(index - 1);
                var cur = prev.Next;
                prev.Next = cur?.Next;
                Size--;
                return cur;
            }
        }

        // Remove a node from the head of the list
        public Node<T> RemoveFirst()
        {
            var head = Head;
            Head = head?.Next;
            Size = Math.Max(0, Size - 1);
            if (Size == 0)
            {
                Tail = null;
            }

            return head;
        }

        // Remove a node from the tail of the list
        public Node<T> RemoveLast()
        {
            Node<T> prev = null;
            var cur = Head;
            while (cur?.Next != null)
            {
                prev = cur;
                cur = cur.Next;
            }

            Size = Math.Max(0, Size - 1);
            if (prev != null)
            {
                prev.Next = null;
            }
            if (Size <= 1)
            {
                Head = prev;
            }
            Tail = prev;

            return cur;
        }

        // Reverse nodes
        public void Reverse()
        {
            Node<T> prev = null;
            var cur = Head;
            while (cur != null)
            {
                var next = cur.Next;
                cur.Next = prev;
                prev = cur;
                cur = next;
            }

            var head = Head;
            Head = Tail;
            Tail = head;
        }

        public class Node<NodeT>
        {
            public Node(
                NodeT value,
                Node<NodeT> next = null)
            {
                Value = value;
                Next = next;
            }

            public NodeT Value { get; set; }
            public Node<NodeT> Next { get; set; }
        }
    }
}