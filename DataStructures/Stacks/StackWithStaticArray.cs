namespace DataStructures.Stacks
{
    using System;

    // Stack implemented using static Array (e.g. Array without resize feature)
    public class StackWithStaticArray<T>
    {
        private readonly T[] _stack;
        private int _size;

        // Represent whether a stack is empty or not
        public bool IsEmpty
        {
            get
            {
                return _size == 0;
            }
        }

        // Represent whether a stack is full or not
        public bool IsFull
        {
            get
            {
                return _size == _stack.Length;
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

        public StackWithStaticArray(int size)
        {
            _stack = new T[size];
            _size = 0;
        }

        // Clear a stack (and free memory) by setting each item to default
        public void Clear()
        {
            Array.Clear(_stack, 0, _size);
            _size = 0;
        }

        // Return an item from the top of the stack
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return _stack[_size - 1];
        }

        // Remove an item (and free memory) from the top of the stack
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            var value = _stack[--_size];
            _stack[_size] = default;
            return value;
        }

        // Add an item to the top of the stack
        public void Push(T value)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("The stack is full.");
            }

            _stack[_size++] = value;
        }
    }
}