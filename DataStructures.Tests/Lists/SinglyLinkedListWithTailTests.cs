namespace DataStructures.Tests.Lists
{
    using System;
    using DataStructures.Lists;
    using NUnit.Framework;
    using static DataStructures.Lists.SinglyLinkedListWithTail<int>;

    public class SinglyLinkedListWithTailTests
    {

        private SinglyLinkedListWithTail<int> _list;

        [SetUp]
        public void SetUp()
        {
            _list = new SinglyLinkedListWithTail<int>();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void GetAt_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _list.GetAt(index));
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetAt_WhenIndexIsNotOutOfRange_ShouldGetNodeAtCorrectIndex(int index)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.AddLast(i);
            }

            // Act
            var result = _list.GetAt(index);

            // Assert
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(Node<int>.Value)).EqualTo(index));
        }

        [Test]
        [TestCase(-1, 100)]
        [TestCase(100, 100)]
        public void SetAt_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index, int value)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _list.SetAt(index, value));
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(5, 100)]
        [TestCase(10, 100)]
        public void SetAt_WhenIndexIsNotOutOfRange_ShouldSetNodeAtCorrectIndex(int index, int value)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.AddLast(i);
            }

            // Act
            _list.SetAt(index, value);

            // Assert
            var result = _list.GetAt(index);
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(Node<int>.Value)).EqualTo(value));
        }

        [Test]
        [TestCase(-1, 100)]
        [TestCase(100, 100)]
        public void AddAt_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index, int value)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _list.AddAt(index, value));
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(5, 100)]
        [TestCase(10, 100)]
        public void AddAt_WhenIndexIsNotOutOfRange_ShouldAddNodeToCorrectIndexAndIncrementSize(int index, int value)
        {
            // Arrange
            for (var i = 0; i < 10; i++)
            {
                _list.AddLast(i);
            }
            var prevSize = _list.Size;
            var prev = index == 0 ? null : _list.GetAt(index - 1);
            var next = index == prevSize ? null : _list.GetAt(index);

            // Act
            _list.AddAt(index, value);

            // Assert
            var result = _list.GetAt(index);
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(Node<int>.Value)).EqualTo(value));
            Assert.That(prev?.Next, Is.EqualTo(index == 0 ? null : result));
            Assert.That(result.Next, Is.EqualTo(next));
            Assert.That(_list.Size, Is.EqualTo(prevSize + 1));
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(5, 100)]
        [TestCase(10, 100)]
        public void AddFirst_WhenCalled_ShouldAddNodeToHeadAndIncrementSize(int range, int value)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.AddLast(i);
            }
            var prevSize = _list.Size;
            var next = range == 0 ? null : _list.GetAt(0);

            // Act
            _list.AddFirst(value);

            // Assert
            var result = _list.GetAt(0);
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(Node<int>.Value)).EqualTo(value));
            Assert.That(result.Next, Is.EqualTo(next));
            Assert.That(_list.Size, Is.EqualTo(prevSize + 1));
            Assert.That(_list.Head, Is.EqualTo(result));
            if (prevSize == 0)
            {
                Assert.That(_list.Tail, Is.EqualTo(result));
            }
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(5, 100)]
        [TestCase(10, 100)]
        public void AddLast_WhenCalled_ShouldAddNodeToTailAndIncrementSize(int range, int value)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.AddLast(i);
            }
            var prevSize = _list.Size;
            var prev = range == 0 ? null : _list.GetAt(_list.Size - 1);

            // Act
            _list.AddLast(value);

            // Assert
            var result = _list.GetAt(_list.Size - 1);
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(Node<int>.Value)).EqualTo(value));
            Assert.That(prev?.Next, Is.EqualTo(range == 0 ? null : result));
            Assert.That(result.Next, Is.Null);
            Assert.That(_list.Size, Is.EqualTo(prevSize + 1));
            Assert.That(_list.Tail, Is.EqualTo(result));
            if (prevSize == 0)
            {
                Assert.That(_list.Head, Is.EqualTo(result));
            }
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void RemoveAt_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _list.RemoveAt(index));
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void RemoveAt_WhenIndexIsNotOutOfRange_ShouldRemoveNodeFromCorrectIndexAndDecrementSize(int index)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.AddLast(i);
            }
            var prevSize = _list.Size;
            var prev = index == 0 ? null : _list.GetAt(index - 1);
            var next = index == _list.Size - 1 ? null : _list.GetAt(index + 1);

            // Act
            var result = _list.RemoveAt(index);

            // Assert
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(Node<int>.Value)).EqualTo(index));
            Assert.That(prev?.Next, Is.EqualTo(index == 0 ? null : next));
            Assert.That(_list.Size, Is.EqualTo(prevSize - 1));
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void RemoveFirst_WhenCalled_ShouldRemoveNodeFromHeadAndDecrementSize(int range)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.AddLast(i);
            }
            var prevSize = _list.Size;
            var head = range == 0 ? null : _list.GetAt(1);

            // Act
            var result = _list.RemoveFirst();

            // Assert
            if (range == 0)
            {
                Assert.That(result, Is.Null);
            }
            else
            {
                Assert.That(
                    result,
                    Is.Not.Null
                    .And.Property(nameof(Node<int>.Value)).EqualTo(0));
                Assert.That(_list.GetAt(0), Is.Not.EqualTo(result));
            }
            Assert.That(_list.Size, Is.EqualTo(Math.Max(0, prevSize - 1)));
            Assert.That(_list.Head, Is.EqualTo(head));
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void RemoveLast_WhenCalled_ShouldRemoveNodeFromTailAndDecrementSize(int range)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.AddLast(i);
            }
            var prevSize = _list.Size;
            var prev = range == 0 ? null : _list.GetAt(range - 2);

            // Act
            var result = _list.RemoveLast();

            // Assert
            if (range == 0)
            {
                Assert.That(result, Is.Null);
            }
            else
            {
                Assert.That(
                    result,
                    Is.Not.Null
                    .And.Property(nameof(Node<int>.Value)).EqualTo(range - 1));
                Assert.That(_list.GetAt(_list.Size - 1), Is.Not.EqualTo(result));
            }
            Assert.That(prev?.Next, Is.Null);
            Assert.That(_list.Size, Is.EqualTo(Math.Max(0, prevSize - 1)));
            Assert.That(_list.Tail, Is.EqualTo(prev));
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Reverse_WhenCalled_ShouldReverseNodes(int range)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.AddLast(i);
            }
            var head = _list.GetAt(0);
            var tail = _list.GetAt(_list.Size - 1);

            // Act
            _list.Reverse();

            // Assert
            for (var i = 0; i < range; i++)
            {
                var result = _list.GetAt(i);
                Assert.That(
                    result,
                    Is.Not.Null
                    .And.Property(nameof(Node<int>.Value)).EqualTo(range - 1 - i));
            }
            Assert.That(_list.Head, Is.EqualTo(tail));
            Assert.That(_list.Tail, Is.EqualTo(head));
        }
    }
}