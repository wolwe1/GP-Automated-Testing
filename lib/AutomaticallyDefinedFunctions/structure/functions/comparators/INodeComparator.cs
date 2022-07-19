using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.structure.functions.comparators
{
    public abstract class NodeComparator<T> : FunctionNode<T> where T : IComparable
    {
        protected NodeComparator<T> Next;
 
        protected NodeComparator(int expectedChildrenAmount) : base(expectedChildrenAmount) { }
        
        public NodeComparator<T> SetAdditionalComparator(NodeComparator<T> next)
        {
            Next = next;
            return this;
        }

        protected bool HandleReturn(bool result)
        {
            if (result || Next == null)
                return result;
            
            return Next.PassesCheck();
        }
        public abstract bool PassesCheck();


        public void SetPredicate<TU>(int i, INode<TU> newPredicate) where TU : IComparable
        {
            Children[0] = newPredicate;
        }
    }
}