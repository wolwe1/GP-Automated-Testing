using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.generators.adf;
using lib.AutomaticallyDefinedFunctions.structure.functions.arithmetic.addFunction;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.functions.forLoop;
using lib.AutomaticallyDefinedFunctions.structure.functions.ifStatement;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace lib.AutomaticallyDefinedFunctions.factories
{
    /*
     * A test helper class that can create INode structures 
     */
    public static class NodeBuilder
    {
        public static AddFunc<double> CreateAdditionFunction(double firstNumber, double secondNumber)
        {
            var nodeOne = new ValueNode<double>(firstNumber);
            var nodeTwo = new ValueNode<double>(secondNumber);
            return CreateAdditionFunction(nodeOne, nodeTwo);
        }
        
        public static AddFunc<double> CreateAdditionFunction(INode<double> firstNode, INode<double> secondNode)
        {
            return new AddFunc<double>(new List<INode<double>>() {firstNode, secondNode}, new NumericAddOperator());
        }

        public static IfNode<T,TU> CreateIfStatement<T,TU>(INode<T> trueBlock, INode<T> falseBlock, NodeComparator<TU> nodeOperator) where T : IComparable where TU : IComparable
        {
            return new IfNode<T,TU>()
                .SetComparisonOperator(nodeOperator)
                .SetFalseCodeBlock(falseBlock)
                .SetTrueCodeBlock(trueBlock);
        }

        public static ForLoopNode<T, double> CreateSimpleForLoop<T>(double bound,T code) where T : IComparable
        {
            return new ForLoopNode<T, double>()
                .SetIncrement(new ValueNode<double>(1d))
                .SetComparator(
                    new LessThanComparator<double>(new ValueNode<double>(0), new ValueNode<double>(bound)))
                .SetCodeBlock(new ValueNode<T>(code));

        }

        public static ForLoopNode<T, TU> CreateForLoop<T,TU>(INode<TU>  increment, NodeComparator<TU> comparator, INode<T> block) where T : IComparable where TU : IComparable
        {
            return new ForLoopNode<T, TU>()
                .SetIncrement(increment)
                .SetComparator(comparator)
                .SetCodeBlock(block);
        }
        
        public static AdfGenerator<T> CreateAdfGenerator<T>() where T : IComparable
        {
            var settings = new AdfSettings(2, 3, 1, 65);

            return new AdfGenerator<T>(1, settings);
        }
        
        public static AdfGenerator<T> CreateStateAdfGenerator<T,TU>() where T : IComparable where TU : IComparable
        {
            var settings = new StateAdfSettings<T,TU>(2, 3, 1, 65);

            return new StateAdfGenerator<T,TU>(1, settings);
        }

        public static FunctionCreator CreateFunctionCreator()
        {
            var settings = new AdfSettings(3, 3, 1, 60);

            var factory = new FunctionCreator(settings, false);

            return factory;
        }

        public static FunctionCreator CreateStateFunctionCreator<TAdf,TProgResp>()
        {
            var settings = new StateAdfSettings<TAdf,TProgResp>(3, 3, 1, 60);

            var factory = new FunctionCreator(settings, false);

            return factory;
        }
    }
}