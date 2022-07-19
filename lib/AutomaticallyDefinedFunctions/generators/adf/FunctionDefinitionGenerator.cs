using lib.AutomaticallyDefinedFunctions.parsing;
using lib.AutomaticallyDefinedFunctions.structure.adf;
using lib.AutomaticallyDefinedFunctions.structure.nodes;

namespace lib.AutomaticallyDefinedFunctions.generators.adf
{
    public class FunctionDefinitionGenerator<T> where T : IComparable
    {
        private readonly FunctionCreator _creator;
        private readonly AdfSettings _settings;

        public FunctionDefinitionGenerator(AdfSettings settings)
        {
            _creator = new FunctionCreator(settings, true);
            _settings = settings;
        }

        public FunctionDefinition<T> GenerateFunctionDefinition(int functionCount)
        {
            return FunctionDefinition<T>.Create($"ADF{functionCount}")
                .UseFunction(_creator.CreateFunction<T>(_settings.MaxFunctionDepth));
        }
        
        public List<FunctionDefinition<T>> GenerateFunctionsFromIdList(IEnumerable<string> ids)
        {
            return ids.Select(GenerateFunctionFromIdNoAdd).ToList();
        }

        //Prevent needless addition of value node factories
        protected FunctionDefinition<T> GenerateFunctionFromIdNoAdd(string id,int functionCount)
        {
            var idWithoutLead = id;
            if (!id.StartsWith(NodeCategory.FunctionDefinition))
                return FunctionDefinition<T>.Create($"ADF{functionCount}")
                    .UseFunction(_creator.GenerateFunctionFromId<T>(idWithoutLead));
            
            var endOfDefinitionTypeInfo = id.IndexOf(">") + 2;
            idWithoutLead = id[endOfDefinitionTypeInfo..^1];

            return FunctionDefinition<T>.Create($"ADF{functionCount}")
                .UseFunction(_creator.GenerateFunctionFromId<T>(idWithoutLead));
        }
        public FunctionDefinition<T> GenerateFunctionFromId(string id,int functionCount)
        {
            return GenerateFunctionFromIdNoAdd(id, functionCount);
        }

        public INode<T> CreateFunction(int maxDepth)
        {
            return _creator.CreateFunction<T>(maxDepth);
        }

        public FunctionCreator GetGenerator()
        {
            return new FunctionCreator(_settings,true);
        }
        
    }
}