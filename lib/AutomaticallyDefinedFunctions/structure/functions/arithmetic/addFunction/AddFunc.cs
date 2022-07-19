using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction
{
    public class AddFunc<T> : ArithmeticFunc<T> where T : IComparable
    {
        private readonly IArithmeticOperator<T> _arithmeticOperator;

        public AddFunc(IEnumerable<INode<T>> nodes, IArithmeticOperator<T> arithmeticOperator) : base(nodes, NodeCategory.Add)
        {
            _arithmeticOperator = arithmeticOperator;
        }

        public AddFunc(IArithmeticOperator<T> arithmeticOperator) : base(NodeCategory.Add)
        {
            _arithmeticOperator = arithmeticOperator;
        }

        public AddFunc(INode<T> firstNode, INode<T> secondNode, IArithmeticOperator<T> arithmeticOperator) : base(firstNode,
            secondNode, NodeCategory.Add)
        {
            _arithmeticOperator = arithmeticOperator;
        }

        public AddFunc<T> Refresh(INode<T> firstNode,INode<T> secondNode)
        {
            Children.Clear();
            Children.Add(firstNode);
            Children.Add(secondNode);
            
            return this;
        }

        public override T GetValue()
        {
            return _arithmeticOperator.GetResult(Children);
        }

        public override INode<T> GetCopy()
        {
            var childCopies = GetChildCopies();

            return new AddFunc<T>(childCopies,_arithmeticOperator);
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            var (left,right) = GetChildrenWithoutNullNodes(maxDepth,creator);

            return new AddFunc<T>(left, right,_arithmeticOperator);
        }

        public void Clear()
        {
           Children.Clear();
        }
    }
}