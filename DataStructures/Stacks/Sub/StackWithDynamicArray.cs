namespace DataStructures.Stacks.Sub
{
    using System;

    // Stack implemented using dynamic Array (e.g. Array with resize feature)
    public class StackWithDynamicArray<T>
    {
        // Represent the stack
        private T[] _stack;
        // Represent the current size of the stack
        private int _size;
        // Represent the default capacity of the stack
        private readonly int _defaultCapacity = 5;

        // Represent whether the stack is empty or not
        public bool IsEmpty
        {
            get
            {
                return _size == 0;
            }
        }

        // Represent whether the stack is full or not
        public bool IsFull
        {
            get
            {
                return _size == _stack.Length;
            }
        }

        // Represent the number of items stored in the stack
        public int Count
        {
            get
            {
                return _size;
            }
        }

        public StackWithDynamicArray()
        {
            _stack = new T[_defaultCapacity];
            _size = 0;
        }

        public StackWithDynamicArray(int capacity)
        {
            _stack = new T[capacity];
            _size = 0;
        }

        // Clear the stack (and free memory) by setting each item to default
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
                Resize(Math.Max(_defaultCapacity, _stack.Length * 2));
            }

            _stack[_size++] = value;
        }

        // Resize the stack
        private void Resize(int size)
        {
            var array = new T[size];
            Array.Copy(_stack, 0, array, 0, _size);
            _stack = array;
        }
    }
}