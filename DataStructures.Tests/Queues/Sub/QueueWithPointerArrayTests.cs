namespace DataStructures.Tests.Queues.Sub
{
    using System;
    using DataStructures.Queues.Sub;
    using NUnit.Framework;

    public class QueueWithPointerArrayTests
    {
        private QueueWithPointerArray<int> _queue;
        private readonly int _capacity = 5;

        [SetUp]
        public void SetUp()
        {
            _queue = new QueueWithPointerArray<int>(_capacity);
        }

        [Test]
        [TestCase(-100)]
        [TestCase(0)]
        public void Constructor_WhenCapacityIsLessThanOrEqualToZero_ShouldThrowInvalidOperationException(int capacity)
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => new QueueWithPointerArray<int>(capacity));
        }

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        public void Constructor_WhenCapacityIsGreaterThanZero_ShouldNotThrowInvalidOperationException(int capacity)
        {
            // Arrange & Act & Assert
            Assert.DoesNotThrow(() => new QueueWithPointerArray<int>(capacity));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Size_WhenCalled_ShouldReturnNumberOfItems(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _queue.Enqueue(i);
            }

            // Act
            var result = _queue.Size;

            // Assert
            Assert.That(result, Is.EqualTo(range));
        }

        [Test]
        public void IsEmpty_WhenQueueIsEmpty_ShouldReturnTrue()
        {
            // Arrange & Act & Assert
            Assert.That(_queue.IsEmpty, Is.EqualTo(true));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void IsEmpty_WhenQueueIsNotEmpty_ShouldReturnFalse(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _queue.Enqueue(i);
            }

            // Act
            var result = _queue.IsEmpty;

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsFull_WhenQueueIsFull_ShouldReturnTrue()
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _queue.Enqueue(i);
            }

            // Act
            var result = _queue.IsFull;

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsFull_WhenQueueIsNotFull_ShouldReturnFalse()
        {
            // Arrange & Act & Assert
            Assert.That(_queue.IsFull, Is.EqualTo(false));
        }

        [Test]
        public void Peek_WhenQueueIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _queue.Peek());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Peek_WhenQueueIsNotEmpty_ShouldReturnItemFromFrontOfQueue(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _queue.Enqueue(i);
            }

            // Act
            var result = _queue.Peek();

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(1, 10)]
        [TestCase(1, 100)]
        [TestCase(50, 1)]
        [TestCase(50, 10)]
        [TestCase(50, 100)]
        [TestCase(100, 1)]
        [TestCase(100, 10)]
        [TestCase(100, 100)]
        public void Enqueue_WhenCalled_ShouldAddItemToBackOfQueue(int capacity, int range)
        {
            // Arrange
            _queue = new QueueWithPointerArray<int>(capacity);

            // Act
            for (var i = 1; i <= range; i++)
            {
                _queue.Enqueue(i);
            }

            // Assert
            Assert.That(_queue.Peek(), Is.EqualTo(1));
            Assert.That(_queue.Size, Is.EqualTo(range));
        }

        [Test]
        public void Dequeue_WhenQueueIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _queue.Dequeue());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Dequeue_WhenQueueIsNotEmpty_ShouldRemoveItemFromFrontOfQueue(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _queue.Enqueue(i);
            }
            var prevSize = _queue.Size;

            // Act
            var result = _queue.Dequeue();

            // Assert
            Assert.That(result, Is.EqualTo(1));
            Assert.That(_queue.Size, Is.EqualTo(prevSize - 1));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Clear_WhenCalled_ShouldClearQueue(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _queue.Enqueue(i);
            }

            // Act
            _queue.Clear();

            // Assert
            Assert.That(_queue.IsEmpty, Is.EqualTo(true));
            Assert.That(_queue.Size, Is.EqualTo(0));
        }
    }
}