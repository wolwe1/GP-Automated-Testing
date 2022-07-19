using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.statenodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes.state
{
    public class LastOutputStateFactory<TAdfOutput> : StateNodeFactory 
    {
        public LastOutputStateFactory() : base(NodeCategory.LastOutput) { }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(TAdfOutput);
        }

        public override INode<T> Get<T>()
        {
            return new LastOutputStateNode<T>();
        }

        protected override INode<T> Get<T>(string id)
        {
            if (id == "")
            {
                return new LastOutputStateNode<T>();
            }
            
            var tVal = (T)Convert.ChangeType(id, typeof(T)); 
            
            return new LastOutputStateNode<T>(tVal);
        }
    }
}