using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.comparators
{
    public class EqualsComparator<T> : NodeComparator<T> where T: IComparable
    {
        private INode<T> LeftPredicate => GetChildAs<T>(0);
        private INode<T> RightPredicate => GetChildAs<T>(1);
        public EqualsComparator(INode<T> leftPredicate, INode<T> rightPredicate) : base(2)
        {
            RegisterChildren(new List<INode>(){leftPredicate,rightPredicate});
        }
        
        public override bool PassesCheck()
        {
            var leftResult = LeftPredicate.GetValue();
            var rightResult = RightPredicate.GetValue();

            var result = leftResult.CompareTo(rightResult) == 0;

            return HandleReturn(result);
        }

        public override T GetValue()
        {
            throw new NotImplementedException();
        }

        public override INode<T> GetCopy()
        {
            var newComp = new EqualsComparator<T>(LeftPredicate.GetCopy(),RightPredicate.GetCopy());

            if (Next != null)
                newComp.Next = (NodeComparator<T>) Next.GetCopy();

            return newComp;
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            var leftWithoutNulls = (INode<T>) ReplaceNullNodesForComponent(LeftPredicate, maxDepth - 1, creator);
            var rightWithoutNulls = (INode<T>) ReplaceNullNodesForComponent(RightPredicate, maxDepth - 1, creator);

            return new EqualsComparator<T>(leftWithoutNulls, rightWithoutNulls);
        }

        public override string GetId()
        {
            return CreateId<T>(NodeCategory.Equal);
        }

    }
}