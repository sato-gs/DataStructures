namespace DataStructures.Graphs.Sub
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    // Undirected graph implemented using an adjacency list
    public class UndirectedListGraph
    {
        // Represent the number of vertices
        private readonly int _numberOfVertices;
        // Represent vertices (e.g. indices as vertices and lists as their neighbours)
        private readonly List<int>[] _vertices;

        public UndirectedListGraph(int numberOfVertices)
        {
            _numberOfVertices = numberOfVertices;
            _vertices = new List<int>[numberOfVertices];
            for (var i = 0; i < numberOfVertices; i++)
            {
                _vertices[i] = new List<int>();
            }
        }

        // Add an undirected edge from a given vertex to a given vertex
        public bool AddEdge(int from, int to)
        {
            if (from < 0
                || from >= _numberOfVertices
                || to < 0
                || to >= _numberOfVertices)
            {
                throw new InvalidOperationException();
            }

            var notExist = !_vertices[from].Contains(to);
            if (notExist)
            {
                _vertices[from].Add(to);
                _vertices[to].Add(from);
            }

            return notExist;
        }

        // Remove an undirected edge from a given vertex to a given vertex
        public bool RemoveEdge(int from, int to)
        {
            if (from < 0
                || from >= _numberOfVertices
                || to < 0
                || to >= _numberOfVertices)
            {
                throw new InvalidOperationException();
            }

            var exist = _vertices[from].Contains(to);
            if (exist)
            {
                _vertices[from].Remove(to);
                _vertices[to].Remove(from);
            }

            return exist;
        }

        // Check whether an undirected edge exists from a given vertex to a given vertex (as a test helper function)
        public bool EdgeAt(int from, int to)
        {
            return _vertices[from].Contains(to);
        }

        // Return a string representing a graph itself
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var vertex = 0; vertex < _numberOfVertices; vertex++)
            {
                sb.Append($"{vertex}");
                foreach (var neighbour in _vertices[vertex])
                {
                    sb.Append($" -> {neighbour}");
                }
                sb.AppendLine(" |");
            }
            return sb.ToString();
        }
    }
}