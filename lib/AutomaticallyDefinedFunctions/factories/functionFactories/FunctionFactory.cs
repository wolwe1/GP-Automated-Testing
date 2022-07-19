using lib.AutomaticallyDefinedFunctions.generators;
using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.functions;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.factories.functionFactories
{
    public abstract class FunctionFactory : IFunctionFactory
    {
        private readonly string _symbol;

        protected FunctionFactory(string symbol)
        {
            _symbol = symbol;
        }

        public abstract FunctionNode<T> CreateFunction<T>(int maxDepth, FunctionCreator parent) where T : IComparable;

        public bool CanMap(string id)
        {
            return id.StartsWith(_symbol);
        }

        public static INode<T> GenerateDefinitionFromId<T>(string id, FunctionCreator functionCreator) where T : IComparable
        {
            var typeInfo = AdfParser.GetTypeInfo(id,NodeCategory.FunctionDefinition);
            var idForChildren = id[typeInfo.Length..];
                
            return new FunctionDefinition<T>("Unknown",functionCreator.GenerateChildFromId<T>(ref idForChildren));
        }

        public INode<T> GenerateFunctionFromId<T>(string id, FunctionCreator functionCreator) where T : IComparable
        {
            string typeInfo;
            string idForChildren;

            if (id.StartsWith(NodeCategory.FunctionDefinition))
                return GenerateDefinitionFromId<T>(id, functionCreator);
                
            
            if(!CanMap(id)) throw new Exception($"Cannot generate statement from ID beginning with {AdfParser.GetTypeInfo(id,_symbol)}");
            
            typeInfo = AdfParser.GetTypeInfo(id,_symbol);
            
            if(typeInfo == "")
                return GenerateFunctionFromId<T,T>(id[1..],functionCreator);
            
            var auxType = AdfParser.GetAuxType(typeInfo);
            
            idForChildren = id[typeInfo.Length..];
            
            if(auxType == typeof(string))
                return GenerateFunctionFromId<T, string>(idForChildren,functionCreator);
            if(auxType == typeof(double))
                return GenerateFunctionFromId<T, double>(idForChildren,functionCreator);
            if(auxType == typeof(bool))
                return GenerateFunctionFromId<T, bool>(idForChildren,functionCreator);
            
            throw new Exception("Type information for function not found");
            
        }

        protected abstract INode<T> GenerateFunctionFromId<T, TU>(string id, FunctionCreator functionCreator)
            where T : IComparable where TU : IComparable;

        public abstract bool CanDispatch<T>();

    }
}