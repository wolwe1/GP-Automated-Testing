using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.factories.comparators
{
    public class LessThanComparatorFactory : ComparatorFactory
    {
        public LessThanComparatorFactory() : base(NodeCategory.LessThan) { }

        public override FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent)
        {
            var leftPredicate = parent.Choose<T>(maxDepth - 1);
            var rightPredicate = parent.Choose<T>(maxDepth - 1);

            return new LessThanComparator<T>(leftPredicate, rightPredicate);
        }

        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            var leftPredicate = functionCreator.GenerateChildFromId<T>(ref id);
            var rightPredicate = functionCreator.GenerateChildFromId<T>(ref id);

            return new LessThanComparator<T>(leftPredicate, rightPredicate);
        }
        
        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(bool) || typeof(T) == typeof(double);
        }
        
    }
}