namespace DataStructures.Lists
{
    using System;

    // Doubly linked list without tail
    public class DoublyLinkedList<T>
    {
        // Represent the current size of linked list
        public int Size { get; private set; }
        // Represent the current head of linked list
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
                var head = new Node<T>(value, null, Head);
                if (Head != null)
                {
                    Head.Prev = head;
                }

                Head = head;
                Size++;
                return;
            }

            var prev = GetAt(index - 1);
            var next = prev.Next;
            var node = new Node<T>(value, prev, next);
            prev.Next = node;
            if (next != null)
            {
                next.Prev = node;
            }
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
                var headNext = head?.Next;
                if (headNext != null)
                {
                    headNext.Prev = null;
                }

                Head = headNext;
                Size = Math.Max(0, Size - 1);
                return head;
            }

            var node = GetAt(index);
            var prev = node.Prev;
            var next = node.Next;
            prev.Next = next;
            if (next != null)
            {
                next.Prev = prev;
            }

            Size--;
            return node;
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
            var cur = Head;
            while (cur != null)
            {
                var next = cur.Next;
                cur.Next = cur.Prev;
                cur.Prev = cur.Next;

                if (next == null)
                {
                    Head = cur;
                }

                cur = next;
            }
        }

        public class Node<NodeT>
        {
            public Node(
                NodeT value,
                Node<NodeT> prev = null,
                Node<NodeT> next = null)
            {
                Value = value;
                Prev = prev;
                Next = next;
            }

            public NodeT Value { get; set; }
            public Node<NodeT> Prev { get; set; }
            public Node<NodeT> Next { get; set; }
        }
    }
}