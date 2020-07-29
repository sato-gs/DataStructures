namespace DataStructures.Stacks
{
    using System;

    // Stack implemented using dynamic Array (e.g. Array with resize feature)
    public class StackWithDynamicArray<T>
    {
        private readonly int _defaultSize = 5;
        private T[] _stack;

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

        public StackWithDynamicArray()
        {
            _stack = new T[_defaultSize];
            Count = 0;
        }

        public StackWithDynamicArray(int size)
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

            if (_stack.Length > _defaultSize && Count < _defaultSize)
            {
                Resize(_defaultSize);
            }

            return value;
        }

        // Add an item to the top of the stack
        public void Push(T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultSize, _stack.Length * 2));
            }

            _stack[Count++] = value;
        }

        // Resize a stack
        private void Resize(int size)
        {
            var array = new T[size];
            Array.Copy(_stack, 0, array, 0, Count);
            _stack = array;
        }
    }
}