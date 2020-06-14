using System;

namespace ex_10
{
    public enum Side
    {
        Left,
        Right
    }
    public class BinaryTree
    {

        public double? Value { get; private set; }
        public BinaryTree Left { get; set; }
        public BinaryTree Right { get; set; }
        public BinaryTree Parent { get; set; }
        public void Add(double data)
        {
            if (Value == null || Value == data)
            {
                Value = data;
                return;
            }
            if (Value > data)
            {
                if (Left == null) Left = new BinaryTree();
                Add(data, Left, this);
            }
            else
            {
                if (Right == null) Right = new BinaryTree();
                Add(data, Right, this);
            }
        }
        private void Add(double data, BinaryTree node, BinaryTree parent)
        {

            if (node.Value == null || node.Value == data)
            {
                node.Value = data;
                node.Parent = parent;
                return;
            }
            if (node.Value > data)
            {
                if (node.Left == null) node.Left = new BinaryTree();
                Add(data, node.Left, node);
            }
            else
            {
                if (node.Right == null) node.Right = new BinaryTree();
                Add(data, node.Right, node);
            }
        }
        private void Add(BinaryTree data, BinaryTree node, BinaryTree parent)
        {

            if (node.Value == null || node.Value == data.Value)
            {
                node.Value = data.Value;
                node.Left = data.Left;
                node.Right = data.Right;
                node.Parent = parent;
                return;
            }
            if (node.Value > data.Value)
            {
                if (node.Left == null) node.Left = new BinaryTree();
                Add(data, node.Left, node);
            }
            else
            {
                if (node.Right == null) node.Right = new BinaryTree();
                Add(data, node.Right, node);
            }
        }
        private Side? MeForParent(BinaryTree node)
        {
            if (node.Parent == null) return null;
            if (node.Parent.Left == node) return Side.Left;
            if (node.Parent.Right == node) return Side.Right;
            return null;
        }
        public void Remove(BinaryTree node)
        {
            if (node == null) return;
            var me = MeForParent(node);
            if (node.Left == null && node.Right == null)
            {
                if (me == Side.Left)
                {
                    node.Parent.Left = null;
                }
                else
                {
                    node.Parent.Right = null;
                }
                return;
            }
            if (node.Left == null)
            {
                if (me == Side.Left)
                {
                    node.Parent.Left = node.Right;
                }
                else
                {
                    node.Parent.Right = node.Right;
                }

                node.Right.Parent = node.Parent;
                return;
            }
            if (node.Right == null)
            {
                if (me == Side.Left)
                {
                    node.Parent.Left = node.Left;
                }
                else
                {
                    node.Parent.Right = node.Left;
                }

                node.Left.Parent = node.Parent;
                return;
            }


            if (me == Side.Left)
            {
                node.Parent.Left = node.Right;
            }
            if (me == Side.Right)
            {
                node.Parent.Right = node.Right;
            }
            if (me == null)
            {
                var bufLeft = node.Left;
                var bufRightLeft = node.Right.Left;
                var bufRightRight = node.Right.Right;
                node.Value = node.Right.Value;
                node.Right = bufRightRight;
                node.Left = bufRightLeft;
                Add(bufLeft, node, node);
            }
            else
            {
                node.Right.Parent = node.Parent;
                Add(node.Left, node.Right, node.Right);
            }
        }
        public void Remove(long data)
        {
            var removeNode = Find(data);
            if (removeNode != null)
            {
                Remove(removeNode);
            }
        }
        public BinaryTree Find(long data)
        {
            if (Value == data) return this;
            if (Value > data)
            {
                return Find(data, Left);
            }
            return Find(data, Right);
        }
        public BinaryTree Find(long data, BinaryTree node)
        {
            if (node == null) return null;

            if (node.Value == data) return node;
            if (node.Value > data)
            {
                return Find(data, node.Left);
            }
            return Find(data, node.Right);
        }
        public long CountElements()
        {
            return CountElements(this);
        }
        private long CountElements(BinaryTree node)
        {
            long count = 1;
            if (node.Right != null)
            {
                count += CountElements(node.Right);
            }
            if (node.Left != null)
            {
                count += CountElements(node.Left);
            }
            return count;
        }

        public static void Show(BinaryTree node)
        {
            if (node != null)
            {
                if (node.Parent == null)
                {
                    Console.WriteLine("ROOT:{0}", node.Value);
                }
                else
                {
                    if (node.Parent.Left == node)
                    {
                        Console.WriteLine("Left for {1}  --> {0}", node.Value, node.Parent.Value);
                    }

                    if (node.Parent.Right == node)
                    {
                        Console.WriteLine("Right for {1} --> {0}", node.Value, node.Parent.Value);
                    }
                }
                if (node.Left != null)
                {
                    Show(node.Left);
                }
                if (node.Right != null)
                {
                    Show(node.Right);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();
            tree.Add(2);
            tree.Add(4);
            tree.Add(1);
            tree.Add(3);
            tree.Add(8);
            tree.Add(2);
            tree.Add(3);
            tree.Add(3);
            tree.Add(7);
            BinaryTree.Show(tree);
            tree.Remove(4);
            BinaryTree.Show(tree);
            Console.ReadKey();
        }
    }
}
