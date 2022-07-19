using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.statenodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes.state
{
    public class OutputFailureStateFactory : StateNodeFactory
    {
        public OutputFailureStateFactory() : base(NodeCategory.OutputFailed) { }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(bool);
        }

        public override INode<T> Get<T>()
        {
            return (INode<T>) new OutputFailedStateNode();
        }

        protected override INode<T> Get<T>(string id)
        {
            return (INode<T>) new OutputFailedStateNode(bool.Parse(id));
        }
    }
}