using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.factories.comparators
{
    public class EqualComparatorFactory : ComparatorFactory
    {
        public EqualComparatorFactory() : base(NodeCategory.Equal) { }

        public override FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent)
        {
            var leftPredicate = parent.Choose<T>(maxDepth - 1);
            var rightPredicate = parent.Choose<T>(maxDepth - 1);

            return new EqualsComparator<T>(leftPredicate, rightPredicate);
        }

        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            var leftPredicate = functionCreator.GenerateChildFromId<T>(ref id);
            var rightPredicate = functionCreator.GenerateChildFromId<T>(ref id);

            return new EqualsComparator<T>(leftPredicate, rightPredicate);
        }
    }
}