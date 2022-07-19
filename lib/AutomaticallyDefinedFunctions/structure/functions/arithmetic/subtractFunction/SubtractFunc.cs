using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.subtractFunction
{
    public class SubtractFunc<T> : ArithmeticFunc<T> where T : IComparable
    {
        private readonly IArithmeticOperator<T> _subtractOperator;

        public SubtractFunc(IEnumerable<INode<T>> nodes, IArithmeticOperator<T> subtractOperator) : base(nodes,NodeCategory.Subtract)
        {
            _subtractOperator = subtractOperator;
        }

        public SubtractFunc(IArithmeticOperator<T> subtractOperator): base(NodeCategory.Subtract)
        {
            _subtractOperator = subtractOperator;
        }

        public SubtractFunc(INode<T> firstNode, INode<T> secondNode, IArithmeticOperator<T> subtractOperator) : base(firstNode,secondNode,NodeCategory.Subtract)
        {
            _subtractOperator = subtractOperator;
        }
        
        public override T GetValue()
        {
            return _subtractOperator.GetResult(Children);
        }
        
        public override INode<T> GetCopy()
        {
            var childCopies = GetChildCopies();

            return new SubtractFunc<T>(childCopies,_subtractOperator);
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            var (left,right) = GetChildrenWithoutNullNodes(maxDepth,creator);

            return new SubtractFunc<T>(left, right,_subtractOperator);
        }
        
        
        
    }
}