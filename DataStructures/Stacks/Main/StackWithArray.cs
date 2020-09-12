namespace DataStructures.Stacks.Main
{
    using System;

    // Stack implemented using array
    public class StackWithArray<T>
    {
        // Represent the stack
        private T[] _stack;
        // Represent the default capacity of the stack
        private readonly int _defaultCapacity = 5;

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

        // Represent whether the stack is full or not
        public bool IsFull
        {
            get
            {
                return Size == _stack.Length;
            }
        }

        public StackWithArray()
        {
            _stack = new T[_defaultCapacity];
            Size = 0;
        }

        public StackWithArray(int capacity)
        {
            if (capacity <= 0)
            {
                throw new InvalidOperationException();
            }

            _stack = new T[capacity];
            Size = 0;
        }

        // Return an item from the top of the stack
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            return _stack[Size - 1];
        }

        // Add an item with a given value to the top of the stack
        public void Push(T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultCapacity, _stack.Length * 2));
            }

            _stack[Size++] = value;
        }

        // Remove an item from the top of the stack
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The stack is empty.");
            }

            var value = _stack[--Size];
            _stack[Size] = default;
            return value;
        }

        // Clear the stack
        public void Clear()
        {
            // Note that C# built-in function can be alternatively used as follows
            // Array.Clear(_stack, 0, Size);
            for (var i = 0; i < Size; i++)
            {
                _stack[i] = default;
            }
            Size = 0;
        }

        // Resize the stack to a given capacity
        private void Resize(int size)
        {
            var stack = new T[size];
            // Note that C# built-in function can be alternatively used as follows
            // Array.Copy(_stack, stack, Size);
            for (var i = 0; i < Size; i++)
            {
                stack[i] = _stack[i];
            }
            _stack = stack;
        }
    }
}