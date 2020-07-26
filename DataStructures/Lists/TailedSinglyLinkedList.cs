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
                var node = GetAt(index - 1);
                var temp = node.Next;
                node.Next = new Node<T>(value, temp);
                Size++;
            }
        }

        // Add a node to the head of the list
        public void AddFirst(T value)
        {
            var node = new Node<T>(value, Head);
            Head = node;
            if (Size == 0)
            {
                Tail = node;
            }
            Size++;
        }

        // Add a node to the tail of the list
        public void AddLast(T value)
        {
            var node = new Node<T>(value);
            if (Size == 0)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Tail.Next = node;
                Tail = node;
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
                var node = GetAt(index - 1);
                var temp = node.Next;
                node.Next = temp?.Next;
                Size--;
                return temp;
            }
        }

        // Remove a node from the head of the list
        public Node<T> RemoveFirst()
        {
            var temp = Head;
            Head = temp?.Next;
            Size = Math.Max(0, Size - 1);
            if (Size == 0)
            {
                Tail = null;
            }

            return temp;
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
                var temp = cur.Next;
                cur.Next = prev;
                prev = cur;
                cur = temp;
            }

            var head = Head;
            Head = Tail;
            Tail = head;
        }

        public class Node<NodeT>
        {
            public Node(NodeT value)
            {
                Value = value;
            }

            public Node(
                NodeT value,
                Node<NodeT> next)
            {
                Value = value;
                Next = next;
            }

            public NodeT Value { get; set; }
            public Node<NodeT> Next { get; set; }
        }
    }
}