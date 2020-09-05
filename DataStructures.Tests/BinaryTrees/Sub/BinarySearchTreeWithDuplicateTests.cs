namespace DataStructures.Tests.BinaryTrees.Sub
{
    using System.Collections.Generic;
    using System.Linq;
    using DataStructures.BinaryTrees.Sub;
    using NUnit.Framework;
    
    public class BinarySearchTreeWithDuplicateTests
    {
        private BinarySearchTreeWithDuplicate<int> _tree;
        private readonly List<int> _values = new List<int>() { 5, 3, 7, 2, 4, 6, 9, 1, 8, 10 };
        /*
                        5
                     /     \ 
                    3       7
                   /  \    /  \ 
                  2    4  6    9
                 /            / \
                1            8   10
        */

        [SetUp]
        public void SetUp()
        {
            _tree = new BinarySearchTreeWithDuplicate<int>();
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Find_WhenNodeDoesNotExist_ShouldReturnNull(int value)
        {
            // Arrange & Act
            var result = _tree.Find(value);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Find_WhenNodeExists_ShouldReturnNode(int value)
        {
            // Arrange
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.Find(value);

            // Assert
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Value))
                .EqualTo(value));
        }

        [Test]
        public void FindMin_WhenNodesDoNotExist_ShouldReturnNull()
        {
            // Arrange & Act
            var result = _tree.FindMin();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void FindMin_WhenNodesExist_ShouldReturnMinNode()
        {
            // Arrange
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.FindMin();

            // Assert
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Value))
                .EqualTo(1));
        }

        [Test]
        public void FindMax_WhenNodesDoNotExist_ShouldReturnNull()
        {
            // Arrange & Act
            var result = _tree.FindMax();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void FindMax_WhenNodesExist_ShouldReturnMaxNode()
        {
            // Arrange
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.FindMax();

            // Assert
            Assert.That(
                result,
                Is.Not.Null
                .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Value))
                .EqualTo(10));
        }

        [Test]
        public void Insert_WhenNodeDoesNotExist_ShouldInsertNode()
        {
            // Arrange & Act
            _values.ForEach(val => _tree.Insert(val));

            // Assert
            _values.ForEach(val =>
            {
                var result = _tree.Find(val);
                Assert.That(
                    result,
                    Is.Not.Null
                    .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Value))
                    .EqualTo(val)
                    .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Counter))
                    .EqualTo(1));
            });
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Insert_WhenNodeExists_ShouldIncrementNode(int value)
        {
            // Arrange
            _values.ForEach(val => _tree.Insert(val));

            // Act
            _tree.Insert(value);

            // Assert
            _values.ForEach(val =>
            {
                var result = _tree.Find(val);
                Assert.That(
                    result,
                    Is.Not.Null
                    .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Value))
                    .EqualTo(val)
                    .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Counter))
                    .EqualTo(val != value ? 1 : 2));
            });
        }

        [Test]
        [TestCase(0)]
        [TestCase(50)]
        [TestCase(100)]
        public void Delete_WhenNodeDoesNotExist_ShouldIgnore(int value)
        {
            // Arrange
            _values.ForEach(val => _tree.Insert(val));

            // Act
            _tree.Delete(value);

            // Assert
            _values.ForEach(val =>
            {
                var result = _tree.Find(val);
                Assert.That(
                    result,
                    Is.Not.Null
                    .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Value))
                    .EqualTo(val)
                    .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Counter))
                    .EqualTo(1));
            });
        }

        [Test]
        [TestCase(1, new[] { 5, 3, 7, 2, 4, 6, 9, 8, 10 })]
        [TestCase(5, new[] { 4, 3, 7, 2, 6, 9, 1, 8, 10 })]
        [TestCase(10, new[] { 5, 3, 7, 2, 4, 6, 9, 1, 8 })]
        public void Delete_WhenNodeExists_ShouldDeleteNode(int value, int[] expected)
        {
            // Arrange
            _values.ForEach(val => _tree.Insert(val));

            // Act
            _tree.Delete(value);

            // Assert
            _values.ForEach(val =>
            {
                var result = _tree.Find(val);
                if (val == value)
                {
                    Assert.That(
                        result,
                        Is.Null);
                }
                else
                {
                    Assert.That(
                        result,
                        Is.Not.Null
                        .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Value))
                        .EqualTo(val)
                        .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Counter))
                        .EqualTo(1));
                }
            });
            foreach (var (val, index) in _tree.LevelOrderTraverse().Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Delete_WhenDuplicateNodeExists_ShouldDecrementNode(int value)
        {
            // Arrange
            _values.ForEach(val => _tree.Insert(val));
            _tree.Insert(value);

            // Act
            _tree.Delete(value);

            // Assert
            _values.ForEach(val =>
            {
                var result = _tree.Find(val);
                Assert.That(
                    result,
                    Is.Not.Null
                    .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Value))
                    .EqualTo(val)
                    .And.Property(nameof(BinarySearchTreeWithDuplicate<int>.Node.Counter))
                    .EqualTo(1));
            });
        }

        [Test]
        public void LevelOrderTraverse_WhenCalled_ShouldTraverseViaLevelOrderTraversalNonRecursively()
        {
            // Arrange
            var expected = new int[] { 5, 3, 7, 2, 4, 6, 9, 1, 8, 10 };
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.LevelOrderTraverse();

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        [Test]
        public void PreOrderTraverse_WhenCalled_ShouldTraverseViaPreOrderTraversalNonRecursively()
        {
            // Arrange
            var expected = new int[] { 5, 3, 2, 1, 4, 7, 6, 9, 8, 10 };
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.PreOrderTraverse();

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        [Test]
        public void PreOrderTraverseRecursive_WhenCalled_ShouldTraverseViaPreOrderTraversalRecursively()
        {
            // Arrange
            var expected = new int[] { 5, 3, 2, 1, 4, 7, 6, 9, 8, 10 };
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.PreOrderTraverseRecursive();

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        [Test]
        public void PostOrderTraverse_WhenCalled_ShouldTraverseViaPostOrderTraversalNonRecursively()
        {
            // Arrange
            var expected = new int[] { 1, 2, 4, 3, 6, 8, 10, 9, 7, 5 };
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.PostOrderTraverse();

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        [Test]
        public void PostOrderTraverseRecursive_WhenCalled_ShouldTraverseViaPostOrderTraversalRecursively()
        {
            // Arrange
            var expected = new int[] { 1, 2, 4, 3, 6, 8, 10, 9, 7, 5 };
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.PostOrderTraverseRecursive();

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        [Test]
        public void InOrderTraverse_WhenCalled_ShouldTraverseViaInOrderTraversalNonRecursively()
        {
            // Arrange
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.InOrderTraverse();

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        [Test]
        public void InOrderTraverseRecursive_WhenCalled_ShouldTraverseViaInOrderTraversalRecursively()
        {
            // Arrange
            var expected = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            _values.ForEach(val => _tree.Insert(val));

            // Act
            var result = _tree.InOrderTraverseRecursive();

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }
    }
}