using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.factories.functionFactories
{
    public interface IFunctionFactory: IDispatcher
    {
        FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent) where T : IComparable;
        INode<T> GenerateFunctionFromId<T>(string id, FunctionCreator functionCreator) where T : IComparable;
    }
}