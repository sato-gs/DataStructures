namespace DataStructures.BinaryTrees
{
    using System.Collections.Generic;
    using System.Diagnostics;

    // Binary search tree with duplicate allowed
    public class BinarySearchTreeWithDuplicate<T>
    {
        // Represent the root node
        public Node<T> Root { get; private set; }

        // Find a node with a given value
        public Node<T> Find(T value)
        {
            return Find(Root, value);
        }

        // Find a node with a given value recursively
        private Node<T> Find(Node<T> node, T value)
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
                return Find(node.Left, value);
            }
            // If a value is greater than the value of the current node
            // Continue with its right sub-tree
            else if (cmp > 0)
            {
                return Find(node.Right, value);
            }
            // If a value is equal to the value of the current node
            // Return it
            else
            {
                return node;
            }
        }

        // Find a node with a minimum value
        public Node<T> FindMin()
        {
            return FindMin(Root);
        }

        // Find a node with a minimum value recursively
        private Node<T> FindMin(Node<T> node)
        {
            if (node?.Left == null)
            {
                return node;
            }
            else
            {
                return FindMin(node.Left);
            }
        }

        // Find a node with a maximum value
        public Node<T> FindMax()
        {
            return FindMax(Root);
        }

        // Find a node with a maximum value recursively
        private Node<T> FindMax(Node<T> node)
        {
            if (node?.Right == null)
            {
                return node;
            }
            else
            {
                return FindMax(node.Right);
            }
        }

        // Insert a node with a given value
        public void Insert(T value)
        {
            Root = Insert(Root, value);
        }

        // Insert a node with a given value recursively
        private Node<T> Insert(Node<T> node, T value)
        {
            if (node == null)
            {
                return new Node<T>(value);
            }

            var cmp = Comparer<T>
                .Default
                .Compare(value, node.Value);
            // If a value is less than the value of the current node
            // Continue with its left sub-tree
            if (cmp < 0)
            {
                node.Left = Insert(node.Left, value);
            }
            // If a value is greater than the value of the current node
            // Continue with its right sub-tree
            else if (cmp > 0)
            {
                node.Right = Insert(node.Right, value);
            }
            // If a value is equal to the value of the current node
            // Increment its counter
            else
            {
                node.Counter++;
            }

            return node;
        }

        // Delete a node with a given value
        public void Delete(T value)
        {
            Root = Delete(Root, value);
        }

        // Delete a node with a given value recursively
        private Node<T> Delete(Node<T> node, T value)
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
                node.Left = Delete(node.Left, value);
            }
            // If a value is greater than the value of the current node
            // Continue with its right sub-tree
            else if (cmp > 0)
            {
                node.Right = Delete(node.Right, value);
            }
            // If a value is equal to the value of the current node
            // Decrement its counter
            else
            {
                node.Counter--;

                // If its counter is less than 0
                // Delete a node
                if (node.Counter <= 0)
                {
                    // If the current node has no left node
                    // Delete the current node and return its right node (as its replacement)
                    if (node.Left == null)
                    {
                        var rightNode = node.Right;

                        node.Value = default;
                        node = null;

                        return rightNode;
                    }
                    // If the current node has no right node
                    // Delete the current node and return its left node (as its replacement)
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
                        var successor = FindMax(node.Left);

                        // Swap its value and counter
                        node.Value = successor.Value;
                        node.Counter = successor.Counter;

                        // Delete the successor
                        node.Left = Delete(node.Left, successor.Value);
                    }
                }
            }

            return node;
        }

        // Traverse via level order traversal (BFS)
        public List<T> LevelOrderTraverse()
        {
            var list = new List<T>();
            if (Root == null)
            {
                return list;
            }

            // Define a queue
            var queue = new Queue<Node<T>>();
            // Add a root node to the queue
            queue.Enqueue(Root);

            // Continue until the queue is empty
            while (queue.Count != 0)
            {
                // Get a node at the beginning of the queue (e.g. Dequeue)
                var node = queue.Dequeue();
                // Do something with a current node as required
                DoSomething(node, list);

                // If there is a left node
                if (node.Left != null)
                {
                    // Add it to the back of the queue (e.g. Enqueue) first
                    queue.Enqueue(node.Left);
                }

                // If there is a right node
                if (node.Right != null)
                {
                    // Add it to the back of the queue (e.g. Enqueue) second
                    queue.Enqueue(node.Right);
                }
            }

            return list;
        }

        // Traverse via pre-order traversal (DFS)
        public List<T> PreOrderTraverse()
        {
            var list = new List<T>();
            // Define a recursive function
            void Traverse(Node<T> node)
            {
                if (node != null)
                {
                    // Do something with a current node as required first
                    DoSomething(node, list);
                    // Call itself with a left node of the current node second
                    Traverse(node.Left);
                    // Call itself with a right node of the current node third
                    Traverse(node.Right);
                }
            }

            // Call the recursive function with a root node
            Traverse(Root);
            return list;
        }

        // Traverse via post-order traversal (DFS)
        public List<T> PostOrderTraverse()
        {
            var list = new List<T>();
            // Define a recursive function
            void Traverse(Node<T> node)
            {
                if (node != null)
                {
                    // Call itself with a left node of the current node first
                    Traverse(node.Left);
                    // Call itself with a right node of the current node second
                    Traverse(node.Right);
                    // Do something with a current node as required third
                    DoSomething(node, list);
                }
            }

            // Call the recursive function with a root node
            Traverse(Root);
            return list;
        }

        // Traverse via in-order traversal (DFS)
        public List<T> InOrderTraverse()
        {
            var list = new List<T>();
            // Define a recursive function
            void Traverse(Node<T> node)
            {
                if (node != null)
                {
                    // Call itself with a left node of the current node first
                    Traverse(node.Left);
                    // Do something with a current node as required second
                    DoSomething(node, list);
                    // Call itself with a right node of the current node third
                    Traverse(node.Right);
                }
            }

            // Call the recursive function with a root node
            Traverse(Root);
            return list;
        }

        private void DoSomething(Node<T> node, List<T> list = null)
        {
            Trace.WriteLine(node.Value);
            if (list != null)
            {
                list.Add(node.Value);
            }
        }

        public class Node<NodeT>
        {
            public NodeT Value { get; set; }
            public int Counter { get; set; }
            public Node<NodeT> Left { get; set; }
            public Node<NodeT> Right { get; set; }

            public Node(NodeT value)
            {
                Value = value;
                Counter = 1;
            }
        }
    }
}