using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;
using lib.AutomaticallyDefinedFunctions.structure.nodes.statenodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes.state
{
    public class RandomStateFactory : StateNodeFactory
    {
        public RandomStateFactory() : base(NodeCategory.Random) { }

        public override bool CanDispatch<T>()
        {
            return typeof(T) == typeof(string) || typeof(T) == typeof(double) || typeof(T) == typeof(bool);
        }

        public override INode<T> Get<T>()
        {
            return new RandomStateNode<T>();
        }

        protected override INode<T> Get<T>(string id)
        {
            return Get<T>(); //No need to load value
        }
    }
}