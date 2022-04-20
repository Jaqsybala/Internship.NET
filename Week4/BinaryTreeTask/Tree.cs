namespace Week4.BinaryTreeTask
{
    public class Tree<T>
    {
        public Tree<T> Left;
        public Tree<T> Right;
        public T Data;

        static void Main(string[] args)
        {
            Tree<int> binaryTree = new Tree<int>()
            {
                Left = new Tree<int>()
                {
                    Left = new Tree<int>()
                    {
                        Data = 4
                    },
                    Right = new Tree<int>()
                    {
                        Data = 5
                    },
                    Data = 2
                },
                Right = new Tree<int>()
                {
                    Left = new Tree<int>()
                    {
                        Data = 6
                    },
                    Right = new Tree<int>()
                    {
                        Right = new Tree<int>()
                        {
                            Data = 8
                        },
                        Data = 7
                    },
                    Data = 3
                },
                Data = 1
            };

            DoTree(binaryTree, x => Console.Write($"{x} "));
        }

        public static void DoTree<T>(Tree<T> tree, Action<T> action)
        {
            if (tree == null) return;
            var left = Task.Factory.StartNew(() => DoTree(tree.Left, action));
            var right = Task.Factory.StartNew(() => DoTree(tree.Right, action));
            action(tree.Data);

            try
            {
                Task.WaitAll(left, right);
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"{e.Message}");
            }
        }
    }
}
