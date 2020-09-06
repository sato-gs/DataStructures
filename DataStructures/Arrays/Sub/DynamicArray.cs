namespace DataStructures.Arrays.Sub
{
    using System;
    using System.Collections.Generic;

    // Dynamic array (e.g. array with resize feature)
    public class DynamicArray<T>
    {
        // Represent the array
        private T[] _array;
        // Represent the default capacity of the array
        private readonly int _defaultCapacity = 5;

        // Represent the number of items stored in the array
        public int Size { get; private set; }

        // Represent whether the array is empty or not
        public bool IsEmpty
        {
            get
            {
                return Size == 0;
            }
        }

        // Represent whether the array is full or not
        public bool IsFull
        {
            get
            {
                return Size == _array.Length;
            }
        }

        public DynamicArray()
        {
            _array = new T[_defaultCapacity];
            Size = 0;
        }

        public DynamicArray(int capacity)
        {
            if (capacity <= 0)
            {
                throw new InvalidOperationException();
            }

            _array = new T[capacity];
            Size = 0;
        }

        // Get an item stored at a given index
        public T Get(int index)
        {
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            return _array[index];
        }

        // Set an item to a given value stored at a given index
        public void Set(int index, T value)
        {
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            _array[index] = value;
        }

        // Add an item with a given value
        public void Add(T value)
        {
            // If the array is full
            // Resize the array by doubling its size
            if (IsFull)
            {
                Resize(Math.Max(_defaultCapacity, Size * 2));
            }

            _array[Size++] = value;
        }

        // Add an item with a given value at a given index
        public void AddAt(int index, T value)
        {
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            // Shift all items on the right side of the added item to the right by 1
            for (var i = Size; i > index; i--)
            {
                _array[i] = _array[i - 1];
            }
            _array[index] = value;
            Size++;
        }

        // Remove an item with a given value
        public bool Remove(T value)
        {
            var index = IndexOf(value);
            if (index == -1)
            {
                return false;
            }

            RemoveAt(index);
            return true;
        }

        // Remove an item stored at a given index
        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Size)
            {
                throw new IndexOutOfRangeException();
            }

            var item = _array[index];
            // Shift all items on the right side of the removed item to the left by 1
            for (var i = index; i < Size - 1; i++)
            {
                _array[i] = _array[i + 1];
            }
            // Free memory of the previously last item by setting it to default
            _array[--Size] = default;

            // If the array is unnecessarily long
            // Resize the array by halving its size
            if (_array.Length / 2 >= _defaultCapacity
                && _array.Length / 2 >= Size)
            {
                Resize(_array.Length / 2);
            }

            return item;
        }

        // Clear the array
        public void Clear()
        {
            // Note that C# built-in function can be alternatively used as follows
            // Array.Clear(_array, 0, Size);
            for (var i = 0; i < Size; i++)
            {
                _array[i] = default;
            }
            Size = 0;
        }

        // Return an index of an item with a given value
        public int IndexOf(T value)
        {
            for (var i = 0; i < Size; i++)
            {
                if (Comparer<T>
                    .Default
                    .Compare(_array[i], value) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        // Check whether an item with a given value exists
        public bool Contains(T value)
        {
            return IndexOf(value) != -1;
        }

        // Resize the array to a given capacity
        private void Resize(int capacity)
        {
            var array = new T[capacity];
            // Note that C# built-in function can be alternatively used as follows
            // Array.Copy(_array, array, Size);
            for (var i = 0; i < Size; i++)
            {
                array[i] = _array[i];
            }
            _array = array;
        }
    }
}