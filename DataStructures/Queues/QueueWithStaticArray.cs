namespace DataStructures.Queues
{
    using System;

    // Queue implemented using static Array (e.g. Array without resize feature)
    public class QueueWithStaticArray<T>
    {
        // Represent the queue
        private readonly T[] _queue;
        // Represent the front of the queue
        private readonly int _front;
        // Represent the back of the queue
        private int _back;

        // Represent whether the queue is empty or not
        public bool IsEmpty
        {
            get
            {
                return _back == 0;
            }
        }

        // Represent whether the queue is full or not
        public bool IsFull
        {
            get
            {
                return _back == _queue.Length;
            }
        }

        // Represent the number of items stored in the queue
        public int Count
        {
            get
            {
                return _back;
            }
        }

        public QueueWithStaticArray(int capacity)
        {
            _queue = new T[capacity];
            _front = _back = 0;
        }

        // Clear the queue (and free memory) by setting each item to default
        public void Clear()
        {
            Array.Clear(_queue, _front, _back);
            _back = 0;
        }

        // Remove an item from the front of the queue
        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            var item = _queue[_front];
            for (var i = _front; i < _back - 1; i++)
            {
                _queue[i] = _queue[i + 1];
            }
            // Reset the back of the queue (which is not processed above)
            _queue[--_back] = default;
            return item;
        }

        // Add an item to the back of the queue
        public void Enqueue(T value)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("The queue is full.");
            }

            _queue[_back++] = value;
        }

        // Return an item from the front of the queue
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _queue[_front];
        }
    }
}