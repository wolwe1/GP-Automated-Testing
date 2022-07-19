using lib.AutomaticallyDefinedFunctions.factories.functionFactories;
using lib.AutomaticallyDefinedFunctions.structure.adf;

namespace lib.AutomaticallyDefinedFunctions.generators.adf
{
    public class MainGenerator<T> where T : IComparable
    {
        private FunctionCreator _creator;
        private readonly AdfSettings _settings;

        public MainGenerator(AdfSettings settings)
        {
            _creator = new FunctionCreator(settings, false);
            _settings = settings;
        }

        public void AddDefinitions(FunctionDefinitionHolder<T> definitionHolder)
        {
            _creator.UseFactory(definitionHolder);
        }
                
        public MainProgram<T> GenerateMainFunction()
        {
            var mainTree = _creator.CreateFunction<T>(_settings.MaxMainDepth);
            var main = new MainProgram<T>(mainTree);
            
            return main;
        }
        
        public MainProgram<T> GenerateMainFromId(string id)
        {
            var functionId = id["Main".Length..];
            var newFunction = _creator.GenerateFunctionFromId<T>(functionId);

            return new MainProgram<T>(newFunction);
        }

        private MainProgram<T> GenerateMainFromIdNoAdd(string id)
        {
            var functionId = id["Main".Length..];
            var newFunction = _creator.GenerateFunctionFromId<T>(functionId);

            return new MainProgram<T>(newFunction);
        }
        
        public IEnumerable<MainProgram<T>> GenerateMainsFromIdList(IEnumerable<string> ids)
        {
            return ids.Select(GenerateMainFromIdNoAdd);
        }

        public FunctionCreator GetGeneratorCopy()
        {
            return new FunctionCreator(_settings, false);
        }

        public void Reset()
        {
            _creator = GetGeneratorCopy();
        }
    }
}