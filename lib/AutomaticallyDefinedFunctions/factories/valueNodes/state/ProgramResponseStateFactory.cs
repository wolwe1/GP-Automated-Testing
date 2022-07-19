using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.statenodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes.state
{
    public class ProgramResponseStateFactory<TProgResponse> : StateNodeFactory
    {
        public ProgramResponseStateFactory() : base(NodeCategory.ProgramResponse) { }

        public override bool CanDispatch<T>()
        {
            return typeof(TProgResponse) == typeof(T);
        }

        public override INode<T> Get<T>()
        {
            return new ProgramResponseStateNode<T>();
        }

        protected override INode<T> Get<T>(string id)
        {
            if (id == "")
            {
                return new ProgramResponseStateNode<T>();
            }
            
            var tVal = (T)Convert.ChangeType(id, typeof(T));
            
            return new ProgramResponseStateNode<T>(tVal);
        }
    }
}