namespace DataStructures.Stacks
{
    using System;

    // Stack implemented using linked list
    public class StackWithLinkedList<T>
    {
        private Node<T> _head;
        private int _size;

        // Represent whether a stack is empty or not
        public bool IsEmpty
        {
            get
            {
                return _size == 0;
            }
        }

        // Represent the number of items stored in a stack
        public int Count
        {
            get
            {
                return _size;
            }
        }

        // Clear a stack (and free memory) by letting GC take charge
        public void Clear()
        {
            var cur = _head;
            while (cur != null)
            {
                var next = cur.Next;
                cur = null;
                cur = next;
            }

            _head = null;
            _size = 0;
        }

        // Return an item from the top of the stack
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return _head.Value;
        }

        // Remove an item (and free memory) from the top of the stack
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            var item = _head;
            _head = item.Next;
            _size--;
            return item.Value;
        }

        // Add an item to the top of the stack
        public void Push(T value)
        {
            var node = new Node<T>(value, _head);
            _size++;
            _head = node;
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