namespace DataStructures.Tests.Graphs.Main
{
    using DataStructures.Graphs.Main;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    public class WeightedGraphTests
    {
        private WeightedGraph<char> _graph;
        private readonly List<char> _vertices = new List<char>() { 'A', 'B', 'C', 'D', 'E' };
        private readonly List<(char, char, int)> _edges = new List<(char, char, int)>
        {
            ('A', 'B', 7),
            ('A', 'C', 1),
            ('A', 'D', 5),
            ('B', 'D', 4),
            ('B', 'E', 6),
            ('C', 'D', 8),
            ('D', 'D', 3),
            ('D', 'E', 2)
        };
        /*
                     7
                A ------- B
                | \       |\
                |  \      | \ 6  
                |   \     |  \
              1 |  5 \  4 |   \ E
                |     \   |   /
                |      \  |  /
                |       \ | / 2
                |        \|/
                C ------- D
                     8   / \
                         \ /
                          3
        */

        [SetUp]
        public void SetUp()
        {
            _graph = new WeightedGraph<char>();
        }

        [Test]
        public void AddVertex_WhenVertexExists_ShouldNotAddVertexAndReturnFalse()
        {
            // Arrange
            var value = 'A';
            _graph.AddVertex(value);

            // Act
            var result = _graph.AddVertex(value);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(
                _graph.VertexWith(value),
                Is.Not.Null
                .And.Property(nameof(WeightedGraph<char>.Vertex.Value)).EqualTo(value));
        }

        [Test]
        public void AddVertex_WhenVertexDoesNotExist_ShouldAddVertexAndReturnTrue()
        {
            // Arrange
            var value = 'A';

            // Act
            var result = _graph.AddVertex(value);

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(
                _graph.VertexWith(value),
                Is.Not.Null
                .And.Property(nameof(WeightedGraph<char>.Vertex.Value)).EqualTo(value));
        }

        [Test]
        public void RemoveVertex_WhenVertexDoesNotExist_ShouldNotRemoveVertexAndAssociatedEdgesAndReturnFalse()
        {
            // Arrange
            var value = 'A';

            // Act
            var result = _graph.RemoveVertex(value);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.VertexWith(value), Is.Null);
        }

        [Test]
        public void RemoveVertex_WhenVertexExists_ShouldRemoveVertexAndAssociatedEdgesAndReturnTrue()
        {
            // Arrange
            var value = 'A';
            _graph.AddVertex(value);
            for (var c = 'B'; c <= 'Z'; c++)
            {
                _graph.AddVertex(c);
                _graph.AddEdge(c, value, (c - value));
            }

            // Act
            var result = _graph.RemoveVertex(value);

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_graph.VertexWith(value), Is.Null);
            for (var c = 'B'; c <= 'Z'; c++)
            {
                Assert.That(_graph.EdgeAt(c, value), Is.EqualTo(false));
            }
        }

        [Test]
        public void AddEdge_WhenVerticesDoNotExist_ShouldNotAddEdgeAndReturnFalse()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';

            // Act
            var result = _graph.AddEdge(from, to, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(false));
        }

        [Test]
        public void AddEdge_WhenFromVertexDoesNotExist_ShouldNotAddEdgeAndReturnFalse()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';
            _graph.AddVertex(to);

            // Act
            var result = _graph.AddEdge(from, to, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(false));
        }

        [Test]
        public void AddEdge_WhenToVertexDoesNotExist_ShouldNotAddEdgeAndReturnFalse()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';
            _graph.AddVertex(from);

            // Act
            var result = _graph.AddEdge(from, to, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(false));
        }

        [Test]
        public void AddEdge_WhenVerticesExistAndEdgeExists_ShouldNotAddEdgeAndReturnFalse()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';
            _graph.AddVertex(from);
            _graph.AddVertex(to);
            _graph.AddEdge(from, to, 1);

            // Act
            var result = _graph.AddEdge(from, to, 1);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(true));
        }

        [Test]
        public void AddEdge_WhenVerticesExistAndEdgeDoesNotExist_ShouldAddEdgeAndReturnTrue()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';
            _graph.AddVertex(from);
            _graph.AddVertex(to);

            // Act
            var result = _graph.AddEdge(from, to, 1);

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(true));
        }

        [Test]
        public void RemoveEdge_WhenVerticesDoNotExist_ShouldNotRemoveEdgeAndReturnFalse()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';

            // Act
            var result = _graph.RemoveEdge(from, to);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(to, from), Is.EqualTo(false));
        }

        [Test]
        public void RemoveEdge_WhenFromVertexDoesNotExist_ShouldNotRemoveEdgeAndReturnFalse()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';
            _graph.AddVertex(to);

            // Act
            var result = _graph.RemoveEdge(from, to);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(to, from), Is.EqualTo(false));
        }

        [Test]
        public void RemoveEdge_WhenToVertexDoesNotExist_ShouldNotRemoveEdgeAndReturnFalse()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';
            _graph.AddVertex(from);

            // Act
            var result = _graph.RemoveEdge(from, to);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(to, from), Is.EqualTo(false));
        }

        [Test]
        public void RemoveEdge_WhenVerticesExistAndEdgeDoesNotExist_ShouldNotRemoveEdgeAndReturnFalse()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';
            _graph.AddVertex(from);
            _graph.AddVertex(to);

            // Act
            var result = _graph.RemoveEdge(from, to);

            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(to, from), Is.EqualTo(false));
        }

        [Test]
        public void RemoveEdge_WhenVerticesExistAndEdgeExists_ShouldRemoveEdgeAndReturnTrue()
        {
            // Arrange
            var from = 'A';
            var to = 'Z';
            _graph.AddVertex(from);
            _graph.AddVertex(to);
            _graph.AddEdge(from, to, 1);

            // Act
            var result = _graph.RemoveEdge(from, to);

            // Assert
            Assert.That(result, Is.EqualTo(true));
            Assert.That(_graph.EdgeAt(from, to), Is.EqualTo(false));
            Assert.That(_graph.EdgeAt(to, from), Is.EqualTo(false));
        }

        [Test]
        public void BreadthFirstSearchTraverse_WhenCalled_ShouldTraverseViaBreadthFirstSearchNonRecursively()
        {
            // Arrange
            var start = 'A';
            var expected = new List<char>() { 'A', 'B', 'C', 'D', 'E' };
            SetUpVerticesAndEdgesForTraversal();

            // Act
            var result = _graph.BreadthFirstSearchTraverse(start);

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        [Test]
        public void DepthFirstSearchTraverse_WhenCalled_ShouldTraverseViaDepthFirstSearchNonRecursively()
        {
            // Arrange
            var start = 'A';
            var expected = new List<char>() { 'A', 'D', 'E', 'B', 'C' };
            SetUpVerticesAndEdgesForTraversal();

            // Act
            var result = _graph.DepthFirstSearchTraverse(start);

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        [Test]
        public void DepthFirstSearchTraverseRecursive_WhenCalled_ShouldTraverseViaDepthFirstSearchRecursively()
        {
            // Arrange
            var start = 'A';
            var expected = new List<char>() { 'A', 'B', 'D', 'C', 'E' };
            SetUpVerticesAndEdgesForTraversal();

            // Act
            var result = _graph.DepthFirstSearchTraverseRecursive(start);

            // Assert
            foreach (var (val, index) in result.Select((val, index) => (val, index)))
            {
                Assert.That(expected[index], Is.EqualTo(val));
            }
        }

        private void SetUpVerticesAndEdgesForTraversal()
        {
            _vertices.ForEach(vertex =>
            {
                _graph.AddVertex(vertex);
            });
            _edges.ForEach(edge =>
            {
                var (from, to, weight) = edge;
                _graph.AddEdge(from, to, weight);
            });
        }
    }
}