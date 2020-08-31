namespace DataStructures.Graphs
{
    using System;
    using System.Text;

    // Directed weighted graph implemented using an adjacency matrix
    public class DirectedWeightedMatrixGraph
    {
        // Represent the number of vertices
        private readonly int _numberOfVertices;
        // Represent the matrix
        private readonly int[,] _matrix;
        // Represent the number indicating that an edge does not exist (e.g. 0)
        private const int EDGE_NOT_EXIST = 0;

        public DirectedWeightedMatrixGraph(int numberOfVertices)
        {
            _numberOfVertices = numberOfVertices;
            _matrix = new int[numberOfVertices, numberOfVertices];
            for (var i = 0; i < numberOfVertices; i++)
            {
                for (var j = 0; j < numberOfVertices; j++)
                {
                    _matrix[i, j] = EDGE_NOT_EXIST;
                }
            }
        }

        // Add a directed weighted edge from a given vertex to a given vertex with a given weight
        public bool AddEdge(int from, int to, int weight)
        {
            if (from < 0
                || from >= _numberOfVertices
                || to < 0
                || to >= _numberOfVertices)
            {
                throw new InvalidOperationException();
            }

            var notExist = _matrix[from, to] == EDGE_NOT_EXIST;
            if (notExist)
            {
                _matrix[from, to] = weight;
            }

            return notExist;
        }

        // Remove a directed weighted edge from a given vertex to a given vertex
        public bool RemoveEdge(int from, int to)
        {
            if (from < 0
                || from >= _numberOfVertices
                || to < 0
                || to >= _numberOfVertices)
            {
                throw new InvalidOperationException();
            }

            var exist = _matrix[from, to] != EDGE_NOT_EXIST;
            if (exist)
            {
                _matrix[from, to] = EDGE_NOT_EXIST;
            }

            return exist;
        }

        // Check whether a directed weighted edge exists from a given vertex to a given vertex (as a test helper function)
        public bool EdgeAt(int from, int to)
        {
            return _matrix[from, to] != EDGE_NOT_EXIST;
        }

        // Return a string representing a graph itself
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _numberOfVertices; i++)
            {
                sb.Append("| ");
                for (var j = 0; j < _numberOfVertices; j++)
                {
                    sb.Append($"{_matrix[i, j]} | ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}