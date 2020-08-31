namespace DataStructures.Tests.Graphs
{
    using System;
    using DataStructures.Graphs;
    using NUnit.Framework;

    public class UndirectedWeightedListGraphTests
    {
        private UndirectedWeightedListGraph _graph;
        private const int _numberOfVertices = 5;

        [SetUp]
        public void SetUp()
        {
            _graph = new UndirectedWeightedListGraph(_numberOfVertices);
        }

        [Test]
        [TestCase(-1, -1)]
        [TestCase(-1, 100)]
        [TestCase(100, -1)]
        [TestCase(100, 100)]
        public void AddEdge_WhenVertexDoesNotExist_ShouldThrowInvalidOperationException(int from, int to)
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _graph.AddEdge(from, to, 1));
        }

        [Test]
        public void AddEdge_WhenEdgeExists_ShouldNotAddUndirectedWeightedEdgeAndReturnFalse()
        {
            // Arrange
            _graph.AddEdge(0, 1, 1);

            // Act
            var result = _graph.AddEdge(0, 1, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(0, 1), Is.EqualTo(true));
            Assert.That(_graph.EdgeAt(1, 0), Is.EqualTo(true));
        }

        [Test]
        public void AddEdge_WhenEdgeDoesNotExist_ShouldAddUndirectedWeightedEdgeAndReturnTrue()
        {
            // Arrange & Act
            var result = _graph.AddEdge(0, 1, 1);

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_graph.EdgeAt(0, 1), Is.EqualTo(true));
            Assert.That(_graph.EdgeAt(1, 0), Is.EqualTo(true));
        }

        [Test]
        [TestCase(-1, -1)]
        [TestCase(-1, 100)]
        [TestCase(100, -1)]
        [TestCase(100, 100)]
        public void RemoveEdge_WhenVertexDoesNotExist_ShouldThrowInvalidOperationException(int from, int to)
        {
            // Arrange & Act & Assert
            Assert.Throws<InvalidOperationException>(() => _graph.RemoveEdge(from, to));
        }

        [Test]
        public void RemoveEdge_WhenEdgeExists_ShouldRemoveUndirectedWeightedEdgeAndReturnTrue()
        {
            // Arrange
            _graph.AddEdge(0, 1, 1);

            // Act
            var result = _graph.RemoveEdge(0, 1);

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_graph.EdgeAt(0, 1), Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(1, 0), Is.EqualTo(false));
        }

        [Test]
        public void RemoveEdge_WhenEdgeDoesNotExist_ShouldNotRemoveUndirectedWeightedEdgeAndReturnFalse()
        {
            // Arrange & Act
            var result = _graph.RemoveEdge(0, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(0, 1), Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(1, 0), Is.EqualTo(false));
        }
    }
}