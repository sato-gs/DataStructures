namespace DataStructures.Lists.Sub
{
    using System;
    using System.Collections.Generic;

    // Doubly linked list with tail
    public class DoublyLinkedListWithTail<T>
    {
        // Represent the number of nodes stored in the linked list
        public int Size { get; private set; }
        // Represent the head of the linked list
        public Node Head { get; private set; }
        // Represent the tail of the linked list
        public Node Tail { get; private set; }

        // Get a node stored at a given index
        public Node Get(int index)
        {
            CheckBounds(index);
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
            if (index == 0)
            {
                AddFirst(value);
                return;
            }

            var prev = Get(index - 1);
            var next = prev.Next;
            var node = new Node(value, prev, next);
            prev.Next = node;
            next.Prev = node;
            Size++;
        }

        // Add a node with a given value to the head of the linked list
        public void AddFirst(T value)
        {
            var head = new Node(value, null, Head);
            if (Size == 0)
            {
                Tail = head;
            }
            else
            {
                Head.Prev = head;
            }

            Head = head;
            Size++;
        }

        // Add a node with a given value to the tail of the linked list
        public void AddLast(T value)
        {
            var tail = new Node(value, Tail);
            if (Size == 0)
            {
                Head = tail;
            }
            else
            {
                Tail.Next = tail;
            }

            Tail = tail;
            Size++;
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

                    if (cur == Tail)
                    {
                        Tail = prev;
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
                return RemoveFirst();
            }
            else if (index == Size - 1)
            {
                return RemoveLast();
            }
            else
            {
                var cur = Get(index);
                var prev = cur.Prev;
                var next = cur.Next;
                prev.Next = next;
                next.Prev = prev;
                Size--;
                return cur;
            }
        }

        // Remove a node from the head of the linked list
        public Node RemoveFirst()
        {
            if (Size < 1)
            {
                throw new IndexOutOfRangeException();
            }

            var head = Head;
            Head = head.Next;
            Size--;
            if (Size == 0)
            {
                Tail = null;
            }
            else
            {
                head.Next.Prev = null;
            }

            return head;
        }

        // Remove a node from the tail of the linked list
        public Node RemoveLast()
        {
            if (Size < 1)
            {
                throw new IndexOutOfRangeException();
            }

            var tail = Tail;
            Tail = tail.Prev;
            Size--;
            if (Size == 0)
            {
                Head = null;
            }
            else
            {
                tail.Prev.Next = null;
            }

            return tail;
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
            Tail = null;
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
                cur = next;
            }

            var head = Head;
            Head = Tail;
            Tail = head;
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