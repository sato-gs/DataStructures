namespace DataStructures.Queues
{
    using System;

    public class QueueWithLinkedList<T>
    {
        private Node<T> _front;
        private Node<T> _back;
        private int _size;

        // Represent whether a queue is empty or not
        public bool IsEmpty
        {
            get
            {
                return _size == 0;
            }
        }

        // Represent the number of items stored in a queue
        public int Count
        {
            get
            {
                return _size;
            }
        }

        // Clear a queue (and free memory) by letting GC take charge
        public void Clear()
        {
            var cur = _front;
            while (cur != null)
            {
                var next = cur.Next;
                cur = null;
                cur = next;
            }
            _front = _back = null;
            _size = 0;
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
            _size--;
            return item.Value;
        }

        // Add an item to the back of the queue
        public void Enqueue(T value)
        {
            var item = new Node<T>(value);
            if (IsEmpty)
            {
                _front = item;
            }
            else
            {
                _back.Next = item;
            }
            _back = item;
            _size++;
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

        public class Node<NodeT>
        {
            public Node(
                NodeT value,
                Node<NodeT> next = null)
            {
                Value = value;
                Next = next;
            }

            public NodeT Value { get; set; }
            public Node<NodeT> Next { get; set; }
        }
    }
}