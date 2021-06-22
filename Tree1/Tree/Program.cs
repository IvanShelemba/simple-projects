using System;
using System.Collections.Generic;

namespace Tree
{
    class Tree<T>
    {
        public T Key { get; set; }
        public List<Tree<T>> Children { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            const int DataSize = 11;

            var dataset = CreateDataset(DataSize);

            dataset.ForEach(e => Console.Write($"{e}, ")); Console.WriteLine(" -> Dataset\n");

            var tree = GetTree(dataset);

            DoAlgorithm(tree, new Algorithm1());
            DoAlgorithm(tree, new Algorithm2());
            DoAlgorithm(tree, new Algorithm3());
            DoAlgorithm(tree, new Algorithm4());

            Console.ReadLine();
        }

        private static void DoAlgorithm(Tree<int> tree, ITreeTraversalAlgorithm algo)
        {
            algo.Traversal(tree, (val) => Console.Write($"{val}, ")); Console.WriteLine($" -> {algo.GetType()}");
        }

        private static List<int> CreateDataset(int size)
        {
            var dataset = new List<int>(size);

            for (int i = 0; i < size; i++)
            {
                dataset.Add(i);
            }

            dataset.Sort();
            
            return dataset;
        }

        private static Tree<int> GetTree(List<int> dataset)
        {
            var tree = new Tree<int>();

            var count = dataset.Count;

            var half = count / 2;

            tree.Key = dataset[half];

            if (count >= 2)
            {
                tree.Children = new List<Tree<int>>();

                tree.Children.Add(GetTree(dataset.GetRange(0, half)));
            }
            if(count > 2)
            {
                tree.Children.Add(GetTree(dataset.GetRange(half + 1, count - half - 1)));
            }

            return tree;
        }
    }

    interface ITreeTraversalAlgorithm
    {
        void Traversal<T>(Tree<T> tree, Action<T> proceed);
    }

    //from left to right from down to up
    class Algorithm1 : ITreeTraversalAlgorithm
    {
        public void Traversal<T>(Tree<T> tree, Action<T> proceed)
        {
            if (tree.Children != null)
            {
                foreach (var child in tree.Children)
                    Traversal(child, proceed);
            }

            proceed(tree.Key);
        }
    }
    
    //left to right from up to down
    class Algorithm2 : ITreeTraversalAlgorithm
    {
        public void Traversal<T>(Tree<T> tree, Action<T> proceed)
        {
            proceed(tree.Key);

            if (tree.Children != null)
            {
                foreach (var child in tree.Children)
                    Traversal(child, proceed);
            }
        }
    }

    //in depth ( all elements on depth 1, all elements on depth 2 ... )
    class Algorithm3 : ITreeTraversalAlgorithm
    {
        public void Traversal<T>(Tree<T> tree, Action<T> proceed)
        {
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(tree);

            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();

                if (subTree.Children != null)
                    foreach (var child in subTree.Children)
                        queue.Enqueue(child);

                proceed(subTree.Key);
            }
        }
    }

    //without recursion
    class Algorithm4 : ITreeTraversalAlgorithm
    {
        public void Traversal<T>(Tree<T> tree, Action<T> proceed)
        {
            var stack = new Stack<Tree<T>>();

            stack.Push(tree);

            while (stack.Count > 0)
            {
                var subTree = stack.Pop();

                if (subTree.Children != null)
                    foreach (var child in subTree.Children)
                        stack.Push(child);

                proceed(subTree.Key);
            }
        }
    }
}
