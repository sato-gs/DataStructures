namespace DataStructures.BinaryTrees.Main
{
    using System.Collections.Generic;
    using System.Linq;

    // Binary search tree with no duplicate allowed
    public class BinarySearchTree<T>
    {
        // Represent the root node
        public Node Root { get; private set; }

        // Find a node with a given value
        public Node Find(T value)
        {
            return FindRecursive(Root, value);
        }

        // Find a node with a given value recursively
        private Node FindRecursive(Node node, T value)
        {
            if (node == null)
            {
                return null;
            }

            var cmp = Comparer<T>
                .Default
                .Compare(value, node.Value);
            // If a value is less than the value of the current node
            // Continue with its left sub-tree
            if (cmp < 0)
            {
                return FindRecursive(node.Left, value);
            }
            // If a value is greater than the value of the current node
            // Continue with its right sub-tree
            else if (cmp > 0)
            {
                return FindRecursive(node.Right, value);
            }
            // If a value is equal to the value of the current node
            // Return it
            else
            {
                return node;
            }
        }

        // Find a node with a minimum value
        public Node FindMin()
        {
            return FindMinRecursive(Root);
        }

        // Find a node with a minimum value recursively
        private Node FindMinRecursive(Node node)
        {
            if (node?.Left == null)
            {
                return node;
            }
            else
            {
                return FindMinRecursive(node.Left);
            }
        }

        // Find a node with a maximum value
        public Node FindMax()
        {
            return FindMaxRecursive(Root);
        }

        // Find a node with a maximum value recursively
        private Node FindMaxRecursive(Node node)
        {
            if (node?.Right == null)
            {
                return node;
            }
            else
            {
                return FindMaxRecursive(node.Right);
            }
        }

        // Insert a node with a given value
        public void Insert(T value)
        {
            Root = InsertRecursive(Root, value);
        }

        // Insert a node with a given value recursively
        private Node InsertRecursive(Node node, T value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            var cmp = Comparer<T>
                .Default
                .Compare(value, node.Value);
            // If a value is less than the value of the current node
            // Continue with its left sub-tree
            if (cmp < 0)
            {
                node.Left = InsertRecursive(node.Left, value);
            }
            // If a value is greater than the value of the current node
            // Continue with its right sub-tree
            else if (cmp > 0)
            {
                node.Right = InsertRecursive(node.Right, value);
            }
            // If a value is equal to the value of the current node
            // Just ignore

            return node;
        }

        // Delete a node with a given value
        public void Delete(T value)
        {
            Root = DeleteRecursive(Root, value);
        }

        // Delete a node with a given value recursively
        private Node DeleteRecursive(Node node, T value)
        {
            if (node == null)
            {
                return null;
            }

            var cmp = Comparer<T>
                .Default
                .Compare(value, node.Value);
            // If a value is less than the value of the current node
            // Continue with its left sub-tree
            if (cmp < 0)
            {
                node.Left = DeleteRecursive(node.Left, value);
            }
            // If a value is greater than the value of the current node
            // Continue with its right sub-tree
            else if (cmp > 0)
            {
                node.Right = DeleteRecursive(node.Right, value);
            }
            // If a value is equal to the value of the current node
            // Delete a node
            else
            {
                // If the current node has no left child node
                // Delete the current node and return its right child node (as its replacement)
                if (node.Left == null)
                {
                    var rightNode = node.Right;

                    node.Value = default;
                    node = null;

                    return rightNode;
                }
                // If the current node has no right child node
                // Delete the current node and return its left child node (as its replacement)
                else if (node.Right == null)
                {
                    var leftNode = node.Left;

                    node.Value = default;
                    node = null;

                    return leftNode;
                }
                // If the current node has two child nodes
                // Replace it with a successor to maintain a binary search tree structure
                // The successor can be either (1) the largest value in its left sub-tree or (2) the smallest value in its right sub-tree
                else
                {
                    // Find the largest value in its left sub-tree as its successor
                    var successor = FindMaxRecursive(node.Left);

                    // Swap its value
                    node.Value = successor.Value;

                    // Delete the successor
                    node.Left = DeleteRecursive(node.Left, successor.Value);
                }
            }

            return node;
        }

        // Traverse via level order traversal (BFS) non-recursively
        public List<T> LevelOrderTraverse()
        {
            var result = new List<T>();
            if (Root == null)
            {
                return result;
            }

            // Create a queue
            var queue = new Queue<Node>();
            // Add a root node to the queue
            queue.Enqueue(Root);

            // Continue until the queue is empty
            while (queue.Any())
            {
                // Remove a node at the beginning of the queue
                var cur = queue.Dequeue();
                // Do something with the current node as required
                DoSomething(result, cur);

                // If there is a left child node
                // Add it to the back of the queue first
                if (cur.Left != null)
                {
                    queue.Enqueue(cur.Left);
                }

                // If there is a right child node
                // Add it to the back of the queue second
                if (cur.Right != null)
                {
                    queue.Enqueue(cur.Right);
                }
            }

            return result;
        }

        // Traverse via pre-order traversal (DFS) non-recursively
        public List<T> PreOrderTraverse()
        {
            var result = new List<T>();
            if (Root == null)
            {
                return result;
            }

            // Create a stack
            var stack = new Stack<Node>();
            // Add a root node to the stack
            stack.Push(Root);

            // Continue until the stack is empty
            while (stack.Any())
            {
                // Remove a node at the top of the stack
                var cur = stack.Pop();
                // Do something with the current node as required
                DoSomething(result, cur);

                // If there is a right child node
                // Add it to the top of the stack first
                if (cur.Right != null)
                {
                    stack.Push(cur.Right);
                }

                // If there is a left child node
                // Add it to the top of the stack second
                if (cur.Left != null)
                {
                    stack.Push(cur.Left);
                }
            }

            return result;
        }

        // Traverse via pre-order traversal (DFS) recursively
        public List<T> PreOrderTraverseRecursive()
        {
            var result = new List<T>();
            // Create a recursive function
            void Traverse(Node node)
            {
                if (node != null)
                {
                    // Do something with the current node as required first
                    DoSomething(result, node);
                    // Call itself with a left child node second
                    Traverse(node.Left);
                    // Call itself with a right child node third
                    Traverse(node.Right);
                }
            }

            // Call the recursive function with a root node
            Traverse(Root);
            return result;
        }

        // Traverse via post-order traversal (DFS) non-recursively
        public List<T> PostOrderTraverse()
        {
            var result = new List<T>();
            if (Root == null)
            {
                return result;
            }

            // Create a stack
            var stack = new Stack<Node>();
            // Add a root node to the stack
            stack.Push(Root);
            // Keep track of the last visited node
            Node last = null;

            // Continue until the stack is empty
            while (stack.Any())
            {
                // Get a node at the top of the stack
                var cur = stack.Peek();

                // Check if the current node has no child node
                var isLeaf = cur.Left == null && cur.Right == null;
                // Check if the subtree of the current node has been processed
                // (1) last != null is to ensure that the first node with one child node is not flagged as processed
                // Because the first node with one child node would return 'true' based on (cur.Left == last || cur.Right == last)
                // (2) (cur.Left == last || cur.Right == last) is to check that the subtree of the current node has been processed
                // Because the subtree has been processed if the current node is not a leaf and its left or right child node has been just visited
                var isSubTreeProcessed = last != null && (cur.Left == last || cur.Right == last);

                // If the current node is a leaf or its subtree has been processed
                if (isLeaf || isSubTreeProcessed)
                {
                    // Remove the current node
                    cur = stack.Pop();
                    // Do something with the current node as required
                    DoSomething(result, cur);
                    // Set the last visited node properly
                    last = cur;
                }
                // Otherwise
                else
                {
                    // If there is a right child node
                    // Add it to the top of the stack first
                    if (cur.Right != null)
                    {
                        stack.Push(cur.Right);
                    }

                    // If there is a left child node
                    // Add it to the top of the stack second
                    if (cur.Left != null)
                    {
                        stack.Push(cur.Left);
                    }
                }
            }

            return result;
        }

        // Traverse via post-order traversal (DFS) recursively
        public List<T> PostOrderTraverseRecursive()
        {
            var result = new List<T>();
            // Create a recursive function
            void Traverse(Node node)
            {
                if (node != null)
                {
                    // Call itself with a left child node first
                    Traverse(node.Left);
                    // Call itself with a right child node second
                    Traverse(node.Right);
                    // Do something with the current node as required third
                    DoSomething(result, node);
                }
            }

            // Call the recursive function with a root node
            Traverse(Root);
            return result;
        }

        // Traverse via in-order traversal (DFS) non-recursively
        public List<T> InOrderTraverse()
        {
            var result = new List<T>();
            if (Root == null)
            {
                return result;
            }

            // Create a stack
            var stack = new Stack<Node>();
            // Set a root node as current initially
            var cur = Root;

            // Continue until the stack is empty and the current is null
            while (stack.Any() || cur != null)
            {
                // If the current is not null
                if (cur != null)
                {
                    // Add it to the top of the stack (to defer it)
                    stack.Push(cur);
                    // Set the current to its left child node
                    cur = cur.Left;
                }
                // If the current is null
                else
                {
                    // Remove a node at the top of the stack (to process it)
                    cur = stack.Pop();
                    // Do something with the current node as required
                    DoSomething(result, cur);
                    // Set the current to its right child node
                    cur = cur.Right;
                }
            }

            return result;
        }

        // Traverse via in-order traversal (DFS) recursively
        public List<T> InOrderTraverseRecursive()
        {
            var result = new List<T>();
            // Create a recursive function
            void Traverse(Node node)
            {
                if (node != null)
                {
                    // Call itself with a left child node first
                    Traverse(node.Left);
                    // Do something with the current node as required second
                    DoSomething(result, node);
                    // Call itself with a right child node third
                    Traverse(node.Right);
                }
            }

            // Call the recursive function with a root node
            Traverse(Root);
            return result;
        }

        private void DoSomething(List<T> result, Node node)
        {
            result.Add(node.Value);
        }

        public class Node
        {
            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(T value)
            {
                Value = value;
            }
        }
    }
}