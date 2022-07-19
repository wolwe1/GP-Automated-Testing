using lib.AutomaticallyDefinedFunctions.structure.nodes.valueNodes;

namespace lib.AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    public interface IStateNode{}
    public abstract class StateNode<T> : ValueNode<T>, IStateNode where T : IComparable
    {
        protected readonly string Name;

        protected StateNode(string name)
        {
            Name = name;
        }

        protected StateNode(T value, string name) : base(value)
        {
            Name = name;
        }

        public override string GetId()
        {
            return $"{Name}['{Value}']";
        }

        public override bool IsValid()
        {
            return true;
        }
        public void UpdateState(T value)
        {
            Value = value;
        }
        
    }
}