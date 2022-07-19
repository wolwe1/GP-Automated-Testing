using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic
{
    public abstract class ArithmeticFunc<T> : FunctionNode<T> where T : IComparable
    {
        private readonly string _category;

        protected ArithmeticFunc(IEnumerable<INode<T>> nodes, string category) : base(nodes)
        {
            _category = category;
        }

        protected ArithmeticFunc(string category) : base(2)
        {
            _category = category;
        }
        
        protected ArithmeticFunc(INode<T> firstNode,INode<T> secondNode,string category) : this(category)
        {
            Children.Add(firstNode);
            Children.Add(secondNode);
        }
        
        public override string GetId()
        {
            return $"{_category}<{typeof(T)},{typeof(T)}>[{GetChildAs<T>(0).GetId()}{GetChildAs<T>(1).GetId()}]";
        }

        protected (INode<T>, INode<T>) GetChildrenWithoutNullNodes(int maxDepth, FunctionCreator creator)
        {
            var newLeftChild = ReplaceNullNodesForComponent(GetChildAs<T>(0),maxDepth - 1,creator);
            var newRightChild = ReplaceNullNodesForComponent(GetChildAs<T>(1),maxDepth - 1,creator);

            return ((INode<T>, INode<T>)) (newLeftChild, newRightChild);
        }

        protected IEnumerable<INode<T>> GetChildCopies()
        {
            return new List<INode<T>>() {GetChildCopyAs<T>(0), GetChildCopyAs<T>(1)};
        }
        

    }
}