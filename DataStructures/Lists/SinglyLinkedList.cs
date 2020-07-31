namespace DataStructures.Lists
{
    using System;

    // Singly linked list without tail
    public class SinglyLinkedList<T>
    {
        // Represent the current size of the linked list
        public int Size { get; private set; }
        // Represent the current head of the linked list
        public Node<T> Head { get; private set; }

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
                var head = new Node<T>(value, Head);
                Head = head;
                Size++;
                return;
            }

            var prev = GetAt(index - 1);
            var cur = prev.Next;
            prev.Next = new Node<T>(value, cur);
            Size++;
        }

        // Add a node to the head of the list
        public void AddFirst(T value)
        {
            AddAt(0, value);
        }

        // Add a node to the tail of the list
        public void AddLast(T value)
        {
            AddAt(Size, value);
        }

        // Remove a node from the specified index
        public Node<T> RemoveAt(int index)
        {
            if (index == 0)
            {
                var head = Head;
                Head = head?.Next;
                Size = Math.Max(0, Size - 1);
                return head;
            }

            var prev = GetAt(index - 1);
            var cur = prev.Next;
            prev.Next = cur?.Next;
            Size--;
            return cur;
        }

        // Remove a node from the head of the list
        public Node<T> RemoveFirst()
        {
            return RemoveAt(0);
        }

        // Remove a node from the tail of the list
        public Node<T> RemoveLast()
        {
            return RemoveAt(Math.Max(0, Size - 1));
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

                if (cur == null)
                {
                    Head = prev;
                }
            }
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