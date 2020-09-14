namespace DataStructures.Heaps.Sub
{
    using System;
    using System.Collections.Generic;

    // Min heap implemented using array
    public class MinHeapWithArray<T>
    {
        // Represent the heap
        private T[] _heap;
        // Represent the default capacity of the heap
        private readonly int _defaultCapacity = 5;

        // Represent the number of items stored in the heap
        public int Size { get; private set; }

        // Represent whether the heap is empty or not
        public bool IsEmpty
        {
            get
            {
                return Size == 0;
            }
        }

        // Represent whether the heap is full or not
        public bool IsFull
        {
            get
            {
                return Size == _heap.Length;
            }
        }

        public MinHeapWithArray()
        {
            _heap = new T[_defaultCapacity];
            Size = 0;
        }

        public MinHeapWithArray(int capacity)
        {
            if (capacity <= 0)
            {
                throw new InvalidOperationException();
            }

            _heap = new T[capacity];
            Size = 0;
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

        // Insert an item with a given value (while maintaining the min heap property)
        public void Insert(T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultCapacity, _heap.Length * 2));
            }

            // Add an item to the end of the heap
            _heap[Size] = value;
            Size++;
            // Restructure the heap to maintain the min heap property
            HeapifyUp(Size - 1);
        }

        // Remove a minimum item (while maintaining the min heap property)
        public T PopMin()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The heap is empty.");
            }

            // Remove a minimum item from the top of the heap and replace it with the last item
            var min = _heap[0];
            _heap[0] = _heap[Size - 1];
            _heap[Size - 1] = default;
            Size--;
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
        private bool HasLeftChild(int index) => GetLeftChildIndex(index) < Size;

        // Check whether an item stored at a given index has a right child or not
        private bool HasRightChild(int index) => GetRightChildIndex(index) < Size;

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

            // If the current child is equal to or greater than its parent
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

            // If the current parent is equal to or less than its smaller child
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

        // Resize the heap to a given capacity
        private void Resize(int capacity)
        {
            var heap = new T[capacity];
            // Note that C# built-in function can be alternatively used as follows
            // Array.Copy(_heap, heap, Size);
            for (var i = 0; i < Size; i++)
            {
                heap[i] = _heap[i];
            }
            _heap = heap;
        }
    }
}