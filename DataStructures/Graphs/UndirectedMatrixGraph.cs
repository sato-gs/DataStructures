namespace DataStructures.Graphs
{
    using System;
    using System.Text;

    public class UndirectedMatrixGraph
    {
        private readonly int _numberOfVertices;
        private readonly int[,] _matrix;
        private const int EDGE_NOT_EXIST = 0;
        private const int EDGE_EXIST = 1;

        public UndirectedMatrixGraph(int numberOfVertices)
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

        public bool AddEdge(int from, int to)
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
                _matrix[from, to] = _matrix[to, from] = EDGE_EXIST;
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

            var exist = _matrix[from, to] == EDGE_EXIST;
            if (exist)
            {
                _matrix[from, to] = _matrix[to, from] = EDGE_NOT_EXIST;
            }

            return exist;
        }

        public bool EdgeAt(int from, int to)
        {
            return _matrix[from, to] == EDGE_EXIST;
        }

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