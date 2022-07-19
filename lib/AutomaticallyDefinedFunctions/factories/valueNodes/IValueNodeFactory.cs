using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.factories.valueNodes
{
    public interface IValueNodeFactory : IDispatcher
    {
        INode<T> Get<T>() where T : IComparable;
        INode<T> GenerateFunctionFromId<T>(string id, FunctionCreator functionCreator) where T : IComparable;
    }
}