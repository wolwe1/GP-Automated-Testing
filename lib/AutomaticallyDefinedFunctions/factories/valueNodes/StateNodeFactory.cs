using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes
{
    public abstract class StateNodeFactory : IValueNodeFactory
    {
        protected readonly string Name;

        protected StateNodeFactory(string name)
        {
            Name = name;
        }

        public INode<T> GenerateFunctionFromId<T>(string id, FunctionCreator functionCreator) where T : IComparable
        {
            return Get<T>(AdfParser.GetValueFromQuotes(id[Name.Length..]));
        }

        public bool CanMap(string id)
        {
            return id.StartsWith(Name);
        }
        
        public abstract bool CanDispatch<T>();
        public abstract INode<T> Get<T>() where T : IComparable;
        protected abstract INode<T> Get<T>(string id) where T : IComparable;
    }
}