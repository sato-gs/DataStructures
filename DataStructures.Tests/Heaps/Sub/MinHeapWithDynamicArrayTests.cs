namespace DataStructures.Tests.Heaps.Sub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataStructures.Heaps.Sub;
    using NUnit.Framework;

    public class MinHeapWithDynamicArrayTests
    {
        private MinHeapWithDynamicArray<int> _heap;
        private readonly int _capacity = 10;
        private readonly int[] _values = new int[] { 5, 3, 7, 2, 4, 6, 9, 1, 8, 10 };

        private readonly Dictionary<int, int[]> _answersForInsertion = new Dictionary<int, int[]>()
        {

            { 1, new int[] { 5 } },
            /* (1)
			            5
	        */
            { 2, new int[] { 3, 5 } },
            /* (2)
			            3
			         /
			        5
	        */
            { 3, new int[] { 3, 5, 7 } },
            /* (3)
			            3
			         /     \ 
			        5       7
	        */
            { 4, new int[] { 2, 3, 7, 5 } },
            /* (4)
			            2
			         /     \ 
			        3       7
                   /
                  5
	        */
            { 5, new int[] { 2, 3, 7, 5, 4 } },
            /* (5)
			            2
			         /     \ 
			        3       7
                   /  \
                  5    4
	        */
            { 6, new int[] { 2, 3, 6, 5, 4, 7 } },
            /* (6)
			            2
			         /     \ 
			        3       6
                   /  \    /
                  5    4  7
	        */
            { 7, new int[] { 2, 3, 6, 5, 4, 7, 9 } },
            /* (7)
			            2
			         /     \ 
			        3       6
                   /  \    /  \
                  5    4  7    9
	        */
            { 8, new int[] { 1, 2, 6, 3, 4, 7, 9, 5 } },
            /* (8)
			            1
			         /     \ 
			        2       6
                   /  \    /  \
                  3    4  7    9
                 /
                5
	        */
            { 9, new int[] { 1, 2, 6, 3, 4, 7, 9, 5, 8 } },
            /* (9)
			            1
			         /     \ 
			        2       6
                   /  \    /  \
                  3    4  7    9
                 /  \
                5    8
	        */
            { 10, new int[] { 1, 2, 6, 3, 4, 7, 9, 5, 8, 10 } }
            /* (10)
			            1
			         /     \ 
			        2       6
                   /  \    /  \
                  3    4  7    9
                 /  \  |
                5    8 10
	        */
        };

        private readonly Dictionary<int, int[]> _answersForDeletion = new Dictionary<int, int[]>()
        {
            { 1, new int[] { 2, 3, 6, 5, 4, 7, 9, 10, 8 } },
            /* (1)
			            2
			         /     \ 
			        3       6
                   /  \    /  \
                  5    4  7    9
                 /  \
                10   8
	        */
            { 2, new int[] { 3, 4, 6, 5, 8, 7, 9, 10 } },
            /* (2)
			            3
			         /     \ 
			        4       6
                   /  \    /  \
                  5    8  7    9
                 /
                10
	        */
            { 3, new int[] { 4, 5, 6, 10, 8, 7, 9 } },
            /* (3)
			            4
			         /     \ 
			        5       6
                   /  \    /  \
                  10   8  7    9
	        */
            { 4, new int[] { 5, 8, 6, 10, 9, 7 } },
            /* (4)
			            5
			         /     \ 
			        8       6
                   /  \    /
                  10   9  7
	        */
            { 5, new int[] { 6, 8, 7, 10, 9 } },
            /* (5)
			            6
			         /     \ 
			        8       7
                   /  \
                  10   9
	        */
            { 6, new int[] { 7, 8, 9, 10 } },
            /* (6)
			            7
			         /     \ 
			        8       9
                   /
                  10
	        */
            { 7, new int[] { 8, 10, 9 } },
            /* (7)
			            8
			         /     \ 
			        10      9
	        */
            { 8, new int[] { 9, 10 } },
            /* (8)
			            9
			         /
			        10
	        */
            { 9, new int[] { 10 } },
            /* (9)
			            10
	        */
            { 10, new int[] { } }
            /* (10)
            
	        */
        };

        [SetUp]
        public void SetUp()
        {
            _heap = new MinHeapWithDynamicArray<int>(_capacity);
        }

        [Test]
        public void IsEmpty_WhenHeapIsEmpty_ShouldReturnTrue()
        {
            // Arrange & Act & Assert
            Assert.That(_heap.IsEmpty, Is.EqualTo(true));
        }

        [Test]
        public void IsEmpty_WhenHeapIsNotEmpty_ShouldReturnFalse()
        {
            // Arrange
            _heap.Insert(1);

            // Act
            var result = _heap.IsEmpty;

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsFull_WhenHeapIsFull_ShouldReturnTrue()
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _heap.Insert(i);
            }

            // Act
            var result = _heap.IsFull;

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsFull_WhenHeapIsNotFull_ShouldReturnFalse()
        {
            // Arrange & Act & Assert
            Assert.That(_heap.IsFull, Is.EqualTo(false));
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
                _heap.Insert(i);
            }

            // Act
            var result = _heap.Count;

            // Assert
            Assert.That(result, Is.EqualTo(range));
        }

        [Test]
        public void Insert_WhenCalled_ShouldInsertItemWithMinHeapPropertyMaintained()
        {
            // Arrange & Act & Assert
            for (var i = 0; i < _values.Length; i++)
            {
                _heap.Insert(_values[i]);
                var answer = _answersForInsertion[i + 1];
                Assert.That(_heap.PeekMin(), Is.EqualTo(answer.Min()));
                Assert.That(_heap.Count, Is.EqualTo(i + 1));
                for (var j = 0; j < answer.Length; j++)
                {
                    Assert.That(_heap.GetAt(j), Is.EqualTo(answer[j]));
                }
            }
        }

        [Test]
        public void PeekMin_WhenHeapIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _heap.PeekMin());
        }

        [Test]
        public void PeekMin_WhenHeapIsNotEmpty_ShouldReturnMinimumItem()
        {
            // Arrange & Act & Assert
            for (var i = 0; i < _values.Length; i++)
            {
                _heap.Insert(_values[i]);
                var answer = _answersForInsertion[i + 1];
                Assert.That(_heap.PeekMin(), Is.EqualTo(answer.Min()));
                Assert.That(_heap.Count, Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void PopMin_WhenHeapIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _heap.PopMin());
        }

        [Test]
        public void PopMin_WhenHeapIsNotEmpty_ShouldRemoveMinimumItemWithMinHeapPropertyMaintained()
        {
            // Arrange
            for (var i = 0; i < _values.Length; i++)
            {
                _heap.Insert(_values[i]);
            }

            // Act & Assert
            for (var i = 0; i < _values.Length; i++)
            {
                var answer = _answersForDeletion[i + 1];
                Assert.That(_heap.PopMin(), Is.EqualTo(i + 1));
                Assert.That(_heap.Count, Is.EqualTo(_values.Length - i - 1));
                for (var j = 0; j < answer.Length; j++)
                {
                    Assert.That(_heap.GetAt(j), Is.EqualTo(answer[j]));
                }
            }
        }
    }
}