namespace DataStructures.Lists.Main
{
    using System;
    using System.Collections.Generic;

    // Doubly linked list
    public class DoublyLinkedList<T>
    {
        // Represent the number of nodes stored in the linked list
        public int Size { get; private set; }
        // Represent the head of the linked list
        public Node Head { get; private set; }

        // Get a node stored at a given index
        public Node Get(int index)
        {
            CheckBounds(index);
            var counter = 0;
            var cur = Head;
            while (counter != index)
            {
                cur = cur.Next;
                counter++;
            }

            return cur;
        }

        // Set a node to a given value stored at a given index
        public void Set(int index, T value)
        {
            var node = Get(index);
            node.Value = value;
        }

        // Add a node with a given value to the tail of the linked list
        public void Add(T value)
        {
            AddLast(value);
        }

        // Add a node with a given value at a given index
        public void AddAt(int index, T value)
        {
            CheckBounds(index);
            AddAtInclusive(index, value);
        }

        // Add a node with a given value at a given index (inclusive of the tail)
        private void AddAtInclusive(int index, T value)
        {
            if (index == 0)
            {
                var head = new Node(value, null, Head);
                if (Head != null)
                {
                    Head.Prev = head;
                }

                Head = head;
                Size++;
                return;
            }

            var prev = Get(index - 1);
            var next = prev.Next;
            var node = new Node(value, prev, next);
            prev.Next = node;
            if (next != null)
            {
                next.Prev = node;
            }
            Size++;
        }

        // Add a node with a given value to the head of the linked list
        public void AddFirst(T value)
        {
            AddAtInclusive(0, value);
        }

        // Add a node with a given value to the tail of the linked list
        public void AddLast(T value)
        {
            AddAtInclusive(Size, value);
        }

        // Remove a node with a given value
        public bool Remove(T value)
        {
            Node prev = null;
            var cur = Head;
            while (cur != null)
            {
                if (cur.EqualTo(value))
                {
                    if (cur == Head)
                    {
                        Head = cur.Next;
                    }
                    else
                    {
                        prev.Next = cur.Next;
                    }

                    if (cur.Next != null)
                    {
                        cur.Next.Prev = prev;
                    }
                    Size--;

                    // Free memory by breaking associations
                    cur.Prev = null;
                    cur.Next = null;
                    cur = null;
                    return true;
                }
                prev = cur;
                cur = cur.Next;
            }

            return false;
        }

        // Remove a node stored at a given index
        public Node RemoveAt(int index)
        {
            CheckBounds(index);
            if (index == 0)
            {
                var head = Head;
                if (head.Next != null)
                {
                    head.Next.Prev = null;
                }

                Head = head.Next;
                Size--;
                return head;
            }

            var cur = Get(index);
            var prev = cur.Prev;
            var next = cur.Next;
            prev.Next = next;
            if (next != null)
            {
                next.Prev = prev;
            }

            Size--;
            return cur;
        }

        // Remove a node from the head of the linked list
        public Node RemoveFirst()
        {
            return RemoveAt(0);
        }

        // Remove a node from the tail of the linked list
        public Node RemoveLast()
        {
            return RemoveAt(Size - 1);
        }

        // Clear nodes
        public void Clear()
        {
            var cur = Head;
            while (cur != null)
            {
                var next = cur.Next;
                // Free memory by breaking associations
                cur.Prev = null;
                cur.Next = null;
                cur = null;
                cur = next;
            }
            Head = null;
            Size = 0;
        }

        // Return the index of a node with a given value
        public int IndexOf(T value)
        {
            var counter = 0;
            var cur = Head;
            while (cur != null)
            {
                if (cur.EqualTo(value))
                {
                    return counter;
                }
                counter++;
                cur = cur.Next;
            }

            return -1;
        }

        // Check whether a node with a given value exists
        public bool Contains(T value)
        {
            return IndexOf(value) != -1;
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

                if (next == null)
                {
                    Head = cur;
                }

                cur = next;
            }
        }

        // Check the bounds of the linked list
        private void CheckBounds(int index)
        {
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException();
            }
        }

        public class Node
        {
            public T Value { get; set; }
            public Node Prev { get; set; }
            public Node Next { get; set; }

            public Node(
                T value,
                Node prev = null,
                Node next = null)
            {
                Value = value;
                Prev = prev;
                Next = next;
            }

            public bool EqualTo(T comparison)
            {
                return Comparer<T>
                    .Default
                    .Compare(Value, comparison) == 0;
            }
        }
    }
}