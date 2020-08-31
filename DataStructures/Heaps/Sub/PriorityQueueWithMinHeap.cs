namespace DataStructures.Heaps.Sub
{
    using System;

    // Priority queue implemented using min heap (e.g. the smaller the priority is, the higher the priority is)
    public class PriorityQueueWithMinHeap<T>
    {
        // Represent the priority queue
        private Node<T>[] _queue;
        // Represent the current size of the priority queue
        private int _size;
        // Represent the default capacity of the priority queue
        private readonly int _defaultCapacity = 5;

        // Represent whether the priority queue is empty or not
        public bool IsEmpty
        {
            get
            {
                return _size == 0;
            }
        }

        // Represent whether the priority queue is full or not
        public bool IsFull
        {
            get
            {
                return _size == _queue.Length;
            }
        }

        // Represent the number of items stored in the priority queue
        public int Count
        {
            get
            {
                return _size;
            }
        }

        public PriorityQueueWithMinHeap()
        {
            _queue = new Node<T>[_defaultCapacity];
            _size = 0;
        }

        public PriorityQueueWithMinHeap(int capacity)
        {
            _queue = new Node<T>[capacity];
            _size = 0;
        }

        // Add an item while maintaining the priority property
        public void Enqueue(
            int priority,
            T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultCapacity, _queue.Length * 2));
            }

            // Add an item to the end of the priority queue
            _queue[_size] = new Node<T>(priority, value);
            _size++;
            // Restructure the priority queue to maintain the priority property
            RestructureUp(_size - 1);
        }

        // Return the highest priority item
        public Node<T> PeekPriority()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            return _queue[0];
        }

        // Remove the highest priority item while maintaining the priority property
        public Node<T> Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            // Remove the highest priority item from the top of the priority queue and replace it with the last item
            var item = _queue[0];
            _queue[0] = _queue[_size - 1];
            _queue[_size - 1] = null;
            _size--;
            // Restructure the priority queue to maintain the priority property
            RestructureDown(0);

            return item;
        }

        // Return an item stored at a given index (as a test helper function)
        public Node<T> GetAt(int index)
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
        private Node<T> GetLeftChild(int index) => _queue[GetLeftChildIndex(index)];

        // Return the right child of an item stored at a given index
        private Node<T> GetRightChild(int index) => _queue[GetRightChildIndex(index)];

        // Check whether an item stored at a given index has a left child or not
        private bool HasLeftChild(int index) => GetLeftChildIndex(index) < _size;

        // Check whether an item stored at a given index has a right child or not
        private bool HasRightChild(int index) => GetRightChildIndex(index) < _size;

        // Restructure the priority queue bottom-up in such ways that the priority property is maintained
        private void RestructureUp(int index)
        {
            // If it is a root item
            // Break out of the recursion
            if (index == 0)
            {
                return;
            }

            // If a current child is equal to or greater than its parent in terms of priority
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

            // If a current parent is equal to or smaller than its smaller child in terms of priority
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

        // Resize the priority queue
        private void Resize(int size)
        {
            var array = new Node<T>[size];
            Array.Copy(_queue, 0, array, 0, _size);
            _queue = array;
        }

        public class Node<NodeT>
        {
            public int Priority { get; }
            public NodeT Value { get; set; }

            public Node(
                int priority,
                NodeT value)
            {
                Priority = priority;
                Value = value;
            }
        }
    }
}