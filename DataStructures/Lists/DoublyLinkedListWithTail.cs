namespace DataStructures.Lists
{
    using System;

    // Doubly linked list with tail
    public class DoublyLinkedListWithTail<T>
    {
        // Represent the current size of linked list
        public int Size { get; private set; }
        // Represent the current head of linked list
        public Node<T> Head { get; private set; }
        // Represent the current tail of linked list
        public Node<T> Tail { get; private set; }

        // Get a node at the specified index
        public Node<T> GetAt(int index)
        {
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            var middle = (Size - 1) / 2;
            var ascending = index <= middle;
            var counter = ascending ? 0 : Size - 1;
            var cur = ascending ? Head : Tail;
            while (counter != index)
            {
                cur = ascending ? cur.Next : cur.Prev;
                counter = ascending ? counter + 1 : counter - 1;
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
                var next = prev.Next;
                var node = new Node<T>(value, prev, next);
                prev.Next = node;
                next.Prev = node;
                Size++;
            }
        }

        // Add a node to the head of the list
        public void AddFirst(T value)
        {
            var node = new Node<T>(value, null, Head);
            if (Head == null)
            {
                Tail = node;
            }
            else
            {
                Head.Prev = node;
            }

            Head = node;
            Size++;
        }

        // Add a node to the tail of the list
        public void AddLast(T value)
        {
            var node = new Node<T>(value, Tail);
            if (Tail == null)
            {
                Head = node;
            }
            else
            {
                Tail.Next = node;
            }

            Tail = node;
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
                var node = GetAt(index);
                var prev = node.Prev;
                var next = node.Next;
                prev.Next = next;
                next.Prev = prev;
                Size--;
                return node;
            }
        }

        // Remove a node from the head of the list
        public Node<T> RemoveFirst()
        {
            var head = Head;
            var next = head?.Next;
            if (next == null)
            {
                Tail = null;
            }
            else
            {
                next.Prev = null;
            }

            Head = next;
            Size = Math.Max(0, Size - 1);
            return head;
        }

        // Remove a node from the tail of the list
        public Node<T> RemoveLast()
        {
            var tail = Tail;
            var prev = tail?.Prev;
            if (prev == null)
            {
                Head = null;
            }
            else
            {
                prev.Next = null;
            }

            Tail = prev;
            Size = Math.Max(0, Size - 1);
            return tail;
        }

        // Reverse nodes
        public void Reverse()
        {
            var cur = Head;
            while (cur != null)
            {
                var next = cur.Next;
                cur.Next = cur.Prev;
                cur.Prev = next;
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