using lib.AutomaticallyDefinedFunctions.parsing;

namespace lib.AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    public class LastOutputStateNode<T> : StateNode<T> where T : IComparable
    {
        public LastOutputStateNode(T value): base(value,NodeCategory.LastOutput) { }

        public LastOutputStateNode() : base(NodeCategory.LastOutput) { }
        
        public override bool IsValid()
        {
            return true;
        }

        public override INode<T> GetCopy()
        {
            return new LastOutputStateNode<T>(Value);
        }
    }
}