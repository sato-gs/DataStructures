namespace DataStructures.Heaps.Main
{
    using System;

    // Priority queue implemented using min heap
    public class PriorityQueueWithMinHeap<T>
    {
        // Represent the priority queue
        private Node[] _queue;
        // Represent the default capacity of the priority queue
        private readonly int _defaultCapacity = 5;

        // Represent the number of items stored in the priority queue
        public int Size { get; private set; }

        // Represent whether the priority queue is empty or not
        public bool IsEmpty
        {
            get
            {
                return Size == 0;
            }
        }

        // Represent whether the priority queue is full or not
        public bool IsFull
        {
            get
            {
                return Size == _queue.Length;
            }
        }

        public PriorityQueueWithMinHeap()
        {
            _queue = new Node[_defaultCapacity];
            Size = 0;
        }

        public PriorityQueueWithMinHeap(int capacity)
        {
            if (capacity <= 0)
            {
                throw new InvalidOperationException();
            }

            _queue = new Node[capacity];
            Size = 0;
        }

        // Return the highest priority item
        public Node PeekPriority()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            return _queue[0];
        }

        // Add an item with a given priority and value (while maintaining the priority property)
        public void Enqueue(int priority, T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultCapacity, _queue.Length * 2));
            }

            // Add an item to the end of the priority queue
            _queue[Size] = new Node(priority, value);
            Size++;
            // Restructure the priority queue to maintain the priority property
            RestructureUp(Size - 1);
        }

        // Remove the highest priority item (while maintaining the priority property)
        public Node Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            // Remove the highest priority item from the top of the priority queue and replace it with the last item
            var item = _queue[0];
            _queue[0] = _queue[Size - 1];
            _queue[Size - 1] = null;
            Size--;
            // Restructure the priority queue to maintain the priority property
            RestructureDown(0);

            return item;
        }

        // Return an item stored at a given index (as a test helper function)
        public Node GetAt(int index)
        {
            return _queue[index];
        }

        // Return the index of the parent of an item stored at a given index
        private int GetParentIndex(int index) => (index - 1) / 2;

        // Return the index of the left child of an item stored at a given index
        private int GetLeftChildIndex(int index) => index * 2 + 1;

        // Return the index of the right child of an item stored at a given index
        private int GetRightChildIndex(int index) => index * 2 + 2;

        // Return the left child of an item stored at a given index
        private Node GetLeftChild(int index) => _queue[GetLeftChildIndex(index)];

        // Return the right child of an item stored at a given index
        private Node GetRightChild(int index) => _queue[GetRightChildIndex(index)];

        // Check whether an item stored at a given index has a left child or not
        private bool HasLeftChild(int index) => GetLeftChildIndex(index) < Size;

        // Check whether an item stored at a given index has a right child or not
        private bool HasRightChild(int index) => GetRightChildIndex(index) < Size;

        // Restructure the priority queue bottom-up in such ways that the priority property is maintained
        private void RestructureUp(int index)
        {
            // If it is a root item
            // Break out of the recursion
            if (index == 0)
            {
                return;
            }

            // If the current child is equal to or greater than its parent in terms of priority
            // Break out of the recursion
            var cur = _queue[index];
            var parentIndex = GetParentIndex(index);
            if (cur.Priority >= _queue[parentIndex].Priority)
            {
                return;
            }

            // Otherwise, swap them and continue recursively
            Swap(index, parentIndex);
            RestructureUp(parentIndex);
        }

        // Restructure the priority queue top-down in such ways that the priority property is maintained
        private void RestructureDown(int index)
        {
            // If there is no left child (meaning no child)
            // Break out of the recursion
            if (!HasLeftChild(index))
            {
                return;
            }

            var smallerChildIndex = GetLeftChildIndex(index);
            if (HasRightChild(index)
                && GetLeftChild(index).Priority > GetRightChild(index).Priority)
            {
                smallerChildIndex = GetRightChildIndex(index);
            }

            // If the current parent is equal to or less than its smaller child in terms of priority
            // Break out of the recursion
            if (_queue[index].Priority <= _queue[smallerChildIndex].Priority)
            {
                return;
            }

            // Otherwise, swap them and continue recursively
            Swap(index, smallerChildIndex);
            RestructureDown(smallerChildIndex);
        }

        // Swap two items
        private void Swap(int index1, int index2)
        {
            var temp = _queue[index1];
            _queue[index1] = _queue[index2];
            _queue[index2] = temp;
        }

        // Resize the priority queue to a given capacity
        private void Resize(int capacity)
        {
            var queue = new Node[capacity];
            // Note that C# built-in function can be alternatively used as follows
            // Array.Copy(_queue, queue, Size);
            for (var i = 0; i < Size; i++)
            {
                queue[i] = _queue[i];
            }
            _queue = queue;
        }

        public class Node
        {
            public int Priority { get; }
            public T Value { get; set; }

            public Node(int priority, T value)
            {
                Priority = priority;
                Value = value;
            }
        }
    }
}