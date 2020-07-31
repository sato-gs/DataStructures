namespace DataStructures.Tests.Stacks
{
    using System;
    using DataStructures.Stacks;
    using NUnit.Framework;

    public class StackWithStaticArrayTests
    {
        private StackWithStaticArray<int> _stack;
        private readonly int _capacity = 5;

        [SetUp]
        public void SetUp()
        {
            _stack = new StackWithStaticArray<int>(_capacity);
        }

        [Test]
        public void IsEmpty_WhenStackIsEmpty_ShouldReturnTrue()
        {
            // Arrange & Act & Assert
            Assert.That(_stack.IsEmpty, Is.EqualTo(true));
        }

        [Test]
        public void IsEmpty_WhenStackIsNotEmpty_ShouldReturnFalse()
        {
            // Arrange
            _stack.Push(1);

            // Act
            var result = _stack.IsEmpty;
            
            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsFull_WhenStackIsFull_ShouldReturnTrue()
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _stack.Push(i);
            }

            // Act
            var result = _stack.IsFull;

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsFull_WhenStackIsNotFull_ShouldReturnFalse()
        {
            // Arrange & Act & Assert
            Assert.That(_stack.IsFull, Is.EqualTo(false));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Count_WhenCalled_ShouldReturnNumberOfItems(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _stack.Push(i);
            }

            // Act
            var result = _stack.Count;

            // Assert
            Assert.That(result, Is.EqualTo(range));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Clear_WhenCalled_ShouldClearStack(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _stack.Push(i);
            }

            // Act
            _stack.Clear();

            // Assert
            Assert.That(_stack.IsEmpty, Is.EqualTo(true));
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Peek_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _stack.Peek());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Peek_WhenStackIsNotEmpty_ShouldReturnItemFromTopOfStack(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _stack.Push(i);
            }

            // Act
            var result = _stack.Peek();

            // Assert
            Assert.That(result, Is.EqualTo(range));
        }

        [Test]
        public void Pop_WhenStackIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _stack.Pop());
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Pop_WhenStackIsNotEmpty_ShouldRemoveItemFromTopOfStack(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _stack.Push(i);
            }
            var prevSize = _stack.Count;

            // Act
            var result = _stack.Pop();

            // Assert
            Assert.That(result, Is.EqualTo(range));
            Assert.That(_stack.Count, Is.EqualTo(prevSize - 1));
        }

        [Test]
        public void Push_WhenStackIsFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _stack.Push(i);
            }

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _stack.Push(100));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Push_WhenStackIsNotFull_ShouldAddItemToTopOfStack(int range)
        {
            // Arrange & Act
            for (var i = 1; i <= range; i++)
            {
                _stack.Push(i);
            }

            // Assert
            Assert.That(_stack.Peek(), Is.EqualTo(range));
            Assert.That(_stack.Count, Is.EqualTo(range));
        }
    }
}