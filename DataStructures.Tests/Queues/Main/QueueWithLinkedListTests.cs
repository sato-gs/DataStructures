namespace DataStructures.Tests.Queues.Main
{
    using System;
    using DataStructures.Queues.Main;
    using NUnit.Framework;

    public class QueueWithLinkedListTests
    {
        private QueueWithLinkedList<int> _queue;

        [SetUp]
        public void SetUp()
        {
            _queue = new QueueWithLinkedList<int>();
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
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Enqueue_WhenCalled_ShouldAddItemToBackOfQueue(int range)
        {
            // Arrange & Act
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