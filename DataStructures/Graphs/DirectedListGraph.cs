namespace DataStructures.Graphs
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DirectedListGraph
    {
        private readonly int _numberOfVertices;
        private readonly List<int>[] _vertices;

        public DirectedListGraph(int numberOfVertices)
        {
            _numberOfVertices = numberOfVertices;
            _vertices = new List<int>[numberOfVertices];
            for (var i = 0; i < numberOfVertices; i++)
            {
                _vertices[i] = new List<int>();
            }
        }

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
            }

            return notExist;
        }

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
            }

            return exist;
        }

        public bool EdgeAt(int from, int to)
        {
            return _vertices[from].Contains(to);
        }

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