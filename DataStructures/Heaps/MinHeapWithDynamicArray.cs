namespace DataStructures.Heaps
{
    using System;
    using System.Collections.Generic;

    // Min heap implemented using dynamic Array (e.g. Array with resize feature)
    public class MinHeapWithDynamicArray<T>
    {
        // Represent the heap
        private T[] _heap;
        // Represent the current size of the heap
        private int _size;
        // Represent the default capacity of the stack
        private readonly int _defaultCapacity = 5;

        // Represent whether the heap is empty or not
        public bool IsEmpty
        {
            get
            {
                return _size == 0;
            }
        }

        // Represent whether the heap is full or not
        public bool IsFull
        {
            get
            {
                return _size == _heap.Length;
            }
        }

        // Represent the number of items stored in the heap
        public int Count
        {
            get
            {
                return _size;
            }
        }

        public MinHeapWithDynamicArray()
        {
            _heap = new T[_defaultCapacity];
            _size = 0;
        }

        public MinHeapWithDynamicArray(int capacity)
        {
            _heap = new T[capacity];
            _size = 0;
        }

        // Insert an item while maintaining the min heap property
        public void Insert(T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultCapacity, _heap.Length * 2));
            }

            // Add an item to the end of the heap
            _heap[_size] = value;
            _size++;
            // Restructure the heap to maintain the min heap property
            HeapifyUp(_size - 1);
        }

        // Return a minimum item
        public T PeekMin()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The heap is empty.");
            }

            return _heap[0];
        }

        // Remove a minimum item while maintaining the min heap property
        public T PopMin()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The heap is empty.");
            }

            // Remove a minimum item from the top of the heap and replace it with the last item
            var min = _heap[0];
            _heap[0] = _heap[_size - 1];
            _heap[_size - 1] = default;
            _size--;
            // Restructure the heap to maintain the min heap property
            HeapifyDown(0);

            return min;
        }

        // Return an item stored at a given index (as a test helper function)
        public T GetAt(int index)
        {
            return _heap[index];
        }

        // Return the index of the parent of an item stored at a given index
        private int GetParentIndex(int index) => (index - 1) / 2;

        // Return the index of the left child of an item stored at a given index
        private int GetLeftChildIndex(int index) => index * 2 + 1;

        // Return the index of the right child of an item stored at a given index
        private int GetRightChildIndex(int index) => index * 2 + 2;

        // Return the left child of an item stored at a given index
        private T GetLeftChild(int index) => _heap[GetLeftChildIndex(index)];

        // Return the right child of an item stored at a given index
        private T GetRightChild(int index) => _heap[GetRightChildIndex(index)];

        // Check whether an item stored at a given index has a left child or not
        private bool HasLeftChild(int index) => GetLeftChildIndex(index) < _size;

        // Check whether an item stored at a given index has a right child or not
        private bool HasRightChild(int index) => GetRightChildIndex(index) < _size;

        // Restructure the heap bottom-up in such ways that the min heap property is maintained
        private void HeapifyUp(int index)
        {
            // If it is a root item
            // Break out of the recursion
            if (index == 0)
            {
                return;
            }

            var cur = _heap[index];
            var parentIndex = GetParentIndex(index);

            // If a current child is equal to or greater than its parent
            // Break out of the recursion
            var cmp = Comparer<T>
                .Default
                .Compare(cur, _heap[parentIndex]);
            if (cmp >= 0)
            {
                return;
            }

            // Otherwise, swap them and continue recursively
            Swap(index, parentIndex);
            HeapifyUp(parentIndex);
        }

        // Restructure the heap top-down in such ways that the min heap property is maintained
        private void HeapifyDown(int index)
        {
            // If there is no left child (meaning no child)
            // Break out of the recursion
            if (!HasLeftChild(index))
            {
                return;
            }

            var smallerChildIndex = GetLeftChildIndex(index);
            if (HasRightChild(index)
                && Comparer<T>.Default.Compare(GetLeftChild(index), GetRightChild(index)) > 0)
            {
                smallerChildIndex = GetRightChildIndex(index);
            }

            // If a current parent is equal to or smaller than its smaller child
            // Break out of the recursion
            var cmp = Comparer<T>
                .Default
                .Compare(_heap[index], _heap[smallerChildIndex]);
            if (cmp <= 0)
            {
                return;
            }

            // Otherwise, swap them and continue recursively
            Swap(index, smallerChildIndex);
            HeapifyDown(smallerChildIndex);
        }

        // Swap two items
        private void Swap(int index1, int index2)
        {
            var temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }

        // Resize the heap
        private void Resize(int size)
        {
            var array = new T[size];
            Array.Copy(_heap, 0, array, 0, _size);
            _heap = array;
        }
    }
}