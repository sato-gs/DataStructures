namespace DataStructures.Graphs.Main
{
    using System.Collections.Generic;
    using System.Linq;

    // Weighted graph (undirected) implemented using an adjacency list
    public class WeightedGraph<T>
    {
        // Represent vertices
        private readonly Dictionary<T, Vertex> _vertices;

        public WeightedGraph()
        {
            _vertices = new Dictionary<T, Vertex>();
        }

        // Add a vertex with a given value
        public bool AddVertex(T value)
        {
            if (_vertices.ContainsKey(value))
            {
                return false;
            }

            _vertices.Add(value, new Vertex(value));
            return true;
        }

        // Remove a vertex with a given value
        public bool RemoveVertex(T value)
        {
            if (!_vertices.ContainsKey(value))
            {
                return false;
            }

            var vertex = _vertices[value];
            // Remove all edges involving the vertex being removed
            foreach (var edge in vertex.Edges)
            {
                // Skip if a vertex has a self edge
                // To prevent the exception of changing the collection during enumeration
                if (edge.Vertex == vertex)
                {
                    continue;
                }
                var edgeToRemove = edge.Vertex.Edges.First(edge => edge.Vertex.EqualTo(value));
                edge.Vertex.Edges.Remove(edgeToRemove);
            }
            vertex.Edges.Clear();
            _vertices.Remove(vertex.Value);
            return true;
        }

        // Check whether a vertex exists with a given value (as a test helper function)
        public Vertex VertexWith(T value)
        {
            return _vertices.GetValueOrDefault(value);
        }

        // Add an edge from a given vertex to a given vertex with a given weight
        public bool AddEdge(T from, T to, int weight)
        {
            if (!_vertices.TryGetValue(from, out var fromVertex)
                || fromVertex.Edges.Exists(edge => edge.Vertex.EqualTo(to))
                || !_vertices.TryGetValue(to, out var toVertex)
                || toVertex.Edges.Exists(edge => edge.Vertex.EqualTo(from)))
            {
                return false;
            }

            _vertices[from].Edges.Add(new Edge(weight, toVertex));
            // Add the other edge only if it's not a self edge
            if (Comparer<T>.Default.Compare(from, to) != 0)
            {
                _vertices[to].Edges.Add(new Edge(weight, fromVertex));
            }
            return true;
        }

        // Remove an edge from a given vertex to a given vertex
        public bool RemoveEdge(T from, T to)
        {
            if (!_vertices.TryGetValue(from, out var fromVertex)
                || !fromVertex.Edges.Exists(edge => edge.Vertex.EqualTo(to))
                || !_vertices.TryGetValue(to, out var toVertex)
                || !toVertex.Edges.Exists(edge => edge.Vertex.EqualTo(from)))
            {
                return false;
            }

            var fromEdge = _vertices[from].Edges.First(edge => edge.Vertex.EqualTo(to));
            _vertices[from].Edges.Remove(fromEdge);
            // Remove the other edge only if it's not a self edge
            if (Comparer<T>.Default.Compare(from, to) != 0)
            {
                var toEdge = _vertices[to].Edges.First(edge => edge.Vertex.EqualTo(from));
                _vertices[to].Edges.Remove(toEdge);
            }
            return true;
        }

        // Check whether an edge exists from a given vertex to a given vertex (as a test helper function)
        public bool EdgeAt(T from, T to)
        {
            return _vertices.TryGetValue(from, out var fromVertex)
                && fromVertex.Edges.Exists(edge => edge.Vertex.EqualTo(to));
        }

        // Traverse via BFS non-recursively
        public List<T> BreadthFirstSearchTraverse(T start)
        {
            var result = new List<T>();
            if (!_vertices.ContainsKey(start))
            {
                return result;
            }

            // Create a container to keep track of visited vertices
            var visited = new Dictionary<T, bool>();
            // Create a queue
            var queue = new Queue<T>();
            // Mark the start vertex as visited and add it to the queue
            visited[start] = true;
            queue.Enqueue(start);

            // Continue until the queue is empty
            while (queue.Any())
            {
                // Remove a vertex at the beginning of the queue
                var cur = queue.Dequeue();
                // Mark unvisited adjacent vertices as visited and add them to the back of the queue
                foreach (var edge in _vertices[cur].Edges)
                {
                    var value = edge.Vertex.Value;
                    if (!visited.GetValueOrDefault(value))
                    {
                        visited[value] = true;
                        queue.Enqueue(value);
                    }
                }
            }

            result.AddRange(visited.Keys);
            return result;
        }

        // Traverse via DFS non-recursively
        public List<T> DepthFirstSearchTraverse(T start)
        {
            var result = new List<T>();
            if (!_vertices.ContainsKey(start))
            {
                return result;
            }

            // Create a container to keep track of visited vertices
            var visited = new Dictionary<T, bool>();
            // Create a stack
            var stack = new Stack<T>();
            // Add the start vertex to the stack
            stack.Push(start);

            // Continue until the stack is empty
            while (stack.Any())
            {
                // Remove a vertex at the top of the stack
                var cur = stack.Pop();
                // If the current vertex is visited
                // Skip it
                if (visited.GetValueOrDefault(cur))
                {
                    continue;
                }
                // If the current vertex is unvisited
                // Mark it as visited and add unvisited adjacent vertices to the top of the stack
                visited[cur] = true;
                foreach (var edge in _vertices[cur].Edges)
                {
                    var value = edge.Vertex.Value;
                    if (!visited.GetValueOrDefault(value))
                    {
                        stack.Push(value);
                    }
                }
            }

            result.AddRange(visited.Keys);
            return result;
        }

        // Traverse via DFS recursively
        public List<T> DepthFirstSearchTraverseRecursive(T start)
        {
            var result = new List<T>();
            if (!_vertices.ContainsKey(start))
            {
                return result;
            }

            // Create a container to keep track of visited vertices
            var visited = new Dictionary<T, bool>();
            // Create a recursive function
            void Traverse(Dictionary<T, bool> visited, T value)
            {
                // If the current vertex is visited
                // Skip it
                if (visited.GetValueOrDefault(value))
                {
                    return;
                }
                // If the current vertex is unvisited
                // Mark it as visited and recursively traverse unvisited adjacent vertices
                visited[value] = true;
                foreach (var edge in _vertices[value].Edges)
                {
                    var val = edge.Vertex.Value;
                    if (!visited.GetValueOrDefault(val))
                    {
                        Traverse(visited, val);
                    }
                }
            }

            // Call the recursive function with the start vertex
            Traverse(visited, start);

            result.AddRange(visited.Keys);
            return result;
        }

        public class Vertex
        {
            public T Value { get; set; }
            public List<Edge> Edges { get; set; }

            public Vertex(T value)
            {
                Value = value;
                Edges = new List<Edge>();
            }

            public bool EqualTo(T comparison)
            {
                return Comparer<T>
                    .Default
                    .Compare(Value, comparison) == 0;
            }
        }

        public class Edge
        {
            public int Weight { get; set; }
            public Vertex Vertex { get; set; }

            public Edge(int weight, Vertex vertex)
            {
                Weight = weight;
                Vertex = vertex;
            }
        }
    }
}