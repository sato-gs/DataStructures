namespace DataStructures.Stacks
{
    using System;

    // Stack implemented using linked list
    public class StackWithLinkedList<T>
    {
        private Node<T> _head;

        // Represent whether a stack is empty or not
        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        // Represent the number of items stored in a stack
        public int Count { get; private set; }

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
            Count = 0;
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
            Count--;
            return item.Value;
        }

        // Add an item to the top of the stack
        public void Push(T value)
        {
            var node = new Node<T>(value, _head);
            Count++;
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