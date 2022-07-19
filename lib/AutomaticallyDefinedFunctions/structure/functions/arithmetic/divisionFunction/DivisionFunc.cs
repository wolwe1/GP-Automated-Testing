using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.divisionFunction
{
    public class DivisionFunc<T> : ArithmeticFunc<T> where T : IComparable
    {
        private readonly IArithmeticOperator<T> _divideOperator;
        public DivisionFunc(IEnumerable<INode<T>> nodes,IArithmeticOperator<T> divideOperator) : base(nodes, NodeCategory.Division)
        {
            _divideOperator = divideOperator;
        }

        public DivisionFunc(IArithmeticOperator<T> divideOperator) : base(NodeCategory.Division)
        {
            _divideOperator = divideOperator;
        }

        public DivisionFunc(INode<T> firstNode, INode<T> secondNode, IArithmeticOperator<T> divideOperator) : base(firstNode, secondNode, NodeCategory.Division)
        {
            _divideOperator = divideOperator;
        }

        public override T GetValue()
        {
            return _divideOperator.GetResult(Children);
        }

        public override INode<T> GetCopy()
        {
            var childCopies = GetChildCopies();

            return new DivisionFunc<T>(childCopies,_divideOperator);
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            var (left,right) = GetChildrenWithoutNullNodes(maxDepth,creator);

            return new DivisionFunc<T>(left, right,_divideOperator);
        }
    }
}