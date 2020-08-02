namespace DataStructures.Queues
{
    using System;

    // Circular queue implemented using dynamic Array (e.g. Array with resize feature)
    public class CircularQueueWithDynamicArray<T>
    {
        // Represent the queue
        private T[] _queue;
        // Represent the front of the queue
        private int _front;
        // Represent the back of the queue
        private int _back;
        // Represent the current size of the queue
        private int _size;
        // Represent the default capacity of the queue
        private readonly int _defaultCapacity = 5;

        // Represent whether the queue is empty or not
        public bool IsEmpty
        {
            get
            {
                return _size == 0;
            }
        }

        // Represent whether the queue is full or not
        public bool IsFull
        {
            get
            {
                return _size == _queue.Length;
            }
        }

        // Represent the number of items stored in the queue
        public int Count
        {
            get
            {
                return _size;
            }
        }

        public CircularQueueWithDynamicArray()
        {
            _queue = new T[_defaultCapacity];
            _front = _back = _size = 0;
        }

        public CircularQueueWithDynamicArray(int capacity)
        {
            _queue = new T[capacity];
            _front = _back = _size = 0;
        }

        // Clear the queue (and free memory) by setting each item to default
        public void Clear()
        {
            Array.Clear(_queue, 0, _queue.Length);
            _front = _back = _size = 0;
        }

        // Remove an item from the front of the queue
        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            var item = _queue[_front];
            _queue[_front] = default;
            _front = (_front + 1) % _queue.Length;
            _size--;

            return item;
        }

        // Add an item to the back of the queue
        public void Enqueue(T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultCapacity, _queue.Length * 2));
            }

            _queue[_back] = value;
            _back = (_back + 1) % _queue.Length;
            _size++;
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

        // Resize the queue
        private void Resize(int capacity)
        {
            var array = new T[capacity];
            // If the front index < the back index
            if (_front < _back)
            {
                // Copy items between _front --- _back onto 0 --- _size
                Array.Copy(_queue, _front, array, 0, _size);
            }
            // If the back index <= the front index
            else
            {
                // Copy items between _front --- end onto 0 --- ... first
                Array.Copy(_queue, _front, array, 0, _queue.Length - _front);
                // Copy items between 0 --- _back onto ... --- ... second
                Array.Copy(_queue, 0, array, _queue.Length - _front, _back);
            }

            _queue = array;
            _front = 0;
            _back = _size == capacity ? 0 : _size;
        }
    }
}