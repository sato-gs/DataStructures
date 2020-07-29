namespace DataStructures.Stacks
{
    using System;

    // Stack implemented using static Array (e.g. Array without resize feature)
    public class StackWithStaticArray<T>
    {
        private readonly T[] _stack;

        // Represent whether a stack is empty or not
        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        // Represent whether a stack is full or not
        public bool IsFull
        {
            get
            {
                return Count == _stack.Length;
            }
        }

        // Represent the number of items stored in a stack
        public int Count { get; private set; }

        public StackWithStaticArray(int size)
        {
            _stack = new T[size];
            Count = 0;
        }

        // Clear a stack (and free memory) by setting each item to default
        public void Clear()
        {
            Array.Clear(_stack, 0, Count);
            Count = 0;
        }

        // Return an item from the top of the stack
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return _stack[Count - 1];
        }

        // Remove an item (and free memory) from the top of the stack
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            var value = _stack[--Count];
            _stack[Count] = default;
            return value;
        }

        // Add an item to the top of the stack
        public void Push(T value)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("The stack is full.");
            }

            _stack[Count++] = value;
        }
    }
}