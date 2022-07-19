using lib.AutomaticallyDefinedFunctions.parsing;

namespace lib.AutomaticallyDefinedFunctions.structure.nodes.statenodes
{
    //Uses double to behave with the rest of the type system
    public class ExecutionCountStateNode : StateNode<double>
    {
        
        public ExecutionCountStateNode(): base(NodeCategory.ExecutionCount){}
        public ExecutionCountStateNode(double value): base(value,NodeCategory.ExecutionCount) { }

        public override INode<double> GetCopy()
        {
            return new ExecutionCountStateNode(Value);
        }
    }
}