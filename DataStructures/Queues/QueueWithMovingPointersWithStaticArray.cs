namespace DataStructures.Queues
{
    using System;

    // Queue with moving pointers implemented using static Array (e.g. Array without resize feature)
    public class QueueWithMovingPointersWithStaticArray<T>
    {
        // Represent the queue
        private readonly T[] _queue;
        // Represent the front of the queue
        private int _front;
        // Represent the back of the queue
        private int _back;
        // Represent the current size of the queue
        private int _size;

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

        public QueueWithMovingPointersWithStaticArray(int capacity)
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
                throw new InvalidOperationException("The queue is full.");
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
    }
}