using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.functions.comparators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.ifStatement
{
    public class IfNode<T,TU> : FunctionNode<T> where T : IComparable where TU : IComparable
    {
        private NodeComparator<TU> Comparator => (NodeComparator<TU>) GetChild(0);
        private INode<T> TrueBlock => GetChildAs<T>(1);
        private INode<T> FalseBlock => GetChildAs<T>(2);

        public IfNode() : base(3) { }
        private IfNode(INode<TU> comparator, INode<T> trueBlock, INode<T> falseBlock) : this()
        {
            RegisterChildren(new List<INode>(){comparator,trueBlock,falseBlock});
        }
        
        public IfNode<T,TU> SetComparisonOperator(NodeComparator<TU> comparator)
        {
            return new IfNode<T, TU>(comparator, TrueBlock?.GetCopy(), FalseBlock?.GetCopy());
        }
        
        public IfNode<T,TU> SetTrueCodeBlock(INode<T> root)
        {
            return new IfNode<T, TU>((NodeComparator<TU>) Comparator?.GetCopy(), root, FalseBlock?.GetCopy());
        }
        
        public IfNode<T,TU> SetFalseCodeBlock(INode<T> root)
        {
            return new IfNode<T, TU>(Comparator?.GetCopy(), TrueBlock?.GetCopy(), root);
        }
        
        public override string GetId()
        {
            return CreateId<TU>(NodeCategory.If);
        }

        public override T GetValue()
        {
            //if (!IsValid()) throw new InvalidStructureException("If statement is invalid");
            
            return Comparator.PassesCheck() ? 
                TrueBlock.GetValue() :
                FalseBlock.GetValue();
        }

        public override INode<T> GetCopy()
        {
            return new IfNode<T, TU>((NodeComparator<TU>) Comparator.GetCopy(),TrueBlock.GetCopy(),FalseBlock.GetCopy());
        }

        public override INode<T> ReplaceNullNodes(int maxDepth, FunctionCreator creator)
        {
            var newTrueBlock = ReplaceNullNodesForComponent(TrueBlock,maxDepth - 1,creator);
            var newFalseBlock = ReplaceNullNodesForComponent(FalseBlock,maxDepth - 1,creator);
            var newComparator = ReplaceNullNodesForComponent(Comparator,maxDepth - 1,creator);

            return new IfNode<T, TU>((NodeComparator<TU>) newComparator, (INode<T>) newTrueBlock,(INode<T>) newFalseBlock);
        }
    }
}