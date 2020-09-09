namespace DataStructures.Tests.Lists.Sub
{
    using System;
    using DataStructures.Lists.Sub;
    using NUnit.Framework;

    public class DoublyLinkedListTests
    {
        private DoublyLinkedList<int> _list;

        [SetUp]
        public void SetUp()
        {
            _list = new DoublyLinkedList<int>();
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void Size_WhenCalled_ShouldReturnNumberOfNodes(int range)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.Add(i);
            }

            // Act
            var result = _list.Size;

            // Assert
            Assert.That(result, Is.EqualTo(range));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void Get_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _list.Get(index));
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void Get_WhenIndexIsNotOutOfRange_ShouldGetNodeAtGivenIndex(int index)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }

            // Act
            var result = _list.Get(index);

            // Assert
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(index));
        }

        [Test]
        [TestCase(-1, -1)]
        [TestCase(100, 100)]
        public void Set_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index, int value)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _list.Set(index, value));
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(5, 100)]
        [TestCase(10, 100)]
        public void Set_WhenIndexIsNotOutOfRange_ShouldSetNodeToGivenValueAtGivenIndex(int index, int value)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }

            // Act
            _list.Set(index, value);

            // Assert
            Assert.That(
                _list.Get(index),
                Is.Not.Null
                .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(value));
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(5, 100)]
        [TestCase(10, 100)]
        public void Add_WhenCalled_ShouldAddNodeToTailOfLinkedList(int range, int value)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.Add(i);
            }
            var prevSize = _list.Size;
            var prev = _list.Size == 0 ? null : _list.Get(_list.Size - 1);

            // Act
            _list.Add(value);

            // Assert
            var result = _list.Get(_list.Size - 1);
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(value));
            Assert.That(prev?.Next, Is.EqualTo(_list.Size == 1 ? null : result));
            Assert.That(result.Prev, Is.EqualTo(prev));
            Assert.That(result.Next, Is.Null);
            Assert.That(_list.Size, Is.EqualTo(prevSize + 1));
            if (prevSize == 0)
            {
                Assert.That(_list.Head, Is.EqualTo(result));
            }
        }

        [Test]
        [TestCase(-1, -1)]
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
        public void AddAt_WhenIndexIsNotOutOfRange_ShouldAddNodeWithGivenValueAtGivenIndex(int index, int value)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }
            var prevSize = _list.Size;
            var prev = index == 0 ? null : _list.Get(index - 1);
            var next = _list.Get(index);

            // Act
            _list.AddAt(index, value);

            // Assert
            var result = _list.Get(index);
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(value));
            Assert.That(prev?.Next, Is.EqualTo(index == 0 ? null : result));
            Assert.That(result.Prev, Is.EqualTo(prev));
            Assert.That(result.Next, Is.EqualTo(next));
            Assert.That(next.Prev, Is.EqualTo(result));
            Assert.That(_list.Size, Is.EqualTo(prevSize + 1));
            if (index == 0)
            {
                Assert.That(_list.Head, Is.EqualTo(result));
            }
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(5, 100)]
        [TestCase(10, 100)]
        public void AddFirst_WhenCalled_ShouldAddNodeWithGivenValueToHeadOfLinkedList(int range, int value)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.Add(i);
            }
            var prevSize = _list.Size;
            var next = _list.Size == 0 ? null : _list.Get(0);

            // Act
            _list.AddFirst(value);

            // Assert
            var result = _list.Get(0);
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(value));
            Assert.That(result.Prev, Is.Null);
            Assert.That(result.Next, Is.EqualTo(next));
            Assert.That(next?.Prev, Is.EqualTo(_list.Size == 1 ? null : result));
            Assert.That(_list.Size, Is.EqualTo(prevSize + 1));
            Assert.That(_list.Head, Is.EqualTo(result));
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(5, 100)]
        [TestCase(10, 100)]
        public void AddLast_WhenCalled_ShouldAddNodeWithGivenValueToTailOfLinkedList(int range, int value)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.Add(i);
            }
            var prevSize = _list.Size;
            var prev = _list.Size == 0 ? null : _list.Get(_list.Size - 1);

            // Act
            _list.AddLast(value);

            // Assert
            var result = _list.Get(_list.Size - 1);
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(value));
            Assert.That(prev?.Next, Is.EqualTo(_list.Size == 1 ? null : result));
            Assert.That(result.Prev, Is.EqualTo(prev));
            Assert.That(result.Next, Is.Null);
            Assert.That(_list.Size, Is.EqualTo(prevSize + 1));
            if (prevSize == 0)
            {
                Assert.That(_list.Head, Is.EqualTo(result));
            }
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void Remove_WhenNodeWithGivenValueDoesNotExist_ShouldNotRemoveNodeWithGivenValueAndReturnFalse(int value)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }
            var prevSize = _list.Size;

            // Act
            var result = _list.Remove(value);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_list.Size, Is.EqualTo(prevSize));
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void Remove_WhenNodeWithGivenValueExists_ShouldRemoveNodeWithGivenValueAndReturnTrue(int index)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }
            var prevSize = _list.Size;
            var prev = index == 0 ? null : _list.Get(index - 1);
            var next = index == _list.Size - 1 ? null : _list.Get(index + 1);

            // Act
            var result = _list.Remove(index);

            // Assert
            if (index != 10)
            {
                Assert.That(
                    _list.Get(index),
                    Is.EqualTo(next));
            }
            Assert.That(result, Is.EqualTo(true));
            Assert.That(prev?.Next, Is.EqualTo(index == 0 ? null : next));
            Assert.That(next?.Prev, Is.EqualTo(index == 10 ? null : prev));
            Assert.That(_list.Size, Is.EqualTo(prevSize - 1));
            if (index == 0)
            {
                Assert.That(_list.Head, Is.Not.EqualTo(result));
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
        public void RemoveAt_WhenIndexIsNotOutOfRange_ShouldRemoveNodeAtGivenIndex(int index)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }
            var prevSize = _list.Size;
            var prev = index == 0 ? null : _list.Get(index - 1);
            var next = index == _list.Size - 1 ? null : _list.Get(index + 1);

            // Act
            var result = _list.RemoveAt(index);

            // Assert
            if (index != 10)
            {
                Assert.That(
                    _list.Get(index),
                    Is.Not.EqualTo(result)
                    .And.EqualTo(next));
            }
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(index));
            Assert.That(prev?.Next, Is.EqualTo(index == 0 ? null : next));
            Assert.That(next?.Prev, Is.EqualTo(index == 10 ? null : prev));
            Assert.That(_list.Size, Is.EqualTo(prevSize - 1));
            if (index == 0)
            {
                Assert.That(_list.Head, Is.Not.EqualTo(result));
            }
        }

        [Test]
        public void RemoveFirst_WhenLinkedListIsEmpty_ShouldThrowIndexOutOfRangeException()
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _list.RemoveFirst());
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void RemoveFirst_WhenLinkedListIsNotEmpty_ShouldRemoveNodeFromHeadOfLinkedList(int range)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.Add(i);
            }
            var prevSize = _list.Size;
            var next = _list.Size <= 1 ? null : _list.Get(1);

            // Act
            var result = _list.RemoveFirst();

            // Assert
            if (_list.Size > 0)
            {
                Assert.That(
                    _list.Get(0),
                    Is.Not.EqualTo(result)
                    .And.EqualTo(next));
            }
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(0));
            Assert.That(next?.Prev, Is.Null);
            Assert.That(_list.Size, Is.EqualTo(prevSize - 1));
            Assert.That(_list.Head, Is.EqualTo(next));
        }

        [Test]
        public void RemoveLast_WhenLinkedListIsEmpty_ShouldThrowIndexOutOfRangeException()
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _list.RemoveLast());
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void RemoveLast_WhenLinkedListIsNotEmpty_ShouldRemoveNodeFromTailOfLinkedList(int range)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.Add(i);
            }
            var prevSize = _list.Size;
            var prev = _list.Size <= 1 ? null : _list.Get(_list.Size - 2);

            // Act
            var result = _list.RemoveLast();

            // Assert
            if (_list.Size > 0)
            {
                Assert.That(
                    _list.Get(_list.Size - 1),
                    Is.Not.EqualTo(result)
                    .And.EqualTo(prev));
            }
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(range - 1));
            Assert.That(prev?.Next, Is.Null);
            Assert.That(_list.Size, Is.EqualTo(prevSize - 1));
            if (_list.Size == 0)
            {
                Assert.That(_list.Head, Is.Null);
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void Clear_WhenCalled_ShouldClearNodes(int range)
        {
            // Arrange
            for (var i = 0; i < range; i++)
            {
                _list.Add(i);
            }

            // Act
            _list.Clear();

            // Assert
            Assert.That(_list.Size, Is.EqualTo(0));
            Assert.That(_list.Head, Is.Null);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void IndexOf_WhenNodeWithGivenValueDoesNotExist_ShouldReturnMinusOne(int value)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }

            // Act
            var result = _list.IndexOf(value);

            // Assert
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void IndexOf_WhenNodeWithGivenValueExists_ShouldReturnIndex(int value)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }

            // Act
            var result = _list.IndexOf(value);

            // Assert
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void Contains_WhenNodeWithGivenValueDoesNotExist_ShouldReturnFalse(int value)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }

            // Act
            var result = _list.Contains(value);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Contains_WhenNodeWithGivenValueExists_ShouldReturnTrue(int value)
        {
            // Arrange
            for (var i = 0; i <= 10; i++)
            {
                _list.Add(i);
            }

            // Act
            var result = _list.Contains(value);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        [TestCase(0)]
        [TestCase(5)]
        [TestCase(10)]
        public void Reverse_WhenCalled_ShouldReverseNodes(int range)
        {
            // Arrange
            for (var i = 0; i <= range; i++)
            {
                _list.Add(i);
            }
            var tail = _list.Get(_list.Size - 1);

            // Act
            _list.Reverse();

            // Assert
            for (var i = 0; i <= range; i++)
            {
                var result = _list.Get(i);
                Assert.That(
                    result,
                    Is.Not.Null
                    .And.Property(nameof(DoublyLinkedList<int>.Node.Value)).EqualTo(range - i));
            }
            Assert.That(_list.Head, Is.EqualTo(tail));
        }
    }
}