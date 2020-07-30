namespace DataStructures.Queues
{
    using System;

    public class QueueWithDynamicArray<T>
    {
        private T[] _queue;
        private readonly int _front;
        private int _back;
        private readonly int _defaultSize = 5;

        // Represent whether a queue is empty or not
        public bool IsEmpty
        {
            get
            {
                return _back == 0;
            }
        }

        // Represent whether a queue is full or not
        public bool IsFull
        {
            get
            {
                return _back == _queue.Length;
            }
        }

        // Represent the number of items stored in a queue
        public int Count
        {
            get
            {
                return _back;
            }
        }

        public QueueWithDynamicArray()
        {
            _queue = new T[_defaultSize];
            _front = _back = 0;
        }

        public QueueWithDynamicArray(int size)
        {
            _queue = new T[size];
            _front = _back = 0;
        }

        // Clear a queue (and free memory) by setting each item to default
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

            if (_queue.Length > _defaultSize && _back < _queue.Length / 2)
            {
                Resize(Math.Max(_defaultSize, _queue.Length / 2));
            }

            return item;
        }

        // Add an item to the back of the queue
        public void Enqueue(T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultSize, _queue.Length * 2));
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

        // Resize a queue
        private void Resize(int size)
        {
            var array = new T[size];
            Array.Copy(_queue, 0, array, 0, _back);
            _queue = array;
        }
    }
}