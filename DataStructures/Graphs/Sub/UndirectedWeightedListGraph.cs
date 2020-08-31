namespace DataStructures.Graphs.Sub
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // Undirected weighted graph implemented using an adjacency list
    public class UndirectedWeightedListGraph
    {
        // Represent the number of vertices
        private readonly int _numberOfVertices;
        // Represent vertices (e.g. indices as vertices and lists as their neighbours)
        private readonly List<Edge>[] _vertices;

        public UndirectedWeightedListGraph(int numberOfVertices)
        {
            _numberOfVertices = numberOfVertices;
            _vertices = new List<Edge>[numberOfVertices];
            for (var i = 0; i < numberOfVertices; i++)
            {
                _vertices[i] = new List<Edge>();
            }
        }

        // Add an undirected weighted edge from a given vertex to a given vertex with a given weight
        public bool AddEdge(int from, int to, int weight)
        {
            if (from < 0
                || from >= _numberOfVertices
                || to < 0
                || to >= _numberOfVertices)
            {
                throw new InvalidOperationException();
            }

            var edge = _vertices[from]
                .FirstOrDefault(edge => edge.Vertex == to);
            if (edge == null)
            {
                _vertices[from].Add(new Edge(to, weight));
                _vertices[to].Add(new Edge(from, weight));
            }

            return edge == null;
        }

        // Remove an undirected weighted edge from a given vertex to a given vertex
        public bool RemoveEdge(int from, int to)
        {
            if (from < 0
                || from >= _numberOfVertices
                || to < 0
                || to >= _numberOfVertices)
            {
                throw new InvalidOperationException();
            }

            var fromEdge = _vertices[from]
                .FirstOrDefault(edge => edge.Vertex == to);
            if (fromEdge != null)
            {
                var toEdge = _vertices[to]
                    .First(edge => edge.Vertex == from);
                _vertices[from].Remove(fromEdge);
                _vertices[to].Remove(toEdge);
            }

            return fromEdge != null;
        }

        // Check whether an undirected weighted edge exists from a given vertex to a given vertex (as a test helper function)
        public bool EdgeAt(int from, int to)
        {
            return _vertices[from].Exists(edge => edge.Vertex == to);
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
                    sb.Append($" --({neighbour.Weight})--> {neighbour.Vertex}");
                }
                sb.AppendLine(" |");
            }
            return sb.ToString();
        }

        public class Edge
        {
            public int Vertex { get; set; }
            public int Weight { get; set; }

            public Edge(int vertex, int weight)
            {
                Vertex = vertex;
                Weight = weight;
            }
        }
    }
}