namespace DataStructures.Stacks.Main
{
    using System;

    // Stack implemented using linked list
    public class StackWithLinkedList<T>
    {
        // Represent the top of the stack
        private Node _top;

        // Represent the number of items stored in the stack
        public int Size { get; private set; }

        // Represent whether the stack is empty or not
        public bool IsEmpty
        {
            get
            {
                return Size == 0;
            }
        }

        // Return an item from the top of the stack
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return _top.Value;
        }

        // Add an item with a given value to the top of the stack
        public void Push(T value)
        {
            var node = new Node(value, _top);
            Size++;
            _top = node;
        }

        // Remove an item from the top of the stack
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            var top = _top;
            _top = top.Next;
            Size--;
            return top.Value;
        }

        // Clear the stack
        public void Clear()
        {
            var cur = _top;
            while (cur != null)
            {
                var next = cur.Next;
                // Free memory by breaking associations
                cur.Next = null;
                cur = null;
                cur = next;
            }
            _top = null;
            Size = 0;
        }

        public class Node
        {
            public Node(
                T value,
                Node next = null)
            {
                Value = value;
                Next = next;
            }

            public T Value { get; set; }
            public Node Next { get; set; }
        }
    }
}