namespace DataStructures.Queues.Sub
{
    using System;

    // Queue implemented using pointer Array
    public class QueueWithPointerArray<T>
    {
        // Represent the queue
        private T[] _queue;
        // Represent the front of the queue
        private int _front;
        // Represent the back of the queue
        private int _back;
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

        public QueueWithPointerArray()
        {
            _queue = new T[_defaultCapacity];
            _front = _back = Size = 0;
        }

        public QueueWithPointerArray(int capacity)
        {
            if (capacity <= 0)
            {
                throw new InvalidOperationException();
            }

            _queue = new T[capacity];
            _front = _back = Size = 0;
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

        // Add an item to the back of the queue
        public void Enqueue(T value)
        {
            if (IsFull)
            {
                Resize(Math.Max(_defaultCapacity, _queue.Length * 2));
            }

            _queue[_back] = value;
            _back = (_back + 1) % _queue.Length;
            Size++;
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
            Size--;

            return item;
        }

        // Clear the queue
        public void Clear()
        {
            // Note that C# built-in function can be alternatively used as follows
            // Array.Clear(_queue, 0, _queue.Length);
            for (var i = 0; i < _queue.Length; i++)
            {
                _queue[i] = default;
            }
            _front = _back = Size = 0;
        }

        // Resize the queue to a given capacity
        private void Resize(int capacity)
        {
            var queue = new T[capacity];
            // If front < back
            // Copy items between _front --- _back onto 0 --- ...
            if (_front < _back)
            {
                // Note that C# built-in function can be alternatively used as follows
                // Array.Copy(_queue, _front, queue, 0, Size);
                for (var i = _front; i < _back; i++)
                {
                    queue[i - _front] = _queue[i];
                }
            }
            // If back <= front
            // Copy items between _front --- end onto 0 --- ... first
            // Copy items between 0 --- _back onto ... --- ... second
            else
            {
                // Note that C# built-in functions can be alternatively used as follows
                // Array.Copy(_queue, _front, queue, 0, _queue.Length - _front);
                // Array.Copy(_queue, 0, queue, _queue.Length - _front, _back);
                var counter = 0;
                for (var i = _front; i < _queue.Length; i++)
                {
                    queue[counter++] = _queue[i];
                }
                for (var i = 0; i < _back; i++)
                {
                    queue[counter++] = _queue[i];
                }
            }

            _queue = queue;
            _front = 0;
            _back = Size;
        }
    }
}