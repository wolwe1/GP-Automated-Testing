using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.boolean
{
    public class NotNullComparator<T> : NodeComparator<T> where T : IComparable
    {
        private INode<T> Argument => GetChildAs<T>(0);

        private NotNullComparator() : base(1) { }
        
        public NotNullComparator(INode<T> argument) : this()
        {
            RegisterChildren(new List<INode>(){argument});
        }

        public override string GetId()
        {
            return CreateId<T>(NodeCategory.NotNull);
        }

        public override T GetValue()
        {
            throw new NotImplementedException();
        }
        
        public override INode<T> GetCopy()
        {
            return new NotNullComparator<T>(Argument.GetCopy());
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            if (Argument.IsNullNode())
                return SetArgument(creator.CreateFunction<T>(maxDepth - 1));
            
            return SetArgument(Argument.ReplaceNullNodes(maxDepth - 1, creator));
        }

        public NotNullComparator<T> SetArgument(INode<T> node)
        {
            return new NotNullComparator<T>(node);
        }

        public override bool PassesCheck()
        {
            return Argument.GetValue() is not null;
        }
    }
}