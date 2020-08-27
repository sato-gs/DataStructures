namespace DataStructures.Tests.Heaps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataStructures.Heaps;
    using NUnit.Framework;

    public class PriorityQueueWithMaxHeapTests
    {
        private PriorityQueueWithMaxHeap<string> _queue;
        private readonly int _capacity = 10;
        private readonly int[] _values = new int[] { 5, 3, 7, 2, 4, 6, 9, 1, 8, 10 };

        private readonly Dictionary<int, int[]> _answersForInsertion = new Dictionary<int, int[]>()
        {

            { 1, new int[] { 5 } },
            /* (1)
			            5
	        */
            { 2, new int[] { 5, 3 } },
            /* (2)
			            5
			         /
			        3
	        */
            { 3, new int[] { 7, 3, 5 } },
            /* (3)
			            7
			         /     \ 
			        3       5
	        */
            { 4, new int[] { 7, 3, 5, 2 } },
            /* (4)
			            7
			         /     \ 
			        3       5
                   /
                  2
	        */
            { 5, new int[] { 7, 4, 5, 2, 3 } },
            /* (5)
			            7
			         /     \ 
			        4       5
                   /  \
                  2    3
	        */
            { 6, new int[] { 7, 4, 6, 2, 3, 5 } },
            /* (6)
			            7
			         /     \ 
			        4       6
                   /  \    /
                  2    3  5
	        */
            { 7, new int[] { 9, 4, 7, 2, 3, 5, 6 } },
            /* (7)
			            9
			         /     \ 
			        4       7
                   /  \    /  \
                  2    3  5    6
	        */
            { 8, new int[] { 9, 4, 7, 2, 3, 5, 6, 1 } },
            /* (8)
			            9
			         /     \ 
			        4       7
                   /  \    /  \
                  2    3  5    6
                 /
                1
	        */
            { 9, new int[] { 9, 8, 7, 4, 3, 5, 6, 1, 2 } },
            /* (9)
			            9
			         /     \ 
			        8       7
                   /  \    /  \
                  4    3  5    6
                 /  \
                1    2
	        */
            { 10, new int[] { 10, 9, 7, 4, 8, 5, 6, 1, 2, 3 } }
            /* (10)
			            10
			         /     \ 
			        9       7
                   /  \    /  \
                  4    8  5    6
                 /  \  |
                1    2 3
	        */
        };

        private readonly Dictionary<int, int[]> _answersForDeletion = new Dictionary<int, int[]>()
        {
            { 1, new int[] { 9, 8, 7, 4, 3, 5, 6, 1, 2 } },
            /* (1)
			            9
			         /     \ 
			        8       7
                   /  \    /  \
                  4    3  5    6
                 /  \
                1    2
	        */
            { 2, new int[] { 8, 4, 7, 2, 3, 5, 6, 1 } },
            /* (2)
			            8
			         /     \ 
			        4       7
                   /  \    /  \
                  2    3  5    6
                 /
                1
	        */
            { 3, new int[] { 7, 4, 6, 2, 3, 5, 1 } },
            /* (3)
			            7
			         /     \ 
			        4       6
                   /  \    /  \
                  2    3  5    1
	        */
            { 4, new int[] { 6, 4, 5, 2, 3, 1 } },
            /* (4)
			            6
			         /     \ 
			        4       5
                   /  \    /
                  2    3  1
	        */
            { 5, new int[] { 5, 4, 1, 2, 3 } },
            /* (5)
			            5
			         /     \ 
			        4       1
                   /  \
                  2    3
	        */
            { 6, new int[] { 4, 3, 1, 2 } },
            /* (6)
			            4
			         /     \ 
			        3       1
                   /
                  2
	        */
            { 7, new int[] { 3, 2, 1 } },
            /* (7)
			            3
			         /     \ 
			        2       1
	        */
            { 8, new int[] { 2, 1 } },
            /* (8)
			            2
			         /
			        1
	        */
            { 9, new int[] { 1 } },
            /* (9)
			            1
	        */
            { 10, new int[] { } }
            /* (10)

	        */
        };

        [SetUp]
        public void SetUp()
        {
            _queue = new PriorityQueueWithMaxHeap<string>(_capacity);
        }

        [Test]
        public void IsEmpty_WhenPriorityQueueIsEmpty_ShouldReturnTrue()
        {
            // Arrange & Act & Assert
            Assert.That(_queue.IsEmpty, Is.EqualTo(true));
        }

        [Test]
        public void IsEmpty_WhenPriorityQueueIsNotEmpty_ShouldReturnFalse()
        {
            // Arrange
            _queue.Enqueue(1, "Item 1");

            // Act
            var result = _queue.IsEmpty;

            // Assert
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        public void IsFull_WhenPriorityQueueIsFull_ShouldReturnTrue()
        {
            // Arrange
            for (var i = 1; i <= _capacity; i++)
            {
                _queue.Enqueue(i, $"Item {i}");
            }

            // Act
            var result = _queue.IsFull;

            // Assert
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        public void IsFull_WhenPriorityQueueIsNotFull_ShouldReturnFalse()
        {
            // Arrange & Act & Assert
            Assert.That(_queue.IsFull, Is.EqualTo(false));
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
                _queue.Enqueue(i, $"Item {i}");
            }

            // Act
            var result = _queue.Count;

            // Assert
            Assert.That(result, Is.EqualTo(range));
        }

        [Test]
        public void Enqueue_WhenCalled_ShouldAddItemWithPriorityPropertyMaintained()
        {
            // Arrange & Act & Assert
            for (var i = 0; i < _values.Length; i++)
            {
                _queue.Enqueue(_values[i], $"Item {_values[i]}");
                var answer = _answersForInsertion[i + 1];
                Assert.That(
                    _queue.PeekPriority(),
                    Is.Not.Null
                    .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Priority)).EqualTo(answer.Max())
                    .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Value)).EqualTo($"Item {answer.Max()}"));
                Assert.That(_queue.Count, Is.EqualTo(i + 1));
                for (var j = 0; j < answer.Length; j++)
                {
                    Assert.That(
                        _queue.GetAt(j),
                        Is.Not.Null
                        .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Priority)).EqualTo(answer[j])
                        .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Value)).EqualTo($"Item {answer[j]}"));
                }
            }
        }

        [Test]
        public void PeekPriority_WhenPriorityQueueIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _queue.PeekPriority());
        }

        [Test]
        public void PeekPriority_WhenPriorityQueueIsNotEmpty_ShouldReturnHighestPriorityItem()
        {
            // Arrange & Act & Assert
            for (var i = 0; i < _values.Length; i++)
            {
                _queue.Enqueue(_values[i], $"Item {_values[i]}");
                var answer = _answersForInsertion[i + 1];
                Assert.That(
                    _queue.PeekPriority(),
                    Is.Not.Null
                    .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Priority)).EqualTo(answer.Max())
                    .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Value)).EqualTo($"Item {answer.Max()}"));
                Assert.That(_queue.Count, Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void Dequeue_WhenPriorityQueueIsEmpty_ShouldThrowInvalidOperationException()
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _queue.Dequeue());
        }

        [Test]
        public void Dequeue_WhenPriorityQueueIsNotEmpty_ShouldRemoveHighestPriorityItemWithPriorityPropertyMaintained()
        {
            // Arrange
            for (var i = 0; i < _values.Length; i++)
            {
                _queue.Enqueue(_values[i], $"Item {_values[i]}");
            }

            // Act & Assert
            for (var i = 0; i < _values.Length; i++)
            {
                var answer = _answersForDeletion[i + 1];
                Assert.That(
                    _queue.Dequeue(),
                    Is.Not.Null
                    .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Priority)).EqualTo(_values.Length - i)
                    .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Value)).EqualTo($"Item {_values.Length - i}"));
                Assert.That(_queue.Count, Is.EqualTo(_values.Length - i - 1));
                for (var j = 0; j < answer.Length; j++)
                {
                    Assert.That(
                        _queue.GetAt(j),
                        Is.Not.Null
                        .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Priority)).EqualTo(answer[j])
                        .And.Property(nameof(PriorityQueueWithMaxHeap<string>.Node<string>.Value)).EqualTo($"Item {answer[j]}"));
                }
            }
        }
    }
}