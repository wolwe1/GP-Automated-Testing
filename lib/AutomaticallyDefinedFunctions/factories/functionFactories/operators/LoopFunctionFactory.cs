using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.functions.forLoop;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace lib.AutomaticallyDefinedFunctions.factories.functionFactories.operators
{
    public class LoopFunctionFactory : FunctionFactory
    {
        private delegate FunctionNode<T> Func<T, TU>(int depth, FunctionCreator parent) where T : IComparable;

        public LoopFunctionFactory() : base(NodeCategory.Loop) { }
        
        public override FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent)
        {
            var choice = RandomNumberFactory.Next(2);

            return choice switch
            {
                0 => CreateFunction<T, double>(maxDepth, parent),
                1 => CreateFunction<T, bool>(maxDepth, parent),
                _ => throw new Exception($"Could not dispatch type {typeof(T)}")
            };
            
        }

        private FunctionNode<T> CreateFunction<T,TU>(int maxDepth, FunctionCreator parent) where TU : IComparable where T : IComparable
        {
            var loop = new ForLoopNode<T, TU>();

            var incremental = parent.GetTerminal<TU>();
            var comparator = parent.ChooseComparator<TU>(maxDepth - 1);

            var block = parent.Choose<T>(maxDepth - 1);

            return loop
                .SetIncrement((ValueNode<TU>) incremental)
                .SetComparator(comparator)
                .SetCodeBlock(block);
        }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(string) || typeof(T) == typeof(double) || typeof(T) == typeof(bool);
        }
        
        protected override INode<T> GenerateFunctionFromId<T,TU>(string id, FunctionCreator functionCreator)
        {
            var incremental = functionCreator.GenerateChildFromId<TU>(ref id);
 
            var comparator = (NodeComparator<TU>)functionCreator.GenerateChildFromId<TU>(ref id);
                //FunctionGenerator.ChooseComparator<TU>(ref id);

            var block = functionCreator.GenerateChildFromId<T>(ref id);
            
            return new ForLoopNode<T,TU>()
                .SetIncrement(incremental)
                .SetComparator(comparator)
                .SetCodeBlock(block);
        }

        
    }
}