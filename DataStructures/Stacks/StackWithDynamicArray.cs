namespace DataStructures.Stacks
{
    using System;

    // Stack implemented using dynamic Array (e.g. Array with resize feature)
    public class StackWithDynamicArray<T>
    {
        private T[] _stack;
        private int _size;
        private readonly int _defaultSize = 5;

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

        public StackWithDynamicArray()
        {
            _stack = new T[_defaultSize];
            _size = 0;
        }

        public StackWithDynamicArray(int size)
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

            if (_stack.Length > _defaultSize && _size < _defaultSize)
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

            _stack[_size++] = value;
        }

        // Resize a stack
        private void Resize(int size)
        {
            var array = new T[size];
            Array.Copy(_stack, 0, array, 0, _size);
            _stack = array;
        }
    }
}