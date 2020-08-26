namespace DataStructures.Heaps
{
    using System;
    using System.Collections.Generic;

    public class MaxHeapWithStaticArray<T>
    {
        // Represent the heap
        private readonly T[] _heap;
        // Represent the current size of the heap
        private int _size;

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

        public MaxHeapWithStaticArray(int capacity)
        {
            _heap = new T[capacity];
            _size = 0;
        }

        // Insert an item while maintaining the max heap property
        public void Insert(T value)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("The heap is full.");
            }

            // Add an item to the end of the heap
            _heap[_size] = value;
            _size++;
            // Restructure the heap to maintain the max heap property
            HeapifyUp(_size - 1);
        }

        // Return a maximum item
        public T PeekMax()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The heap is empty.");
            }

            return _heap[0];
        }

        // Remove a maximum item while maintaining the max heap property
        public T PopMax()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The heap is empty.");
            }

            // Remove a maximum item from the top of the heap and replace it with the last item
            var max = _heap[0];
            _heap[0] = _heap[_size - 1];
            _heap[_size - 1] = default;
            _size--;
            // Restructure the heap to maintain the max heap property
            HeapifyDown(0);

            return max;
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

        // Restructure the heap bottom-up in such ways that the max heap property is maintained
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

            // If a current child is equal to or smaller than its parent
            // Break out of the recursion
            var cmp = Comparer<T>
                .Default
                .Compare(cur, _heap[parentIndex]);
            if (cmp <= 0)
            {
                return;
            }

            // Otherwise, swap them and continue recursively
            Swap(index, parentIndex);
            HeapifyUp(parentIndex);
        }

        // Restructure the heap top-down in such ways that the max heap property is maintained
        private void HeapifyDown(int index)
        {
            // If there is no left child (meaning no child)
            // Break out of the recursion
            if (!HasLeftChild(index))
            {
                return;
            }

            var largerChildIndex = GetLeftChildIndex(index);
            if (HasRightChild(index)
                && Comparer<T>.Default.Compare(GetLeftChild(index), GetRightChild(index)) < 0)
            {
                largerChildIndex = GetRightChildIndex(index);
            }

            // If a current parent is equal to or larger than its larger child
            // Break out of the recursion
            var cmp = Comparer<T>
                .Default
                .Compare(_heap[index], _heap[largerChildIndex]);
            if (cmp >= 0)
            {
                return;
            }

            // Otherwise, swap them and continue recursively
            Swap(index, largerChildIndex);
            HeapifyDown(largerChildIndex);
        }

        // Swap two items
        private void Swap(int index1, int index2)
        {
            var temp = _heap[index1];
            _heap[index1] = _heap[index2];
            _heap[index2] = temp;
        }
    }
}