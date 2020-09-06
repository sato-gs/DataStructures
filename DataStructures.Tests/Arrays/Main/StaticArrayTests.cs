namespace DataStructures.Tests.Arrays.Main
{
    using System;
    using DataStructures.Arrays.Main;
    using NUnit.Framework;

    public class StaticArrayTests
    {
        private StaticArray<int> _array;
        private readonly int _capacity = 5;

        [SetUp]
        public void SetUp()
        {
            _array = new StaticArray<int>(_capacity);
        }

        [Test]
        [TestCase(-100)]
        [TestCase(0)]
        public void Constructor_WhenCapacityIsLessThanOrEqualToZero_ShouldThrowInvalidOperationException(int capacity)
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => new StaticArray<int>(capacity));
        }

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        public void Constructor_WhenCapacityIsGreaterThanZero_ShouldNotThrowInvalidOperationException(int capacity)
        {
            // Arrange & Act & Assert
            Assert.DoesNotThrow(() => new StaticArray<int>(capacity));
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
                _array.Add(i);
            }

            // Act
            var result = _array.Size;

            // Assert
            Assert.That(result, Is.EqualTo(range));
        }

        [Test]
        public void IsEmpty_WhenArrayIsEmpty_ShouldReturnTrue()
        {
            // Arrange & Act & Assert
            Assert.That(_array.IsEmpty, Is.EqualTo(true));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void IsEmpty_WhenArrayIsNotEmpty_ShouldReturnFalse(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.IsEmpty;

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsFull_WhenArrayIsFull_ShouldReturnTrue()
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.IsFull;

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsFull_WhenArrayIsNotFull_ShouldReturnFalse()
        {
            // Arrange & Act & Assert
            Assert.That(_array.IsFull, Is.EqualTo(false));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void Get_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _array.Get(index));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Get_WhenIndexIsNotOutOfRange_ShouldGetItemAtGivenIndex(int index)
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.Get(index);

            // Assert
            Assert.That(result, Is.EqualTo(index + 1));
        }

        [Test]
        [TestCase(-1, -1)]
        [TestCase(100, 100)]
        public void Set_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index, int value)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _array.Set(index, value));
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(1, 100)]
        [TestCase(2, 100)]
        [TestCase(3, 100)]
        [TestCase(4, 100)]
        public void Set_WhenIndexIsNotOutOfRange_ShouldSetItemToGivenValueAtGivenIndex(int index, int value)
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            _array.Set(index, value);

            // Assert
            Assert.That(_array.Get(index), Is.EqualTo(value));
        }

        [Test]
        public void Add_WhenArrayIsFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _array.Add(100));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Add_WhenArrayIsNotFull_ShouldAddItemWithGivenValueToEndOfArray(int range)
        {
            // Arrange & Act
            for (var i = 1; i <= range; i++)
            {
                _array.Add(i);
            }

            // Assert
            Assert.That(_array.Size, Is.EqualTo(range));
            for (var i = 1; i <= range; i++)
            {
                Assert.That(_array.Get(i - 1), Is.EqualTo(i));
            }
        }

        [Test]
        [TestCase(-1, -1)]
        [TestCase(100, 100)]
        public void AddAt_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index, int value)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _array.AddAt(index, value));
        }

        [Test]
        [TestCase(0, 100)]
        [TestCase(1, 100)]
        [TestCase(2, 100)]
        [TestCase(3, 100)]
        public void AddAt_WhenIndexIsNotOutOfRange_ShouldAddItemWithGivenValueAtGivenIndex(int index, int value)
        {
            // Arrange
            for (var i = 0; i < _capacity - 1; i++)
            {
                _array.Add(i);
            }

            // Act
            _array.AddAt(index, value);

            // Assert
            Assert.That(_array.Size, Is.EqualTo(_capacity));
            for (var i = 0; i < index; i++)
            {
                Assert.That(_array.Get(i), Is.EqualTo(i));
            }
            for (var i = index; i < _capacity; i++)
            {
                Assert.That(_array.Get(i), Is.EqualTo(i == index ? value : i - 1));
            }
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void Remove_WhenItemWithGivenValueDoesNotExist_ShouldNotRemoveItemAndReturnFalse(int value)
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.Remove(value);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_array.Size, Is.EqualTo(_capacity));
            for (var i = 1; i <= _capacity; i++)
            {
                Assert.That(_array.Get(i - 1), Is.EqualTo(i));
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Remove_WhenItemWithGivenValueExists_ShouldRemoveItemAndReturnTrue(int value)
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.Remove(value);

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_array.Size, Is.EqualTo(_capacity - 1));
            for (var i = 1; i < value; i++)
            {
                Assert.That(_array.Get(i - 1), Is.EqualTo(i));
            }
            for (var i = value; i < _capacity; i++)
            {
                Assert.That(_array.Get(i - 1), Is.EqualTo(i + 1));
            }
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void RemoveAt_WhenIndexIsOutOfRange_ShouldThrowOutOfRangeException(int index)
        {
            // Arrange & Act & Assert
            Assert.Throws<IndexOutOfRangeException>(() => _array.RemoveAt(index));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void RemoveAt_WhenIndexIsNotOutOfRange_ShouldRemoveAndReturnItemAtGivenIndex(int index)
        {
            // Arrange
            for (var i = 0; i < _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.RemoveAt(index);

            // Assert
            Assert.That(result, Is.EqualTo(index));
            Assert.That(_array.Size, Is.EqualTo(_capacity - 1));
            for (var i = 0; i < index; i++)
            {
                Assert.That(_array.Get(i), Is.EqualTo(i));
            }
            for (var i = index; i < _capacity - 1; i++)
            {
                Assert.That(_array.Get(i), Is.EqualTo(i + 1));
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Clear_WhenCalled_ShouldClearArray(int range)
        {
            // Arrange
            for (var i = 1; i <= range; i++)
            {
                _array.Add(i);
            }

            // Act
            _array.Clear();

            // Assert
            Assert.That(_array.IsEmpty, Is.EqualTo(true));
            Assert.That(_array.Size, Is.EqualTo(0));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void IndexOf_WhenItemWithGivenValueDoesNotExist_ShouldReturnMinusOne(int value)
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.IndexOf(value);

            // Assert
            Assert.That(result, Is.EqualTo(-1));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void IndexOf_WhenItemWithGivenValueExists_ShouldReturnIndex(int value)
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.IndexOf(value);

            // Assert
            Assert.That(result, Is.EqualTo(value - 1));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(100)]
        public void Contains_WhenItemWithGivenValueDoesNotExist_ShouldReturnFalse(int value)
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.Contains(value);

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void Contains_WhenItemWithGivenValueExists_ShouldReturnTrue(int value)
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _array.Add(i);
            }

            // Act
            var result = _array.Contains(value);

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }
    }
}