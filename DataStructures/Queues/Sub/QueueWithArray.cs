namespace DataStructures.Queues.Sub
{
    using System;

    // Queue implemented using Array
    public class QueueWithArray<T>
    {
        // Represent the queue
        private T[] _queue;
        // Represent the default capacity of the queue
        private readonly int _defaultCapacity = 5;

        // Represent the number of items stored in the queue
        public int Size { get; private set; }

        // Represent whether the queue is empty or not
        public bool IsEmpty
        {
            get
            {
                return Size == 0;
            }
        }

        // Represent whether the queue is full or not
        public bool IsFull
        {
            get
            {
                return Size == _queue.Length;
            }
        }
        
        public QueueWithArray()
        {
            _queue = new T[_defaultCapacity];
            Size = 0;
        }

        public QueueWithArray(int capacity)
        {
            if (capacity <= 0)
            {
                throw new InvalidOperationException();
            }

            _queue = new T[capacity];
            Size = 0;
        }

        // Return an item from the front of the queue
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _queue[0];
        }

        // Add an item to the back of the queue
        public void Enqueue(T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultCapacity, _queue.Length * 2));
            }

            _queue[Size++] = value;
        }

        // Remove an item from the front of the queue
        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            var item = _queue[0];
            for (var i = 0; i < Size - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }
            _queue[--Size] = default;
            return item;
        }

        // Clear the queue
        public void Clear()
        {
            // Note that C# built-in function can be alternatively used as follows
            // Array.Clear(_queue, 0, Size);
            for (var i = 0; i < Size; i++)
            {
                _queue[i] = default;
            }
            Size = 0;
        }

        // Resize the queue to a given capacity
        private void Resize(int capacity)
        {
            var queue = new T[capacity];
            // Note that C# built-in function can be alternatively used as follows
            // Array.Copy(_queue, queue, Size);
            for (var i = 0; i < Size; i++)
            {
                queue[i] = _queue[i];
            }
            _queue = queue;
        }
    }
}