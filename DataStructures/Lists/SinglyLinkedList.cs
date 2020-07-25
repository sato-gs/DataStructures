namespace DataStructures.Lists
{
    using System;

    // Singly linked list without tail pointer
    public class SinglyLinkedList<T>
    {
        public int Size { get; private set; }
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

            var node = GetAt(index - 1);
            var temp = node.Next;
            node.Next = new Node<T>(value, temp);
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

            var node = GetAt(index - 1);
            var temp = node.Next;
            node.Next = temp?.Next;
            Size--;
            return temp;
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
                var temp = cur.Next;
                cur.Next = prev;
                prev = cur;
                cur = temp;

                if (cur == null)
                {
                    Head = prev;
                }
            }
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