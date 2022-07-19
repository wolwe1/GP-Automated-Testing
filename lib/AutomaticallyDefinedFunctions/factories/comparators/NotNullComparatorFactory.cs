using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.functions.boolean;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.factories.comparators
{
    public class NotNullComparatorFactory : ComparatorFactory
    {
        public NotNullComparatorFactory() : base(NodeCategory.NotNull) { }

        public override FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent)
        {
            return new NotNullComparator<T>(parent.Choose<T>(maxDepth - 1));
        }
        
        protected override INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
        {
            return new NotNullComparator<T>(functionCreator.GenerateChildFromId<T>(ref id));
        }
        
        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(string);
        }
    }
}