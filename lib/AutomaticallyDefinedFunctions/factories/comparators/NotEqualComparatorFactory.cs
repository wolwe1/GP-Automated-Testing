using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.factories.comparators
{
    public class NotEqualComparatorFactory : ComparatorFactory
    {
        public NotEqualComparatorFactory() : base(NodeCategory.NotEqual) { }

        public override FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent)
        {
            return new NotEqualComparator<T>(parent.Choose<T>(maxDepth - 1),parent.Choose<T>(maxDepth - 1));
        }

        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            return new NotEqualComparator<T>(
                functionCreator.GenerateChildFromId<T>(ref id),
                functionCreator.GenerateChildFromId<T>(ref id));

        }
    }
}