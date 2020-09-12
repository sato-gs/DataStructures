namespace DataStructures.Queues.Sub
{
    using System;

    // Queue implemented using linked list
    public class QueueWithLinkedList<T>
    {
        // Represent the front of the queue
        private Node _front;
        // Represent the back of the queue
        private Node _back;

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

        // Return an item from the front of the queue
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            return _front.Value;
        }

        // Add an item to the back of the queue
        public void Enqueue(T value)
        {
            var item = new Node(value);
            if (IsEmpty)
            {
                _front = item;
            }
            else
            {
                _back.Next = item;
            }
            _back = item;
            Size++;
        }

        // Remove an item from the front of the queue
        public T Dequeue()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("The queue is empty.");
            }

            var item = _front;
            _front = _front.Next;
            Size--;
            return item.Value;
        }

        // Clear the queue
        public void Clear()
        {
            var cur = _front;
            while (cur != null)
            {
                var next = cur.Next;
                // Free memory by breaking associations
                cur.Next = null;
                cur = null;
                cur = next;
            }
            _front = _back = null;
            Size = 0;
        }

        public class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }
            public Node(
                T value,
                Node next = null)
            {
                Value = value;
                Next = next;
            }
        }
    }
}